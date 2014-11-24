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
            <asp:Button runat="server" ID="dtnSubmit" OnClick="btnSubmit_Click" Text="Save" />
        </div>
    </div>
</asp:Content>
