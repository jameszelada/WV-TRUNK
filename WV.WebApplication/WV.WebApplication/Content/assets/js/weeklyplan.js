$(window).load(function () {

    var t;// reference to table object
    //Execution

    applyOptionPermissions();
    initializeControls();
    attachEventsToToolbar();
    getAllWeeklyPlans();

    function initializeControls() {
        t = $('#example').DataTable({
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "fnCreatedRow": function (nRow, aData, iDataIndex) {
                //$('td:not(.select-checkbox)', nRow).append("<textarea rows='4' cols='40'></textarea>");
                $('td:eq(1)', nRow).append("<textarea class='actividad' rows='4' cols='28'>" + aData[1] + "</textarea>");
                $('td:eq(2)', nRow).append("<textarea class='observaciones'rows='4' cols='28'>" + aData[2] + "</textarea>");
                $('td:eq(3)', nRow).append("<textarea class='recursos'rows='4' cols='28'>" + aData[3] + "</textarea>");
            },
            columnDefs: [{
                orderable: false,
                className: 'select-checkbox',
                targets: 0
            }
            ],
            select: {
                style: 'os',
                selector: 'td:first-child'
            },
            order: [[1, 'asc']]
        });

        $('#addRow').on('click', function () {
            t.row.add(['', '', '','']).draw(false);

        });

        $('#deleterow').click(function () {
            t.row('.selected').remove().draw(false);
            //disableElements();
        });

        $("#datepicker").datepicker({
            showWeek: true,
            firstDay: 1,
            onSelect: function (dateText, inst) {

                $(this).val();
                var date = new Date( $(this).val());
                var firstday = new Date(date.setDate(date.getDate() - date.getDay()+1));
                var lastday = new Date(date.setDate(date.getDate() - date.getDay() + 5));

                $("#rangofechas").html(" Del: "+firstday.toLocaleDateString()+ " Al: "+lastday.toLocaleDateString());

                $("#fechainicio").val(firstday.getTime());
                $("#fechafinal").val(lastday.getTime());

                //$(this).val($.datepicker.iso8601Week(new Date(dateText)));
            }
        });

        $("#btnshowside").click(displaySidebar);

        $("#menu-toggle").click(function () {
            //e.preventDefault();
            $("#sidebar-wrapper").hide(90);
            $("#wrapperinside").toggleClass("active");
            $("#btnshowside").click(displaySidebar);
        });



    }

    function displaySidebar() {
        $("#sidebar-wrapper").show();
        $("#wrapperinside").toggleClass("active");
        $(this).unbind();
    }

    function disableControls(mode) {
        var table = $('#example').DataTable();
        if (mode == "edit") {
            $("#datepicker").prop('disabled', true);
            $('#addRow').prop('disabled', true);
            $('#deleterow').prop('disabled', true);

            var rowcount = $('#example tbody tr').length;

            if (rowcount > 0) {
                $("textarea").prop('disabled', true);
            }
        } else {
            $("#rangofechas").html("");
            $("#datepicker").prop('disabled', true).val("");
            $('#addRow').prop('disabled', true);
            $('#deleterow').prop('disabled', true);

            table.clear().draw();
        }

    }

    function enableControls(mode) {

        $('#addRow').prop('disabled', false);
        $('#deleterow').prop('disabled', false);

        var table = $('#example').DataTable();

        if (mode == "edit") {
            var rowcount = $('#example tbody tr').length;

            if (rowcount > 0) {
                $("textarea").prop('disabled', false);
            }
        } else {
            table.clear().draw();
            $("#rangofechas").html("");
            $("#datepicker").prop('disabled', false).val("");
        }

    }

    function onAdd() {
        $("#screenmode").html("add");

        enableControls("add");
        $("#action-add").prop('disabled', true);
        $("#action-delete").prop('disabled', true);
        $("#action-edit").prop('disabled', true);
        $("#action-cancel").prop('disabled', false);
        $("#action-save").prop('disabled', false);

    }

    function onCancel() {
        //var mode = $("#screenmode").html();
        //if (mode == "add")
        //{
        disableControls("add");
        $("#action-add").prop('disabled', false);
        $("#action-delete").prop('disabled', false);
        $("#action-edit").prop('disabled', true);
        $("#action-cancel").prop('disabled', true);
        $("#action-save").prop('disabled', true);

        //}
        //else
        //{
        //    disableControls(mode);
        //    $("#action-add").prop('disabled', false);
        //    //****
        //    $("#action-delete").prop('disabled', false);
        //    $("#action-edit").prop('disabled', false);
        //    $("#action-cancel").prop('disabled', true);
        //    $("#action-save").prop('disabled', true);

        //}
    }

    function onload() {
        var mode = $("#screenmode").html();


        disableControls(mode);
        $("#action-add").prop('disabled', false);
        //****
        $("#action-delete").prop('disabled', true);
        $("#action-edit").prop('disabled', true);
        $("#action-cancel").prop('disabled', true);
        $("#action-save").prop('disabled', true);


    }

    function onEdit() {
        $("#screenmode").html("edit");
        enableControls("edit");
        $("#action-add").prop('disabled', true);
        $("#action-delete").prop('disabled', true);
        $("#action-edit").prop('disabled', true);
        $("#action-cancel").prop('disabled', false);
        $("#action-save").prop('disabled', false);
    }

    function onSave() {
        var date = $("#datepicker").val();

        if (date == null || date == undefined || date == "") {
            displayErrorMessage("Debe Ingresar Fecha de Bitacora");
        }
        else {
            var screenmode = $("#screenmode").html();
            saveWeeklyPlan(screenmode);

            disableControls("add");
            $("#action-add").prop('disabled', false);
            $("#action-delete").prop('disabled', false);
            $("#action-edit").prop('disabled', false);
            $("#action-cancel").prop('disabled', true);
            $("#action-save").prop('disabled', true);
        }

    }

    function onDelete() {
        disableControls("add");
        $("#action-add").prop('disabled', false);
        $("#action-delete").prop('disabled', false);
        $("#action-edit").prop('disabled', true);
        $("#action-cancel").prop('disabled', true);
        $("#action-save").prop('disabled', true);
    }

    function onView() {
        disableControls("edit");
        $("#action-add").prop('disabled', false);
        $("#action-delete").prop('disabled', false);
        $("#action-edit").prop('disabled', false);
        $("#action-cancel").prop('disabled', true);
        $("#action-save").prop('disabled', true);
    }

    function attachEventsToToolbar() {
        $("#action-add").click(onAdd);
        $("#action-edit").click(onEdit);
        $("#action-cancel").click(onCancel);
        $("#action-save").click(onSave);
    }

    function displayErrorMessage(message) {

        //$("#errorcontainer").html(message);
        //$("#myErrorDialog").modal('show');
        BootstrapDialog.alert({
            title: 'Error',
            message: message,
            type: BootstrapDialog.TYPE_DANGER, // <-- Default value is BootstrapDialog.TYPE_PRIMARY
            closable: true, // <-- Default value is false
            draggable: false // <-- Default value is false

            //callback: function (result) {
            //    // result will be true if button was click, while it will be false if users close the dialog directly.
            //    alert('Result is: ' + result);
            //}
        });
    }

    function displayMessage(message) {

        BootstrapDialog.alert({
            title: 'Información',
            message: message,
            type: BootstrapDialog.TYPE_PRIMARY, // <-- Default value is BootstrapDialog.TYPE_PRIMARY
            closable: true, // <-- Default value is false
            draggable: false // <-- Default value is false

            //callback: function (result) {
            //    // result will be true if button was click, while it will be false if users close the dialog directly.
            //    alert('Result is: ' + result);
            //}
        });


        //$("#messagecontainer").html(message);
        //$("#myMessageDialog").modal('show');
        $("#pagebtndelete").unbind();
        $("#tabdetails").unbind();
    }


    function attachClickToDates() {
        $("#logbookdates").find("li").each(function (index, value) {
            $(value).click(function () {

                $('#pagetoedit').val($(this).attr("data-weeklyplan-header"));
                $('#pagetodelete').val($(this).attr("data-weeklyplan-header"));
                getSingleWeeklyPlan($(this).attr("data-weeklyplan-header"));

            });
        });
    }

    function getAllWeeklyPlans() {
        var id = $("#idperson").html();
        var dataToSend =
            {
                ID_Persona: id
            };

        $.ajax({
            type: 'POST',
            url: '/Handlers/WeeklyPlan.ashx?method=getall',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    setWeeklyPlanSidebar(response);
                    attachClickToDates();
                    attachClickToModal();
                }
                else {
                    var error = "Error de Conexión, Intente nuevamente  ";
                    displayErrorMessage(error);
                }
            },
            error: function () {
                var error = "Error de Conexión, Intente nuevamente";
                displayErrorMessage(error);
            }
        });
    }

    function setWeeklyPlanSidebar(response) {
        $("#logbookdates").html(response.ResponseData);
        $("#lbl_nombre").html(response.ResponseAdditional);
        onload();

    }

    function attachClickToModal() {
        $("#pagebtndelete").click(function () {
            deleteWeeklyPlan();
            onDelete();
        });
    }

    function deleteWeeklyPlan() {
        var idToDelete = $("#pagetodelete").val();
        var dataToSend =
            {
                ID_PlanSemanal: idToDelete
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/WeeklyPlan.ashx?method=delete',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getAllWeeklyPlans();
                }
                else {
                    displayErrorMessage(response.Message);
                }
            },
            error: function () {
                var error = "Error de Conexión, Intente nuevamente";
                displayErrorMessage(error);
            }
        });
    }

    function getSingleWeeklyPlan(id) {
        var dataToSend =
            {
                ID_PlanSemanal: id
            };

        $.ajax({
            type: 'POST',
            url: '/Handlers/WeeklyPlan.ashx?method=getsingle',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    fillData(response.ResponseData);
                    onView();


                }
                else {
                    var error = "Error de Conexión, Intente nuevamente  ";
                    displayErrorMessage(error);
                }
            },
            error: function () {
                var error = "Error de Conexión, Intente nuevamente";
                displayErrorMessage(error);
            }
        });
    }

    function fillData(weeklyplan) {
        //Llenando encabezado
        var date = new Date(parseInt(weeklyplan.ObjetoPlanSemanal.FechaInicio.substr(6)));
        $("#datepicker").datepicker("setDate", date);

        var firstday = new Date(date.setDate(date.getDate() - date.getDay() + 1));
        var lastday = new Date(date.setDate(date.getDate() - date.getDay() + 5));

        $("#rangofechas").html(" Del: " + firstday.toLocaleDateString() + " Al: " + lastday.toLocaleDateString());

        $("#fechainicio").val(firstday.getTime());
        $("#fechafinal").val(lastday.getTime());

        //llenando Detalle
        var detalle = weeklyplan.ObjetoPlanSemanalDetalle;
        t.clear().draw();
        for (var i = 0; i < detalle.length; i++) {

            t.row.add(["", detalle[i]["Actividad"], detalle[i]["Observaciones"], detalle[i]["Recurso"]]).draw(false);

        }

        $("tbody > tr > td > textarea").each(function (value, index) {

            $(this).parent().html($(this));

        });

    }

    function attachEventsToToolbar() {
        $("#action-add").click(onAdd);
        $("#action-edit").click(onEdit);
        $("#action-cancel").click(onCancel);
        $("#action-save").click(onSave);
    }

    function saveWeeklyPlan(screenmode) {
        var id = $("#idperson").html();

        //Here I create the array of detail weeklyplans

        if (screenmode === "add") {

            var planSemanalDetalle = [];
            if (t.data().count() > 0) {
                $("table#example > tbody > tr").each(function (index, value) {


                    var actividad = $(value).find('textarea.actividad').val();
                    var observaciones = $(value).find('textarea.observaciones').val();
                    var recurso = $(value).find('textarea.recursos').val();

                    planSemanalDetalle.push({ "ID_PlanSemanalDetalle": 0, "Actividad": actividad, "Observaciones": observaciones ,"Recurso":recurso});

                });
            }

            var dataToSend =
                {
                    ID_Persona: parseInt(id),
                    FechaInicio: $("#fechainicio").val(),
                    FechaFinal: $("#fechafinal").val(),
                    PlanSemanalDetalle: planSemanalDetalle,
                    ToDelete: []
                };

            var data = JSON.stringify(dataToSend);

            makeAjaxCall(data, screenmode);
            //$("#tabtable").tab("show");
            //$("#cancelpage").unbind();
            //$("#savepage").unbind();
            //clearControls();
        }
        else if (screenmode === "edit") {
            // To implement****************
            var id = $('#pagetoedit').val();

            var planSemanalDetalle = [];
            if (t.data().count() > 0) {
                $("table#example > tbody > tr").each(function (index, value) {


                    var actividad = $(value).find('textarea.actividad').val();
                    var observaciones = $(value).find('textarea.observaciones').val();
                    var recurso = $(value).find('textarea.recursos').val();

                    planSemanalDetalle.push({ "ID_PlanSemanalDetalle": 0, "Actividad": actividad, "Observaciones": observaciones, "Recurso": recurso });

                });
            }

            var dataToSend =
                {
                    ID_Persona: parseInt(id),
                    FechaInicio: "",
                    FechaFinal:"",
                    PlanSemanalDetalle: planSemanalDetalle,
                    ToDelete: []

                };

            var data = JSON.stringify(dataToSend);
            makeAjaxCall(data, screenmode);
        }
    }

    function makeAjaxCall(dataToSend, action) {
        $.ajax({
            type: 'POST',
            url: '/Handlers/WeeklyPlan.ashx?method=' + action,
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getAllWeeklyPlans();
                } else {
                    displayErrorMessage(response.Message);
                }
            },
            error: function () {
                var error = "Error de Conexión, Intente nuevamente";
                displayErrorMessage(error);
            }
        });
    }

    function applyOptionPermissions() {
        if (!Security.editar) {
            $("#action-edit").addClass("hidden");
        }
        if (!Security.eliminar) {
            $("#action-delete").addClass("hidden");
        }
        if (!Security.agregar) {
            $("#action-add").addClass("hidden");

        }
    }

});