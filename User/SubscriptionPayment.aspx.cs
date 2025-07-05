using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SikshaNew.User
{
    public partial class SubscriptionPayment : System.Web.UI.Page
    {
        SqlConnection conn;
        string toEmail = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);
            conn.Open();
            if (Session["userEmail"] != null)
            {
                toEmail = Session["userEmail"].ToString();
            }

            string Subprice = Session["subprice"].ToString();
            lblTotalAmount.Text = Subprice;

        }

        public void sendMail(string toEmail, string otp)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("krishsecured@gmail.com");
            mail.To.Add(toEmail);
            mail.Body = string.Format("Your OTP:{0}", otp);
            mail.Subject = "OTP Verification";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Credentials = new NetworkCredential("krishsecured@gmail.com", "ocaawkxidxgzwlnq");
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Send(mail);

            Response.Write("<script>alert('Mail Send Successfully')</script>");

        }


        public int generateOTP()
        {
            Random random = new Random();
            return random.Next(000001, 999999);
        }




        protected void Button1_Click(object sender, EventArgs e)
        {
            string otp = generateOTP().ToString();
            Session["OTP"] = otp;
            sendMail(toEmail, otp);
            TextBox1.Visible = true;
            Button2.Visible = true;

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            string notp = TextBox1.Text;
            if (Session["OTP"].Equals(notp))
            {
                Response.Write("<script>alert('Validation Successfull')</script>");
                btnPayNow.Visible = true;
            }
            else
            {
                Response.Write("<script>alert('Invalid OTP')</script>");
            }
        }


        protected void btnPayNow_Click(object sender, EventArgs e)
        {
            string amtl = lblTotalAmount.Text;

            if (!decimal.TryParse(amtl, out decimal amt))
            {
                // Handle conversion error
                Response.Write("<script>alert('Invalid amount format');</script>");
                return;
            }
            int amountInPaise = (int)(amt * 100);
            string keyId = "rzp_test_Kl7588Yie2yJTV";
            string keySecret = "6dN9Nqs7M6HPFMlL45AhaTgp";


            RazorpayClient razorpayClient = new RazorpayClient(keyId, keySecret);

            // Create an order
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", amt * 100); // Amount should be in paisa (multiply by 100 for rupees)
            options.Add("currency", "INR");
            options.Add("receipt", "order_receipt_123");
            options.Add("payment_capture", 1); // Auto capture payment

            Razorpay.Api.Order order = razorpayClient.Order.Create(options);

            string orderId = order["id"].ToString();
            Session["orderId"] = orderId;

            // Generate checkout form and redirect user to Razorpay payment page
            string razorpayScript = $@"
            var options = {{
                'key': '{keyId}',
                'amount': {amt * 100},
                'currency': 'INR',
                'name': 'Masstech Business Solutions Pvt.Ltd',
                'description': 'Checkout Payment',
                'order_id': '{orderId}',
                'handler': function(response) {{
                    // Handle successful payment response
                    alert('Payment successful. Payment ID: ' + response.razorpay_payment_id);
					window.location.href = '{"paymentsuccess.aspx"}?payment_id=' + response.razorpay_payment_id;
                }},
                'prefill': {{
                    'name': 'Krish Kheloji',
                    'email': 'khelojikrish@gmail.com',
                    'contact': '7208921898'
                }},
                'theme': {{
                    'color': '#F37254'
                }}
            }};
            var rzp1 = new Razorpay(options);
            rzp1.open();";

            // Register the script on the page

            ClientScript.RegisterStartupScript(this.GetType(), "razorpayScript", razorpayScript, true);

        }


    }
}