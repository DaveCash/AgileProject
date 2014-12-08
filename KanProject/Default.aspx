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
        var intervalId = window.setInterval(function () { window.location.reload() }, 10 * 1000);

        $(document).ready(function () {
            Kanboard.Init();
            $("#btnClose").click(function () {
                $("#divWin").hide();

                intervalId = window.setInterval(function () { window.location.reload() }, 10 * 1000);
            })
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
            });
        }
        function edit(id) {
            clearInterval(intervalId);
            $.ajax({
                url: "/kanp.ashx?action=edit&id=" +id,
                dataType: "json",
                success: function (data) {
                    if (data.msg == "1") {
                        $("#plhContentMain_Assign_task_TaskId").attr("value", data.TaskId).val(data.TaskId);
                        $("#plhContentMain_Assign_task_projectId").attr("value", data.ProjectId).val(data.ProjectId);
                        $("#plhContentMain_Assign_task_colIndex").attr("value", data.ColIndex).val(data.ColIndex);
                        $("#plhContentMain_Assign_task_txtTaskName").attr("value", data.TaskName).val(data.TaskName);
                        $("#plhContentMain_Assign_task_taskDes").attr("value", data.TaskDetail).val(data.TaskDetail);
                        $("#plhContentMain_Assign_task_Complexity").attr("value", data.TaskComplexity).val(data.TaskComplexity);
                        $("#plhContentMain_Assign_task_Estimate").attr("value", data.TaskEstimate).val(data.TaskEstimate);
                        $("#divWin").show();

                        getProjectUsers(data.ProjectId, function (users) {
                            var $userList = $(".users-list");
                            $userList.empty();

                            $userList.append("<option value='0'>-Select user-</option>");

                            $.each(users, function (index, user) {
                                $userList.append("<option value='"+ user.UserId +"'>"+ user.UserName +"</option>");
                            });

                            if(data)
                                $userList.val(data.TaskUser);
                        });
                    }
                    else
                        alert(data.msg);
                }

            })

        }

        function getProjectUsers(projectId, callback) {
            $.ajax({
                type: "POST",
                url: "api/Projects.asmx/GetProjectUsers",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({projectId: projectId}),
                dataType: "json",
                success: function (response) {
                    console.log(response.d.success);

                    if (response.d.success) {
                        console.log(response.d.users);
                        callback(response.d.users);
                    }
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
    </script>
    <div  id="divWin" style="display:none;position:fixed;top:100px;left:300px; background-color:#FFFFE0;border:5px solid #F6A828;width:600px;height:400px;padding:10px;">
    <uc1:Assign_task runat="server" ID="Assign_task" />
    </div>
</asp:Content>
