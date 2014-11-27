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

            DBConnection con = new DBConnection();
            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@taskuser", OleDbType.VarChar) { Value = assig });
            parameters.Add(new OleDbParameter("@estimate", OleDbType.VarChar) { Value = timeEstimate });
            parameters.Add(new OleDbParameter("@taskName", OleDbType.VarChar) { Value = subtask });
            parameters.Add(new OleDbParameter("@ParentID", OleDbType.VarChar) { Value = parentP });

            con.ExecuteNonQuery("" +
           "INSERT INTO" + " Task([TaskUser],[TaskName],[TaskEstimate],[ParentId]) " +
           "VALUES (@taskuser,@taskName,@estimate,@ParentID);" , parameters);

            if (moreSubTask.Checked == true)
            {
                Response.Redirect("Sub_task.aspx");
            }
            con.Close();
        }

        
    }
}