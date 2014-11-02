using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DAL.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public static User Create(IDataRecord record)
        {
            return new User
            {
                UserId = Convert.ToInt32(record["UserId"]),
                UserName = record["UserName"].ToString()
            };
        }
    }
}