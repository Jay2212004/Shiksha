using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SikshaNew.User
{
    public partial class MyProfile : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cnf = ConfigurationManager.ConnectionStrings["ShikshaCon"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();

            
            if (!IsPostBack)
            {

                LoadCertificate();
                LoadUserProgress();

            }
        }


        public void LoadCertificate()
        {
            string userEmail = Session["userEmail"]?.ToString();
            if (string.IsNullOrEmpty(userEmail)) return;


            SqlCommand cmd = new SqlCommand("SELECT subcourse_name, completed_on FROM UserCertificate WHERE user_email = @email", conn);
            cmd.Parameters.AddWithValue("@email", userEmail);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvCertificates.DataSource = dt;
            gvCertificates.DataBind();
        }


        public void LoadUserProgress()
        {
            string userEmail = Session["userEmail"]?.ToString();
            if (string.IsNullOrEmpty(userEmail)) return;

            SqlCommand cmd = new SqlCommand("sp_GetCourseProgress", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@user_email", userEmail);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvProgress.DataSource = dt;
            gvProgress.DataBind();
        }

        protected void gvCertificates_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                string subcourseName = e.CommandArgument.ToString();
                string userEmail = Session["userEmail"]?.ToString();

                if (string.IsNullOrEmpty(userEmail)) return;

                // Set PDF response
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", $"attachment;filename=Certificate_{subcourseName}.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                // Initialize PDF document
                using (MemoryStream ms = new MemoryStream())
                {
                    Document doc = new Document(PageSize.A4, 50, 50, 50, 50);
                    PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    doc.Open();

                    // Add Logo (optional)
                    // Image logo = Image.GetInstance(Server.MapPath("~/assets/logo.png")); // Optional
                    // logo.ScaleToFit(100f, 100f);
                    // logo.Alignment = Element.ALIGN_CENTER;
                    // doc.Add(logo);

                    // Title
                    Paragraph title = new Paragraph("Certificate of Completion", new Font(Font.FontFamily.HELVETICA, 20f, Font.BOLD, BaseColor.DARK_GRAY));
                    title.Alignment = Element.ALIGN_CENTER;
                    doc.Add(title);

                    doc.Add(new Paragraph("\n"));

                    // Certificate body
                    Paragraph body = new Paragraph(
                        $"This is to certify that\n\n{userEmail}\n\nhas successfully completed the course\n\n{subcourseName}\n\non {DateTime.Now:dd MMMM yyyy}.",
                        new Font(Font.FontFamily.HELVETICA, 14f, Font.NORMAL, BaseColor.BLACK)
                    );
                    body.Alignment = Element.ALIGN_CENTER;
                    doc.Add(body);

                    doc.Add(new Paragraph("\n\n"));
                    doc.Add(new Paragraph("Authorized Signature", new Font(Font.FontFamily.HELVETICA, 10f, Font.ITALIC)));
                    doc.Add(new Paragraph("Shiksha Academy"));

                    doc.Close();

                    // Send PDF to browser
                    byte[] bytes = ms.ToArray();
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }
}