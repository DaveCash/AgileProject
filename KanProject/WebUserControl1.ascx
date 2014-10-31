<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl1.ascx.cs" Inherits="KanProject.WebUserControl1" %>
<asp:panel ID="panel1" runat="server">
    <h1>Comment</h1>
    <asp:TextBox ID="comment" runat="server" Width="400" Height="200"></asp:TextBox>
    <table>
        <tr>
            <td><asp:Button ID="submit" runat="server" Text="Submit" OnClick="submit_Click" /></td>
            <td><asp:Button ID="cancel" runat="server" Text="Cancel" OnClick="cancel_Click" /></td>
        </tr>
    </table>
</asp:panel>