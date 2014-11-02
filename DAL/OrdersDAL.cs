using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Models;
using System.Data;
using System.Data.OleDb;

namespace DAL
{
    public class OrdersDAL
    {
        public static DBConnection GetOrders(User user)
        {
            DBConnection dbConnection = new DBConnection();
            dbConnection.AddCmd("SecondCmd");

            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@UserName", OleDbType.VarChar) { Value = user.UserName });

            switch (user.UserLevel)
            {
                case UserLevel.Admin:
                    dbConnection.Cmds["default"].CommandText = "SELECT * FROM Orders";
                    break;
                case UserLevel.Seller:
                    dbConnection.ExecuteCmd("SELECT * FROM Salesmen WHERE Name='@UserName'", parameters, "SecondCmd");
                    dbConnection.Reader.Read();

                    dbConnection.Cmds["default"].CommandText = "SELECT * FROM Orders WHERE CustID IN (SELECT CustID FROM Customer WHERE Area='" + dbConnection.Reader["Area"].ToString() + "')";
                    break;
                case UserLevel.Client:
                    dbConnection.ExecuteCmd("SELECT * FROM Customer WHERE Name='@UserName'", parameters, "SecondCmd");
                    dbConnection.Reader.Read();

                    dbConnection.Cmds["default"].CommandText = "SELECT * FROM Orders WHERE CustID=" + dbConnection.Reader["CustID"].ToString() + "";
                    break;
            }
            dbConnection.Close();

            return dbConnection;
        }
    }
}