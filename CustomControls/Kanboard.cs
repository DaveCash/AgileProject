using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Models;

namespace CustomControls
{
    [ToolboxData("<{0}:ServerControl1 runat=server></{0}:Kanboard>")]
    public class Kanboard : WebControl
    {
        public Project Project { get; set; }


        protected override void RenderContents(HtmlTextWriter output)
        {
            if (this.Project != null)
            {
                output.AddAttribute(HtmlTextWriterAttribute.Id, this.ID);
                output.AddAttribute("class", "kanboard");
                output.AddAttribute("data-project-id", this.Project.ProjectId.ToString());
                output.RenderBeginTag("table");
                output.RenderBeginTag("tr");

                int numCols = Project.ProjectSwimlanes.Count == 0 ? 5 : Project.ProjectSwimlanes.Count;

                if (Project.ProjectSwimlanes != null && Project.ProjectSwimlanes.Count > 0)
                {
                    foreach (Swimlane swimlane in Project.ProjectSwimlanes)
                    {
                        output.AddAttribute("data-col-index", swimlane.ColIndex.ToString());                       
                        output.RenderBeginTag("th");
                        output.RenderBeginTag("button");
                        output.Write("+");
                        output.RenderEndTag();
                        output.Write(swimlane.SwimlaneName);                        
                        output.RenderEndTag();
                    }
                }
                else
                {

                    for (var i = 1; i <= numCols; i++)
                    {
                        output.AddAttribute("data-col-index", i.ToString());
                        output.RenderBeginTag("th");
                        output.RenderBeginTag("button");
                        output.Write("+");
                        output.RenderEndTag();
                        output.Write("Column " + i);                        
                        output.RenderEndTag();
                    }
                }
                output.RenderEndTag();
                output.RenderBeginTag("tr");

                for (var i = 1; i <= numCols; i++)
                {
                    output.AddAttribute("class", "column");
                    output.AddAttribute("data-col-index", i.ToString());
                    output.RenderBeginTag("td");

                    if (this.Project.ProjectTasks != null && this.Project.ProjectTasks.Count != 0)
                    {
                        foreach (var task in this.Project.ProjectTasks.OrderBy(t => t.RowIndex))
                        {
                            if (task.ColIndex == i)
                            {
                                output.AddAttribute("class", "task");
                                output.AddAttribute("data-task-id", task.TaskId.ToString());
                                output.RenderBeginTag("div");

                                output.AddAttribute("class", "task_header");
                                output.RenderBeginTag("div");

                                output.AddAttribute("class", "task_title");



                                output.RenderBeginTag("a");
                                output.Write(task.TaskName);
                                output.RenderEndTag();

                                output.Write("&nbsp ");

                                output.AddAttribute("href", "javascript:void(0)");
                                output.AddAttribute("onclick", "edit(" + task.TaskId.ToString() + ")");
                                output.RenderBeginTag("a");
                                output.Write("Edit");
                                output.RenderEndTag();


                                output.Write("&nbsp ");

                                output.AddAttribute("href", "javascript:void(0)");
                                output.AddAttribute("onclick", "del(" + task.TaskId.ToString() + ")");
                                output.RenderBeginTag("a");
                                output.Write("Delete");
                                output.RenderEndTag();

                                output.AddAttribute("class", "assigned_person");
                                output.RenderBeginTag("a");
                                output.Write(task.TaskUser);
                                output.RenderEndTag();

                                output.RenderEndTag();

                                output.AddAttribute("class", "task_body");
                                output.RenderBeginTag("div");
                                output.Write(task.TaskDescription);
                                output.RenderEndTag();

                                output.AddAttribute("class", "task_footer");
                                
                                output.RenderBeginTag("div");
                                output.AddAttribute("class", "information_footer");
                                output.AddAttribute("href", "Sub_task.aspx?task_id=" + task.TaskId.ToString());
                                output.RenderBeginTag("a");
                                output.Write("Sub Task");

                                //output.RenderBeginTag("div");
                                //output.AddAttribute("class", "upload");
                                //output.AddAttribute("href", "upload.aspx");
                                //output.RenderBeginTag("a");
                                //output.Write("Upload file");

                                

                                ////output.AddAttribute("href", "comment.aspx");
                                ////output.RenderBeginTag("a");
                                ////output.Write("Give comment");



                                output.RenderEndTag();
                                output.RenderEndTag();
                                output.RenderEndTag();
                            }
                        }
                    }
                    output.RenderEndTag();
                }
                output.RenderEndTag();
                output.RenderEndTag();
            }
            else
            {
                output.AddAttribute("class", "create-project-btn");
                output.RenderBeginTag("button");
                output.Write("Create Project");
                output.RenderEndTag();
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.RenderContents(writer);
        }
    }
}
