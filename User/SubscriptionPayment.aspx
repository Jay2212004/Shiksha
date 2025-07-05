<%@ Page Title="Subscription Payment" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="SubscriptionPayment.aspx.cs" Inherits="SikshaNew.User.SubscriptionPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://checkout.razorpay.com/v1/checkout.js"></script>
    <style>
        .payment-container {
            margin: 40px auto;
            max-width: 600px;
            font-family: 'Segoe UI', sans-serif;
        }

        .payment-container h2 {
            color: #007bff;
            margin-bottom: 30px;
        }

        .form-label {
            font-weight: bold;
            display: block;
            margin-bottom: 10px;
        }

        .form-control {
            margin-bottom: 20px;
            width: 100%;
            padding: 10px;
            font-size: 16px;
        }

        .btn {
            padding: 10px 20px;
            font-size: 16px;
            margin-right: 10px;
        }

        .hidden {
            display: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="payment-container">
        <h2>Subscription Payment</h2>

        <label class="form-label">Total Amount (INR):</label>
        <asp:Label ID="lblTotalAmount" runat="server" CssClass="form-control" />

        <hr />

        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning" Text="Please Verify Email" OnClick="Button1_Click" />

        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Visible="False" placeholder="Enter OTP"></asp:TextBox>

        <asp:Button ID="Button2" runat="server" CssClass="btn btn-success" Text="Verify" OnClick="Button2_Click" Visible="False" />

        <br /><br />

        <asp:Button ID="btnPayNow" runat="server" CssClass="btn btn-primary" Text="Pay Now" OnClick="btnPayNow_Click" Visible="False" />
    </div>
</asp:Content>