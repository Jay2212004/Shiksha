using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
namespace SikshaNew
{
    public partial class LoginPage : System.Web.UI.Page
    {
        SqlConnection conn;
        string user_id;
        string user_name;
        string user_role;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            string cnf = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();
        }
        public void sendemail(string toemail, string sub, string body)
        {
            MailMessage mail = new MailMessage();
            string fromemail = "";
            string psswd = "";
            mail.Subject = sub;
            mail.Body = body;
            mail.From = new MailAddress(fromemail);
            mail.To.Add(toemail);
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Credentials = new NetworkCredential(fromemail, psswd);
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Send(mail);

        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            string email = TextBox1.Text, pass = TextBox2.Text;
            string q = $"exec LoginProc '{email}','{pass}','Admin'";
            SqlCommand cmd = new SqlCommand(q, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    Session["userid"] = rdr["userId"].ToString();
                    user_id = Session["userid"].ToString();
                    Session["username"] = rdr["fullname"].ToString();
                    user_name = Session["username"].ToString();
                    Session["userEmail"] = rdr["email"].ToString();
                    Session["userrole"] = rdr["Role"].ToString();
                    user_role = Session["userrole"].ToString();
                    if (user_role == "User")
                    {
                        if ((rdr["email"].ToString().Equals(email) || rdr["fullname"].ToString().Equals(email)) &&
                         rdr["pass"].ToString().Equals(pass))
                        {
                            string lastLoginCheck = $"select top 1 lastlogin from loginlogs where user_id = '{user_id}' order by lastlogin desc";
                            SqlCommand cmd2 = new SqlCommand(lastLoginCheck, conn);
                            SqlDataReader rdr2 = cmd2.ExecuteReader();
                            DateTime lastLogin = DateTime.MinValue;

                            if (rdr2.Read())
                            {
                                lastLogin = Convert.ToDateTime(rdr2["lastlogin"]);
                                rdr2.Close();


                                if ((DateTime.Now - lastLogin).TotalDays > 2)
                                {
                                    string q3 = $"update users set status = 'Inactive' where UserID={user_id}";
                                    SqlCommand cmd3 = new SqlCommand(q3, conn);
                                    cmd3.ExecuteNonQuery();

                                    DateTime ctime = DateTime.Now;
                                    string q5 = $"update loginlogs set lastlogin='{ctime.ToString("yyyy - MM - dd HH: mm: ss")}' where user_id='{user_id}'";
                                    SqlCommand cmd5 = new SqlCommand(q5, conn);
                                    cmd5.ExecuteNonQuery();

                                    Response.Write("<script>alert('Account inactive due to no login in last 2 days');</script>");
                                    string toemail = Session["userEmail"].ToString();
                                    string sub = "Account Is deactivated Due to Inactivity";
                                    string body = $"Hello{user_name},\nYour account has been deactivated\nUnder our policies and guidlines user must not stay inactive for more than 2 days\nPlease contact Admin to get your account activated\nTry not to stay inactive from now on keep learning\nBest regards AdminTeam\n";
                                    sendemail(toemail, sub, body);
                                    return;
                                }
                            }
                            else
                            {

                                string insertLog = $"exec addloginlog '{user_id}','{user_name}','{user_role}'";
                                SqlCommand cmdInsert = new SqlCommand(insertLog, conn);
                                cmdInsert.ExecuteNonQuery();
                                if (user_role == "User")
                                {
                                    Response.Redirect("/User/UserDashboard.aspx");
                                }
                                return;
                            }

                          
                            string q4 = $"exec addloginlog '{user_id}','{user_name}','{user_role}'";
                            SqlCommand cmd4 = new SqlCommand(q4, conn);
                            cmd4.ExecuteNonQuery();

                            if (user_role == "User")
                            {
                                Response.Redirect("/User/UserDashboard.aspx");
                            }

                        }
                    }
                    else
                    {
                        if (user_role == "Admin")
                        {
                            Response.Redirect("/Admin/AdminDashboard.aspx");
                        }

                    }
                }
            }
        }
    }

}
