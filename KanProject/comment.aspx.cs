using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Models;
using System.Data.OleDb;

namespace KanProject
{
    public partial class comment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DBConnection con = new DBConnection();
            //string comMent = con.ExecuteTypedList<comment>;
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            string comments = commentBox.Text;

            DBConnection con = new DBConnection();
            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@comMent", OleDbType.VarChar) { Value = comments });

            con.ExecuteNonQuery("" + "INSERT INTO" + " Task([TaskDetail])" + " VALUES (@comMent);", parameters);
            Response.Redirect("Default.aspx");
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            commentBox.Text = String.Empty;
        }

        
    }
}