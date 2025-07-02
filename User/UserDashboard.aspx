<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="UserDashboard.aspx.cs" Inherits="SikshaNew.User.UserDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Admin Dashboard</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />
    <style>
  body {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
    font-family: 'Inter', sans-serif;
    background-color: #f1f2f6;
    overflow-x: hidden;
    color: #2f3640;
}

.sidebar {
    width: 250px;
    background: #2f3640;
    color: white;
    padding: 20px;
    height: 100vh;
    overflow-y: auto;
    position: fixed;
    top: 0;
    left: 0;
}

.sidebar h2 {
    margin-bottom: 20px;
    color: #00a8ff;
}

.menu {
    list-style: none;
    padding: 0;
    margin: 0;
}

.menu > li {
    margin-bottom: 10px;
}

.menu-header {
    padding: 10px;
    border-radius: 5px;
    cursor: pointer;
    background-color: #353b48;
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.menu-header:hover {
    background-color: #00a8ff;
}

.submenu {
    list-style: none;
    padding-left: 20px;
    display: none;
}

.submenu li a {
    display: block;
    padding: 8px;
    border-radius: 5px;
    color: #dcdde1;
    text-decoration: none;
}

.submenu li a:hover {
    background-color: #00a8ff;
    color: white;
}

.float-right {
    margin-left: auto;
}



.dashboard-cards {
    display: flex;
    gap: 20px;
    flex-wrap: wrap;
    margin-bottom: 30px;
}

.dashboard-card {
    background-color: white;
    flex: 1;
    min-width: 250px;
    padding: 20px;
    border-radius: 16px;
    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.05);
}

.dashboard-card.wide {
    flex: 2;
}

.dashboard-card h4 {
    margin-bottom: 10px;
}

.dashboard-count {
    font-size: 26px;
    font-weight: bold;
    color: #00a8ff;
    margin-bottom: 10px;
    display: block;
}

.chart-box {
    height: 150px;
    background-color: #dcdde1;
    border-radius: 12px;
    display: flex;
    align-items: center;
    justify-content: center;
    font-weight: bold;
}

.section {
    background-color: white;
    padding: 20px;
    border-radius: 16px;
    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.05);
    margin-top: 30px;
}

.section h4 {
    margin-bottom: 15px;
}

.section ul {
    list-style: disc;
    padding-left: 20px;
}

</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
    <div class="main">
<h2>Welcome, <asp:Label ID="lblUserName" runat="server" /></h2>


   <div class="dashboard-cards">
    <div class="dashboard-card">
        <h4>Courses Subscribed</h4>
        <asp:Label ID="lblCoursesSubscribed" runat="server" CssClass="dashboard-count" />
        <div class="chart-box">Subscribed</div>
    </div>
    <div class="dashboard-card">
        <h4>Active Courses</h4>
        <asp:Label ID="lblActiveCourses" runat="server" CssClass="dashboard-count" />
        <div class="chart-box">Active</div>
    </div>
    <div class="dashboard-card">
        <h4>Total Amount Spent</h4>
        <asp:Label ID="lblAmountSpent" runat="server" CssClass="dashboard-count" />
        <div class="chart-box">₹ Spent</div>
    </div>
</div>

<div class="dashboard-cards">
    <div class="dashboard-card">
        <h4>All Available Subcourses</h4>
        <asp:Label ID="lblAllSubCourses" runat="server" CssClass="dashboard-count" />
        <div class="chart-box">Subcourses</div>
    </div>
    <div class="dashboard-card wide">
        <h4>Latest Enrolled Course</h4>
        <asp:Label ID="lblLatestCourse" runat="server" CssClass="dashboard-count" />
        <div class="chart-box">Recent Enroll</div>
    </div>
</div>

</asp:Content>


