<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeFile="UserCourseList.aspx.cs" Inherits="SikshaNew.User.UserCourseList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .cart-header {
            margin: 20px 0;
            font-size: 18px;
            font-weight: 600;
        }

        .cart-count {
            font-weight: bold;
            color: #00a8ff;
            margin-left: 10px;
        }
    
        .filters {
            display: flex;
            gap: 20px;
            margin-bottom: 30px;
        }

        .filters select {
            padding: 8px;
            border-radius: 6px;
            border: 1px solid #ccc;
        }

        .course-listing {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
        }

        .course-card {
            width: 250px;
            border: 1px solid #ddd;
            border-radius: 10px;
            padding: 15px;
            box-shadow: 0 4px 10px rgba(0,0,0,0.05);
            background-color: #fff;
            transition: 0.3s ease;
        }

        .course-card:hover {
            box-shadow: 0 8px 18px rgba(0,0,0,0.1);
        }

        .course-card img {
            width: 100%;
            height: 150px;
            object-fit: cover;
            border-radius: 6px;
        }

        .course-card strong {
            display: block;
            font-size: 16px;
            margin-top: 10px;
            color: #333;
        }

        .course-card p {
            margin: 5px 0;
        }

        .course-card .btn-add {
            background-color: #00a8ff;
            color: white;
            border: none;
            padding: 8px 12px;
            border-radius: 6px;
            cursor: pointer;
            margin-top: 10px;
        }

        .course-card .btn-add:hover {
            background-color: #007acc;
        }
        .action-buttons {
            display: flex;
            gap: 20px;
            margin-top: 20px;
        }

        .action-buttons input[type="submit"] {
            padding: 10px 20px;
            border: none;
            border-radius: 6px;
            background-color: #00a8ff;
            color: white;
            cursor: pointer;
            transition: background-color 0.3s;
        }

        .action-buttons input[type="submit"]:hover {
            background-color: #007acc;
        }

        .cart-button {
            margin-top: 40px;
            padding: 10px 20px;
            background-color: #00a8ff;
            border: none;
            color: white;
            font-weight: bold;
            border-radius: 6px;
            cursor: pointer;
        }

        .cart-button:hover {
            background-color: #007acc;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
            <div class="cart-header">
        Cart Items: <asp:Label ID="lblCartCount" runat="server" Text="0" CssClass="cart-count" />
    </div>

    <div class="filters">
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" />
        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
            <asp:ListItem>Low to High</asp:ListItem>
            <asp:ListItem>High to Low</asp:ListItem>
        </asp:DropDownList>
    </div>

    <div class="course-listing">
        <asp:Repeater ID="rptSubCourses" runat="server">
            <ItemTemplate>
                <div class="course-card">
                    <img src='<%# Eval("subcourse_thumbnail") %>' alt="Thumbnail" />
                    <strong><%# Eval("subcourse_name") %></strong>
                    <p>Review: <%# Eval("stars") %></p>
                    <p>Price: ₹<%# Eval("subcourses_price") %></p>
                    <asp:Button 
                        ID="btnAddToCart" 
                        runat="server" 
                        Text="Add to Cart" 
                        CssClass="btn-add"
                        OnClick="btnAddToCart_Click" 
                        CommandArgument='<%# Eval("subcourse_id") + "|" + Eval("subcourse_name") + "|" + Eval("subcourses_price") + "|" + Eval("stars") + "|" + Eval("subcourse_thumbnail") %>' />
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>


    <br /><br />
<div class="action-buttons">
    <asp:Button ID="Cart" runat="server" Text="Go to Cart" OnClick="Cart_Click" />
</div>



</asp:Content>
