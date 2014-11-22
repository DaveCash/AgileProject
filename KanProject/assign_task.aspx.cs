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
    public partial class assign_task : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = DAL.UsersDAL.GetAllUsers();
            foreach (var item in user)
            {
                listUser.Items.Add(new ListItem(item.UserName, item.UserId.ToString()));
            }

            var task = DAL.TasksDAL.GetProjectTasks(1);
            foreach (var item in task)
            {
                listTask.Items.Add(new ListItem(item.TaskName, item.TaskId.ToString()));
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            string taskname = listTask.SelectedItem.ToString();
            string username = listUser.SelectedItem.ToString();

            DBConnection con = new DBConnection();
            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@taskName", OleDbType.VarChar) { Value = taskname });
            parameters.Add(new OleDbParameter("@TaskUser", OleDbType.VarChar) { Value = taskname });

            con.ExecuteNonQuery("" +
            "INSERT INTO" + " Task([TaskUser])" +
            "VALUES (@TaskUSer)" +
            "WHERE TaskName=@taskName;" + parameters);
        }

        protected void listTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            string taskname = listTask.SelectedItem.ToString();

            DBConnection con = new DBConnection();
            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@taskName", OleDbType.VarChar) { Value = taskname });

            con.ExecuteNonQuery("" + "SELECT TaskDetail" + " FROM Task" + "WHERE TaskName=@taskName;" + parameters);
        }
    }
}