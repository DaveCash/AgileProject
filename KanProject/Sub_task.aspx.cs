using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using DAL.Models;
namespace KanProject
{
    public partial class Sub_task : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = DAL.UsersDAL.GetAllUsers();
            foreach (var item in user)
            {
                assignee.Items.Add(new ListItem(item.UserName, item.UserId.ToString()));
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string assig = assignee.SelectedItem.ToString();
            string timeEstimate = origEstimate.Text;
            string subtask = title.Text;
            string parentP = Request.Params["task_id"];

            string projectPath = @"|DataDirectory|\AccessDB.mdb;";
            string conStr = "Provider = Microsoft.Jet.OLEDB.4.0;" + "Data Source = " + projectPath;
            OleDbConnection Connection = new OleDbConnection();

            Connection = new OleDbConnection();
            Connection.ConnectionString = conStr;
            Connection.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = Connection;
            cmd.CommandText = "INSERT INTO Task (TaskName, TaskEstimate, ParentId,TaskUser)"+
                              "VALUES ("+subtask+","+Convert.ToInt32(timeEstimate)+","+Convert.ToInt32(parentP)+",("+
                              "SELECT UserId FROM UserData WHERE UserName="+assig+"))";
           

            if (moreSubTask.Checked == true)
            {
                Response.Redirect("Sub_task.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
            Connection.Close();
        }

        
    }
}