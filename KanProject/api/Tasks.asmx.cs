using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DAL;
using DAL.Models;

namespace KanProject.api
{
    /// <summary>
    /// Summary description for Tasks
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Tasks : System.Web.Services.WebService
    {

        [WebMethod]
        public object CreateTask(int projectId, string taskName, string taskDescription, int colIndex, int rowIndex)
        {
            TasksDAL.CreateTask(projectId, taskName, taskDescription, colIndex, rowIndex);

            return new { success = true};
        }

        [WebMethod]
        public object SaveTask(int taskId, int colIndex, int rowIndex)
        {
            TasksDAL.SaveTask(taskId, colIndex, rowIndex);

            return new { success = true };
        }
    }
}
