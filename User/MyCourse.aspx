<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="MyCourse.aspx.cs" Inherits="SikshaNew.User.MyCourse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <%--<asp:Repeater ID="rptSubCourseCard" runat="server" OnItemCommand="rptSubCourseCard_ItemCommand1">
<ItemTemplate>
    <div class="card m-3" style="width: 18rem;">
        <img src='<%# Eval("subcourse_thumbnail") %>' class="card-img-top" alt="Thumbnail" style="height:180px; object-fit:cover;">
        <div class="card-body">
            <h5 class="card-title"><%# Eval("subcourse_name") %></h5>
            <p class="card-text">
                Total Topics: <%# Eval("TotalTopics") %><br />
                Price: ₹<%# Eval("Price") %>
            </p>
            <asp:Button ID="btnPlay" runat="server" Text="Play Course" CommandName="Play" CommandArgument='<%# Eval("subcourse_name") %>' CssClass="btn btn-primary" />
        </div>
    </div>
</ItemTemplate>
</asp:Repeater>

<div class="container-fluid mt-5">
    <div class="row">
        <!-- Left Panel: Video -->
        <div class="col-md-8">
            <asp:Literal ID="litVideo" runat="server" />
        </div>

        <!-- Right Panel: Topics -->
        <div class="col-md-4">
            <h5>Topics</h5>
            <asp:Repeater ID="rptTopics" runat="server" OnItemCommand="rptTopics_ItemCommand1">
                <ItemTemplate>
                    <div class="mb-2">
                        <asp:LinkButton runat="server" CommandName="PlayTopic" CommandArgument='<%# Eval("topic_id") + "," + Eval("video_url") + "," + Eval("is_completed") %>'>
                            <%# Eval("Topic") %>
                        </asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>--%>


    <asp:Panel ID="pnlCourseCards" runat="server">
    <asp:Repeater ID="rptSubCourseCard" runat="server" OnItemCommand="rptSubCourseCard_ItemCommand1">
        <ItemTemplate>
            <div class="card m-3" style="width: 18rem;">
                <img src='<%# Eval("subcourse_thumbnail") %>' class="card-img-top" alt="Thumbnail" style="height:180px; object-fit:cover;">
                <div class="card-body">
                    <h5 class="card-title"><%# Eval("subcourse_name") %></h5>
                    <p class="card-text">
                        Total Topics: <%# Eval("TotalTopics") %><br />
                        Price: ₹<%# Eval("Price") %></p>
                        Rating: <span style="color: orange;"><%# Eval("avgStars") %></span>
                    <asp:Button ID="btnPlay" runat="server" Text="Play Course" CommandName="Play" CommandArgument='<%# Eval("subcourse_name") %>' CssClass="btn btn-primary" />
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>


    <asp:Panel ID="pnlVideoTopics" runat="server" Visible="false">
    <div class="container-fluid mt-5">
        <div class="row">
            <!-- Left Panel: Video -->
            <div class="col-md-8">
                <asp:Literal ID="litVideo" runat="server" />
            </div>

            <!-- Right Panel: Topics -->
            <div class="col-md-4">
                <h5>Topics</h5>
                <asp:Repeater ID="rptTopics" runat="server" OnItemCommand="rptTopics_ItemCommand1">
                    <ItemTemplate>
                        <div class="mb-2">
                            <asp:LinkButton runat="server" CommandName="PlayTopic" CommandArgument='<%# Eval("topic_id") + "," + Eval("video_url") + "," + Eval("is_completed") %>'>
                                <%# Eval("Topic") %>
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <br />
        <asp:Button ID="btnBackToCourses" runat="server" CssClass="btn btn-secondary" Text="← Back to All Courses" OnClick="btnBackToCourses_Click" />
    </div>
</asp:Panel>


    <div class="mt-4">
    <ul class="nav nav-tabs">
        <li class="nav-item">
            <asp:LinkButton ID="lnkMCQ" runat="server" CssClass="nav-link" OnClick="lnkMCQ_Click">MCQ</asp:LinkButton>
        </li>
        <li class="nav-item">
            <asp:LinkButton ID="lnkAssignments" runat="server" CssClass="nav-link" OnClick="lnkAssignments_Click">Assignments</asp:LinkButton>
        </li>
        <li class="nav-item">
            <asp:LinkButton ID="lnkReviews" runat="server" CssClass="nav-link" OnClick="lnkReviews_Click">Review</asp:LinkButton>
        </li>
    </ul>
</div>

<div class="mt-3">
    <asp:Panel ID="pnlMCQ" runat="server" Visible="false">
        <h4>MCQs</h4>
        
        <div>
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" />
        <asp:Button ID="btn" runat="server" Text="Submit Answers" OnClick="btn_Click" />
        <br />
        <br />
        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
            <ItemTemplate>
                <div style="padding: 10px;">
                    <b>Q<%# Container.ItemIndex + 1 %>: <%# Eval("question") %></b><br />
                    <asp:RadioButtonList ID="rblOptions" runat="server" />
                    <asp:HiddenField ID="hfCorrect" runat="server" Value='<%# Eval("correct_answer") %>' />
                    <asp:HiddenField ID="hfOptionA" runat="server" Value='<%# Eval("option_a") %>' />
                    <asp:HiddenField ID="hfOptionB" runat="server" Value='<%# Eval("option_b") %>' />
                    <asp:HiddenField ID="hfOptionC" runat="server" Value='<%# Eval("option_c") %>' />
                    <asp:HiddenField ID="hfOptionD" runat="server" Value='<%# Eval("option_d") %>' />
                </div>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <div style="background-color: #f9f9f9; padding: 10px;">
                    <b>Q<%# Container.ItemIndex + 1 %>: <%# Eval("question") %></b><br />
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" />
                    <asp:HiddenField ID="hfOptionA" runat="server" Value='<%# Eval("option_a") %>' />
                    <asp:HiddenField ID="hfOptionB" runat="server" Value='<%# Eval("option_b") %>' />
                    <asp:HiddenField ID="hfOptionC" runat="server" Value='<%# Eval("option_c") %>' />
                    <asp:HiddenField ID="hfOptionD" runat="server" Value='<%# Eval("option_d") %>' />
                    <asp:HiddenField ID="hfCorrect" runat="server" Value='<%# Eval("correct_answer") %>' />

                </div>
            </AlternatingItemTemplate>
        </asp:Repeater>
        <asp:Label ID="lblScore" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Blue" Visible="false" />
    </div>



    </asp:Panel>

    <asp:Panel ID="pnlAssignments" runat="server" Visible="false">
        <h4>Assignments</h4>
        <!-- Aur assignm. ka idher -->
    </asp:Panel>

    <asp:Panel ID="pnlReviews" runat="server" Visible="false">
        <div>
       <h1>Give Your Review For the Course!</h1>
       <p>
           Rating:
           <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
           &nbsp;&nbsp;&nbsp;&nbsp;
           <asp:Label ID="Label1" runat="server" ForeColor="Orange" Font-Bold="true"></asp:Label>
       </p>
       <p>
           Comment:
           <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Columns="50" Rows="4"></asp:TextBox>
       </p>
       <p>
           <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
       </p>
   </div>
    </asp:Panel>
</div>





</asp:Content>
