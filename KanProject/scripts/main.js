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

            $(".kanboard tr th button").click(function(e){
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
        ,CreateTask: function (colIndex) {
            var task = {};

            task.name = "new task";
            task.id = 0;
            task.colIndex = colIndex;

            var $task = $(".kanboard tr td[data-col-index=" + colIndex + "]").append(Kanboard.TaskTemplate(task.name));

            $task.find(".save-task").click(function (e) {
                e.preventDefault();
                var data = {
                    projectId: $(".kanboard").data("project-id"),
                    taskName: $task.find("input[name=taskName]").val(),
                    taskDescription: $task.find("textarea").val(),
                    colIndex: colIndex,
                    rowIndex: 1
                };

                $.ajax({
                    type: "POST",
                    url: "api/Tasks.asmx/CreateTask",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(data),
                    dataType: "json",
                    success: function (response) {
                        console.log(response.d.success);

                        if (!response.d.success)
                            alert("SOMETHING WENT WRONG!");

                        window.setTimeout(window.location.reload.bind(window.location), 500);
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            });
        }
        ,SaveTask: function (taskId, colIndex, rowIndex) {
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
        ,TaskTemplate: function (taskName) {
            var html = 
            "<div class='task ui-sortable-handle'>" +
                "<div class='task_header'>" +
                    "<label class='label'>Name:</label><input class='control' type='text' name='taskName' value='New task'/>" +
                    "<a class='assigned_person'>" + 0 + "</a>" +
                "</div>" +
                "<div class='task_body' style='vertical-align:middle;'>" +
                    "<label class='label'>Description:</label><textarea class='control' rows='4'>Task description</textarea>" +
                "</div>" +
                "<div class='task_footer'>" +
                    "<button class='save-task'>Save</button>" +
                "</div>" +
            "</div>";

            return html;
        }
    };
})();