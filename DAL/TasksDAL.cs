using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Models;
using System.Data;
using System.Data.OleDb;

namespace DAL
{
    public class TasksDAL
    {
        public static void CreateTask(int projectId, string taskName, string taskDescription, int colIndex, int rowIndex)
        {
            DBConnection dbConnection = new DBConnection();

            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@projectId", OleDbType.Integer) { Value = projectId });
            parameters.Add(new OleDbParameter("@taskName", OleDbType.VarChar) { Value = taskName });
            parameters.Add(new OleDbParameter("@taskDescription", OleDbType.VarChar) { Value = taskDescription });
            parameters.Add(new OleDbParameter("@colIndex", OleDbType.Integer) { Value = colIndex });
            parameters.Add(new OleDbParameter("@rowIndex", OleDbType.Integer) { Value = rowIndex });

            dbConnection.ExecuteNonQuery("" +
            "INSERT INTO " +
            "Task ([ProjectId], [TaskName], [TaskDetail], [ColIndex], [RowIndex]) " +
            "VALUES (@projectId,@taskName,@taskDescription,@colIndex,@rowIndex);", parameters);
        }

        public static void SaveTask(int taskId, int colIndex, int rowIndex)
        {
            DBConnection dbConnection = new DBConnection();

            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@colIndex", OleDbType.Integer) { Value = colIndex });
            parameters.Add(new OleDbParameter("@rowIndex", OleDbType.Integer) { Value = rowIndex });
            parameters.Add(new OleDbParameter("@taskId", OleDbType.Integer) { Value = taskId });

            dbConnection.ExecuteNonQuery("" +
            "UPDATE Task " +
            "SET colIndex=@colIndex,rowIndex=@rowIndex " +
            "WHERE taskId=@taskId;", parameters);
        }

        public static List<Task> GetProjectTasks(int projectId)
        {
            List<Task> tasks = new List<Task>();

            DBConnection dbConnection = new DBConnection();

            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@projectId", OleDbType.Integer) { Value = projectId });

            tasks = dbConnection.ExecuteTypedList<Task>("SELECT * FROM Task where ProjectId=@projectId", Task.Create, parameters).ToList();

            return tasks;
        }

        public static object GetProjectTasks()
        {
            throw new NotImplementedException();
        }
    }
}