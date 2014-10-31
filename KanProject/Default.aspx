<%@ Page Title="" Language="C#" MasterPageFile="~/KanProject.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KanProject.Default" %>

<%@ Register Assembly="CustomControls" Namespace="CustomControls" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="css/kanboard.css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhContentMain" runat="server">
    <cc1:Kanboard runat="server" NumCols="5" ID="Kanboard"></cc1:Kanboard>
    <button id="testAjax">AJAX!</button>
    <p id="ajaxContent">
    </p>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".column").sortable({
                distance: 5,
                connectWith: ".column",
                placeholder: "draggable-placeholder",
                stop: function (event, ui) {
                    saveBoard(ui.item.attr('data-task-id'), ui.item.parent().attr("data-col-index"), ui.item.index() + 1);
                }
            });

            Kanboard.Init();

            //window.setInterval(checkBoard, 10 * 1000);

            function saveBoard(taskId, colIndex, index) {
                var data = {
                    taskId: taskId,
                    colIndex: colIndex,
                    index: index,
                };

                $.ajax({
                    type: "POST",
                    url: "api/Projects.asmx/SaveProject",
                    data: JSON.stringify(data),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        console.log(response.d.message);
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }
            function checkBoard() {
                $.ajax({
                    type: "POST",
                    url: "api/Projects.asmx/CheckProject",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        console.log(response.d.message);
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }

            $("#testAjax").click(function (e) {
                e.preventDefault();
                var uri = "api/Test.asmx/HelloWorld";

                $.ajax({
                    type: "POST",
                    url: uri,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        console.log(response);
                        $("#ajaxContent").html(response.d.message);
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            });
        });
    </script>
</asp:Content>
