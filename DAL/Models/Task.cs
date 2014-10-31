using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Models
{
    public class Task
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public int TaskColumn { get; set; }

        public int TaskOrder { get; set; }

        public int TaskUser { get; set; }
    }
}