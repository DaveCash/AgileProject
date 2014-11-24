using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using DAL.Models;

namespace KanProject
{
    public partial class ProjectEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Project project = ProjectsDAL.GetProjectByProjectId(Convert.ToInt32(Request["ProjectId"]));

                List<User> users = UsersDAL.GetUsersByProject(project.ProjectId);

                tbProjectName.Text = project.ProjectName;

                ddlUsers.DataSource = users;
                ddlUsers.DataValueField = "UserId";
                ddlUsers.DataTextField = "UserName";
                ddlUsers.DataBind();

                List<User> allUsers = UsersDAL.GetAllUsers();

                cblUsers.DataSource = allUsers;
                cblUsers.DataValueField = "UserId";
                cblUsers.DataTextField = "UserName";
                cblUsers.DataBind();

                int[] selectedUsers = users.Select(u => u.UserId).ToArray();
                for (int i = 0; i < cblUsers.Items.Count; i++)
                {
                    if (selectedUsers.Contains(Convert.ToInt32(cblUsers.Items[i].Value)))
                        cblUsers.Items[i].Selected = true;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int projectId = Convert.ToInt32(Request["ProjectId"]);
            string projectName = tbProjectName.Text;
            int ownerId = Convert.ToInt32(ddlUsers.SelectedItem.Value);

            int[] projectUserIds = cblUsers.Items.Cast<ListItem>().Select(i => Convert.ToInt32(i.Value)).ToArray();

            ProjectsDAL.UpdateProject(projectId, projectName, ownerId, projectUserIds);

            


            System.Threading.Thread.Sleep(500);
            Response.Redirect("Default.aspx?ProjectId=" + projectId);
        }
    }
}