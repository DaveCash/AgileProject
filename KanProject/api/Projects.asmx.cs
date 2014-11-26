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
    /// Summary description for Projects
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Projects : System.Web.Services.WebService
    {
        [WebMethod]
        public object SaveProject(int taskId, int colIndex, int index)
        {
            return new { message = "SAVING NOT YET IMPLEMENTED" };
        }

        [WebMethod]
        public object CheckProject()
        {
            return new { message = "CHECKING NOT YET IMPLEMENTED" };
        }

        [WebMethod(EnableSession = true)]
        public object CreateProject()
        {
            User user = (User)Session["User"];

            Project project = ProjectsDAL.CreateProject(user.UserId, "Default Project");

            return new { success = true };
        }

        [WebMethod(EnableSession = true)]
        public object SaveProjectSwimlanes(int projectId, string[] swimlaneNames, int[] colIndexes)
        {
            User user = (User)Session["User"];

            List<Swimlane> swimlanes = new List<Swimlane>();

            for(var i = 0; i < swimlaneNames.Length; i++){
                Swimlane lane = new Swimlane();
                lane.ProjectId = projectId;
                lane.SwimlaneName = swimlaneNames[i];
                lane.ColIndex = colIndexes[i];

                swimlanes.Add(lane);
            }

            ProjectsDAL.SaveProjectSwimlanes(projectId, swimlanes);

            return new { success = true };
        }
    }
}
