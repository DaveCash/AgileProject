<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="KanProject.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KanBan</title>
    <script src="scripts/jquery-1.11.1.js"></script>
    <script src="scripts/jquery-ui.js"></script>

    <link rel="stylesheet" type="text/css" href="css/general.css"/>
    <link rel="stylesheet" type="text/css" href="css/jquery-ui.css" />
</head>
<body>
    <form id="form1" runat="server" style="background-image: url('Image/背景1.jpg')">
        <p>
   <a href="http://www.lamk.fi"><asp:Image ID="Image1" runat="server" Height="75px" ImageUrl="~/Image/LAMK_logo_RGB.gif" Width="359px" /></a>
        </p>
        <div id="login-tabs" class="login-form login-form ui-tabs ui-widget ui-widget-content ui-corner-all">
          <ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all" style="background-image: none">
            <li class="ui-state-default ui-corner-top"><a href="Login.aspx" class="ui-tabs-anchor">Login</a></li>
            <li class="ui-state-default ui-corner-top ui-tabs-active ui-state-active"><a href="#register-tab" class="ui-tabs-anchor">Register</a></li>
          </ul>
        <div id="register-tab" class="ui-tabs-panel ui-widget-content ui-corner-bottom">
            <asp:Panel runat="server" ID="pnlErrors" Visible="false">

            </asp:Panel>
            <div class="form-control">
                <label for="username" class="form-label">Username:</label>
                <asp:TextBox runat="server" ID="UserName" ></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic"  CssClass="validator" ForeColor="Red" controltovalidate="username" ID="usernameValidator" ErrorMessage="Username is required!"></asp:RequiredFieldValidator>
            </div>
            <div class="form-control">
                <label for="password" class="form-label">Password:</label>
                <asp:TextBox runat="server" ID="PassWord" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" CssClass="validator" ForeColor="Red" controltovalidate="Password" ID="passwordValidator" ErrorMessage="Password is required!"></asp:RequiredFieldValidator>
            </div>
            <div class="form-control">
                <asp:Button runat="server" OnClick="btnRegister_Click" Text="Register"/>
            </div>
        </div>
        </div>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>