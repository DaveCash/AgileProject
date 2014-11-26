<%@ Page Title="" Language="C#" MasterPageFile="~/KanProject.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KanProject.Default" %>

<%@ Register Assembly="CustomControls" Namespace="CustomControls" TagPrefix="cc1" %>
<%@ Register Src="~/Assign_task.ascx" TagPrefix="uc1" TagName="Assign_task" %>


<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="css/kanboard.css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhContentMain" runat="server">
    <cc1:Kanboard runat="server" ID="Kanboard"></cc1:Kanboard>
    <script src="scripts/jquery_dialog.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Kanboard.Init();
            $("#btnClose").click(function () {
                $("#divWin").hide();
            })
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

        function del(id) {
            $.ajax({
                url: "/kanp.ashx?action=del&id=" + id,
                dataType: "json",
                success: function (data) {
                    if (data.msg == "1") {
                        alert("delete success!");
                        window.location.reload();
                    }
                    else
                        alert(data.msg);

                }

            })

        }
        function edit(id) {
            $.ajax({
                url: "/kanp.ashx?action=edit&id=" +id,
                dataType: "json",
                success: function (data) {
                    if (data.msg == "1") {
                        $("#plhContentMain_Assign_task_TaskId").attr("value", data.TaskId);
                        $("#plhContentMain_Assign_task_projectId").attr("value", data.ProjectId)
                        $("#plhContentMain_Assign_task_colIndex").attr("value", data.ColIndex);
                        $("#plhContentMain_Assign_task_txtTaskName").attr("value", data.TaskName);
                        $("#plhContentMain_Assign_task_taskDes").attr("value", data.TaskDetail);
                        $("#plhContentMain_Assign_task_Complexity").attr("value", data.TaskComplexity);
                        $("#divWin").show();
                    }
                    else
                        alert(data.msg);
                }

            })

        }
    </script>
    <div  id="divWin" style="display:none;position:fixed;top:100px;left:300px; background-color:#FFFFE0;border:5px solid #F6A828;width:600px;height:400px;padding:10px;">
    <uc1:Assign_task runat="server" ID="Assign_task" />
    </div>
</asp:Content>
