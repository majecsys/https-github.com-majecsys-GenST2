<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewStudents.aspx.cs" Inherits="GenST2.AddNewStudents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <div class="form-group">
        <label class="form-check-label">
            Check if current Student: 
        <asp:DropDownList runat="server" ID="ddlCurrent"
            ItemType="GenST2.Models.students"
            SelectMethod="getCurrentStudents"
            DataTextField="lastname"
            DataValueField="studentID"
            AppendDataBoundItems="true"
            CssClass="form-control col-md-3"
            AutoPostBack="true" OnSelectedIndexChanged="ddlCurrent_SelectedIndexChanged">
            <asp:ListItem Value="0">Select One:</asp:ListItem>

        </asp:DropDownList>

            <asp:FormView runat="server" ID="currentStudentDemo"
                ItemType="GenST2.Models.students"
                SelectMethod="returnDemo"
                DataKeyNames="studentID"
                DefaultMode="Edit"
                UpdateMethod="currentStudentDemo_UpdateItem"
                CssClass="form-group">

                <ItemTemplate>

                    <table>
                        <div class="form-group">
                            <tr>
                                <td>Firstname:
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="Server" CssClass="form-control" Text='<%#: Item.firstname%>'></asp:TextBox>
                                </td>
                            </tr>
                        </div>
                        <div class="form-group">
                            <tr>
                                <td>Lastname:
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBox2" runat="Server" CssClass="form-control" Text='<%#: Item.lastname%>'></asp:TextBox>
                                </td>
                            </tr>
                        </div>
                        <div class="form-group">
                            <tr>
                                <td>Email:
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBox3" runat="Server" CssClass="form-control" Text='<%#: Item.email%>'></asp:TextBox>
                                </td>
                            </tr>
                        </div>
                        <div class="form-group">
                            <tr>
                                <td>Phone:
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBox4" runat="Server" CssClass="form-control" Text='<%#: Item.phone%>'></asp:TextBox>
                                </td>
                            </tr>
                        </div>

                        <tr>
                            <td colspan="2">
                                <asp:LinkButton ID="UpdateButton"
                                    Text="Update"
                                    CommandName="Update"
                                    runat="server" />
                                &nbsp;
                        <asp:LinkButton ID="CancelUpdateButton"
                            Text="Cancel"
                            CommandName="Cancel"
                            runat="server" />
                            </td>
                        </tr>
                    </table>

                </ItemTemplate>
                <EditItemTemplate>
                    <div>
                        <table>
                            <div class="form-group">
                                <tr>
                                    <td>Firstname:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="Server" CssClass="form-control" Text='<%#: BindItem.firstname%>'></asp:TextBox>
                                    </td>
                                </tr>
                            </div>
                            <div class="form-group">
                                <tr>
                                    <td>Lastname:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TextBox2" runat="Server" CssClass="form-control" Text='<%#: BindItem.lastname%>'></asp:TextBox>
                                    </td>
                                </tr>
                            </div>
                            <div class="form-group">
                                <tr>
                                    <td>Email:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TextBox3" runat="Server" CssClass="form-control" Text='<%#: BindItem.email%>'></asp:TextBox>
                                    </td>
                                </tr>
                            </div>
                            <div class="form-group">
                                <tr>
                                    <td>Phone:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TextBox4" runat="Server" CssClass="form-control" Text='<%#: BindItem.phone%>'></asp:TextBox>
                                    </td>
                                </tr>
                            </div>
                            <tr>
                                <td colspan="2">
                                    <asp:LinkButton ID="UpdateButton"
                                        Text="Update"
                                        CommandName="Update"
                                        runat="server" />
                                    &nbsp;
                        <asp:LinkButton ID="CancelUpdateButton"
                            Text="Cancel"
                            CommandName="Cancel"
                            runat="server" />
                                </td>
                            </tr>


                        </table>
                    </div>
                </EditItemTemplate>

            </asp:FormView>

            Check if Private Lesson: 
      <input type="checkbox" class="form-check-input">
        </label>
        <asp:Panel runat="server" ID="hideForms" Visible="true">

            <div class="form-group">
                <label for="firstName">FirstName:</label>
                <input type="text" class="form-control" id="firstName" aria-describedby="name" runat="server" placeholder="FirstName">
                <%--<small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small>--%>
            </div>
            <div class="form-group">
                <label for="text">Lastname:</label>
                <input type="text" class="form-control" id="lastname" itemtype="GenST2.Models.students" runat="server" value="" placeholder="Lastname or Family Name">
            </div>
            <div class="form-group">
                <label for="email">Email address</label>
                <input type="email" class="form-control" id="email" aria-describedby="emailHelp" runat="server" placeholder="Enter email">
            </div>
            <div class="form-group">
                <label for="phone">Phone</label>
                <input type="tel" class="form-control" id="phone" aria-describedby="tel" runat="server" placeholder="xxx-xxx-xxxx">
            </div>

        </asp:Panel>
    </div>
    <div class="row" id="classesRow">
        <div class="col-md-2">
            <label for="text">Classes:</label>
            <br />
            <asp:ListBox ID="lbClasses"
                CssClass="form-control"
                DataTextField="description"
                DataValueField="classID"
                runat="server"
                ItemType="GenST2.Models.classes"
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
                        <asp:HiddenField runat="server" ID="HiddenFieldAmtDue" />
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
        <div class="col-md-2">
            <label for="text">Courses:</label>
            <br />
            <asp:ListBox
                CssClass="form-control"
                DataTextField="name"
                DataValueField="courseID"
                runat="server"
                ItemType="GenST2.Models.courses"
                SelectMethod="LoadCourses"
                ID="lbCourses">

            </asp:ListBox>
        </div>
        <div class="col-md-2">
            <label for="text">Number Of Weeks:</label>

            <asp:DropDownList
                ID="ddlNumWeeks"
                runat="server"
                CssClass="form-control">
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
        <div class="col-md-2">
            <div class="form-group">
                <label for="text">Fees:</label>
                <asp:HiddenField runat="server" ID="hiddenTotalFees" />
                <asp:HiddenField runat="server" ID="hiddenValueNumWeeks" />
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
    </div>
    <%--paidByRow--%>
    <br />
    <div class="form-group">
        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnSubmitRec" OnClick="btnSubmitRec_Click" Text="Submit Student Record" />
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
                    if ($('#<%=lbClasses.ClientID%>').val()) {
                        $(classIDs).each(function (index, classIDs) {
                            selected.push([$(this).val()]);
                            processClassChoices(selected);
                        });
                    }
                    else {
                        $('#<%=lbl_ClassesPrice.ClientID%>').text('');
                    }
                }
            });
            function processClassChoices(selected) {
                var sum = 0;
                $.each(selected, function (index, value) {
                    $.ajax({
                        type: "POST",
                        url: "AddNewStudents.aspx/GetPrices",
                        data: '{selectedID : "' + value + '"}',
                        contentType: "application/json; charset=utf-8",

                        success: function (msg) {
                            sum += parseInt(msg.d);
                            $('#<%=lbl_ClassesPrice.ClientID%>').text(sum);
                            $('#<%=lbl_totalFees.ClientID%>').text(sum);
                            $('#<%=HiddenFieldAmtDue.ClientID%>').val(sum);
                            //  alert("in success  " + msg.d);
                        },
                        error: function (msg) {
                            //    alert("in error  " + msg.d);
                        }
                    });
                });



<%--               var sum = 0;
                $.each(selected, function (index, value) {
                    if (value == 8) {
                        sum += 72;
                        $('#<%=lbl_ClassesPrice.ClientID%>').text(sum);
                    }
                    else if (value == 9) {
                        sum += 136
                        $('#<%=lbl_classesprice.clientid%>').text(sum);
                    }
                    else if (value == 10) {
                        sum += 192
                        $('#<%=lbl_classesprice.clientid%>').text(sum);
                    }
                    else if (value == 11) {
                        sum += 228
                        $('#<%=lbl_classesprice.clientid%>').text(sum);
                    }
                    else if (value == 12) {
                        sum += 20
                        $('#<%=lbl_classesprice.clientid%>').text(sum);
                    }
                    else if (value == 13) {
                        sum += 100
                        $('#<%=lbl_classesprice.clientid%>').text(sum);
                    }
                    else if (value == 14) {
                        sum += 30
                        $('#<%=lbl_classesprice.clientid%>').text(sum);
                    }
                    $('#<%=lbl_totalfees.clientid%>').text(sum);
                    $('#<%=hiddenfieldamtdue.clientid%>').val(sum);
                });--%>
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
                        processCourseChoices(selected, description);
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
                if ($('#<%=lbl_ClassesPrice.ClientID%>').text() != "") {
                    var classAMTs = $('#<%=lbl_ClassesPrice.ClientID%>').text();
                } else {
                    classAMTs = 0;
                }
                var id = $(this).find("option:selected").attr("value");
                
                $('#<%=hiddenValueNumWeeks.ClientID%>').val(id);

                var total = priceMultiplier * id;
                $('#<%=hiddenTotalFees.ClientID%>').val(parseInt(total) + parseInt(classAMTs));
                $('#<%=lbl_totalFees.ClientID%>').text(parseInt(total) + parseInt(classAMTs));
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
