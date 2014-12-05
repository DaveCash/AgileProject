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
    public partial class ProjectManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                User currentUser = (User)Session["User"];

                List<Project> projects;
                projects = ProjectsDAL.GetAllProjects();

                rptProjects.DataSource = projects;
                rptProjects.DataBind();

                List<User> users;
                users = UsersDAL.GetAllUsers();

                ddlOwner.DataSource = users;
                ddlOwner.DataTextField = "UserName";
                ddlOwner.DataValueField = "UserId";
                ddlOwner.DataBind();

                ddlOwner.Items.FindByValue(currentUser.UserId.ToString()).Selected = true;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            ProjectsDAL.CreateProject(Convert.ToInt32(ddlOwner.SelectedItem.Value), tbProjectName.Text);

            Response.Redirect(Request.RawUrl);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}