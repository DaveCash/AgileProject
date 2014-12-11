<%@ Page Language="C#" MasterPageFile="~/KanProject.Master" AutoEventWireup="true" CodeBehind="Chart.aspx.cs" Inherits="KanProject.Chart" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="scripts/Chart.js"></script>
    <link rel="stylesheet" type="text/css" href="css/kanboard.css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhContentMain" runat="server">
    <div style="width: 25%; margin-top: 20px;">
    
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
  </div>
</asp:Content>