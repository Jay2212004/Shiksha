<%@ Page Title="Payment Invoice" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="invoice.aspx.cs" Inherits="SikshaNew.User.invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .invoice-container {
            max-width: 800px;
            margin: 30px auto;
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 10px;
            font-family: Arial, sans-serif;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
        .invoice-header {
            text-align: center;
            margin-bottom: 20px;
        }
        .invoice-header h2 {
            margin: 0;
            color: #333;
        }
        .invoice-table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }
        .invoice-table th, .invoice-table td {
            padding: 10px;
            border: 1px solid #ccc;
        }
        .invoice-table th {
            background-color: #f0f0f0;
        }
        .total-amount {
            text-align: right;
            font-weight: bold;
            font-size: 16px;
            margin-top: 10px;
        }
        .pdf-btn {
            display: inline-block;
            padding: 10px 20px;
            background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 5px;
            text-align: center;
            cursor: pointer;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="invoice-container">
        <div class="invoice-header">
            <h2>Payment Invoice</h2>
        </div>

        <!-- Display Payment Details -->
        <p><strong>Payment ID:</strong> <asp:Label ID="lblPaymentId" runat="server" /></p>
        <p><strong>Status:</strong> <asp:Label ID="lblStatus" runat="server" /></p>
        <p><strong>Payment Date:</strong> <asp:Label ID="lblDate" runat="server" /></p>

        <!-- Table -->
        <asp:Repeater ID="rptInvoice" runat="server" OnItemDataBound="rptInvoice_ItemDataBound">
            <HeaderTemplate>
                <table class="invoice-table">
                    <tr>
                        <th>Subcourse Name</th>
                        <th>Price</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="lblSubcourseName" runat="server" Text='<%# Eval("SubcourseName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("SubcoursePrice", "{0:F2}") %>' />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <div class="total-amount">
            <asp:Label ID="lblTotal" runat="server" Text="Total Amount: ₹0.00" />
        </div>

        <asp:Button ID="btnDownloadPDF" runat="server" Text="Download PDF" CssClass="pdf-btn" OnClick="btnDownloadPDF_Click" />
    </div>
</asp:Content>
