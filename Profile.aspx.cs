using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Data;

public partial class Profile1 : System.Web.UI.Page
{
    //private readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UserProfileDB"].ConnectionString;
    string connectionString = ConfigurationManager.ConnectionStrings["UserProfileDB"].ConnectionString;
  //  SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                LoadProfile();
            }
        }
    }

    private void LoadProfile()
    {
        string username = Session["Username"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            DataTable dtt = new DataTable();
            dtt = GetDetails(username);
            if (dtt.Rows.Count > 0)
            {
                txtUsername.Text = dtt.Rows[0]["Username"].ToString();
                txtEmail.Text = dtt.Rows[0]["Email"].ToString();
                txtPassword.Text = dtt.Rows[0]["Password"].ToString();
            }
        }
    }
    public DataTable GetDetails(string UserId)
    {
        SqlConnection con = new SqlConnection(connectionString);
        DataTable dt = new DataTable();
        try
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = " SELeCT * From Users where Username = '" + UserId + "'";

            SqlDataAdapter adq = new SqlDataAdapter();
            adq.SelectCommand = cmd;
            adq.Fill(dt);
            con.Close();
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            string message = ex.Message;
        }
        return dt;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "UPDATE Users SET Email = @Email, Password = @Password WHERE Username = @Username";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
            con.Open();
            cmd.ExecuteNonQuery();
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "msg", "alert('Profile Updated.');", true);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string username = Session["Username"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Users WHERE Username = @Username";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username", username);
            con.Open();
            cmd.ExecuteNonQuery();
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "msg", "alert('Profile Deleted.');", true);
        Session.Clear();
        Response.Redirect("Login.aspx");
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("Login.aspx");
    }

}