using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

public partial class PaymentDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack && Request.Form["razorpay_payment_id"] != null)
        {
            // Handle the Razorpay payment response
            HandlePaymentResponse();
        }
    }

    // This method is called when the user clicks the "Pay Now" button
    protected void btnPay_Click(object sender, EventArgs e)
    {
        string name = txtName.Text;
        string email = txtEmail.Text;
        string phone = txtPhone.Text;
        int amount = Convert.ToInt32(txtAmount.Text) * 100; // Amount in paise

        try
        {
            // Generate Razorpay order
            Dictionary<string, object> options = new Dictionary<string, object>
            {
                { "amount", amount }, // Amount in paise
                { "currency", "INR" },
                { "receipt", Guid.NewGuid().ToString() }, // Generate unique receipt
                { "payment_capture", 1 } // 1 = Auto Capture
            };

            RazorpayClient client = new RazorpayClient(
                ConfigurationManager.AppSettings["RazorpayKeyId"],
                ConfigurationManager.AppSettings["RazorpayKeySecret"]
            );

            Order order = client.Order.Create(options);

            // Save payer details and order details to the database before redirecting to Razorpay
            SavePaymentDetails(name, email, phone, amount, order["id"].ToString());

            // Redirect to Razorpay payment page with order details
            string redirectUrl = $"https://checkout.razorpay.com/v1/checkout.js?order_id={order["id"]}&amount={amount}&currency=INR&key={ConfigurationManager.AppSettings["RazorpayKeyId"]}";
            Response.Redirect(redirectUrl);
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error: " + ex.Message;
        }
    }

    // Save the payer details and Razorpay order ID into the database
    private void SavePaymentDetails(string name, string email, string phone, int amount, string orderId)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["UserProfileDB"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO PaymentDetails (Name, Email, Phone, Amount, RazorpayOrderId, PaymentStatus) VALUES (@Name, @Email, @Phone, @Amount, @RazorpayOrderId, 'Pending')";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Phone", phone);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@RazorpayOrderId", orderId);
            cmd.ExecuteNonQuery();
        }
    }

    // Handle the payment response after Razorpay redirects back to your site
    private void HandlePaymentResponse()
    {
        string razorpayPaymentId = Request.Form["razorpay_payment_id"];
        string razorpayOrderId = Request.Form["razorpay_order_id"];
        string razorpaySignature = Request.Form["razorpay_signature"];

        if (!string.IsNullOrEmpty(razorpayPaymentId))
        {
            // Update the payment details in the database
            UpdatePaymentDetails(razorpayOrderId, razorpayPaymentId, razorpaySignature, "Success");

            lblMessage.Text = "Payment Successful! Payment ID: " + razorpayPaymentId;
        }
        else
        {
            lblMessage.Text = "Payment Failed!";
        }
    }

    // Update payment details after receiving the response from Razorpay
    private void UpdatePaymentDetails(string orderId, string paymentId, string signature, string status)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["UserProfileDB"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "UPDATE PaymentDetails SET RazorpayPaymentId = @PaymentId, RazorpaySignature = @Signature, PaymentStatus = @Status WHERE RazorpayOrderId = @OrderId";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PaymentId", paymentId);
            cmd.Parameters.AddWithValue("@Signature", signature);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@OrderId", orderId);
            cmd.ExecuteNonQuery();
        }
    }
}
