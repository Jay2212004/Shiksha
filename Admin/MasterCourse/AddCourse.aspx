<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeFile="AddCourse.aspx.cs" Inherits="SikshaNew.Admin.MasterCourse.AddCourse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <style>
        .form-container {
            background-color: white;
            padding: 30px;
            border-radius: 16px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.05);
            max-width: 600px;
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
            border: 1px solid #ccc;
            border-radius: 8px;
            margin-bottom: 20px;
            font-size: 14px;
        }

        .form-container input[type="file"] {
            margin-bottom: 20px;
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
    </style>
    <title></title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-container">
        <h2>Add New Course</h2>

        <label>Course Name:</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Course Name Can't be Empty!" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

        <label>Course Thumbnail:</label><br />
        <asp:FileUpload ID="FileUpload1" runat="server" /><br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DropDownList1" ErrorMessage="Select Status!" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />

        <label>Status:</label>
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Text="-- Select Status --" Value=""></asp:ListItem>
            <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
            <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
        </asp:DropDownList><br /><br />

        <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn-submit" OnClick="Button1_Click" />
        </div>

</asp:Content>
