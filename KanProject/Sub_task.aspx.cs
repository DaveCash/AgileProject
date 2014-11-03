using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
namespace KanProject
{
    public partial class Sub_task : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string projectPath = @"|DataDirectory|\UserLevelsAndCustomerOrders2014.mdb;";
            string conStr = "Provider = Microsoft.Jet.OLEDB.4.0;" + "Data Source = " + projectPath;
            OleDbConnection Connection = new OleDbConnection();

            Connection = new OleDbConnection();
            Connection.ConnectionString = conStr;
            Connection.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = Connection;
            cmd.CommandText = "SELECT Name FROM UserData";
            cmd.CommandType = CommandType.Text;

            OleDbDataReader myReader = cmd.ExecuteReader();
            bool notEoF;
            notEoF = myReader.Read();

            while (notEoF)
            {
                assignee.Items.Add(myReader["name"].ToString());
                notEoF = myReader.Read();
            }
            myReader.Close();
            Connection.Close();
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string projectPath = @"|DataDirectory|\AccessDB.mdb;";
            string conStr = "Provider = Microsoft.Jet.OLEDB.4.0;" + "Data Source = " + projectPath;
            OleDbConnection Connection = new OleDbConnection();

            Connection = new OleDbConnection();
            Connection.ConnectionString = conStr;
            Connection.Open();


            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = Connection;
            cmd.CommandText = "INSERT INTO Task(TaskName, TaskUSer, TaskEstimate)" + " VALUES('" + title.Text + "," + assignee.Items.ToString() + "," + origEstimate.Text + "')";
            cmd.CommandType = CommandType.Text;

            if (moreSubTask.Checked==true)
            {
                Response.Redirect("Sub_task.aspx");
            }
            Connection.Close();
        }
    }
}