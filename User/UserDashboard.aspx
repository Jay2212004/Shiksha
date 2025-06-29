<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="UserDashboard.aspx.cs" Inherits="SikshaNew.User.UserDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Elearn Dashboard</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;600&display=swap" rel="stylesheet" />
    <style>
        body {
            font-family: 'Inter', sans-serif;
            margin: 0;
            background: linear-gradient(to right, #6C5CE7, #341f97);
        }
        .sidebar {
            position: fixed;
            top: 0;
            left: 0;
            height: 100vh;
            width: 240px;
            background-color: white;
            padding: 30px 20px;
            box-shadow: 2px 0 5px rgba(0,0,0,0.1);
        }
        .sidebar h2 {
            color: #6C5CE7;
            margin-bottom: 30px;
        }
        .sidebar ul {
            list-style: none;
            padding: 0;
        }
        .sidebar ul li {
            padding: 12px 10px;
            color: #444;
            margin-bottom: 8px;
            border-radius: 8px;
            cursor: pointer;
        }
        .sidebar ul li.active,
        .sidebar ul li:hover {
            background-color: #6C5CE7;
            color: white;
        }
        .main {
            margin-left: 260px;
            padding: 30px;
            background-color: #f7f8fc;
            min-height: 100vh;
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
        .line-chart, .radar-chart, .bar-chart, .donut-chart {
            height: 180px;
            background-color: #e9e9ff;
            border-radius: 12px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-weight: bold;
            color: #6C5CE7;
        }
        .right-panel {
            width: 300px;
            margin-left: 20px;
        }
        .success-section, .activity-section {
            background-color: white;
            padding: 20px;
            border-radius: 16px;
            margin-bottom: 20px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.05);
        }
        .success-bar {
            height: 6px;
            background-color: #ddd;
            border-radius: 6px;
            margin: 5px 0 15px;
            overflow: hidden;
        }
        .success-bar .fill {
            height: 100%;
            background-color: #6C5CE7;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <div class="sidebar">
     <h2>Shiksha.</h2>
     <ul>
         <li class="active">Dashboard</li>
         <li>Schedules</li>
         <li>Campaigns</li>
         <li>Courses</li>
         <li class="nav-item">
  <a class="nav-link" href="MyCourse.aspx">My Courses</a>
</li>
         <li>Video</li>
         <li>Analytics</li>
         <li>Settings</li>
     </ul>
 </div>

 <div class="main">
     <div class="top-cards">
         <div class="card">
             <h4>Time Spend</h4>
             <p>40:16:13</p>
             <div class="line-chart">Line Chart</div>
         </div>
         <div class="card">
             <h4>Avg. Activity</h4>
             <p>58%</p>
             <div class="line-chart">Line Chart</div>
         </div>
         <div class="card">
             <h4>Assign Courses</h4>
             <p>4</p>
             <div class="line-chart">Line Chart</div>
         </div>
     </div>

     <div class="top-cards">
         <div class="card" style="flex: 2">
             <h4>Studying Process</h4>
             <div class="line-chart">Studying Graph</div>
         </div>
         <div class="card">
             <h4>Skill Developed</h4>
             <div class="radar-chart">Radar Chart</div>
         </div>
     </div>

     <div class="top-cards">
         <div class="card">
             <h4>Study Time</h4>
             <div class="bar-chart">Bar Graph</div>
         </div>
     </div>
 </div>

</asp:Content>

