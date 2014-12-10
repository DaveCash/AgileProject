using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DAL.Models
{
    public class Task
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        public int ParentId { get; set; }

        public int ColIndex { get; set; }

        public int RowIndex { get; set; }

        public int TaskUser { get; set; }
        public int TaskComplexity { get; set; }

        public static Task Create(IDataRecord record)
        {
            return new Task
            {
                TaskId = Convert.ToInt32(record["TaskId"]),
                TaskName = record["TaskName"].ToString(),
                TaskDescription = record["TaskDetail"].ToString(),
                ColIndex = Convert.ToInt32(record["ColIndex"]),
                RowIndex = Convert.ToInt32(record["RowIndex"]),
                TaskUser = Convert.ToInt32(record["TaskUser"]),
                TaskComplexity = Convert.ToInt32(record["TaskComplexity"]),
            };
        }
    }
}