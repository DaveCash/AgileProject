var Kanboard = (function () {
    return {
        Init: function(){
            $(".kanboard tr th button").click(function(e){
                e.preventDefault();

                Kanboard.CreateTask($(e.currentTarget).closest("th").data("col-index"));
            });
        }
        ,CreateTask: function (colIndex) {
            var task = {};

            task.name = "new task";
            task.id = 0;
            task.colIndex = colIndex;

            $(".kanboard tr td[data-col-index=" + colIndex + "]").append(Kanboard.TaskTemplate(task.name));


        }
        ,TaskTemplate: function (taskName) {
            var html = 
            "<div class='task ui-sortable-handle'>" +
                "<div class='task_header'>" +
                    taskName +
                "</div>" +
            "</div>";

            return html;
        }
    };
})();