<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="SikshaNew.Admin.AdminDashboard" %>

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

.table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 15px;
}

.table-bordered {
    border: 1px solid #dee2e6;
}

.table-bordered th,
.table-bordered td {
    border: 1px solid #dee2e6;
    padding: 10px;
    text-align: left;
    vertical-align: middle;
}

.table th {
    background-color: #f8f9fa;
    color: #2f3640;
    font-weight: 600;
}

.table tr:hover {
    background-color: #f1f2f6;
}

.btn-sm {
    padding: 4px 10px;
    font-size: 13px;
    border-radius: 4px;
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
    <h2>Welcome, Admin</h2>
        <div class="dashboard-cards">
    <div class="dashboard-card">
        <h4>Total Users</h4>
        <asp:Label ID="lblTotalUsers" runat="server" CssClass="dashboard-count" />
        <div class="chart-box">Users Graph</div>
    </div>
    <div class="dashboard-card">
        <h4>Active Users</h4>
        <asp:Label ID="lblActiveUsers" runat="server" CssClass="dashboard-count" />
        <div class="chart-box">Active Status</div>
    </div>
    <div class="dashboard-card">
        <h4>Inactive Users</h4>
        <asp:Label ID="lblInactiveUsers" runat="server" CssClass="dashboard-count" />
        <div class="chart-box">Inactive Status</div>
    </div>
</div>

<div class="dashboard-cards">
    <div class="dashboard-card">
        <h4>Total Courses</h4>
        <asp:Label ID="lblCourses" runat="server" CssClass="dashboard-count" />
        <div class="chart-box">Course Info</div>
    </div>
    <div class="dashboard-card">
        <h4>Total Subcourses</h4>
        <asp:Label ID="lblSubCourses" runat="server" CssClass="dashboard-count" />
        <div class="chart-box">Subcourse Info</div>
    </div>
    <div class="dashboard-card">
        <h4>Sold Courses</h4>
        <asp:Label ID="lblSoldCourses" runat="server" CssClass="dashboard-count" />
        <div class="chart-box">Sales</div>
    </div>
</div>

</asp:Content>
