using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Models;
using DAL;

namespace KanProject
{
    public partial class Default : System.Web.UI.Page
    {
        public int numCols;
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                string projectId = Request["ProjectId"];

                Project project;

                if (String.IsNullOrEmpty(projectId))
                    project = ProjectsDAL.GetProjectByUserId(user.UserId);
                else
                    project = ProjectsDAL.GetProjectByProjectId(Convert.ToInt32(projectId));

                if (project != null)
                {
                    project.ProjectTasks = TasksDAL.GetProjectTasks(project.ProjectId);
                    project.ProjectSwimlanes = ProjectsDAL.GetProjectSwimlanes(project.ProjectId);
                }

                Kanboard.Project = project;

                // THIS IS TEST DATA
                //numCols = 5;

                //Kanboard.Project = ProjectsDAL.GetTestProject();
            }
        }
    }
}