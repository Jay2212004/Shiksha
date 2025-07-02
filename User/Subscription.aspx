<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeFile="Subscription.aspx.cs" Inherits="SikshaNew.User.Subscription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" >
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="margin:20px;">
        <asp:Repeater ID="rptCards" runat="server" OnItemCommand="rptCards_ItemCommand">
            <ItemTemplate>
                <div class="col-md-4">
                    <div class="card mb-4 shadow-sm border-primary">
                        <img src='<%# Eval("IconUrl") %>' alt="Icon" class="card-img-top" style="height: 150px; object-fit: contain;" />
                        <div class="card-body">
                            <h4 class="card-title text-primary"><%# Eval("SubscriptionType") %> Subscription</h4>
                            <p><strong>Duration:</strong> <%# Eval("Duration") %></p>
                            <p><strong>Price:</strong> ₹<%# Eval("Price") %></p>
                            <p><strong>Includes:</strong></p>
                            <ul>
                                <asp:Repeater ID="rptSubcourses" runat="server" DataSource='<%# Eval("Subcourses") %>'>
                                    <ItemTemplate>
                                        <li><%# Container.DataItem %></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                            <asp:Button ID="btnBuy" runat="server" Text="Buy Now"
                                CssClass="btn btn-outline-primary"
                                CommandName="Buy"
                                CommandArgument='<%# Eval("SubscriptionType") %>' />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
