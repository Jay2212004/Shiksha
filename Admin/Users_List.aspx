<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Users_List.aspx.cs" Inherits="SikshaNew.Admin.Users_List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>User List</title>
<style>
        .table-container {
            margin: 30px;
            font-family: 'Segoe UI', sans-serif;
        }

        .table-container h2 {
            margin-bottom: 20px;
            color: #007BFF;
            font-weight: 600;
        }

        .gridview {
            width: 100%;
            border-collapse: collapse;
        }

        .gridview th {
            background-color: #007BFF;
            color: white;
            text-align: left;
            padding: 10px;
            border: 1px solid #ddd;
        }

        .gridview td {
            padding: 10px;
            border: 1px solid #ddd;
        }

        .gridview tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        .gridview tr:hover {
            background-color: #f1f1f1;
        }

        .gridview .aspNetDisabled {
            color: gray;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
        <div class="table-container">
        <h2>User List</h2>
        <asp:GridView 
            ID="GridViewUsers" 
            runat="server" 
            AutoGenerateColumns="False" 
            CssClass="gridview" 
            GridLines="None"
            OnRowEditing="GridViewUsers_RowEditing" 
            OnRowCancelingEdit="GridViewUsers_RowCancelingEdit"
            OnRowUpdating="GridViewUsers_RowUpdating" 
            OnRowDeleting="GridViewUsers_RowDeleting"
            DataKeyNames="UserID">

            <Columns>
                <asp:BoundField DataField="UserID" HeaderText="User ID" ReadOnly="True" />
                <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Role" HeaderText="Role" />
                <asp:BoundField DataField="status" HeaderText="Status" />
                <asp:BoundField DataField="CreatedAt" HeaderText="Created At" DataFormatString="{0:yyyy-MM-dd}" ReadOnly="True" />
                <asp:BoundField DataField="CreatedBy" HeaderText="Created By" ReadOnly="True" />
                <asp:BoundField DataField="ModifiedAt" HeaderText="Modified At" DataFormatString="{0:yyyy-MM-dd}" ReadOnly="True" />
                <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By" ReadOnly="True" />
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>

        </asp:GridView>
    </div>
</asp:Content>