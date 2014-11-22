<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="comment.aspx.cs" Inherits="KanProject.comment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Comment</h1>
        <asp:TextBox ID="commentBox" runat="server" Height="175" Width="400"></asp:TextBox>
        <table>
            <tr>
                <td><asp:Button ID="submit" runat="server" Text="Submit" OnClick="submit_Click1" /></td>
                <td><asp:Button ID="cancel" runat="server" Text="Cancel" OnClick="cancel_Click" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
