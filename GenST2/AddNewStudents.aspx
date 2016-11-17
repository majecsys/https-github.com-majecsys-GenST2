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
        <br>

    <div class="row">
            <div class="col-xs-4">
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
             AutoPostBack="true" >
            <asp:ListItem Value="0" Text="">--Select --</asp:ListItem>
        </asp:DropDownList>

                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                     <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="ddlClasses" EventName="SelectedIndexChanged" />
                     </Triggers>
                 </asp:UpdatePanel>

    </div>
        <div class="col-xs-3">
            <select class="form-control">
                <option>Select</option>
            </select>
        </div>

        <div class="col-xs-5">
            <select class="form-control">
                <option>Select</option>
            </select>
        </div>
    </div>



</asp:Content>
