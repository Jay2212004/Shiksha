<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="SubCourse.aspx.cs" Inherits="SikshaNew.Admin.MasterCourse.SubCourse" %>
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

        .form-container h3 {
            margin-bottom: 25px;
            font-weight: 600;
            color: #2f3640;
        }

        .form-label {
            font-weight: 500;
            display: block;
            margin-bottom: 8px;
        }

        .form-control {
            width: 100%;
            padding: 10px 12px;
            margin-bottom: 20px;
            border-radius: 8px;
            border: 1px solid #ccc;
        }

        .btn-submit {
            background-color: #2f3640;
            color: white;
            padding: 10px 24px;
            border: none;
            border-radius: 8px;
            cursor: pointer;
        }

        .btn-submit:hover {
            background-color: #00a8ff;
        }
    </style>
    <title></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
            <div class="form-container">
        <h3>Add SubCourse</h3>

        <label class="form-label">Course Name:&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="DropDownList1" ErrorMessage="Select Course!" ForeColor="Red"></asp:RequiredFieldValidator>
                </label>
        &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" />

        <label class="form-label">SubCourse Name:&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox1" ErrorMessage="Select Sub Course!" ForeColor="Red"></asp:RequiredFieldValidator>
                </label>
        &nbsp;<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" />

        <label class="form-label">Thumbnail:</label>
        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />

        <label class="form-label">Course Price:&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox2" ErrorMessage="Add Price!" ForeColor="Red"></asp:RequiredFieldValidator>
                </label>
        &nbsp;<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" />

        <label class="form-label">Status:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="DropDownList2" ErrorMessage="Select Status!" ForeColor="Red"></asp:RequiredFieldValidator>
                </label>
        &nbsp;<asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>Active</asp:ListItem>
            <asp:ListItem>Inactive</asp:ListItem>
        </asp:DropDownList>

        <asp:Button ID="Button1" runat="server" Text="Create" CssClass="btn-submit" OnClick="Button1_Click" />
    </div>
        </div>

</asp:Content>
