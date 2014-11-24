<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="assign_task.aspx.cs" Inherits="KanProject.assign_task" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>Adding Task</h1>
       <table>
            <tr>
                <td>Member</td>
                <td><asp:DropDownList ID="listUser" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Task</td>
                <td><asp:DropDownList ID="listTask" runat="server" OnSelectedIndexChanged="listTask_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Task Description</td>
                <td><asp:TextBox ID="taskDes" runat="server" Height="100px" Width="400px" ></asp:TextBox></td>
            </tr>
       </table><br />
        <asp:Button ID="submit" runat="server" Text="Submit" OnClick="submit_Click"/>
    </form>
</body>
</html>
