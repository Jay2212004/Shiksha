<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Subscription_List.aspx.cs" Inherits="SikshaNew.Admin.Subscription.Subscription_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-container { margin: 30px; font-family: Arial; }
        .table-container h2 { margin-bottom: 20px; color: #007BFF; }
        .gridview th { background-color: #007BFF; color: white; padding: 8px; }
        .gridview td { padding: 8px; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table-container">
        <h2>Subscription List</h2>
       <asp:GridView ID="GridViewSubscriptions" runat="server" AutoGenerateColumns="False" CssClass="gridview" GridLines="Both"
    DataKeyNames="id"
    OnRowEditing="GridViewSubscriptions_RowEditing"
    OnRowUpdating="GridViewSubscriptions_RowUpdating"
    OnRowCancelingEdit="GridViewSubscriptions_RowCancelingEdit"
    OnRowDeleting="GridViewSubscriptions_RowDeleting"
    OnRowDataBound="GridViewSubscriptions_RowDataBound">
    <Columns>
        <asp:BoundField HeaderText="Subcourse Name" DataField="SubcourseName" ReadOnly="True" />
        <asp:TemplateField HeaderText="Status">
            <ItemTemplate><%# Eval("Status") %></ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtStatus" runat="server" Text='<%# Bind("Status") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="Master Course" DataField="MasterCourse" ReadOnly="True" />

        <asp:TemplateField HeaderText="Subscription Type">
            <ItemTemplate><%# Eval("SubscriptionType") %></ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="ddlSubscriptionType" runat="server"></asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Price">
            <ItemTemplate><%# Eval("Price") %></ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtPrice" runat="server" Text='<%# Bind("Price") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Duration">
            <ItemTemplate><%# Eval("Duration") %></ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtDuration" runat="server" Text='<%# Bind("Duration") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:BoundField HeaderText="Created At" DataField="CreatedAt" DataFormatString="{0:yyyy-MM-dd}" ReadOnly="True" />
        <asp:BoundField HeaderText="Created By" DataField="CreatedBy" ReadOnly="True" />
        <asp:BoundField HeaderText="Modified At" DataField="ModifiedAt" DataFormatString="{0:yyyy-MM-dd}" ReadOnly="True" />
        <asp:BoundField HeaderText="Modified By" DataField="ModifiedBy" ReadOnly="True" />
        <asp:BoundField HeaderText="Validity" DataField="Validity" ReadOnly="True" />
        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
    </Columns>
</asp:GridView>

    </div>
</asp:Content>
