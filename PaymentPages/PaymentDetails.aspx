<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentDetails.aspx.cs" Inherits="PaymentPages_PaymentDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- PAGE TITLE HERE -->
    <title>::SKETCHTECH:ERP::</title>

    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <link href="../css/menu.css" rel="stylesheet" />
    <!-- BS Stepper -->
    <link rel="stylesheet" href="plugins/bs-stepper/css/bs-stepper.min.css" />
</head>
<body>
    <form id="form1" runat="server">

        <div class="container">
            <asp:Label ID="lblUsername" runat="server"></asp:Label>
            <div class="row">
                <div class="col-md-6">
                    <h3>Name</h3>
                    <asp:TextBox ID="txtFirsttextt" runat="server" placeholder="Enter Name here" class="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <h3>Phone</h3>
                    <asp:TextBox ID="txtPhone" runat="server" placeholder="Enter Phone no.." class="form-control"></asp:TextBox>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
