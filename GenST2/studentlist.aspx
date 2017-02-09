<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="studentlist.aspx.cs" Inherits="GenST2.studentlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<div class="col-md-3">--%>
    <div>
        <asp:ListView ID="lvStudents" runat="server"
            Style="background-color: blueviolet"
            ItemType="GenST2.Models.students"
            SelectMethod="getStudents"
            ItemPlaceholderID="placeHolder1"
            DataKeyNames="studentID" OnItemDataBound="lvStudents_ItemDataBound">
            <LayoutTemplate>
                <br />
                <br />
                <br />
                <br />
                <table  class="table table-hover table-condensed">
                    <thead >
                        <tr >
                            <th >Student</th>
                            <th>Class Type</th>
                           <%-- <th>Present</th>--%>
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
                            <b style="font-size: medium; font-style: normal">
                                <asp:HyperLink ID="studentDetails" runat="server" NavigateUrl='<%# string.Format("studentList.aspx?id={0}",Item.studentID)%>'>
                                    <%#: Item.firstname +"  "%>  <%#: Item.lastname %>
                                </asp:HyperLink>
                            </b>
                        </td>
            
                        <td ><%--start of inner lv--%>
                            <asp:ListView runat="server" ID="lvStudentDetails" ItemType="GenST2.Models.StudentDetailsDisplayItems"
                                SelectMethod="lvStudentDetails_GetDetails"
                                DataKeyNames="studentID"
                                ItemPlaceholderID="details" OnItemDataBound="lvStudentDetails_ItemDataBound">
                                <LayoutTemplate>
                                    <table class="table-hover">
                                        <thead>
                                            <tr>
                                                <%--<asp:Label runat="server" CssClass="form-control" ID="lblTest"></asp:Label>--%>
                                                <%--                                                 <asp:CheckBox runat="server" AutoPostBack="true"  OnCheckedChanged="waldo_CheckedChanged" ID="waldo" CssClass="form-check-input"></asp:CheckBox>
                                                --%>

                                                <asp:PlaceHolder runat="server" ID="details"></asp:PlaceHolder>
                                            </tr>
                                        </thead>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <div class="row" >
                                       
                                            <tr>
                                                <div>
                                                    <td >
                                                        <asp:Label ID="lblClassDesc" runat="server" Text=" <%#: Item.classDescription %>"></asp:Label>

                                                        <asp:HiddenField ID="hiddenStudentID" runat="server" Value="<%# Item.studentID %>" />
                                                        <asp:HiddenField ID="hiddenPkgID" runat="server" Value="<%# Item.classID %>" />
                                                    </td>
                                                    
                                                    <td class="col-md-4">
                                                        <asp:CheckBox runat="server" AutoPostBack="true" OnCheckedChanged="present_CheckedChanged" ID="cbPresent" CssClass="form-check-input"></asp:CheckBox>
                                                    </td>

                                                </div>
                                            </tr>
                                       
                                        <tr id="courseNameRow" runat="server">
                                            <td>
                                                <asp:Label ID="lblCourseName"  runat="server" Text=" <%#: Item.name %>"></asp:Label></td>
                                           <td class="text-right"> 
                                                <asp:Label ID="lblExpiration" runat="server" Text="<%#:Item.expiration%>" Style="color: green; margin-left: 30px;"></asp:Label>
                                               
                                               </td>
                                            
                                        </tr>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                            
                        </td>
          
                        <%--end of inner LV--%>
                    </tr>
                </div>
                    
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scriptTail" runat="server">
</asp:Content>
