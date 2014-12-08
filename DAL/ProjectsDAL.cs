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

        public static void UpdateProject(int projectId, string projectName, int ownerId, int[] userIds)
        {
            DBConnection dbConnection = new DBConnection();

            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@ProjectName", OleDbType.VarChar) { Value = projectName });
            parameters.Add(new OleDbParameter("@OwnerId", OleDbType.Integer) { Value = ownerId });
            parameters.Add(new OleDbParameter("@ProjectId", OleDbType.Integer) { Value = projectId });

            dbConnection.ExecuteNonQuery("UPDATE Project SET ProjectName=@ProjectName, OwnerId=@OwnerId WHERE ProjectId=@ProjectId", parameters);

            List<OleDbParameter> paramList = new List<OleDbParameter>();
            paramList.Add(new OleDbParameter("@ProjectId", OleDbType.Integer) { Value = projectId });
            dbConnection.ExecuteNonQuery("DELETE FROM ProjectUsers WHERE ProjectId=@ProjectId", paramList);

            foreach (int userId in userIds)
            {
                List<OleDbParameter> newParamList = new List<OleDbParameter>();
                newParamList.Add(new OleDbParameter("@ProjectId", OleDbType.Integer) { Value = projectId });
                newParamList.Add(new OleDbParameter("@UserId", OleDbType.Integer) { Value = userId });

                dbConnection.ExecuteNonQuery("INSERT INTO ProjectUsers VALUES (@ProjectId, @UserId)", newParamList);
            }
        }

        public static void SaveProjectSwimlanes(int projectId, List<Swimlane> swimlanes)
        {
            DBConnection dbConnection = new DBConnection();

            List<OleDbParameter> paramList = new List<OleDbParameter>();
            paramList.Add(new OleDbParameter("@ProjectId", OleDbType.Integer) { Value = projectId });
            dbConnection.ExecuteNonQuery("DELETE FROM ProjectSwimlanes WHERE ProjectId=@ProjectId", paramList);

            foreach (Swimlane lane in swimlanes)
            {
                List<OleDbParameter> newParamList = new List<OleDbParameter>();
                newParamList.Add(new OleDbParameter("@ProjectId", OleDbType.Integer) { Value = projectId });
                newParamList.Add(new OleDbParameter("@SwimlaneName", OleDbType.VarChar) { Value = lane.SwimlaneName });
                newParamList.Add(new OleDbParameter("@ColIndex", OleDbType.Integer) { Value = lane.ColIndex });

                dbConnection.ExecuteNonQuery("INSERT INTO ProjectSwimlanes VALUES (@ProjectId, @SwimlaneName, @ColIndex)", newParamList);
            }
        }
        public static Project GetProjectByUserId(int userId)
        {
            Project project = new Project();

            DBConnection dbConnection = new DBConnection();

            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@OwnerId", OleDbType.Integer) { Value = userId });

            project = dbConnection.ExecuteTypedList<Project>("SELECT * FROM Project WHERE OwnerId=@OwnerId", Project.Create, parameters).FirstOrDefault();

            if (project == null)
            {
                project = dbConnection.ExecuteTypedList<Project>("SELECT Project.* FROM Project INNER JOIN ProjectUsers ON Project.ProjectId = ProjectUsers.ProjectId WHERE ProjectUsers.UserId=@OwnerId", Project.Create, parameters).FirstOrDefault();
            }

            return project;
        }

        public static Project GetProjectByProjectId(int projectId)
        {
            Project project = new Project();

            DBConnection dbConnection = new DBConnection();

            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@ProjectId", OleDbType.Integer) { Value = projectId });

            project = dbConnection.ExecuteTypedList<Project>("SELECT * FROM Project WHERE ProjectId=@ProjectId", Project.Create, parameters).FirstOrDefault();

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

        public static List<Project> GetUserProjects(int userId)
        {
            List<Project> projects = new List<Project>();

            DBConnection dbConnection = new DBConnection();

            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@UserId", OleDbType.Integer) { Value = userId });

            projects = dbConnection.ExecuteTypedList<Project>("SELECT DISTINCT Project.* FROM Project LEFT JOIN ProjectUsers ON Project.ProjectId = ProjectUsers.ProjectId WHERE ProjectUsers.UserId=@UserId OR Project.OwnerId=@UserId", Project.Create, parameters).ToList();

            return projects;
        }

        public static List<Project> GetAllProjects()
        {
            List<Project> projects = new List<Project>();

            DBConnection dbConnection = new DBConnection();

            projects = dbConnection.ExecuteTypedList<Project>("SELECT * FROM Project", Project.Create).ToList();

            return projects;
        }

        public static void DeleteProject(int projectId)
        {
            // DELETE TASKS

            // DELETE PROJECT USERS

            // DELETE PROJECT SWIMLANES

            // DELETE PROJECT 

            DBConnection dbConnection = new DBConnection();

            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@ProjectId", OleDbType.Integer) { Value = projectId });

            dbConnection.ExecuteNonQuery("DELETE FROM Task WHERE ProjectId=@ProjectId", parameters);
            dbConnection.ExecuteNonQuery("DELETE FROM ProjectUsers WHERE ProjectId=@ProjectId", parameters);
            dbConnection.ExecuteNonQuery("DELETE FROM ProjectSwimlanes WHERE ProjectId=@ProjectId", parameters);
            dbConnection.ExecuteNonQuery("DELETE FROM Project WHERE ProjectId=@ProjectId", parameters);
        }
    }
}