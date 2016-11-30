﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewStudents.aspx.cs" Inherits="GenST2.AddNewStudents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-check">
        <label class="form-check-label">
            Check if Private Lesson: 
      <input type="checkbox" class="form-check-input">
        </label>

        <div class="form-group">
            <label for="firstName">FirstName:</label>
            <input type="text" class="form-control" id="firstName" aria-describedby="name" runat="server"  placeholder="FirstName">
            <%--<small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small>--%>
        </div>
        <div class="form-group">
            <label for="text">Lastname:</label>
            <input type="text" class="form-control" id="lastname" runat="server" placeholder="Lastname or Family Name">
        </div>
        <div class="form-group">
            <label for="email">Email address</label>
            <input type="email" class="form-control" id="email" aria-describedby="emailHelp" runat="server" placeholder="Enter email">
        </div>
        <div class="form-group">
            <label for="phone">Phone</label>
            <input type="tel" class="form-control" id="phone" aria-describedby="tel" runat="server" placeholder="phone">
        </div>
  </div>
    <div class="row" id="classesRow">
        <div class="col-md-3">

            <select id="ex" multiple="multiple">
                <option value="cheese">Cheese</option>
                <option value="tomatoes">Tomatoes</option>
                <option value="mozarella">Mozzarella</option>
                <option value="mushrooms">Mushrooms</option>
                <option value="pepperoni">Pepperoni</option>
                <option value="onions">Onions</option>
            </select>

            <label for="text">Classes:</label>
            <asp:ListBox ID="lbClasses"
                DataTextField="classDescriptions"
                DataValueField="classID"
                runat="server"
                ItemType="GenST2.Models._class"
                SelectMethod="LoadClasses"
                SelectionMode="Multiple"
                OnSelectedIndexChanged="lbClasses_SelectedIndexChanged"
                >
            </asp:ListBox>
        </div>
        <div class="col-md-1 form-group">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <%--<legend>UpdatePanel</legend>--%>
                        <label for="text">Amt Due:</label>
                        <asp:Label runat="server" CssClass="form-control" ID="lbl_ClassesPrice"></asp:Label>
                    </fieldset>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lbClasses" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <%--classesRow--%>
<%--    <div class="row" id="classesRow">
        <div class="col-md-3">
            <label for="text">Classes:</label>
            <asp:DropDownList
                ID="ddlClasses"
                runat="server"
                DataTextField="classDescriptions"
                SelectMethod="LoadClasses"
                DataValueField="classID"
                ItemType="GenST2.Models._class"
                CssClass="selectpicker "
                AppendDataBoundItems="True"
                OnSelectedIndexChanged="ddlClasses_SelectedIndexChanged"
                AutoPostBack="true"
                 >
                <asp:ListItem Value="0" Text="">--Select --</asp:ListItem>
            </asp:DropDownList>

        </div>

    </div>--%> <%--classesRow--%>

    <div class="row" id="coursesRow">
        <div class="col-md-3">
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
                        >
                        <asp:ListItem Value="0" Text="">--Select --</asp:ListItem>
                    </asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlCourses" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="col-md-3">

                    <label for="text">Number Of Weeks:</label>
                    <asp:DropDownList
                        ID="ddlNumWeeks"
                        runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlNumWeeks_SelectedIndexChanged">
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
    <asp:UpdatePanel ID="updateNumWeeks" runat="server">
        <ContentTemplate>
            <div class="row" id="feeRow">
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="text">Fees:</label>
                        <asp:Label runat="server" CssClass="form-control" ID="lbl_CoursePrice"></asp:Label>
                        <%--<input type="text" class="form-control" id="Fees" placeholder="Fee Amount">--%>
                    </div>
                </div>
              </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlNumWeeks" />
        </Triggers>
    </asp:UpdatePanel>

    <div class="row" id="paidByRow">
        <div class="col-md-3">
            <label class="form-check-label">
                Cash
        <asp:CheckBox runat="server" ID="cbCash" CssClass="form-check-input"></asp:CheckBox>
            </label>
            <label class="form-check-label">
                Check
        <asp:CheckBox runat="server" ID="cbCheck" CssClass="form-check-input"></asp:CheckBox>
            </label>
            <label class="form-check-label">
                Credit Card
        <asp:CheckBox runat="server" ID="cbCreditCard" CssClass="form-check-input"></asp:CheckBox>
            </label>
        </div>
    </div> <%--paidByRow--%>
<br />
<div class="form-group">
    <asp:Button runat="server" CssClass="btn btn-primary" id="btnSubmitRec" OnClick="btnSubmitRec_Click" text="Submit Student Record" />
  <%--<button type="button" runat="server" class="btn btn-primary">Submit Student Record</button> --%>
</div>


</asp:Content>
<asp:Content ID="scriptstail" ContentPlaceHolderID="scriptTail" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
<%--            $('#<%=lbClasses.ClientID%>').multiselect({
                includeSelectAllOption: true,
                noneSelectedText: "select Ranges",
                checkAllText: "select all",
                uncheckAllText: 'select none',
                selectedList: 4,
                minWidth: 100
            });
        });
        $('#<%=lbClasses.ClientID%>').multiselect({
            selectAllValue: 'multiselect-all',
            enableCaseInsensitiveFiltering: true,
            enableFiltering: true,
            maxHeight: '300',
            buttonWidth: '235',
            onChange: function(element, checked) {
                var brands = $('#<%=lbClasses.ClientID%> option:selected');
                var selected = [];
                $(brands).each(function(index, brand){
                    selected.push([$(this).text()]);
                });
   //             console.log(selected);
            }          
        });--%> 

    
    });

    
    </script>
    <script>
        $(document).ready(function () {
            var foo = [];
            $('#<%=lbClasses.ClientID%> option:selected').each(function (i, selected) {
                foo[i] = $(selected).text();
                console.log('worte here'+selected);
            });

            $('#ex:selected').each(function (i, selected) {
                foo[i] = $(selected).text();
                console.log('and here'+selected);
            });

            console.log($("#ex option:selected").text());
<%--            $('#<%=lbClasses.ClientID%>').click(function () {
                $('#ex').hide();
            });--%>
        });

    </script>

</asp:Content>
