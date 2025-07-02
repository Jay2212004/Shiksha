<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UserAssignments.aspx.cs" Inherits="SikshaNew.Admin.UserAssignments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .assignment-container {
            padding: 20px;
            background-color: #f7f9fc;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
        }
        .grid-header {
            font-size: 24px;
            font-weight: 600;
            margin-bottom: 20px;
            color: #333;
        }
        .btn-success {
            margin-left: 10px;
        }
        .gridview th {
            background-color: #343a40;
            color: white;
        }
        .gridview td, .gridview th {
            text-align: center;
            vertical-align: middle;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="assignment-container">
        <div class="grid-header">User Assignment Submissions</div>
        <asp:GridView ID="gvUserAssignments" runat="server" AutoGenerateColumns="false"
            OnRowCommand="gvUserAssignments_RowCommand"
            OnRowDataBound="gvUserAssignments_RowDataBound"
            CssClass="table table-bordered table-striped gridview">
            <Columns>
                <asp:BoundField DataField="Uid" HeaderText="ID" />
                <asp:BoundField DataField="user_email" HeaderText="User Email" />
                <asp:BoundField DataField="subcoursename" HeaderText="Subcourse" />
                <asp:BoundField DataField="topicname" HeaderText="Topic" />
                <asp:BoundField DataField="uploaded_on" HeaderText="Uploaded On" DataFormatString="{0:dd-MM-yyyy HH:mm}" />
                <asp:TemplateField HeaderText="Download">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlDownload" runat="server" 
                            NavigateUrl='<%# "~/Assignments/" + Eval("filename") %>' 
                            Text="Download" Target="_blank" CssClass="btn btn-primary btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Score">
                    <ItemTemplate>
                        <div class="d-flex justify-content-center align-items-center">
                            <asp:TextBox ID="txtScore" runat="server" Text='<%# Eval("score") %>' Width="50px" CssClass="form-control form-control-sm d-inline" />
                            <asp:Button ID="btnSaveScore" runat="server" CommandName="SaveScore"
                                CommandArgument='<%# Eval("Uid") %>' Text="Save" CssClass="btn btn-sm btn-success" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
