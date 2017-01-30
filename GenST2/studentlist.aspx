<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="studentlist.aspx.cs" Inherits="GenST2.studentlist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<div class="col-md-3">--%>
    <div>
    <asp:ListView ID="lvStudents" runat="server" 
        Style="background-color: blueviolet"
         ItemType="GenST2.Models.students" 
        SelectMethod="getStudents" 
        ItemPlaceholderID="placeHolder1"  
        DataKeyNames ="studentID" OnItemDataBound="lvStudents_ItemDataBound" >
        <LayoutTemplate>
                            <br />
                <br />
                <br />
                <br />
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th>Student</th>
                            <th>Class Type</th>
                            <th>Present</th>
                            <%--<th>Lastname</th>--%>
                        </tr>
                    </thead>
                    <asp:PlaceHolder runat="server" ID="placeHolder1"></asp:PlaceHolder>
                </table>
        </LayoutTemplate>

        <ItemTemplate>
            <div class="list-group">
                    <tr>
                        <asp:HiddenField ID="selectionKeyForNestedLV" runat="server" Value="<%# Item.studentID %>" />
                        <td>
                            <b style="font-size: large; font-style: normal">
                                <asp:HyperLink ID="studentDetails" runat="server" NavigateUrl='<%# string.Format("studentList.aspx?id={0}",Item.studentID)%>'>
                                    <%#: Item.firstname +"  "%>  <%#: Item.lastname %>
                                </asp:HyperLink>
                            </b>
                        </td>
                        <td> <%--start of inner lv--%>
                            <asp:ListView runat="server" ID="lvStudentDetails" ItemType="GenST2.Models.StudentDetailsDisplayItems"
                                SelectMethod="lvStudentDetails_GetDetails"
                                DataKeyNames="studentID"
                                ItemPlaceholderID="details" OnItemDataBound="lvStudentDetails_ItemDataBound">
                                <LayoutTemplate>
                                    <table class="table table-hover">
                                        <thead>
                                            <tr>
                                                <asp:Label runat="server" CssClass="form-control" ID="lblTest"></asp:Label>
<%--                                                 <asp:CheckBox runat="server" AutoPostBack="true"  OnCheckedChanged="waldo_CheckedChanged" ID="waldo" CssClass="form-check-input"></asp:CheckBox>
                                                 --%>

                                                <asp:PlaceHolder runat="server" ID="details"></asp:PlaceHolder>
                                            </tr>
                                        </thead>
                                    </table>
  
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <div class="list-group">
                                        <tr>
                                            <td>
                                                <%#: Item.classDescription %>
                                            </td>
                                            <td>   
                                                <asp:CheckBox runat="server" AutoPostBack="true"  OnCheckedChanged="present_CheckedChanged" ID="cbPresent" CssClass="form-check-input"></asp:CheckBox>                                      
                                            </td> 
                                        </tr>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                          
                        </td> <%--end of inner LV--%>
                    </tr>
            </div>
        </ItemTemplate>
    </asp:ListView>
</div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scriptTail" runat="server">
</asp:Content>
 