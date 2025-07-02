<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeFile="paymentsuccess.aspx.cs" Inherits="SikshaNew.User.paymentsuccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .success-container {
            max-width: 700px;
            margin: 60px auto;
            background-color: #ffffff;
            padding: 40px;
            border-radius: 16px;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.05);
            text-align: center;
            font-family: 'Inter', sans-serif;
        }

        .success-container h2 {
            font-size: 28px;
            font-weight: 600;
            color: #2ecc71;
            margin-bottom: 20px;
        }

        .success-container p {
            font-size: 16px;
            color: #2f3640;
            margin-bottom: 30px;
        }

        .success-icon {
            font-size: 50px;
            color: #2ecc71;
            margin-bottom: 20px;
        }

        .btn-dashboard {
            background-color: #2f3640;
            color: white;
            padding: 12px 24px;
            border-radius: 8px;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            transition: background-color 0.3s ease;
        }

        .btn-dashboard:hover {
            background-color: #00a8ff;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
         <div class="success-container">
        <div class="success-icon">
            <i class="fas fa-check-circle"></i>
        </div>
        <h2>Payment Successful</h2>
        <p>Thank you for your purchase. Your transaction has been completed successfully.</p>
        <a href="MyCourse.aspx" class="btn-dashboard">Go to your courses</a>
    </div>



</asp:Content>
