<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="MasterSubscription.aspx.cs" Inherits="SikshaNew.Admin.Subscription.MasterSubscription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-container {
            background-color: white;
            padding: 30px;
            border-radius: 16px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.05);
            max-width: 700px;
            margin: 40px auto;
            font-family: 'Inter', sans-serif;
        }

        .form-container h2 {
            margin-bottom: 25px;
            color: #2f3640;
        }

        .form-container label {
            font-weight: 600;
            margin-bottom: 6px;
            display: inline-block;
            color: #2f3640;
        }

        .form-container input[type="text"],
        .form-container select {
            width: 100%;
            padding: 10px;
            margin-bottom: 20px;
            border: 1px solid #ccc;
            border-radius: 8px;
            font-size: 14px;
        }

        .form-container .btn-submit {
            background-color: #2f3640;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 8px;
            font-size: 16px;
            cursor: pointer;
        }

        .form-container .btn-submit:hover {
            background-color: #00a8ff;
        }

        .form-container .message {
            margin-top: 10px;
            font-weight: 600;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-container">
        <h2>Add Master Subscription</h2>

        <label>Subscription Type:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="TextBox1" ErrorMessage="Add Subscribtion Type!" ForeColor="Red"></asp:RequiredFieldValidator>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </label>
        &nbsp;<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" />

        <label>Status:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="DropDownList1" ErrorMessage="Select Status!" ForeColor="Red"></asp:RequiredFieldValidator>
        </label>
        &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
            <asp:ListItem Text="-- Select Status --" Value="" />
            <asp:ListItem Text="Active" Value="Active" />
            <asp:ListItem Text="Inactive" Value="Inactive" />
        </asp:DropDownList>

        <asp:Button ID="Button1" runat="server" CssClass="btn-submit" OnClick="Button1_Click" Text="Add Master Subscription" />

        <asp:Label ID="LabelMessage" runat="server" CssClass="message" ForeColor="Green" />
    </div>
</asp:Content>
