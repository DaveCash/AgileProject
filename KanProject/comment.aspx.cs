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
           
            string projectPath = @"|DataDirectory|\AccessDB.mdb;";
            string conStr = "Provider = Microsoft.Jet.OLEDB.4.0;" + "Data Source = " + projectPath;
            OleDbConnection Connection = new OleDbConnection();

            Connection = new OleDbConnection();
            Connection.ConnectionString = conStr;
            Connection.Open();

            if (Request.QueryString["taskId"] != null)
            {
                int i = Convert.ToInt32(Request.QueryString["TaskId"]);
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = Connection;
                cmd.CommandText = "SELECT CommentText FROM Comments WHERE TaskId=" + i;
                string textComment = Convert.ToString(cmd.ExecuteScalar());
                commentBox.Text = textComment;
            }
           
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            string projectPath = @"|DataDirectory|\AccessDB.mdb;";
            string conStr = "Provider = Microsoft.Jet.OLEDB.4.0;" + "Data Source = " + projectPath;
            OleDbConnection Connection = new OleDbConnection();

            Connection = new OleDbConnection();
            Connection.ConnectionString = conStr;
            Connection.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = Connection;
            cmd.CommandText = "INSERT INTO Comments (CommentText, TaskId) VALUES ('"+commentBox.Text+"',"+Convert.ToInt32(Request.QueryString["taskId"])+");";
           
            Response.Redirect("Default.aspx");
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            commentBox.Text = String.Empty;
        }

        
    }
}