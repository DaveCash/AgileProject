var Kanboard = (function () {
    return {
        Init: function () {
            $(".column").sortable({
                distance: 5,
                connectWith: ".column",
                placeholder: "draggable-placeholder",
                stop: function (event, ui) {
                    Kanboard.SaveTask(ui.item.attr('data-task-id'), ui.item.parent().attr("data-col-index"), ui.item.index() + 1);
                }
            });

            $(".kanboard tr th button").click(function (e) {
                e.preventDefault();

                Kanboard.CreateTask($(e.currentTarget).closest("th").data("col-index"));
            });

            $(".create-project-btn").click(function (e) {
                e.preventDefault();

                $.ajax({
                    type: "POST",
                    url: "api/Projects.asmx/CreateProject",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        console.log(response.d.success);

                        if (response.d.success)
                            window.location.reload();
                        else
                            alert("SOMETHING WENT WRONG!");
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            });
        }
        , CreateTask: function (colIndex) {
            $("#divWin").show();

            var task = {};

            task.name = "new task";
            task.id = 0;
            task.colIndex = colIndex;

            $(".kanboard tr td[data-col-index=" + colIndex + "]").append(Kanboard.TaskTemplate(task.name));

            var data = {
                projectId: $(".kanboard").data("project-id"),
                taskName: "new task",
                taskDescription: "",
                colIndex: colIndex,
                rowIndex: 1
            };

            $("#plhContentMain_Assign_task_projectId").attr("value", $(".kanboard").data("project-id"))
            $("#plhContentMain_Assign_task_colIndex").attr("value", colIndex)
            /*  $.ajax({
                  type: "POST",
                  url: "api/Tasks.asmx/CreateTask",
                  contentType: "application/json; charset=utf-8",
                  data: JSON.stringify(data),
                  dataType: "json",
                  success: function (response) {
                      console.log(response.d.success);
                      $("#divWin").show();
                      if (!response.d.success)
                          alert("SOMETHING WENT WRONG!");
                  },
                  failure: function (response) {
                      alert(response.d);
                  }
              });*/
        }
        , SaveTask: function (taskId, colIndex, rowIndex) {
            var data = {
                taskId: taskId,
                colIndex: colIndex,
                rowIndex: rowIndex,
            };

            $.ajax({
                type: "POST",
                url: "api/Tasks.asmx/SaveTask",
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
        , TaskTemplate: function (taskName) {
            var html =
            "<div class='task ui-sortable-handle'>" +
                "<div class='task_header'>" +
                    "<a class='task_title'>" + taskName + "</a>" +
                    "<a class='assigned_person'>" + 0 + "</a>" +
                "</div>" +
                "<div class='task_body'>" +
                    "THIS IS TASK " +
                "</div>" +
                "<div class='task_footer'>" +
                    "<a class='information_footer'>THIS IS TASK FOOTER</a>" +
                "</div>" +
            "</div>";

            return html;
        }
    };
})();
