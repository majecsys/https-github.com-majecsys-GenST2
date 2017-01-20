<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="studentlist.aspx.cs" Inherits="GenST2.studentlist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListView ID="lvStudents" runat="server" 
        Style="background-color: blueviolet"
         ItemType="GenST2.Models.students" 
        SelectMethod="getStudents" 
        ItemPlaceholderID="placeHolder1"  
          DataKeyNames ="studentID" 
        GroupPlaceholderID="gp1" 
        >
        <LayoutTemplate>
                            <br />
                <br />
                <br />
                <br />
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th>Firstname</th>
                            <th>Lastname</th>
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
                                    <%#: Item.firstname%>  
                                </asp:HyperLink>
                            </b>
                        </td>
                        <td>
                            <%# Item.lastname %>
                        </td>

                        <asp:ListView runat="server" ID="lvStudentDetails" ItemType="GenST2.Models.StudentDetailsDisplayItems"
                            SelectMethod="lvStudentDetails_GetDetails"
                             DataKeyNames="studentID"
                             ItemPlaceholderID ="details">
                            <LayoutTemplate>
                                <table class="table table-hover">
                                    <thead>
                                        <tr>
                                            <th>Class Type</th>
                                            <th>Present</th>
                                        </tr>
                                    </thead>
                                </table>
                                <asp:PlaceHolder runat="server" ID="details"></asp:PlaceHolder>
                            </LayoutTemplate>
                           <ItemTemplate>
                                <div class="list-group">
                                    <tr>
                                        <td>
                                            <%#: Item.classDescription %>
                                        </td>
                                        <td>
                                            Gonna be Checkbox
                                        </td>
                                    </tr>
                                    </div>

                          </ItemTemplate>

                        </asp:ListView>
                    </tr>
            </div>
        </ItemTemplate>
    </asp:ListView>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scriptTail" runat="server">
</asp:Content>
