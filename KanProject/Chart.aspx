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
        <canvas id="chart-area" width="300" height="300"></canvas>
    </div>

    <div style="width:50%">
        <canvas id="canvas" height="450" width="1000"></canvas>
    </div>

    <script>
        window.onload = function () {
            var ctx_pie = document.getElementById("chart-area").getContext("2d");
            window.myPie = new Chart(ctx_pie).Pie(data_piechart);

            var ctx = document.getElementById("canvas").getContext("2d");
            window.myBar = new Chart(ctx).Bar(data_point, {
                responsive: true
            });
        }
    </script>
    </form>
</body>
</html>
