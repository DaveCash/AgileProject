using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace KanProject
{
    public class sqlHelp
    {
        public void excuSql(string strSql)
        {
            OleDbConnection Connection = null;
            try
            {
                string projectPath = @"|DataDirectory|\AccessDB.mdb;";
                string conStr = "Provider = Microsoft.Jet.OLEDB.4.0;" + "Data Source = " + projectPath;
                Connection = new OleDbConnection();
                Connection.ConnectionString = conStr;
                Connection.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = Connection;
                cmd.CommandText = strSql;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }
        }
        public DataTable queryBySql(string strSql)
        {
            OleDbConnection Connection = null;
            try
            {
                string projectPath = @"|DataDirectory|\AccessDB.mdb;";
                string conStr = "Provider = Microsoft.Jet.OLEDB.4.0;" + "Data Source = " + projectPath;
                Connection = new OleDbConnection();
                Connection.ConnectionString = conStr;
                Connection.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = Connection;
                OleDbDataAdapter da = new OleDbDataAdapter(strSql, Connection);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }
        }
    }
}