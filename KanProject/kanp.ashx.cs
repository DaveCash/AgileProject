using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Text;
namespace KanProject
{
    /// <summary>
    /// kanp 的摘要说明
    /// </summary>
    public class kanp : IHttpHandler
    {
        sqlHelp sh = new sqlHelp();
        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request.QueryString["action"];
            switch (action)
            {
                case "del":
                    del();
                    break;
                case "edit":
                    getTask();
                    break;
            }
        }
        public void del()
        {
            try
            {
               string strSql = "delete from Task where TaskId="+context.Request.QueryString["id"];
               sh.excuSql(strSql);
                context.Response.Write("{\"msg\":1}");
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"msg\":\""+ex.Message+"\"}");
            }
           
        }
        public void getTask()
        {
            try
            {
                string strSql = "select * from Task where TaskId=" + context.Request.QueryString["id"];
                DataTable dt = sh.queryBySql(strSql);
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"msg\":1,");
                sb.Append("\"TaskId\":\"" + dt.Rows[0]["TaskId"].ToString() + "\",");
                sb.Append("\"ProjectId\":\"" + dt.Rows[0]["ProjectId"].ToString() + "\",");
                sb.Append("\"TaskUser\":\"" + dt.Rows[0]["TaskUser"].ToString() + "\",");
                sb.Append("\"ColIndex\":\"" + dt.Rows[0]["ColIndex"].ToString() + "\",");
                sb.Append("\"TaskDetail\":\"" + dt.Rows[0]["TaskDetail"].ToString() + "\",");
                sb.Append("\"TaskComplexity\":\"" + dt.Rows[0]["TaskComplexity"].ToString() + "\",");
                sb.Append("\"TaskEstimate\":\"" + dt.Rows[0]["TaskEstimate"].ToString() + "\",");
                sb.Append("\"TaskName\":\"" + dt.Rows[0]["TaskName"].ToString() + "\"}");
                context.Response.Write(sb.ToString());
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"msg\":\"" + ex.Message + "\"}");
            }
        }
        private void excuSql(string strSql)
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
        private HttpContext context
        {
            get { return HttpContext.Current; }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}