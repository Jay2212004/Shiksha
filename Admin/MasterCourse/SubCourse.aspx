<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="SubCourse.aspx.cs" Inherits="SikshaNew.Admin.MasterCourse.SubCourse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
    Course Name:<asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
</p>
<p>
    SubCourse Name:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
</p>
<p>
    Thumbnail:<asp:FileUpload ID="FileUpload1" runat="server" />
</p>
<p>
    Course Price:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
</p>
<p>
    Status:<asp:DropDownList ID="DropDownList2" runat="server">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>Active</asp:ListItem>
        <asp:ListItem>Inactive</asp:ListItem>
    </asp:DropDownList>
</p>
<p>
    <asp:Button ID="Button1" runat="server" Text="Create" OnClick="Button1_Click" />
</p>
</asp:Content>
