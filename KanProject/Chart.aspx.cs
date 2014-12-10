using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;

namespace KanProject
{
    public partial class Chart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string projectPath = @"|DataDirectory|\AccessDB.mdb;";
            string conStr = "Provider = Microsoft.Jet.OLEDB.4.0;" + "Data Source = " + projectPath;
            OleDbConnection Connection = new OleDbConnection();

            Connection = new OleDbConnection();
            Connection.ConnectionString = conStr;
            Connection.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = Connection;
            cmd.CommandText = "SELECT ProjectName  FROM Project";
            cmd.CommandType = CommandType.Text;

            OleDbDataReader myReader = cmd.ExecuteReader();
            bool notEoF;
            notEoF = myReader.Read();

            while (notEoF)
            {
                project.Items.Add(myReader["ProjectName"].ToString());
                notEoF = myReader.Read();
            }
            Connection.Close();
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            int count = 0;
            string[] name = new String[100];

            string projectPath = @"|DataDirectory|\AccessDB.mdb;";
            string conStr = "Provider = Microsoft.Jet.OLEDB.4.0;" + "Data Source = " + projectPath;
            OleDbConnection Connection = new OleDbConnection();

            Connection = new OleDbConnection();
            Connection.ConnectionString = conStr;
            Connection.Open();

            OleDbCommand getProjectID = new OleDbCommand();
            getProjectID.Connection = Connection;
            getProjectID.CommandText = "SELECT ProjectId FROM ProjectUsers INNER JOIN Project ON ProjectUsers.ProjectId = Project.ProjectId WHERE Project.ProjectName =" + project.SelectedItem.ToString();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = Connection;
            cmd.CommandText = "SELECT UserId, UserName  FROM UserData WHERE ProjectId=" + getProjectID.ToString() + " ORDER BY UserId";
            cmd.CommandType = CommandType.Text;

            OleDbDataReader myReader = cmd.ExecuteReader();
            bool notEoF;
            notEoF = myReader.Read();

            while (notEoF)
            {
                name[count] = myReader["UserName"].ToString();
                count++;
            }
            String[,] info = new String[count, 2];
            for (int i = 0; i < count; i++)
            {
                info[i, 0] = name[i];
                info[i, 1] = "0";
            }
            //PieChart

            OleDbCommand getUserId = new OleDbCommand();
            getUserId.Connection = Connection;
            getUserId.CommandText = "SELECT TaskUser FROM Task WHERE ProjectId=" + getProjectID.ToString() + " AND TaskDone='yes'";

            OleDbDataReader myReader1 = getUserId.ExecuteReader();
            bool reader = myReader1.Read();

            while (reader)
            {
                String str = myReader1["TaskUser"].ToString();
                int temp = Convert.ToInt32(str);
                int sum = Convert.ToInt32(info[temp - 1, 1]);
                sum++;
                info[temp - 1, 1] = sum.ToString();
            }


            String[] color = { "#00E6E6", "#FF9933", "#99FF33", "#FF66CC", "#FFFF00", "#0066FF", "#FF0000", "#009999", "#99FF33", "#CC0000" };
            String[] highlight = { "#99FFFF", "#FFD6AD", "#C2FF85", "#FFB2E6", "#FFFF66", "	#B2D1FF", "#FF9999", "#B2E0E0", "	#CCFF99", "#EB9999" };

            TextBox hiddenTB = new TextBox();
            hiddenTB.Text = "[";
            for (int i = 0; i < count - 1; i++)
            {
                hiddenTB.Text += "{ ";
                hiddenTB.Text += "value: " + info[i, 1] + ", ";
                hiddenTB.Text += "color: '" + color[i] + "' , highlight: '" + highlight[i] + "',";
                hiddenTB.Text += "label: '" + info[i, 0] + "'},";
            }
            hiddenTB.Text += "{ ";
            hiddenTB.Text += "value: " + info[count - 1, 1] + ", ";
            hiddenTB.Text += "color: '" + color[count - 1] + "' , highlight: '" + highlight[count - 1] + "',";
            hiddenTB.Text += "label: '" + info[count - 1, 0] + "'}]";

        }

    }
}