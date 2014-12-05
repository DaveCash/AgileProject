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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlErrors.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var userName = UserName.Text;
            var passWord = PassWord.Text;

            if (!String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(passWord))
            {
                User user = UsersDAL.ValidateUser(userName, passWord);
                if (user != null)
                {
                    Session["User"] = user;
                    Project project = ProjectsDAL.GetProjectByUserId(user.UserId);

                    Response.Redirect("Default.aspx?ProjectId=" + project.ProjectId);
                }
                else
                {
                    pnlErrors.Controls.Add(new LiteralControl("Incorrect login!"));
                    pnlErrors.Visible = true;
                }
            }
        }
    }
}