<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="SikshaNew.LoginPage" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> <title>Login</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;600&display=swap" rel="stylesheet" />
    <style>
        body {
            font-family: 'Inter', sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f1f2f6;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }

        .login-box {
            background-color: white;
            padding: 40px;
            border-radius: 16px;
            box-shadow: 0 2px 15px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 400px;
        }

        .login-box h3 {
            margin-bottom: 25px;
            color: #2f3640;
            text-align: center;
        }

        label {
            display: block;
            margin-bottom: 5px;
            color: #2f3640;
            font-weight: 600;
        }

        .form-control {
            width: 100%;
            padding: 10px;
            margin-bottom: 20px;
            border: 1px solid #ccc;
            border-radius: 8px;
            font-size: 14px;
        }

        .btn-dark {
            background-color: #2f3640;
            color: white;
            padding: 10px;
            border: none;
            border-radius: 8px;
            width: 100%;
            font-size: 16px;
            cursor: pointer;
        }

        .btn-dark:hover {
            background-color: #00a8ff;
        }

        .text-danger {
            color: red;
            margin-bottom: 10px;
            display: block;
        }

        .login-box p {
            text-align: center;
            margin-top: 15px;
        }

        .login-box a {
            color: #00a8ff;
            text-decoration: none;
        }

        .login-box a:hover {
            text-decoration: underline;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="login-box">
            <h3>Login</h3>
            <asp:Label ID="Label1" runat="server" CssClass="text-danger"></asp:Label>

            <div class="form-group">
                <label>Email:-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="TextBox1" ErrorMessage="Invalid Email" ForeColor="Red"></asp:RequiredFieldValidator>
                </label>&nbsp;<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label>Password:-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="TextBox2" ErrorMessage="Invalid Password" ForeColor="Red"></asp:RequiredFieldValidator>
                </label>
&nbsp;<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" TextMode="Password" />
            </div>

            <!-- No OnClick defined to avoid CS1061 error -->
            <asp:Button ID="Button1" runat="server" Text="Login" CssClass="btn-dark"
                OnClientClick="simulateLogin(); return false;" OnClick="Button1_Click1" />

            <p>Don't have an account? <a href="Registration.aspx">Register</a></p>
        </div>
    </form>
</body>
</html>