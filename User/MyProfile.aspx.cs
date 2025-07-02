using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;


namespace SikshaNew.User
{
    public partial class MyProfile : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            string cnf = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(cnf);
            conn.Open();


            if (!IsPostBack)
            {

                LoadCertificate();
                LoadUserProgress();
                LoadUserInfo();
                LoadMyReviews();

            }
        }


        public void LoadUserInfo()
        {
            string userEmail = Session["userEmail"]?.ToString();
            if (string.IsNullOrEmpty(userEmail)) return;

            SqlCommand cmd = new SqlCommand("sp_GetUserProfileInfo", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", userEmail);
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                lblFullName.Text = rdr["FullName"].ToString();
                lblEmail.Text = rdr["Email"].ToString();
                lblRegDate.Text = Convert.ToDateTime(rdr["CreatedAt"]).ToString("dd MMM yyyy");

                imgProfile.ImageUrl = "https://www.svgrepo.com/show/384674/account-avatar-profile-user-11.svg";
            }

            rdr.Close();
        }



        public void LoadCertificate()
        {
            string userEmail = Session["userEmail"]?.ToString();
            if (string.IsNullOrEmpty(userEmail)) return;


            SqlCommand cmd = new SqlCommand("select subcourse_name, completed_on from UserCertificate where user_email = @email", conn);
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


        public void LoadMyReviews()
        {
            string userEmail = Session["userEmail"]?.ToString();
            if (string.IsNullOrEmpty(userEmail)) return;

            SqlCommand cmd = new SqlCommand("sp_GetMyReviews", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@user_email", userEmail);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvMyReviews.DataSource = dt;
            gvMyReviews.DataBind();
        }

        protected void gvCertificates_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                string subcourseName = e.CommandArgument.ToString();
                string userEmail = Session["userEmail"]?.ToString();

                if (string.IsNullOrEmpty(userEmail)) return;


                SqlCommand cmd = new SqlCommand("select FullName from Users where Email = @userEmail", conn);
                cmd.Parameters.AddWithValue("@userEmail", userEmail);
                string fullName = cmd.ExecuteScalar()?.ToString() ?? userEmail; // fallback if name not found

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", $"attachment;filename=Certificate_{subcourseName}.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                // document
                using (MemoryStream ms = new MemoryStream())
                {
                    Document doc = new Document(PageSize.A4, 50, 50, 50, 50);
                    PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    doc.Open();

                    // Optional border
                    PdfContentByte canvas = writer.DirectContent;
                    Rectangle rect = new Rectangle(doc.PageSize);
                    rect.Left += doc.LeftMargin - 20;
                    rect.Right -= doc.RightMargin - 20;
                    rect.Top -= doc.TopMargin - 20;
                    rect.Bottom += doc.BottomMargin - 20;
                    rect.BorderWidth = 3;
                    rect.BorderColor = new BaseColor(0, 102, 204); // Blue
                    rect.Border = Rectangle.BOX;
                    canvas.Rectangle(rect);

                    // Title
                    Paragraph title = new Paragraph("Certificate of Completion", new Font(Font.FontFamily.TIMES_ROMAN, 24f, Font.BOLD, BaseColor.DARK_GRAY));
                    title.Alignment = Element.ALIGN_CENTER;
                    title.SpacingAfter = 20f;
                    doc.Add(title);

                    // Line separator
                    LineSeparator separator = new LineSeparator(1f, 100f, BaseColor.LIGHT_GRAY, Element.ALIGN_CENTER, -2);
                    doc.Add(new Chunk(separator));
                    doc.Add(new Paragraph("\n"));

                    // Body Content
                    Paragraph body = new Paragraph();
                    body.Alignment = Element.ALIGN_CENTER;
                    body.SpacingBefore = 10f;
                    body.SpacingAfter = 20f;

                    body.Add(new Chunk("This is to certify that\n\n", new Font(Font.FontFamily.HELVETICA, 14f, Font.NORMAL)));
                    body.Add(new Chunk(fullName + "\n\n", new Font(Font.FontFamily.HELVETICA, 16f, Font.BOLD, new BaseColor(0, 51, 102))));
                    body.Add(new Chunk("has successfully completed the course\n\n", new Font(Font.FontFamily.HELVETICA, 14f)));
                    body.Add(new Chunk(subcourseName + "\n\n", new Font(Font.FontFamily.HELVETICA, 15f, Font.BOLD, BaseColor.BLACK)));
                    body.Add(new Chunk("on " + DateTime.Now.ToString("dd MMMM yyyy") + ".", new Font(Font.FontFamily.HELVETICA, 12f)));

                    doc.Add(body);

                    // Add signature and academy
                    doc.Add(new Paragraph("\n\n"));
                    Paragraph signature = new Paragraph("Authorized Signature", new Font(Font.FontFamily.HELVETICA, 10f, Font.ITALIC));
                    signature.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(signature);

                    Paragraph academy = new Paragraph("Jay Academy", new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD));
                    academy.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(academy);

                    doc.Close();

                    byte[] bytes = ms.ToArray();
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }
}