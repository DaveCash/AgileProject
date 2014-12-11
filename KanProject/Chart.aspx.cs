using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Newtonsoft.Json;
using System.Web.UI;

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
            myReader.Close();
            Connection.Close();
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            
            string projectPath = @"|DataDirectory|\AccessDB.mdb;";
            string conStr = "Provider = Microsoft.Jet.OLEDB.4.0;" + "Data Source = " + projectPath;
            OleDbConnection Connection = new OleDbConnection();

            Connection = new OleDbConnection();
            Connection.ConnectionString = conStr;
            Connection.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = Connection;
            cmd.CommandText = "SELECT ProjectId FROM Project WHERE ProjectName ='" + project.SelectedItem.ToString()+"'";
            int projectID = Convert.ToInt32(cmd.ExecuteScalar());
            
             cmd.CommandText = "SELECT UserName, TaskNum  FROM UserData INNER JOIN "+
                    "(SELECT COUNT(*) AS TaskNum, TaskUser FROM Task WHERE Task.TaskDone= true AND ProjectId="+projectID+" GROUP BY TaskUser) AS A " +
                    "ON A.TaskUser = UserData.UserId";
            OleDbDataReader myReader = cmd.ExecuteReader();
            var pieChart = new PieChart();
            var parts = pieChart.parts;
            int i = 0;
            while (myReader.Read())
            {
                var part = new PieChartPart();
                part.label = myReader[0].ToString();
                part.value = Convert.ToInt32(myReader[1]);
                part.color = color[1, i];
                part.highlight = color[0, i];
                parts.Add(part);
                i++;
            }
            myReader.Close();
            cmd.CommandText = "SELECT UserName, NumComplexity FROM UserData INNER JOIN"+
                              "(SELECT SUM(TaskComplexity) AS NumComplexity, TaskUser FROM Task GROUP BY TaskUser) AS A "+
                              "ON A.TaskUser=UserData.UserId";
            OleDbDataReader myReader1 = cmd.ExecuteReader();
            var barChart = new BarChart();
            var bcDataset1= new bcDataset();
            barChart.datasets.Add(bcDataset1);
            while (myReader1.Read())
            {
                barChart.labels.Add(myReader1[0].ToString());
                bcDataset1.data.Add(Convert.ToInt32(myReader1[1]));
            }
            string script = "var data1=" + JsonConvert.SerializeObject(barChart)+";";
            

           script+= "var data=" + JsonConvert.SerializeObject(parts)+";";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
        }

        string[,] color = {{"#ff9898", "#ffc864", "#ffff95", "#98ff98", "#caffff", "#adadff", "#d598ff" },
                            { "#ff4b4b", "#ffa500", "#ffff4b", "#4bff4b", "#80ffff", "#6464ff", "#b64bff" }};

    }
}



public class PieChart
{
    public List<PieChartPart> parts {get; set;}

    public PieChart()
    {
        parts = new List<PieChartPart>();
    }
}

public class PieChartPart
{
    public string label { get; set; }
    public string color { get; set; }
    public string highlight { get; set; }
    public int value { get; set; }
}

public class BarChart
{
   public  List<string> labels {get;set;}
   public List<bcDataset> datasets { get; set; }
    public BarChart()
    {
        datasets=new List<bcDataset>();
        labels = new List<string>();
    }
}

public class bcDataset
{
    public string fillColor = "#ff9898" ;
    
    public List<int> data { get; set; }
    public bcDataset()
    {
        data = new List<int>();
    }
}
