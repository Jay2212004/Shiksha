<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="SikshaNew.Admin.AdminDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Admin Dashboard</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;600&display=swap" rel="stylesheet" />
    <style>
        body {
            font-family: 'Inter', sans-serif;
            margin: 0;
            background: linear-gradient(to right, #1e272e, #485460);
            color: #fff;
        }
        .sidebar {
            position: fixed;
            top: 0;
            left: 0;
            height: 100vh;
            width: 250px;
            background-color: #2f3640;
            padding: 30px 20px;
            box-shadow: 2px 0 5px rgba(0,0,0,0.1);
        }
        .sidebar h2 {
            color: #00a8ff;
            margin-bottom: 30px;
        }
        .sidebar ul {
            list-style: none;
            padding: 0;
        }
        .sidebar ul li {
            padding: 12px 10px;
            color: #dcdde1;
            margin-bottom: 10px;
            border-radius: 8px;
            cursor: pointer;
        }
        .sidebar ul li.active,
        .sidebar ul li:hover {
            background-color: #00a8ff;
            color: white;
        }
        .main {
            margin-left: 270px;
            padding: 30px;
            background-color: #f5f6fa;
            min-height: 100vh;
            color: #2f3640;
        }
        .top-cards, .graphs-row {
            display: flex;
            gap: 20px;
            margin-bottom: 20px;
        }
        .card {
            background-color: white;
            flex: 1;
            padding: 20px;
            border-radius: 16px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.05);
        }
        .chart-box {
            height: 180px;
            background-color: #dcdde1;
            border-radius: 12px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-weight: bold;
            color: #2f3640;
        }
        .right-panel {
            width: 300px;
            margin-left: 20px;
        }
        .section {
            background-color: white;
            padding: 20px;
            border-radius: 16px;
            margin-bottom: 20px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.05);
        }
        .section h4 {
            margin-bottom: 10px;
        }
        .progress-bar {
            height: 6px;
            background-color: #ccc;
            border-radius: 6px;
            margin: 5px 0 15px;
            overflow: hidden;
        }
        .progress-bar .fill {
            height: 100%;
            background-color: #00a8ff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="sidebar">
    <h2>AdminPanel</h2>
    <ul>
        <li class="active">Dashboard</li>
        <li>Users</li>
        <li>Reports</li>
        <li>Performance</li>
        <li>Departments</li>
        <li>Settings</li>
    </ul>
</div>

<div class="main">
    <h2>Welcome, Admin</h2>

    <div class="top-cards">
        <div class="card">
            <h4>Total Users</h4>
            <p>1,245</p>
            <div class="chart-box">Users Graph</div>
        </div>
        <div class="card">
            <h4>Active Sessions</h4>
            <p>312</p>
            <div class="chart-box">Session Chart</div>
        </div>
        <div class="card">
            <h4>Reports Generated</h4>
            <p>85</p>
            <div class="chart-box">Report Stats</div>
        </div>
    </div>

    <div class="top-cards">
        <div class="card" style="flex: 2">
            <h4>Department Activity</h4>
            <div class="chart-box">Department Graph</div>
        </div>
        <div class="card">
            <h4>Top Performing Team</h4>
            <div class="chart-box">Team Chart</div>
        </div>
    </div>

    <div class="top-cards">
        <div class="card">
            <h4>Daily Logins</h4>
            <div class="chart-box">Bar Graph</div>
        </div>
    </div>
</div>
    <div class="section">
        <h4>Notifications</h4>
        <ul style="padding-left: 20px; color: #2f3640">
            <li>New user registered</li>
            <li>Server backup completed</li>
            <li>Report generation scheduled</li>
        </ul>
    </div>
</asp:Content>

