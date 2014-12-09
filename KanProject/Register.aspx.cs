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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlErrors.Visible = false;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            var userName = UserName.Text;
            var passWord = PassWord.Text;

            if (!String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(passWord))
            {
                User user = UsersDAL.RegisterUser(userName, passWord, txtQuestion.Text, txtAnswer.Text, txtEmail.Text);
                if (user != null)
                {
                    Session["User"] = user;
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    pnlErrors.Visible = true;
                    pnlErrors.Controls.Add(new LiteralControl("Username already exists!"));
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}