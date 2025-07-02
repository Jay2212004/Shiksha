<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeFile="trans.aspx.cs" Inherits="SikshaNew.Admin.trans" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .card-container {
            margin: 30px;
            padding: 30px;
            background-color: #ffffff;
            border-radius: 16px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
            font-family: 'Inter', sans-serif;
        }

        .card-container h2 {
            font-weight: 600;
            color: #2f3640;
            margin-bottom: 25px;
        }

        .styled-gridview {
            width: 100%;
            border-collapse: collapse;
        }

        .styled-gridview th {
            background-color: #00a8ff;
            color: white;
            text-align: left;
            padding: 12px 10px;
            font-weight: 600;
        }

        .styled-gridview td {
            background-color: #f8f9fa;
            padding: 10px;
            border-bottom: 1px solid #ddd;
        }

        .styled-gridview tr:nth-child(even) td {
            background-color: #e9ecef;
        }
    </style>
    <title></title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <div class="card-container">
        <h2>Transaction History</h2>

        <asp:GridView ID="gvTransactions" runat="server" AutoGenerateColumns="False" CssClass="styled-gridview" GridLines="None">
            <Columns>
                <asp:BoundField DataField="PaymentId" HeaderText="Payment ID" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:BoundField DataField="Amount" HeaderText="Amount (INR)" DataFormatString="{0:F2}" />
                <asp:BoundField DataField="PaymentDate" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy HH:mm}" />
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
