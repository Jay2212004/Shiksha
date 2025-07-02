<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandingPage.aspx.cs" Inherits="SikshaNew.LandingPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Courses</title>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <style>
        ..course-thumbnail {
    width: 100%;
    height: 180px;
    object-fit: cover;
}

        .review-box {
            background-color: #f9f9f9;
            border-left: 4px solid #007bff;
            padding: 10px;
            margin-top: 10px;
            border-radius: 5px;
        }
        .comment-item {
            font-size: 0.9rem;
            margin-bottom: 5px;
            padding-left: 10px;
        }
        .top-bar {
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Top Navigation Bar -->
        <div class="container-fluid bg-light py-2">
            <div class="container d-flex justify-content-between align-items-center">
                <h3 class="text-primary mb-0">Shiksha Courses</h3>
                <div style="text-align:right; margin: 10px;">
    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-success" OnClick="btnLogin_Click" />
</div>
            </div>
        </div>

        <!-- Course List Section -->
        <div class="container mt-4">
            <h4 class="mb-4 text-dark">Available Courses</h4>
            <div class="row">
                <asp:Repeater ID="rptSubcourses" runat="server" OnItemDataBound="rptSubcourses_ItemDataBound">
                    <ItemTemplate>
                        <div class="col-md-4 mb-4">
                            <div class="card h-100 shadow-sm">
                               <img class="card-img-top course-thumbnail" 
                          src='<%# ResolveUrl("~/" + Eval("subcourse_thumbnail")) %>'

                             alt="Course Image" />                               
                                <div class="card-body">
                                    <h5 class="card-title text-dark"><%# Eval("subcourse_name") %></h5>
                                    <p class="card-text mb-1"><strong>Rating:</strong> <%# Eval("avgStars") %> ★</p>
                                    <p class="card-text mb-2"><strong>Price:</strong> ₹<%# Eval("subcourses_price") %></p>
                                    <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart"
                                        CssClass="btn btn-primary btn-sm"
                                        CommandArgument='<%# Eval("subcourse_id") %>'
                                        OnClick="btnAddToCart_Click" />

                                    <!-- Comments Section -->
                                    <div class="review-box mt-3">
                                        <strong class="text-secondary">Feedback:</strong>
                                        <asp:Repeater ID="rptComments" runat="server">
                                            <ItemTemplate>
                                                <div class="comment-item">• <%# Eval("comment") %></div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>

    <!-- Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
