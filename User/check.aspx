<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="check.aspx.cs" Inherits="SikshaNew.User.check" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="https://checkout.razorpay.com/v1/checkout.js"></script>
     <style>
        .checkout-container {
            background-color: white;
            padding: 30px;
            border-radius: 16px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.05);
            max-width: 900px;
            margin: 40px auto;
            font-family: 'Inter', sans-serif;
        }

        .checkout-container h2 {
            font-weight: 600;
            margin-bottom: 25px;
            color: #2f3640;
        }

        .table {
            border: 1px solid #dcdde1;
            border-radius: 12px;
            overflow: hidden;
        }

        .table th {
            background-color: #f5f6fa;
            color: #2f3640;
        }

        .form-label {
            font-size: 16px;
            color: #2f3640;
        }

        .pay-btn {
            margin-top: 20px;
            background-color: #2f3640;
            color: white;
            border: none;
            padding: 12px 28px;
            border-radius: 10px;
            font-size: 16px;
            cursor: pointer;
        }

        .pay-btn:hover {
            background-color: #00a8ff;
        }

        .total-amount-label {
            font-size: 18px;
            margin-top: 20px;
            color: #2f3640;
        }
    </style>
    <title></title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    
       <div class="checkout-container">
        <h2>Checkout</h2>

        <div style="margin-bottom: 20px;">
            <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" 
                          EmptyDataText="No items in the cart">
                <Columns>
                    <asp:BoundField DataField="SubcourseName" HeaderText="Name" />
                    <asp:BoundField DataField="SubcoursePrice" HeaderText="Price (INR)" />
                    <asp:BoundField DataField="Stars" HeaderText="Stars" />
                    <asp:TemplateField HeaderText="Thumbnail">
                        <ItemTemplate>
                            <asp:Image ID="imgThumb" runat="server" ImageUrl='<%# Eval("Thumbnail") %>' Width="100px" Height="60px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <div class="total-amount-label">
            <label><strong>Total Amount (INR):</strong></label>
            <asp:Label ID="lblTotalAmount" runat="server" CssClass="form-label" Font-Bold="true" />
        </div>

       
    <asp:Button ID="Button1" runat="server" Text="Send OTP" OnClick="Button1_Click" CssClass="pay-btn" />
<br /><br />
<asp:TextBox ID="TextBox1" runat="server" Visible="false" CssClass="form-label" Placeholder="Enter OTP"></asp:TextBox>
&nbsp;
           <br /><br />
<asp:Button ID="verify" runat="server" Text="Verify OTP" OnClick="Button2_Click" CssClass="pay-btn" Visible="false" />
            &nbsp;<asp:Button ID="btnPayNow" runat="server" Text="Pay Now" CssClass="pay-btn" OnClick="btnPayNow_Click" Visible="False"/>
       </div>


</asp:Content>
