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
    public partial class KanProject : System.Web.UI.MasterPage
    {
        public User user;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) { 
                user = (User)Session["User"];

                if (user == null || String.IsNullOrEmpty(user.UserName))
                    Response.Redirect("Login.aspx");

                btnLogout.Text = "Logout: " + user.UserName;

                List<Project> projects = ProjectsDAL.GetUserProjects(user.UserId);

                ddlProjects.DataSource = projects;
                ddlProjects.DataValueField = "ProjectId";
                ddlProjects.DataTextField = "ProjectName";
                ddlProjects.DataBind();

                string projectId = Request["ProjectId"];

                if (!String.IsNullOrEmpty(projectId)){
                    var proj = ddlProjects.Items.FindByValue(projectId);
                    if(proj != null){
                        proj.Selected = true;
                        hlMain.NavigateUrl += "?ProjectId=" + projectId;
                    }
                }
                else if(projects.Count > 0)
                    ddlProjects.Items.FindByValue(ProjectsDAL.GetProjectByUserId(user.UserId).ProjectId.ToString()).Selected = true;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("User");
            Response.Redirect("Login.aspx");
        }
        protected void btnEditProject_Click(object sender, EventArgs e)
        {
            string url = "ProjectEdit.aspx?ProjectId=";

            if (ddlProjects.SelectedItem != null)
            {
                url += ddlProjects.SelectedItem.Value;

                Response.Redirect(url);
            }
            
        }

        protected void btnProjectManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjectManagement.aspx");
        }

        protected void ddlProjects_IndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx?ProjectId=" + ddlProjects.SelectedValue);
        }
    }
}