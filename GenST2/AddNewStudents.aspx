<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewStudents.aspx.cs" Inherits="GenST2.AddNewStudents" %>
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
            <input type="tel" class="form-control" id="phone" aria-describedby="tel" runat="server" placeholder="xxx-xxx-xxxx">
        </div>
  </div>
    <div class="row" id="classesRow">
        <div class="col-md-3">
            <label for="text">Classes:</label>
                   <br />
            <asp:ListBox ID="lbClasses"
                CssClass="form-control"
                DataTextField="classDescriptions"
                DataValueField="classID"
                runat="server"
                ItemType="GenST2.Models._class"
                SelectMethod="LoadClasses"
                SelectionMode="Multiple"
                OnSelectedIndexChanged="lbClasses_SelectedIndexChanged"></asp:ListBox>
        </div>
        <div class="col-md-2 form-group">
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


    </div>--%> <%--classesRow--%>

    <div class="row" id="coursesRow">
        <div class="col-md-3">
            <label for="text">Courses:</label>
            <br />
            <asp:ListBox
                CssClass="form-control"
                DataTextField="courseDescription"
                DataValueField="courseID"
                runat="server"
                ItemType="GenST2.Models.course"
                SelectMethod="LoadCourses"
                SelectionMode="Multiple"
                ID="lbCourses"></asp:ListBox>
        </div>
        <div class="col-md-3">
            <label for="text">Number Of Weeks:</label>

            <asp:DropDownList
                ID="ddlNumWeeks"
                runat="server"
                CssClass="form-control"
               >
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
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="text">Fees:</label>
                        <asp:HiddenField runat="server" ID="hiddenTotalFees" />
                        <asp:Label runat="server" CssClass="form-control" ID="lbl_totalFees"></asp:Label>

                        <%--<input type="text" class="form-control" id="Fees" placeholder="Fee Amount">--%>
                    </div>
                </div>
              </div>


    <div class="row" id="paidByRow" runat="server">
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
<%--        <asp:UpdatePanel ID="btn" runat="server">
        <ContentTemplate>
             
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmitRec" />
        </Triggers>
    </asp:UpdatePanel>--%>


</div>


</asp:Content>
<asp:Content ID="scriptstail" ContentPlaceHolderID="scriptTail" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            $('#<%=ddlNumWeeks.ClientID%>').attr("disabled", "disabled");

  //****************   
            $('#<%=lbClasses.ClientID%>').multiselect({
                selectAllValue: 'multiselect-all',
                enableCaseInsensitiveFiltering: false,
                enableFiltering: false,

                onChange: function (element, checked) {
                    var classIDs = $('#<%=lbClasses.ClientID%> option:selected');
                    var selected = [];
                    $(classIDs).each(function (index, classIDs) {
                        selected.push([$(this).val()]);
                        processClassChoices(selected);
                    });
                }
            });
            function processClassChoices(selected) {
                
                var sum = 0;
                $.each(selected, function (index, value) {
                    if (value == 1) {
                        sum += 72;
                        $('#<%=lbl_ClassesPrice.ClientID%>').text(sum);
                    }
                    else if (value == 2) {
                        sum += 136
                        $('#<%=lbl_ClassesPrice.ClientID%>').text(sum);
                    }
                     else if (value == 3) {
                        sum += 192
                        $('#<%=lbl_ClassesPrice.ClientID%>').text(sum);
                     }
                     else if (value == 4) {
                        sum += 228
                        $('#<%=lbl_ClassesPrice.ClientID%>').text(sum);
                     }
                     else if (value == 5) {
                        sum += 20
                        $('#<%=lbl_ClassesPrice.ClientID%>').text(sum);
                     }
                     else if (value == 6) {
                        sum += 100
                        $('#<%=lbl_ClassesPrice.ClientID%>').text(sum);
                     }
                     else if (value == 7) {
                        sum += 30
                        $('#<%=lbl_ClassesPrice.ClientID%>').text(sum);
                    }
                });
        }
  //****************          
            $('#<%=lbCourses.ClientID%>').multiselect({
                selectAllValue: 'multiselect-all',
                enableCaseInsensitiveFiltering: false,
                enableFiltering: false,
                onChange: function (element, checked) {
                    var courseIds = $('#<%=lbCourses.ClientID%> option:selected');
                    var selected = [];
                    var description = "";

                    $(courseIds).each(function (index, courseIds) {
                        description = $(this).text();
                        selected.push([$(this).val()]);
                        processCourseChoices(selected,description);
                    });
                }

            });
            function processCourseChoices(selected, description) {
                $('#<%=ddlNumWeeks.ClientID%>').removeAttr("disabled");
                $.each(selected, function (index, value) {
                    if (value == 3) {
                        priceMultiplier = 18;
                    }
                    else {
                        priceMultiplier = 15;
                    }
                });
            };

            $('#<%=ddlNumWeeks.ClientID%>').change(function () {
                var classAMTs = $('#<%=lbl_ClassesPrice.ClientID%>').text();
                var id = $(this).find("option:selected").attr("value");
                var total = priceMultiplier * id;
                $('#<%=hiddenTotalFees.ClientID%>').val(parseInt(total) + parseInt(classAMTs));
                $('#<%=lbl_totalFees.ClientID%>').text(parseInt(total)+ parseInt(classAMTs));
            });
            //*******************    
        }); //end of main group
        
        //$(document).ready(function () {
        //    $("select").mouseenter(function (e) {
        //        e.stopPropagation();
        //        $id = $(this).attr("id");
        //        console.log($id);
        //    });      
        //});
    </script>

</asp:Content>
