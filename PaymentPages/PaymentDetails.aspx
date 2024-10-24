<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentDetails.aspx.cs" Inherits="YourNamespace.PaymentDetails" %>

<!DOCTYPE html>
<html>
<head>
    <title>Payment Details</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Enter Your Details</h2>
            <label for="Name">Name:</label>
            <asp:TextBox ID="txtName" runat="server" /><br />

            <label for="Email">Email:</label>
            <asp:TextBox ID="txtEmail" runat="server" /><br />

            <label for="Phone">Phone:</label>
            <asp:TextBox ID="txtPhone" runat="server" /><br />

            <label for="Amount">Amount (INR):</label>
            <asp:TextBox ID="txtAmount" runat="server" /><br />

            <asp:Button ID="btnPay" Text="Pay Now" runat="server" OnClick="btnPay_Click" /><br />
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
