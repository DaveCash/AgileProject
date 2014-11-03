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
   
    public partial class WebUserControl2 : System.Web.UI.UserControl
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            string projectPath = @"|DataDirectory|\Access.mdb;";
            string conStr = "Provider = Microsoft.Jet.OLEDB.4.0;" + "Data Source = " + projectPath;
            OleDbConnection Connection = new OleDbConnection();

            Connection = new OleDbConnection();
            Connection.ConnectionString = conStr;
            Connection.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = Connection;
            cmd.CommandText = "SELECT TaskName  FROM Task";
            cmd.CommandType = CommandType.Text;

            OleDbCommand cmd1 = new OleDbCommand();
            cmd1.Connection = Connection;
            cmd1.CommandText = "SELECT UserName FROM UserData";
            cmd1.CommandType = CommandType.Text;

            OleDbDataReader myReader = cmd.ExecuteReader();
            OleDbDataReader myReader1 = cmd1.ExecuteReader();
            bool notEoF;
            notEoF = myReader.Read();

            while (notEoF)
            {
                listTask.Items.Add(myReader["TaskName"].ToString());
                listUser.Items.Add(myReader1["TaskUser"].ToString());
                notEoF = myReader.Read();
            }
            myReader.Close();
            Connection.Close();
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            string projectPath = @"|DataDirectory|\Access.mdb;";
            string conStr = "Provider = Microsoft.Jet.OLEDB.4.0;" + "Data Source = " + projectPath;
            OleDbConnection Connection = new OleDbConnection();

            Connection = new OleDbConnection();
            Connection.ConnectionString = conStr;
            Connection.Open();
            
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = Connection;
            cmd.CommandText = "INSERT INTO Task(TaskUser)" + " VALUES('" + listUser.SelectedItem.ToString() +"') WHERE TaskName="+listTask.SelectedItem.ToString();
            cmd.CommandType = CommandType.Text;



        }

        protected void listTask_SelectedIndexChanged(object sender, EventArgs e)
        {

            string projectPath = @"|DataDirectory|\Access.mdb;";
            string conStr = "Provider = Microsoft.Jet.OLEDB.4.0;" + "Data Source = " + projectPath;
            OleDbConnection Connection = new OleDbConnection();

            Connection = new OleDbConnection();
            Connection.ConnectionString = conStr;
            Connection.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = Connection;
            cmd.CommandText = "SELECT TaskDetail FROM Task WHERE TaskName ="+listTask.SelectedItem.Text;
            cmd.CommandType = CommandType.Text;

            OleDbDataReader myReader = cmd.ExecuteReader();
            bool notEoF;
            notEoF = myReader.Read();

            if (myReader!=null)
            {
                taskDes.Text = myReader.ToString();
            }
        }
    }
}