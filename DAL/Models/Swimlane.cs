using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DAL.Models
{
    public class Swimlane
    {
        public int ProjectId { get; set; }
        public string SwimlaneName { get; set; }
        public int ColIndex { get; set; }

        public static Swimlane Create(IDataRecord record)
        {
            return new Swimlane
            {
                ProjectId = Convert.ToInt32(record["ProjectId"]),
                SwimlaneName = record["SwimlaneName"].ToString(),
                ColIndex = Convert.ToInt32(record["ColIndex"]),
            };
        }
    }
}