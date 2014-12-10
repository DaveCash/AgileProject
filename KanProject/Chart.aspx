<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chart.aspx.cs" Inherits="KanProject.Chart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chart</title>
    <script src="scripts/Chart.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:DropDownList ID="project" runat="server"></asp:DropDownList>
    <asp:Button ID="submit" runat="server" Text="Submit" OnClick="submit_Click" />
    <div id="canvas-holder">
        <canvas id="piePC" width="300" height="300"></canvas>
    </div>

    <div style="width:50%">
        <canvas id="barPC" height="300" width="300"></canvas>
    </div>

    <script>
        window.onload = function () {
            console.log(data1);
            var ctx = document.getElementById("piePC").getContext('2d');
            var myPieChart = new Chart(ctx).Pie(data);
          
            var ctx = document.getElementById("barPC").getContext('2d');
            var myBarChart = new Chart(ctx).Bar(data1);
            
        }
           
    </script>
    </form>
</body>
</html>
