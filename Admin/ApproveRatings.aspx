<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ApproveRatings.aspx.cs" Inherits="SikshaNew.Admin.ApproveRatings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:GridView ID="gvReviews" runat="server" AutoGenerateColumns="False" OnRowCommand="gvReviews_RowCommand" DataKeyNames="review_id">
    <Columns>
        <asp:BoundField DataField="review_id" HeaderText="ID" />
        <asp:BoundField DataField="subcourse_name" HeaderText="Subcourse" />
        <asp:BoundField DataField="user_email" HeaderText="User Email" />
        <asp:BoundField DataField="rating" HeaderText="Rating" />
        <asp:BoundField DataField="comment" HeaderText="Comment" />
        <asp:BoundField DataField="stars" HeaderText="Stars" />
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:Button ID="btnApprove" runat="server" Text="Approve" CommandName="Approve" CommandArgument='<%# Eval("review_id") %>' CssClass="btn btn-success btn-sm" />
                <asp:Button ID="btnReject" runat="server" Text="Reject" CommandName="Reject" CommandArgument='<%# Eval("review_id") %>' CssClass="btn btn-danger btn-sm" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>



</asp:Content>
