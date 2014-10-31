using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Models;

namespace DAL
{
    public class ProjectsDAL
    {
        public static Project GetProject() {
            // TEST DATA

            Project project = new Project();
            project.ProjectName = "TestProject";

            List<Task> tasks = new List<Task>();
            tasks.Add(new Task { TaskName = "Task1", TaskColumn = 1, TaskOrder = 2, TaskId= 1, TaskUser=1});
            tasks.Add(new Task { TaskName = "Task2", TaskColumn = 2, TaskOrder = 1, TaskId= 2, TaskUser=2});
            tasks.Add(new Task { TaskName = "Task4", TaskColumn = 3, TaskOrder = 2, TaskId= 3, TaskUser=3});
            tasks.Add(new Task { TaskName = "Task5", TaskColumn = 3, TaskOrder = 1, TaskId= 4, TaskUser=4});
            tasks.Add(new Task { TaskName = "Task6", TaskColumn = 1, TaskOrder = 1, TaskId= 5, TaskUser=5});
            tasks.Add(new Task { TaskName = "Task7", TaskColumn = 2, TaskOrder = 2, TaskId= 6, TaskUser=3});
            tasks.Add(new Task { TaskName = "Task8", TaskColumn = 4, TaskOrder = 1, TaskId= 7, TaskUser=2});
            tasks.Add(new Task { TaskName = "Task9", TaskColumn = 5, TaskOrder = 1, TaskId= 8, TaskUser=1});
            tasks.Add(new Task { TaskName = "Task3", TaskColumn = 4, TaskOrder = 2, TaskId= 9, TaskUser=4});

            project.ProjectTasks = tasks;

            return project;
        }
    }
}