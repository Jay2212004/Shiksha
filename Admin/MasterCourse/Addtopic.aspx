<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Addtopic.aspx.cs" Inherits="SikshaNew.Admin.MasterCourse.Addtopic" %>
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
        .form-container input[type="text"] {
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
    </style>
    <title></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
            <div class="form-container">
        <h2>Add Topic</h2>

        <label>MasterCourse Name:&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropDownList1" ErrorMessage="Select Master Course!" ForeColor="Red"></asp:RequiredFieldValidator>
                </label>
        &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1"></asp:DropDownList>

        <label>SubCourse Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DropDownList2" ErrorMessage="Select Sub Course!" ForeColor="Red"></asp:RequiredFieldValidator>
                </label>
        &nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"></asp:DropDownList>

        <label>Topic Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox1" ErrorMessage="Topic Name Can't be empty" ForeColor="Red"></asp:RequiredFieldValidator>
                </label>
        &nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

        <label>Video URL:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox2" ErrorMessage="Video URL Can't be empty" ForeColor="Red"></asp:RequiredFieldValidator>
                </label>
&nbsp;<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>

        <label>Status:&nbsp;&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="DropDownList3" ErrorMessage="Select Status!" ForeColor="Red"></asp:RequiredFieldValidator>
                </label>
&nbsp;<asp:DropDownList ID="DropDownList3" runat="server">
            <asp:ListItem Text="-- Select Status --" Value="" />
            <asp:ListItem Text="Active" Value="Active" />
            <asp:ListItem Text="Inactive" Value="Inactive" />
        </asp:DropDownList>

        <asp:Button ID="Button1" runat="server" Text="Add" CssClass="btn-submit" OnClick="Button1_Click" />
    </div>
        </div>

</asp:Content>