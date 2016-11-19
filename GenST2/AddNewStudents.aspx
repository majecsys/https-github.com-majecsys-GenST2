<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewStudents.aspx.cs" Inherits="GenST2.AddNewStudents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-check">
        <label class="form-check-label">
            Check if Private Lesson: 
      <input type="checkbox" class="form-check-input">
        </label>

        <div class="form-group">
            <label for="firstName">FirstName:</label>
            <input type="text" class="form-control" id="firstName" aria-describedby="name" placeholder="FirstName">
            <%--<small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small>--%>
        </div>
        <div class="form-group">
            <label for="text">Lastname:</label>
            <input type="text" class="form-control" id="lastname" placeholder="Lastname">
        </div>
        <div class="form-group">
            <label for="exampleInputEmail1">Email address</label>
            <input type="email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="Enter email">
        </div>
        <div class="form-group">
            <label for="phone">Phone</label>
            <input type="tel" class="form-control" id="phone" aria-describedby="tel" placeholder="phone">
<%--            <small id="phoneHelp" class="form-text text-muted">We'll never share your phone with anyone else.</small>--%>
        </div>

      <%-- <div class="form-group">
    <label for="exampleInputEmail1">Email address</label>
    <input type="email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="Enter email">
    <small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small>
  </div>
 <div class="form-group">
    <label for="exampleInputEmail1">Email address</label>
    <input type="email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="Enter email">
    <small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small>
  </div>
  <div class="form-group">
    <label for="exampleInputPassword1">Password</label>
    <input type="password" class="form-control" id="exampleInputPassword1" placeholder="Password">
  </div>--%>


  </div>


    <div class="row" id="classesRow">
        <div class="col-xs-3">
            <label for="text">Classes:</label>
            <asp:DropDownList
                ID="ddlClasses"
                runat="server"
                DataTextField="classDescriptions"
                SelectMethod="LoadClasses"
                DataValueField="classID"
                ItemType="GenST2.Models._class"
                CssClass="form-control"
                AppendDataBoundItems="True"
                OnSelectedIndexChanged="ddlClasses_SelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Value="0" Text="">--Select --</asp:ListItem>
            </asp:DropDownList>

        </div>
         <div class="col-xs-1 form-group">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <fieldset>
                    <%--                            <legend>UpdatePanel</legend>--%>
                        <label for="text">Amt Due:</label>
                        <asp:Label runat="server" CssClass="form-control" ID="lbl_ClassesPrice"></asp:Label>
                </fieldset>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlClasses" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        </div>
    </div> <%--classesRow--%>

    <div class="row" id="coursesRow">
        <div class="col-xs-3">
            <asp:UpdatePanel ID="updateCourses" runat="server">
                <ContentTemplate>
                    <label for="text">Courses:</label>
                    <asp:DropDownList
                        ID="ddlCourses"
                        runat="server"
                        DataTextField="courseDescription"
                        SelectMethod="LoadCourses"
                        DataValueField="courseID"
                        ItemType="GenST2.Models.course"
                        CssClass="form-control"
                        AppendDataBoundItems="True"
                        OnSelectedIndexChanged="ddlCourses_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Value="0" Text="">--Select --</asp:ListItem>
                    </asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlCourses" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="col-xs-3">
            <label for="text">Number Of Weeks:</label>
            <asp:DropDownList
                ID="ddlNumWeeks"
                runat="server"
                CssClass="form-control"
                AutoPostBack="true"
                 OnSelectedIndexChanged="ddlNumWeeks_SelectedIndexChanged" >
                <asp:ListItem Value="0">Num Weeks</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
                <asp:ListItem Value="5">5</asp:ListItem>
                <asp:ListItem Value="6">6</asp:ListItem>
                <asp:ListItem Value="7">7</asp:ListItem>
                <asp:ListItem Value="8">8</asp:ListItem>
            </asp:DropDownList>
        </div>  
        <%--endCol--%>

    </div>
    <%--coursesRow--%>

            <div class="row" id="feeRow">
            <div class="col-xs-3">
                <div class="form-group">
                    <label for="text">Fees:</label>
                    <asp:Label runat="server" CssClass="form-control" ID="LblFees"></asp:Label>
                    <%--<input type="text" class="form-control" id="Fees" placeholder="Fee Amount">--%>
                </div>
            </div>
        </div>







</asp:Content>
