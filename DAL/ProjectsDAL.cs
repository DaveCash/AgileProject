using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Models;
using System.Data;
using System.Data.OleDb;

namespace DAL
{
    public class ProjectsDAL
    {
        public static Project GetTestProject() {
            // TEST DATA

            Project project = new Project();
            project.ProjectName = "TestProject";

            List<Task> tasks = new List<Task>();
            tasks.Add(new Task { TaskName = "Task1", ColIndex = 1, RowIndex = 2, TaskId= 1, TaskUser=1});
            tasks.Add(new Task { TaskName = "Task2", ColIndex = 2, RowIndex = 1, TaskId = 2, TaskUser = 2 });
            tasks.Add(new Task { TaskName = "Task4", ColIndex = 3, RowIndex = 2, TaskId = 3, TaskUser = 3 });
            tasks.Add(new Task { TaskName = "Task5", ColIndex = 3, RowIndex = 1, TaskId = 4, TaskUser = 4 });
            tasks.Add(new Task { TaskName = "Task6", ColIndex = 1, RowIndex = 1, TaskId = 5, TaskUser = 5 });
            tasks.Add(new Task { TaskName = "Task7", ColIndex = 2, RowIndex = 2, TaskId = 6, TaskUser = 3 });
            tasks.Add(new Task { TaskName = "Task8", ColIndex = 4, RowIndex = 1, TaskId = 7, TaskUser = 2 });
            tasks.Add(new Task { TaskName = "Task9", ColIndex = 5, RowIndex = 1, TaskId = 8, TaskUser = 1 });
            tasks.Add(new Task { TaskName = "Task3", ColIndex = 4, RowIndex = 2, TaskId = 9, TaskUser = 4 });

            project.ProjectTasks = tasks;

            return project;
        }

        public static Project CreateProject(int userId, string projectName)
        {
            Project project = new Project();

            DBConnection dbConnection = new DBConnection();

            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@ProjectName", OleDbType.VarChar) { Value = projectName });
            parameters.Add(new OleDbParameter("@UserId", OleDbType.Integer) { Value = userId });

            dbConnection.ExecuteNonQuery("" +
            "INSERT INTO " +
            "Project ([ProjectName], [OwnerId]) " +
            "VALUES (@ProjectName,@UserId);", parameters);

            project.ProjectName = projectName;
            project.OwnerId = userId;

            return project;
        }

        public static Project GetProject(int userId)
        {
            Project project = new Project();

            DBConnection dbConnection = new DBConnection();

            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@OwnerId", OleDbType.Integer) { Value = userId });

            project = dbConnection.ExecuteTypedList<Project>("SELECT * FROM Project WHERE OwnerId=@OwnerId", Project.Create, parameters).FirstOrDefault();



            return project;
        }

        public static List<Swimlane> GetProjectSwimlanes(int projectId)
        {
            List<Swimlane> lanes = new List<Swimlane>();

            DBConnection dbConnection = new DBConnection();

            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@ProjectId", OleDbType.Integer) { Value = projectId });

            lanes = dbConnection.ExecuteTypedList<Swimlane>("SELECT * FROM ProjectSwimlanes WHERE ProjectId=@ProjectId", Swimlane.Create, parameters).ToList();

            return lanes;
        }

        public static List<Swimlane> GetDefaultSwimlanes()
        {
            List<Swimlane> lanes = new List<Swimlane>();

            lanes.Add(new Swimlane() { SwimlaneName = "Col1", ColIndex = 1 });
            lanes.Add(new Swimlane() { SwimlaneName = "Col2", ColIndex = 2 });
            lanes.Add(new Swimlane() { SwimlaneName = "Col3", ColIndex = 3 });
            lanes.Add(new Swimlane() { SwimlaneName = "Col4", ColIndex = 4 });
            lanes.Add(new Swimlane() { SwimlaneName = "Col5", ColIndex = 5 });

            return lanes;
        }
    }
}