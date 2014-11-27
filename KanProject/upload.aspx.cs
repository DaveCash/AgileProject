using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using DAL.Models;
using System.IO;

namespace KanProject
{
    public partial class upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            DBConnection con = new DBConnection();
            if (FileUploadControl.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(FileUploadControl.FileName);
                    FileUploadControl.SaveAs(Server.MapPath("~/") + filename);

                    string upload = Server.MapPath("~/") + filename;
                    
                    List<OleDbParameter> parameters = new List<OleDbParameter>();
                    parameters.Add(new OleDbParameter("@link", OleDbType.VarChar) { Value = upload });
                    
                    con.ExecuteNonQuery("" +
                    "INSERT INTO" + " Task([TaskAttachment]) " +
                    "VALUES (@link);" , parameters);
                    StatusLabel.Text = "Upload status: File uploaded!";
                    
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
        }
    }
}