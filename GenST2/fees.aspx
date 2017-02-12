<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="fees.aspx.cs" Inherits="GenST2.fees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br /><br /><br /><br /><br />
    <div class="form-group">
    <asp:DropDownList ID="ddlFeeReportTypes" runat="server" 
        OnSelectedIndexChanged="ddlFeeReportTypes_SelectedIndexChanged" 
        SelectMethod="processDDL"
         AutoPostBack="true">
        <asp:ListItem Value="0">Select</asp:ListItem> 
        <asp:ListItem Value="1">Fees Collected last 30 days</asp:ListItem> 
        <asp:ListItem Value="2">Fees Collected last 12 months</asp:ListItem> 
    </asp:DropDownList>
        <p style="font-size: medium; font-style: normal">
    <asp:Label Font-Bold="true" BorderWidth="1" ID="lblAmts" runat="server" Visible ="true" CssClass="form-check-label" ></asp:Label>
       </p>
        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scriptTail" runat="server">
</asp:Content>
