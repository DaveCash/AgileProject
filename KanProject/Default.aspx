<%@ Page Title="" Language="C#" MasterPageFile="~/KanProject.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KanProject.Default" %>

<%@ Register Assembly="CustomControls" Namespace="CustomControls" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="css/kanboard.css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhContentMain" runat="server">
    <cc1:Kanboard runat="server" ID="Kanboard"></cc1:Kanboard>
    <h1>CHANGES!!!!!</h1>
    <script type="text/javascript">
        $(document).ready(function () {
            Kanboard.Init();

            //window.setInterval(checkBoard, 10 * 1000);
            function checkBoard() {
                //window.location.reload();
                //$.ajax({
                //    type: "POST",
                //    url: "api/Projects.asmx/CheckProject",
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    success: function (response) {
                //        console.log(response.d.message);
                //    },
                //    failure: function (response) {
                //        alert(response.d);
                //    }
                //});
            }
        });
    </script>
</asp:Content>
