using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DAL.Models;

namespace KanProject.api
{
    /// <summary>
    /// Summary description for Test
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Test : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public object HelloWorld()
        {
            User user = new User();

            user = (User)Session["User"];

            return new { message = "Hello World" };
        }

        [System.Web.Services.WebMethod]
        public object GetAllProducts()
        {
            return new { message = "Success!" };
        }
    }
}
