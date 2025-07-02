<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="MCQ.aspx.cs" Inherits="SikshaNew.Admin.Material.MCQ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"><title>Add MCQs</title>

     <style>
        .form-section {
            background-color: white;
            padding: 30px;
            border-radius: 16px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.05);
            font-family: 'Inter', sans-serif;
            max-width: 1000px;
            margin: 40px auto;
        }

        .form-section select,
        .form-section input[type="text"],
        .form-section .aspNet-TextBox {
            width: 100%;
            padding: 8px 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 6px;
        }

        .form-section .btn {
            background-color: #2f3640;
            color: white;
            padding: 8px 16px;
            border: none;
            border-radius: 8px;
            cursor: pointer;
            margin-bottom: 20px;
        }

        .form-section .btn:hover {
            background-color: #00a8ff;
        }

        .form-section table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 25px;
            background-color: #f9f9f9;
            border: 1px solid #ddd;
        }

        .form-section table td {
            padding: 10px;
            vertical-align: top;
        }

        .form-section table tr:nth-child(even) {
            background-color: #f1f2f6;
        }

        .form-section table tr:first-child td {
            font-weight: bold;
            background-color: #dcdde1;
        }

        .form-section .delete-link {
            color: red;
            text-decoration: none;
            font-weight: bold;
        }

        .form-section .delete-link:hover {
            text-decoration: underline;
        }

        .form-section .gridview-style {
            width: 100%;
            margin-top: 20px;
            border: 1px solid #ccc;
        }
    </style>
    <title></title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
                <div class="form-section">
        <h2>Add MCQ Questions</h2>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="DropDownList1" ErrorMessage="Need to select one!" ForeColor="Red"></asp:RequiredFieldValidator>

        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="form-select" />
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="DropDownList2" ErrorMessage="Need to select one!" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <br />
        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" CssClass="form-select" />
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="DropDownList3" ErrorMessage="Need to select one!" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
        <asp:DropDownList ID="DropDownList3" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" CssClass="form-select" />

        <asp:Button ID="Button1" runat="server" Text="Add Question" CssClass="btn" OnClick="Button1_Click" />
        <br /><br />

        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand1">
            <ItemTemplate>
                <table>
                    <tr>
                        <td colspan="2"><b>Question #<%# Container.ItemIndex + 1 %></b></td>
                    </tr>
                    <tr>
                        <td style="width: 120px;">Question:</td>
                        <td><asp:TextBox ID="TextBox1" runat="server" CssClass="aspNet-TextBox" Text='<%# Eval("Question") %>' /></td>
                    </tr>
                    <tr>
                        <td>Option A:</td>
                        <td><asp:TextBox ID="TextBox2" runat="server" CssClass="aspNet-TextBox" Text='<%# Eval("OptionA") %>' /></td>
                    </tr>
                    <tr>
                        <td>Option B:</td>
                        <td><asp:TextBox ID="TextBox3" runat="server" CssClass="aspNet-TextBox" Text='<%# Eval("OptionB") %>' /></td>
                    </tr>
                    <tr>
                        <td>Option C:</td>
                        <td><asp:TextBox ID="TextBox4" runat="server" CssClass="aspNet-TextBox" Text='<%# Eval("OptionC") %>' /></td>
                    </tr>
                    <tr>
                        <td>Option D:</td>
                        <td><asp:TextBox ID="TextBox5" runat="server" CssClass="aspNet-TextBox" Text='<%# Eval("OptionD") %>' /></td>
                    </tr>
                    <tr>
                        <td>Correct Answer:</td>
                        <td>
                            <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-select">
                                <asp:ListItem Text="Select" Value="" />
                                <asp:ListItem Text="A" Value="A" />
                                <asp:ListItem Text="B" Value="B" />
                                <asp:ListItem Text="C" Value="C" />
                                <asp:ListItem Text="D" Value="D" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="DeleteRow" CommandArgument='<%# Container.ItemIndex %>' Text="Delete" CssClass="delete-link" />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Button ID="Button2" runat="server" Text="Save All" CssClass="btn" OnClick="Button2_Click" />

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered gridview-style"
            HeaderStyle-BackColor="#d3d3d3" BorderStyle="Solid" BorderWidth="1px" CellPadding="5" />
    </div>

        </div>

</asp:Content>
