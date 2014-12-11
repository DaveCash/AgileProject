<%@ Page Language="C#" MasterPageFile="~/KanProject.Master" AutoEventWireup="true" CodeBehind="ProjectManagement.aspx.cs" Inherits="KanProject.ProjectManagement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="css/kanboard.css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhContentMain" runat="server">
    <div style="width: 25%; margin-top: 20px;">
        Projects

        <asp:Repeater runat="server" ID="rptProjects">
            <HeaderTemplate>
                <table style="margin-top: 10px;">
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("ProjectName") %>
                    </td>
                    <td>
                        <asp:HyperLink runat="server" Text="Edit project" NavigateUrl='<%# Eval("ProjectId", "ProjectEdit.aspx?ProjectId={0}") %>'></asp:HyperLink>
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnDelete" OnClick="btnDelete_Click" Text="Delete project" ProjectId='<%# Eval("ProjectId") %>'/>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>

    <div style="width: 25%; margin-top: 20px;">
        Create project
        <div class="field">
            <label class="label">Project name: </label>
            <asp:TextBox runat="server" ID="tbProjectName"></asp:TextBox>
        </div>
        <div class="field">
            <label class="label">Project owner: </label>
            <asp:DropDownList runat="server" ID="ddlOwner"></asp:DropDownList>
        </div>

        <asp:Button runat="server" ID="btnCreate" OnClick="btnCreate_Click" Text="Create"/>
        <asp:Button runat="server" ID="btnCancel" OnClick="btnCancel_Click" Text="Cancel" />
        <asp:Button runat="server" ID="btnChart" Text="Chart Page" OnClick="btnChart_Click" />
    </div>

    <script type="text/javascript">
        

    </script>
</asp:Content>