<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="SikshaNew.Admin.AdminDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Admin Dashboard</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script> 
    <style>
        .main {
            padding: 20px;
        }
        .dashboard-cards {
            display: flex;
            gap: 20px;
            flex-wrap: wrap;
        }
        .dashboard-card {
            background: #f5f6fa;
            border-radius: 12px;
            padding: 20px;
            flex: 1 1 30%;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
        }
        .dashboard-count {
            font-size: 24px;
            font-weight: bold;
            color: #2f3640;
        }
        .chart-box {
            width: 100%;
            height: 200px;
        }
        .section {
            margin-top: 40px;
        }
        .dropdown-row {
            margin-bottom: 20px;
        }
        .dropdown-row label {
            margin-right: 10px;
            font-weight: bold;
        }
        .styled-dropdown {
            padding: 5px 10px;
            font-size: 14px;
        }
        .styled-grid {
            margin-top: 20px;
            border-collapse: collapse;
            width: 100%;
        }
        .styled-grid th, .styled-grid td {
            border: 1px solid #ccc;
            padding: 8px;
            text-align: left;
        }
        .styled-grid th {
            background-color: #e1e1e1;
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
                <canvas id="totalUsersChart" class="chart-box"></canvas>
            </div>
            <div class="dashboard-card">
                <h4>Active Users</h4>
                <asp:Label ID="lblActiveUsers" runat="server" CssClass="dashboard-count" />
                <canvas id="userStatusChart" class="chart-box"></canvas>
            </div>
            <div class="dashboard-card">
                <h4>Inactive Users</h4>
                <asp:Label ID="lblInactiveUsers" runat="server" CssClass="dashboard-count" />
                <canvas id="inactiveUsersChart" class="chart-box"></canvas>
            </div>
        </div>

        <div class="dashboard-cards">
            <div class="dashboard-card">
                <h4>Total Courses</h4>
                <asp:Label ID="lblCourses" runat="server" CssClass="dashboard-count" />
                <canvas id="courseChart" class="chart-box"></canvas>
            </div>
            <div class="dashboard-card">
                <h4>Total Subcourses</h4>
                <asp:Label ID="lblSubCourses" runat="server" CssClass="dashboard-count" />
                <canvas id="subcourseChart" class="chart-box"></canvas>
            </div>
            <div class="dashboard-card">
                <h4>Sold Courses</h4>
                <asp:Label ID="lblSoldCourses" runat="server" CssClass="dashboard-count" />
                <canvas id="salesChart" class="chart-box"></canvas>
            </div>
        </div>

    
        <div class="section">
            <div class="dropdown-row">
                <label>Select Mastercourse:</label>
                <asp:DropDownList ID="ddlMastercourse" runat="server" AutoPostBack="true" CssClass="styled-dropdown" OnSelectedIndexChanged="ddlMastercourse_SelectedIndexChanged" />
            </div>
            <asp:GridView ID="gvSubcourses" runat="server" CssClass="styled-grid" AutoGenerateColumns="true" />
        </div>

    
        <div class="section">
            <div class="dropdown-row">
                <label>Select Year for Transactions:</label>
                <asp:DropDownList ID="ddlYear" runat="server" CssClass="styled-dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                    <asp:ListItem Text="2024" Value="2024" />
                    <asp:ListItem Text="2025" Value="2025" Selected="True" />
                </asp:DropDownList>
            </div>
            <canvas id="transactionChart" class="chart-box" style="height: 300px;"></canvas>
        </div>
    </div>


    <script>
        const totalUsers = parseInt("<%= lblTotalUsers.Text %>");
        const activeUsers = parseInt("<%= lblActiveUsers.Text %>");
        const inactiveUsers = parseInt("<%= lblInactiveUsers.Text %>");
        const totalCourses = parseInt("<%= lblCourses.Text %>");
        const totalSubcourses = parseInt("<%= lblSubCourses.Text %>");
        const soldCourses = parseInt("<%= lblSoldCourses.Text %>");
        const monthlySales = <%= monthlySalesJson %>;

        new Chart(document.getElementById("totalUsersChart"), {
            type: 'doughnut',
            data: {
                labels: ['Active Users', 'Inactive Users'],
                datasets: [{ data: [activeUsers, inactiveUsers], backgroundColor: ['#00a8ff', '#dcdde1'] }]
            }
        });

        new Chart(document.getElementById("userStatusChart"), {
            type: 'bar',
            data: {
                labels: ['Active', 'Inactive'],
                datasets: [{ label: 'Users', data: [activeUsers, inactiveUsers], backgroundColor: ['#00a8ff', '#f19066'] }]
            },
            options: { plugins: { legend: { display: false } } }
        });

        new Chart(document.getElementById("courseChart"), {
            type: 'bar',
            data: {
                labels: ['Courses', 'Subcourses'],
                datasets: [{ label: 'Course Info', data: [totalCourses, totalSubcourses], backgroundColor: ['#00cec9', '#6c5ce7'] }]
            },
            options: { plugins: { legend: { display: false } } }
        });

        new Chart(document.getElementById("salesChart"), {
            type: 'pie',
            data: {
                labels: ['Sold Courses', 'Remaining Courses'],
                datasets: [{ data: [soldCourses, totalCourses - soldCourses], backgroundColor: ['#00b894', '#ffeaa7'] }]
            }
        });

        new Chart(document.getElementById("subcourseChart"), {
            type: 'bar',
            data: {
                labels: ['Subcourses'],
                datasets: [{ label: 'Total Subcourses', data: [totalSubcourses], backgroundColor: ['#6c5ce7'] }]
            },
            options: { plugins: { legend: { display: false } } }
        });

        new Chart(document.getElementById("inactiveUsersChart"), {
            type: 'bar',
            data: {
                labels: ['Inactive Users'],
                datasets: [{ label: 'Inactive Users', data: [inactiveUsers], backgroundColor: ['#e17055'] }]
            },
            options: { plugins: { legend: { display: false } } }
        });

        new Chart(document.getElementById("transactionChart"), {
            type: 'bar',
            data: {
                labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                datasets: [{ label: 'Transaction Amount ₹', data: monthlySales, backgroundColor: '#0984e3' }]
            },
            options: { responsive: true, scales: { y: { beginAtZero: true } } }
        });
    </script>
</asp:Content>
