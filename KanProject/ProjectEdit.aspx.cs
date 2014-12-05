using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAL;
using DAL.Models;

namespace KanProject
{
    public partial class ProjectEdit : System.Web.UI.Page
    {
        public int projectId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Project project = ProjectsDAL.GetProjectByProjectId(Convert.ToInt32(Request["ProjectId"]));
                projectId = project.ProjectId;

                List<User> users = UsersDAL.GetUsersByProject(project.ProjectId);
                
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

                User projectOwner = UsersDAL.GetProjectOwner(project.ProjectId);
                if (users.Where(u => u.UserId == projectOwner.UserId).Count() == 0)
                    users.Insert(0, projectOwner);

                tbProjectName.Text = project.ProjectName;

                ddlUsers.DataSource = users;
                ddlUsers.DataValueField = "UserId";
                ddlUsers.DataTextField = "UserName";
                ddlUsers.DataBind();

                ddlUsers.Items.FindByValue(projectOwner.UserId.ToString()).Selected = true;

                List<Swimlane> swimlanes = ProjectsDAL.GetProjectSwimlanes(project.ProjectId).OrderBy(s => s.ColIndex).ToList();
                rptSwimlanes.DataSource = swimlanes;
                rptSwimlanes.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int projectId = Convert.ToInt32(Request["ProjectId"]);
            string projectName = tbProjectName.Text;
            int ownerId = Convert.ToInt32(ddlUsers.SelectedItem.Value);

            int[] projectUserIds = cblUsers.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => Convert.ToInt32(i.Value)).ToArray();

            ProjectsDAL.UpdateProject(projectId, projectName, ownerId, projectUserIds);

            var items = rptSwimlanes.Items;

            System.Threading.Thread.Sleep(500);
            Response.Redirect("Default.aspx?ProjectId=" + projectId);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int projectId = Convert.ToInt32(Request["ProjectId"]);

            Response.Redirect("Default.aspx?ProjectId=" + projectId);
        }
    }
}