<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucTreeViewPrincipal.ascx.cs" Inherits="wucTreeViewPrincipal" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>



<link href="css/StyleSheet.css" rel="stylesheet" />
<style type="text/css">
    a:link {
        text-decoration: none;
    }
</style>

<asp:TreeView ID="TreeView1" CssClass="masterPageMenuPrincipalLinkButton"
    EnableClientScript="true"
    PopulateNodesFromClient="true"
    OnTreeNodePopulate="PopulateNode" runat="server" ShowLines="True">
    <Nodes>
        <asp:TreeNode Text="COMAU"
            SelectAction="Expand"
            PopulateOnDemand="true" Value="0" >
        </asp:TreeNode>
    </Nodes>

    <SelectedNodeStyle BackColor="#CCCCCC" />

</asp:TreeView>
