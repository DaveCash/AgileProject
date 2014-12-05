<%@ Page Language="C#" MasterPageFile="~/KanProject.Master" AutoEventWireup="true" CodeBehind="ProjectEdit.aspx.cs" Inherits="KanProject.ProjectEdit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="css/kanboard.css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhContentMain" runat="server">
    <div style="width: 25%; margin-top: 20px;">
        <div class="field">
            <label class="label">Project name: </label>
        <asp:TextBox runat="server" ID="tbProjectName" CssClass="control"></asp:TextBox>
        </div>
        <div class="field">
            <label class="label">Project owner: </label>
            <asp:DropDownList runat="server" ID="ddlUsers" CssClass="control"></asp:DropDownList>
        </div>
        <div class="field">
            <label class="label">Project users: </label>
            <asp:CheckBoxList runat="server" ID="cblUsers" CssClass="control"></asp:CheckBoxList>
        </div>
        <div class="field">
            <label class="label">Project swimlanes: </label>
            <asp:Repeater runat="server" ID="rptSwimlanes">
                <HeaderTemplate>
                    <table id="swimlane-table" data-project-id="<%= projectId %>">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="swimlaneName" Text='<%#Eval("SwimlaneName") %>'></asp:TextBox>
                        </td>
                        <td>
                            <button class="btnDeleteSwimlane">Delete swimlane</button>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <div class="field">
            <button id="btnAddSwimlanes">Add swimlane</button>
        </div>
        <div class="field">
            <asp:Button runat="server" ID="dtnSubmit" OnClientClick="return saveSwimlanes();" Text="Save" />
            <asp:Button runat="server" ClientIDMode="Static" ID="btnSubmitHidden" OnClick="btnSubmit_Click" CssClass="no-display" />
            <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click"/> 
        </div>
    </div>

    <script type="text/javascript">
        function saveSwimlanes() {
            var projectId = $("#swimlane-table").data("project-id");
            var $trs = $("#swimlane-table tr");

            var data = { projectId: projectId, swimlaneNames: [], colIndexes: [] };

            $.each($trs, function (index, tr) {
                data.swimlaneNames.push($(tr).find("input").val());
                data.colIndexes.push(index + 1);
            });

            console.log(JSON.stringify(data));

            $.ajax({
                type: "POST",
                url: "api/Projects.asmx/SaveProjectSwimlanes",
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(response.d.message);
                    $("#btnSubmitHidden").click();
                },
                failure: function (response) {
                    alert(response.d);
                }
            });

            return false;
        }
        
        $("#swimlane-table tr").on("click", ".btnDeleteSwimlane", function (e) {
            e.preventDefault();
            $(this).closest("tr").remove();
        });
        $("#btnAddSwimlanes").click(function (e) {
            e.preventDefault();
            $("#swimlane-table").append("<tr><td><input type='Text' value='New swimlane'/></td><td><button class='btnDeleteSwimlane'>Delete swimlane</button></td></tr>");
        });
            
    </script>
</asp:Content>
