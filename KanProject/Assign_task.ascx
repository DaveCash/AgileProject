<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Assign_task.ascx.cs" Inherits="KanProject.WebUserControl2" %>
<asp:Panel ID="Panel1" runat="server">
    <h1>Adding Task</h1>
       <table>
            <tr style="display:none;">
                <td>Member</td>
                <td>
                    <asp:HiddenField ID="TaskId" runat="server" />
                    <asp:HiddenField ID="projectId" runat="server" />
                    <asp:HiddenField ID="colIndex" runat="server" />
            </tr>
            <tr>
                <td>Task</td>
                <td><asp:TextBox ID="txtTaskName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Task Description</td>
                <td><asp:TextBox ID="taskDes" runat="server" Height="100px" Width="400px" ></asp:TextBox></td>
            </tr>
            <tr>
                <td>Complexity</td>
                <td><asp:TextBox ID="Complexity" runat="server" Height="19px" Width="56px" ></asp:TextBox></td>
            </tr>
           <tr>
               <td>Work estimation(hours)</td>
               <td><asp:TextBox runat="server" ID="Estimate"></asp:TextBox></td>
           </tr>
           <tr>
               <td>Assigned user</td>
               <td>
                   <select name="TaskUser" class="users-list">

                   </select>
               </td>
           </tr>
       </table><br />
        <asp:Button ID="submit" runat="server" Text="Submit" OnClick="submit_Click"/><input type="button" value="close"  id="btnClose"/>
 </asp:Panel>
