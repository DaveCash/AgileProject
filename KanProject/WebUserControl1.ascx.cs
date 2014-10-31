using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;

namespace KanProject
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            string projectPath = @"|DataDirectory|\AccessDB.mdb;";
            string conStr = "Provider = Microsoft.Jet.OLEDB.4.0;" + "Data Source = " + projectPath;

            OleDbConnection Connection = new OleDbConnection();
            Connection.ConnectionString = conStr;
            Connection.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = Connection;
            cmd.CommandText = "INSERT INTO Comments(CommentText)" + "VALUES('" + comment.Text + "')";
            cmd.CommandType = CommandType.Text;

            Connection.Open();
            cmd.ExecuteNonQuery();
            Connection.Close();
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            comment.Text = string.Empty;
        }
    }
}