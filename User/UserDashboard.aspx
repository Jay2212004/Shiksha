<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="UserDashboard.aspx.cs" Inherits="SikshaNew.User.UserDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>User Dashboard</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
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
        }

        .card {
            background-color: #ffffff;
            border-radius: 16px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.08);
        }

        .progress {
            background-color: #e0e0e0;
        }

        .progress-bar {
            font-weight: 600;
            font-size: 15px;
        }

        h4.text-success {
            text-align: center;
        }

        .table {
            background-color: white;
            margin-top: 30px;
            border-radius: 12px;
            overflow: hidden;
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
                <canvas id="subscribedChart" class="chart-box"></canvas>
            </div>
            <div class="dashboard-card">
                <h4>Active Courses</h4>
                <asp:Label ID="lblActiveCourses" runat="server" CssClass="dashboard-count" />
                <canvas id="activeChart" class="chart-box"></canvas>
            </div>
            <div class="dashboard-card">
                <h4>Total Amount Spent</h4>
                <asp:Label ID="lblAmountSpent" runat="server" CssClass="dashboard-count" />
                <canvas id="amountChart" class="chart-box"></canvas>
            </div>
        </div>

        <div class="dashboard-cards">
            <div class="dashboard-card">
                <h4>All Available Subcourses</h4>
                <asp:Label ID="lblAllSubCourses" runat="server" CssClass="dashboard-count" />
                <canvas id="availableChart" class="chart-box"></canvas>
            </div>
           <div class="dashboard-card wide">
    <h4>Courses Enrolled</h4>
    <asp:Repeater ID="rptCoursesEnrolled" runat="server">
        <ItemTemplate>
            <div style="padding: 10px; border-bottom: 1px solid #e0e0e0;">
                <i class="fa-solid fa-book" style="color: #00a8ff;"></i>
                <span style="margin-left: 10px;"><%# Eval("subcourse_name") %></span>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>

        </div>

      
        <h4 class="text-success mt-5 mb-3">📈 Overall Course Progress</h4>
        <div class="card shadow-sm p-4 mb-5 bg-light border border-2 border-success rounded-4" style="max-width: 700px; margin: 0 auto;">
            <p class="fw-semibold mb-3 text-secondary">Your total progress across all enrolled courses:</p>
            <div class="progress" style="height: 30px; width: 100%;">
                <div class="progress-bar progress-bar-striped progress-bar-animated bg-success" role="progressbar"
                    style='width: <%: ViewState["ProgressPercent"] ?? "0" %>%' aria-valuenow='<%: ViewState["ProgressPercent"] ?? "0" %>' aria-valuemin="0" aria-valuemax="100">
                    <%: ViewState["ProgressPercent"] ?? "0" %>% Completed
                </div>
            </div>
        </div>

        <asp:GridView ID="gvAssignments" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
            <Columns>
                <asp:BoundField DataField="Subcourse" HeaderText="Subcourse" />
                <asp:BoundField DataField="Topic" HeaderText="Topic" />
                <asp:BoundField DataField="File Name" HeaderText="File Name" />
                <asp:BoundField DataField="Submitted On" HeaderText="Submitted On" DataFormatString="{0:dd MMM yyyy HH:mm}" />
                <asp:BoundField DataField="Score" HeaderText="Score" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
            </Columns>
        </asp:GridView>
    </div>


    <script>
        const subscribed = parseInt("<%= lblCoursesSubscribed.Text %>") || 0;
        const active = parseInt("<%= lblActiveCourses.Text %>") || 0;
        const spent = parseInt("<%= lblAmountSpent.Text.Replace("₹", "").Trim() %>") || 0;
        const available = parseInt("<%= lblAllSubCourses.Text %>") || 0;

        new Chart(document.getElementById("subscribedChart"), {
            type: 'doughnut',
            data: {
                labels: ['Subscribed', 'Remaining'],
                datasets: [{
                    data: [subscribed, Math.max(available - subscribed, 0)],
                    backgroundColor: ['#00a8ff', '#dcdde1']
                }]
            }
        });

        new Chart(document.getElementById("activeChart"), {
            type: 'bar',
            data: {
                labels: ['Active Courses'],
                datasets: [{
                    label: 'Active',
                    data: [active],
                    backgroundColor: ['#6ab04c']
                }]
            },
            options: { plugins: { legend: { display: false } } }
        });

        new Chart(document.getElementById("amountChart"), {
            type: 'bar',
            data: {
                labels: ['₹ Spent'],
                datasets: [{
                    label: 'Amount',
                    data: [spent],
                    backgroundColor: ['#e67e22']
                }]
            },
            options: { plugins: { legend: { display: false } } }
        });

        new Chart(document.getElementById("availableChart"), {
            type: 'bar',
            data: {
                labels: ['Available Subcourses'],
                datasets: [{
                    label: 'Subcourses',
                    data: [available],
                    backgroundColor: ['#9b59b6']
                }]
            },
            options: { plugins: { legend: { display: false } } }
        });
    </script>
</asp:Content>
