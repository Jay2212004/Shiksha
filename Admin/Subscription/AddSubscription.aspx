<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddSubscription.aspx.cs" Inherits="SikshaNew.Admin.Subscription.AddSubscription" %>

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

        .form-container select,
        .form-container input[type="file"],
        .form-container .form-control,
        .form-container .checkboxlist {
            width: 100%;
            padding: 10px;
            margin-bottom: 20px;
            border: 1px solid #ccc;
            border-radius: 8px;
            font-size: 14px;
        }

        .form-container .checkboxlist label {
            display: block;
            margin: 4px 0;
            color: #2f3640;
        }

        .btn-submit {
            background-color: #2f3640;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 8px;
            font-size: 16px;
            cursor: pointer;
        }

        .btn-submit:hover {
            background-color: #00a8ff;
        }

        .message {
            margin-top: 10px;
            font-weight: 600;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-container">
        <h2>Add Subscription</h2>

        <label>Subscription Type:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="DropDownList1" ErrorMessage="Select Subscribtion Type" ForeColor="Red"></asp:RequiredFieldValidator>
        </label>
        &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control"></asp:DropDownList>

        <label>Course Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="DropDownList2" ErrorMessage="Select Course Name" ForeColor="Red"></asp:RequiredFieldValidator>
        </label>
        &nbsp;<asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True"
            OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" CssClass="form-control">
            <asp:ListItem Text="-- Select Course --" Value="" />
        </asp:DropDownList>

        <label>Price:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="TextBox1" ErrorMessage="Add a price" ForeColor="Red"></asp:RequiredFieldValidator>
        </label>
        &nbsp;<asp:TextBox ID="TextBox1" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>

        <label>Duration:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="TextBox2" ErrorMessage="Add a Duration" ForeColor="Red"></asp:RequiredFieldValidator>
        </label>
&nbsp;<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>

        <label>Icon:</label>
        <br />
        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />

        <br />
        <br />

        <label>Select Subcourses:</label>
        <asp:CheckBoxList ID="CheckBoxList1" runat="server" CssClass="checkboxlist" />

        <asp:Button ID="Button1" runat="server" Text="Add Subscription" CssClass="btn-submit" OnClick="Button1_Click" />

        <asp:Label ID="LabelMessage" runat="server" CssClass="message" ForeColor="Green" />
    </div>
</asp:Content>
