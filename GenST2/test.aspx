<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="GenST2.test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id=wrapper>


    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scriptTail" runat="server">
                <br>
            <br>            <br>
            <br>
    <asp:Panel ID="p1" runat="server">
        <div class="panel-head">
            A basic TextBox:
            <asp:TextBox ID="tb1" CssClass="tb" runat="server" />
            <br>
            <br>
        </div>
        <div class="panl-body">
            A TextBox with text:
            <asp:TextBox ID="tb2" Text="Hello World!" runat="server" />
            <br>
            <br>
            <input type="text" id="salmon" />
        </div>
                    <asp:ListBox ID="lbClasses" 

                runat="server"


                SelectionMode="Multiple"

                >
            </asp:ListBox>
    </asp:Panel>
    <script>
        $(document).ready(function () {
            $("[ID$='tb1']").hide(5000);
        });
        $("select").mouseenter(function (e) {
            e.stopPropagation();
            $id = $(this).attr("id");
            console.log($id);
        });
    </script>
</asp:Content>
