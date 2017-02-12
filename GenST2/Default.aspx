    <%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GenST2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Austin Flamenco Academy Tracker</h1>
        <p class="lead">Login in to track student attendance, payments, and contact information.</p>
        <%--<p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>--%>
    </div>

    <div class="row">
        <div class="col-md-6">
            <h2>Adding New Students</h2>

            <p>
                <a class="btn btn-default" href="AddNewStudents.aspx">Add New Students &raquo;</a>
            </p>
        </div>
        <div class="col-md-6">
            <h2>Check In Students</h2>

            <p>
                <a class="btn btn-default" href="studentlist.aspx">Check In &raquo;</a>
            </p>
        </div>
        <div class="col-md-6">
            <h2>Payments</h2>
            <p>
                <a class="btn btn-default" href="fees.aspx">Check Income &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
