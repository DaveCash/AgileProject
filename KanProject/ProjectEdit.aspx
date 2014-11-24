<%@ Page Language="C#" MasterPageFile="~/KanProject.Master" AutoEventWireup="true" CodeBehind="ProjectEdit.aspx.cs" Inherits="KanProject.ProjectEdit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="css/kanboard.css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhContentMain" runat="server">
    <div>
        <asp:TextBox runat="server" ID="tbProjectName"></asp:TextBox>
    </div>
</asp:Content>
