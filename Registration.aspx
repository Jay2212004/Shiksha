<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="SikshaNew.Registration" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f1f2f6;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        .register-box {
            background: white;
            padding: 40px;
            border-radius: 16px;
            box-shadow: 0 4px 25px rgba(0, 0, 0, 0.15);
            margin-top: 80px;
        }
        .form-label { font-weight: 600; }
        .form-control { border-radius: 8px; }
        .btn-success {
            background-color: #2f3640;
            border: none;
            border-radius: 8px;
            font-weight: 600;
        }
        .btn-success:hover { background-color: #00a8ff; }
        .text-danger { font-size: 13px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container d-flex justify-content-center">
            <div class="register-box col-md-6 col-lg-5">
                <h3 class="mb-4 text-center">Register Here!</h3>

                <asp:Label ID="Label1" runat="server" CssClass="text-danger"></asp:Label>

                <!-- Full Name -->
                <div class="mb-3">
                    <label class="form-label">Full Name</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                        ErrorMessage="Name is required" CssClass="text-danger" Display="Dynamic" ForeColor="Red" />
                </div>

                <!-- Email -->
                <div class="mb-3">
                    <label class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="Email is required" CssClass="text-danger" Display="Dynamic" ForeColor="Red" />
                </div>

                <!-- Password -->
                <div class="mb-3">
                    <label class="form-label">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                        ErrorMessage="Password is required" CssClass="text-danger" Display="Dynamic" ForeColor="Red" />
                </div>

                <!-- Confirm Password (not posted to DB) -->
                <div class="mb-4">
                    <label class="form-label">Confirm Password</label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                        ErrorMessage="Confirm Password is required" CssClass="text-danger" Display="Dynamic" ForeColor="Red" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CompareValidator ID="cvPasswords" runat="server" ControlToCompare="txtPassword"
                        ControlToValidate="txtConfirmPassword" ErrorMessage="Passwords do not match"
                        CssClass="text-danger" Display="Dynamic" ForeColor="Red" />
                </div>

                <!-- Register Button -->
                <asp:Button ID="btnRegister" runat="server" Text="Register"
                    CssClass="btn btn-success w-100" OnClick="btnRegister_Click" />

                <!-- Login Link -->
                <p class="mt-4 text-center">Already have an account? <a href="LoginPage.aspx">Login</a></p>
            </div>
        </div>
    </form>
</body>
</html>
