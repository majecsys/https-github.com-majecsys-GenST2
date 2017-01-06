<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkin.aspx.cs" Inherits="GenST2.Checkin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>

        <asp:ListView ID="lvCheckIn" runat="server"
            Style="background-color: blueviolet"
            ItemType="GenST2.Models.checkins"
            SelectMethod="lvcheckin_getdata" GroupPlaceholderID="groupPlaceHolder1"
            ItemPlaceholderID="itemPlaceHolder1">

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
                            <th>Class</th>
                            <th>Course</th>
                            <th>Present</th>
                        </tr>
                    </thead>
                    <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <GroupTemplate>
                <tr>
                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                </tr>
            </GroupTemplate>
            <ItemTemplate>
                <div class="list-group">
                    <tbody>
                        <tr>
                            <td><%# Item.FirstName %> </td>
                            <td><%# Item.Lastname %></td>

                            <td><%# Item.classDesc %></td>
                            <td><%# Item.courseDesc %></td>
                            <td><%--<%# Item.present %>--%>
                                <asp:CheckBox AutoPostBack="true" runat="server" ID="cbPresent" CssClass="form-check-input" OnCheckedChanged="cbPresent_CheckedChanged" />
                                <%--<input runat="server" type="radio" class="form-check-input">--%>

                            </td>
                            <asp:HiddenField ID="studentID" runat="server"  Value="<%# Item.studentID %>" />
                            <asp:HiddenField ID="classID" runat="server"  Value="<%# Item.classID%>" />
                        </tr>
                    </tbody>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
