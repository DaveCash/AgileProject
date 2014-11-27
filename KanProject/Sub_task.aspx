<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sub_task.aspx.cs" Inherits="KanProject.Sub_task" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Sub-Task</h1>
            <asp:Panel ID="sTask" runat="server" >
            <table>
              <tr>
                  <td>Title</td>
                  <td>Assignee</td>
                  <td>complexity</td>
              </tr>
              <tr>
                  <td><asp:TextBox ID="title" runat="server" Width="400" ></asp:TextBox></td>
                  <td><asp:DropDownList ID="assignee" runat="server"></asp:DropDownList></td>
                  <td><asp:TextBox ID="complexity" runat="server" Width="73px" Height="19px"></asp:TextBox></td>
              </tr>
            </table><br />
            Orignal Estimate<asp:TextBox ID="origEstimate" runat="server"></asp:TextBox> hours<br />
            <asp:CheckBox ID="moreSubTask" runat="server" text="Create another sub-task"/><br />
            <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" />
        </asp:Panel>
    </div>
    </form>
</body>
</html>
