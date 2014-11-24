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
            Project project = ProjectsDAL.GetProjectByProjectId(Convert.ToInt32(Request["ProjectId"]));

            List<User> users = UsersDAL.GetUsersByProject(project.ProjectId);

            tbProjectName.Text = project.ProjectName;
        }
    }
}