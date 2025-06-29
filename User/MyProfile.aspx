<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="SikshaNew.User.MyProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:GridView ID="gvProgress" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
    <Columns>
        <asp:BoundField DataField="subcourse" HeaderText="Course Name" />
        <asp:BoundField DataField="TotalTopics" HeaderText="Total Topics" />
        <asp:BoundField DataField="CompletedTopics" HeaderText="Completed" />
        <asp:TemplateField HeaderText="Progress">
            <ItemTemplate>
                <%# Eval("CompletionPercent") %>%
                <div class="progress">
                    <div class="progress-bar bg-success" role="progressbar"
                         style='width: <%# Eval("CompletionPercent") %>%'>
                    </div>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>


    <asp:GridView ID="gvCertificates" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="gvCertificates_RowCommand">
    <Columns>
        <asp:BoundField DataField="subcourse_name" HeaderText="Subcourse" />
        <asp:BoundField DataField="completed_on" HeaderText="Completed On" DataFormatString="{0:dd MMM yyyy}" />
        <asp:TemplateField HeaderText="Download">
            <ItemTemplate>
                <asp:LinkButton ID="lnkDownload" runat="server" CommandName="Download" CommandArgument='<%# Eval("subcourse_name") %>'>Download Certificate</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

</asp:Content>
