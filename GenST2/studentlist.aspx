﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="studentlist.aspx.cs" Inherits="GenST2.studentlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<div class="col-md-3">--%>
    <div>
        <asp:ListView ID="lvStudents" runat="server"
            Style="background-color: blueviolet"
            ItemType="GenST2.Models.students"
            SelectMethod="getStudents"
            ItemPlaceholderID="ItemplaceHolder1"
             GroupItemCount="2" GroupPlaceholderID="grpPlaceHolder1"
            DataKeyNames="studentID">
            <LayoutTemplate>
                <br />
                <br />
                <br />
                <br />
                <table class="table table-hover table-condensed ">
                    <tr>
                        <th>Student</th>
                        <th>Class Type</th>
                       <%-- <th>Present</th>--%>
                    </tr>
                    <asp:PlaceHolder ID="grpPlaceHolder1" runat="server"></asp:PlaceHolder>

                </table>

<%--                <table  class="table table-hover info table-condensed">
                    <thead >
                        <tr >
                            <th >Student</th>
                            <th>Class Type</th>--%>
                           <%-- <th>Present</th>--%>
                            <%--<th>Lastname</th>--%>
<%--                        </tr>
                    </thead>
                    <asp:PlaceHolder runat="server" ID="placeHolder1"></asp:PlaceHolder>
                </table>--%>
            </LayoutTemplate>
            <GroupTemplate>
                <tr>
                    <asp:PlaceHolder runat="server" ID="ItemplaceHolder1"></asp:PlaceHolder>
                </tr>
            </GroupTemplate>

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
                                ItemPlaceholderID="details" 
                               
                                OnItemDataBound="lvStudentDetails_ItemDataBound">
                                <LayoutTemplate>
                                    <table class="table-hover">
                                     
                                            <tr>
                                                <%--<asp:Label runat="server" CssClass="form-control" ID="lblTest"></asp:Label>--%>
                                                <%--                                                 <asp:CheckBox runat="server" AutoPostBack="true"  OnCheckedChanged="waldo_CheckedChanged" ID="waldo" CssClass="form-check-input"></asp:CheckBox>
                                                --%>

                                                <asp:PlaceHolder runat="server" ID="details"></asp:PlaceHolder>
                                            </tr>
                                        
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <div class="col-mid-4 form-group" >
                                       
                                            <tr>
                                                
                                                    <td >
                                                        <asp:Label ID="lblCardDesc" runat="server" Text=" <%#: Item.classDescription %>"></asp:Label>

                                                        <asp:HiddenField ID="hiddenStudentID" runat="server" Value="<%# Item.studentID %>" />
                                                        <asp:HiddenField ID="hiddenPkgID" runat="server" Value="<%# Item.classcardID %>" />
                                                    </td>
                                                    
                                                    <td  >
                                                        <asp:CheckBox runat="server" AutoPostBack="true" OnCheckedChanged="present_CheckedChanged" ID="cbPresent" ></asp:CheckBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="classExpiration"  Style="color: black;" runat="server">></asp:Label>
                                                    </td>

                                               
                                            </tr>
                                       
                                        <tr id="courseNameRow" runat="server">
                                            <td>
                                                <asp:Label ID="lblCourseName"  runat="server" Text=" <%#: Item.name %>"></asp:Label>
                                            </td>
                                            <td class="col-md-4">
                                                <asp:Label ID="lblExpiration" runat="server" Text="<%#:Item.expiration%>" Style="color: black;"></asp:Label>
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
