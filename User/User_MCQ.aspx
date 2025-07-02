<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="User_MCQ.aspx.cs" Inherits="SikshaNew.User.User_MCQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <style>
        .mcq-container {
            background-color: white;
            padding: 30px;
            border-radius: 16px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.05);
            max-width: 800px;
            margin: 40px auto;
            font-family: 'Inter', sans-serif;
        }

        .mcq-container h3 {
            margin-bottom: 20px;
            font-weight: 600;
            color: #2f3640;
        }

        .question-box {
            margin-bottom: 25px;
            padding: 20px;
            border: 1px solid #dcdde1;
            border-radius: 12px;
            background-color: #f5f6fa;
        }

        .question-box.alternate {
            background-color: #eef2f5;
        }

        .question-label {
            font-weight: bold;
            color: #2f3640;
        }

        .submit-btn {
            background-color: #2f3640;
            color: white;
            border: none;
            padding: 10px 24px;
            border-radius: 8px;
            font-size: 16px;
            cursor: pointer;
            margin-top: 20px;
        }

        .submit-btn:hover {
            background-color: #00a8ff;
        }

        .result-label {
            display: block;
            text-align: center;
            margin-top: 30px;
            font-weight: bold;
            font-size: 18px;
            color: #00a8ff;
        }
    </style>


    <title>MCQ Test</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
                <div class="mcq-container">
        <h3>Multiple Choice Questions</h3>

        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" CssClass="form-control mb-3" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" />

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="DropDownList1" ErrorMessage="Select Topic!" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />

        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
            <ItemTemplate>
                <div class="question-box">
                    <div class="question-label">
                        Q<%# Container.ItemIndex + 1 %>: <%# Eval("question") %>
                    </div>
                    <asp:RadioButtonList ID="rblOptions" runat="server" CssClass="form-control" />
                    <asp:HiddenField ID="hfCorrect" runat="server" Value='<%# Eval("correct_answer") %>' />
                    <asp:HiddenField ID="hfOptionA" runat="server" Value='<%# Eval("option_a") %>' />
                    <asp:HiddenField ID="hfOptionB" runat="server" Value='<%# Eval("option_b") %>' />
                    <asp:HiddenField ID="hfOptionC" runat="server" Value='<%# Eval("option_c") %>' />
                    <asp:HiddenField ID="hfOptionD" runat="server" Value='<%# Eval("option_d") %>' />
                </div>
            </ItemTemplate>

            <AlternatingItemTemplate>
                <div class="question-box alternate">
                    <div class="question-label">
                        Q<%# Container.ItemIndex + 1 %>: <%# Eval("question") %>
                    </div>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="form-control" />
                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("correct_answer") %>' />
                    <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("option_a") %>' />
                    <asp:HiddenField ID="HiddenField3" runat="server" Value='<%# Eval("option_b") %>' />
                    <asp:HiddenField ID="HiddenField4" runat="server" Value='<%# Eval("option_c") %>' />
                    <asp:HiddenField ID="HiddenField5" runat="server" Value='<%# Eval("option_d") %>' />
                </div>
            </AlternatingItemTemplate>
        </asp:Repeater>

        <asp:Button ID="Button1" runat="server" Text="Submit Answers" CssClass="submit-btn" OnClick="Button1_Click" />

        <asp:Label ID="Label1" runat="server" CssClass="result-label" Visible="false" />
    </div>
        </div>

</asp:Content>
