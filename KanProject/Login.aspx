<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="KanProject.Login" %>

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
    <div style="position:relative;height:100%">
    <form id="form1" runat="server" style="background: url('image/Background.jpg') ; height:800px">
        <br />
    <a href="http://www.lamk.fi" style="margin:12px"><asp:Image ID="Image1" runat="server"  Height="45px" ImageUrl="~/image/logo-white.png" Width="458px" /></a>
        <div id="login-tabs" class="login-form login-form ui-tabs ui-widget ui-widget-content ui-corner-all">
          <ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
            <li class="ui-state-default ui-corner-top ui-tabs-active ui-state-active"><a href="#login-tab" class="ui-tabs-anchor">Login</a></li>
            <li class="ui-state-default ui-corner-top"><a href="Register.aspx" class="ui-tabs-anchor">Register</a></li>
          </ul>
          <div id="login-tab" class="ui-tabs-panel ui-widget-content ui-corner-bottom">
            <asp:Panel runat="server" ID="pnlErrors" Visible="false">

            </asp:Panel>
            <div class="form-control">
                <label for="username" class="form-label">Username:</label>
                <asp:TextBox runat="server" ID="UserName" Width="196px" ></asp:TextBox>
                <asp:RequiredFieldValidator runat="server"  Display="Dynamic" CssClass="validator" ForeColor="Red" controltovalidate="username" ID="usernameValidator" ErrorMessage="Username is required!"></asp:RequiredFieldValidator>
            </div>
            <div class="form-control">
                <label for="password" class="form-label">Password:</label>
                <asp:TextBox runat="server" ID="PassWord" TextMode="Password" Width="196px"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server"  Display="Dynamic" CssClass="validator" ForeColor="Red" controltovalidate="PassWord" ID="passwordValidator" ErrorMessage="Password is required!"></asp:RequiredFieldValidator>
            </div>
            <div class="form-control">
                <asp:Button runat="server" OnClick="btnLogin_Click" Text="Sign in"/>
            </div>
        </div>
        </div>
      
    </form>
        </div>
</body>
</html>
