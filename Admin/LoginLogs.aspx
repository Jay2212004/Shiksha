<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="LoginLogs.aspx.cs" Inherits="SikshaNew.Admin.LoginLogs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <title>Login Logs</title>
    <style>
        .gridview {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
            font-family: Arial;
        }

        .gridview th, .gridview td {
            padding: 12px;
            border: 1px solid #ddd;
            text-align: center;
        }

        .gridview th {
            background-color: #007BFF;
            color: white;
        }

        .gridview tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .gridview tr:hover {
            background-color: #e0e0e0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:Label ID="lblMessage" runat="server" CssClass="error-label"></asp:Label>

     <asp:GridView ID="GridView1" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataKeyNames="user_id"
            OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting">
            <Columns>
                <asp:BoundField DataField="user_id" HeaderText="User ID" ReadOnly="True" />
                <asp:BoundField DataField="username" HeaderText="Username" />
                <asp:BoundField DataField="user_role" HeaderText="User Role" />
                <asp:BoundField DataField="lastlogin" HeaderText="Last Login" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />

                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
</asp:Content>
