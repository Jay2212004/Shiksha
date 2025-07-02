<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="SikshaNew.User.MyProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container py-4">

        <div class="container-fluid">
<div class="mx-auto" style="max-width: 900px;">
    
    <!-- User Info Card -->
    <asp:Panel ID="pnlUserInfo" runat="server" CssClass="card shadow-lg p-4 mb-4" Style="max-width: 600px;">
        <div class="d-flex align-items-center">
            <asp:Image ID="imgProfile" runat="server" Width="100px" Height="100px" CssClass="rounded-circle me-4 border border-2 border-secondary" />
            <div>
                <h4 class="fw-bold mb-1 text-primary"><asp:Label ID="lblFullName" runat="server" /></h4>
                <p class="mb-1 text-muted">📧 <asp:Label ID="lblEmail" runat="server" /></p>
                <p class="mb-0 text-muted">🗓️ Registered On: <asp:Label ID="lblRegDate" runat="server" /></p>
            </div>
        </div>
    </asp:Panel>

    

        <h4 class="text-primary mb-3">My Course Progress</h4>
        <asp:GridView ID="gvProgress" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered shadow-sm mb-5">
            <Columns>
                <asp:BoundField DataField="subcourse" HeaderText="Course Name" />
                <asp:BoundField DataField="TotalTopics" HeaderText="Total Topics" />
                <asp:BoundField DataField="CompletedTopics" HeaderText="Completed" />
                <asp:TemplateField HeaderText="Progress">
                    <ItemTemplate>
                        <%# Eval("CompletionPercent") %>%
                        <div class="progress">
                            <div class="progress-bar bg-success" role="progressbar"
                                 style='width: <%# Eval("CompletionPercent") %>%' />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <h4 class="text-primary mb-3"> Certificates</h4>
        <asp:GridView ID="gvCertificates" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-bordered shadow-sm mb-5" OnRowCommand="gvCertificates_RowCommand">
            <Columns>
                <asp:BoundField DataField="subcourse_name" HeaderText="Subcourse" />
                <asp:BoundField DataField="completed_on" HeaderText="Completed On" DataFormatString="{0:dd MMM yyyy}" />
                <asp:TemplateField HeaderText="Download">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" runat="server" CommandName="Download" CommandArgument='<%# Eval("subcourse_name") %>' CssClass="btn btn-sm btn-outline-primary">
                            Download
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <h4 class="text-primary mb-3">My Course Reviews</h4>
        <asp:GridView ID="gvMyReviews" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered shadow-sm mb-5">
            <Columns>
                <asp:BoundField DataField="subcourse_name" HeaderText="Sub Course" />
                <asp:BoundField DataField="rating" HeaderText="Rating" />
                <asp:BoundField DataField="stars" HeaderText="Stars" />
                <asp:BoundField DataField="comment" HeaderText="Comment" />
                <asp:BoundField DataField="ReviewStatus" HeaderText="Status" />
                <asp:BoundField DataField="submitted_at" HeaderText="Submitted On" DataFormatString="{0:dd MMM yyyy}" />
            </Columns>
        </asp:GridView>

    </div>
</div>

</div>

</asp:Content>
