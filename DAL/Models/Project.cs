using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DAL.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int OwnerId { get; set; }

        public List<Task> ProjectTasks { get; set; }

        public List<Swimlane> ProjectSwimlanes { get; set; }

        public static Project Create(IDataRecord record)
        {
            return new Project
            {
                ProjectId = Convert.ToInt32(record["ProjectId"]),
                ProjectName = record["ProjectName"].ToString(),
                OwnerId = Convert.ToInt32(record["OwnerId"]),
            };
        }
    }
}