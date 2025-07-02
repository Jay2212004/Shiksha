using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
namespace SikshaNew.User
{
    public partial class Subscription : System.Web.UI.Page
    {
        SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);
            conn.Open();
            if (!IsPostBack)
            {
                LoadSubscriptionCards();
            }
        }

        private void LoadSubscriptionCards()
        {
            string q = "SELECT subscription_type, subcourse_name, price, duration, iconurl FROM subcoursesubscriptions ORDER BY subscription_type";
            SqlCommand cmd = new SqlCommand(q, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            List<SubscriptionGroup> data = new List<SubscriptionGroup>();

            while (rdr.Read())
            {
                string type = rdr["subscription_type"].ToString();
                string sub_course = rdr["subcourse_name"].ToString();
                string price = rdr["price"].ToString();
                string duration = rdr["duration"].ToString();
                string icon = rdr["iconurl"].ToString();

                SubscriptionGroup existing = null;
                foreach (var item in data)
                {
                    if (item.SubscriptionType == type)
                    {
                        existing = item;
                        break;
                    }
                }


                if (existing != null)
                {
                    existing.Subcourses.Add(sub_course);
                }
                else
                {
                    data.Add(new SubscriptionGroup
                    {
                        SubscriptionType = type,
                        Subcourses = new List<string> { sub_course },
                        Price = price,
                        Duration = duration,
                        IconUrl = icon
                    });
                }
            }
            rdr.Close();

            rptCards.DataSource = data;
            rptCards.DataBind();
        }

        protected void rptCards_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Buy")
            {
                string selectedPlan = e.CommandArgument.ToString();
                Response.Write($"<script>alert('You selected {selectedPlan} subscription');</script>");
            }
        }
    }

    public class SubscriptionGroup
    {
        public string SubscriptionType { get; set; }
        public List<string> Subcourses { get; set; }
        public string Price { get; set; }
        public string Duration { get; set; }
        public string IconUrl { get; set; }
    }
}