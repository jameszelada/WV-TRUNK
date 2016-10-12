$(window).load(function () {

    var tas;
    //Function calling
    getProgramsCombo();
    InitializeControls();



    function InitializeControls()
    {
        $('#calendar').fullCalendar({
            lang: 'es',
            editable: true,
            selectable: true,
            eventLimit: true,
            dayClick: function (date, jsEvent, view) {

                var dateSelected = moment(date);
                VerifyDay(dateSelected.toDate().getTime());
            },
            disableDragging: true,
            header: {
                left: 'prev,next',
                center: 'title',
                right: 'today'
            }, eventClick: function (calEvent, jsEvent, view) {
                var date = moment(calEvent.start);
                var day = date.format("dddd");
                var month = date.format("MMMM");
                var year = date.format("YYYY");
                var numberDate = date.format("DD");

                var fulldate = day + ", " + numberDate + " de " + month + ", " + year;
                $("#attendance_date").html(fulldate);
                VerifyDay(date.toDate().getTime());
                
            },
        });

        tas = $('#dataTables-example').DataTable({
            "bPaginate": true,
            "bFilter": true,
            "bInfo": false,
            "bLengthChange": false,
            "pagingType": "numbers",
            "fnCreatedRow": function (nRow, aData, iDataIndex) {
                //$('td:not(.select-checkbox)', nRow).append("<textarea rows='4' cols='40'></textarea>");
                //$('td:eq(0)', nRow).append("<label >" + aData[0] + "</label>");

            },
            language: {
                searchPlaceholder: "Búsqueda",
                "search": "",
                "emptyTable": "No hay datos encontrados",
                "zeroRecords": "No hay datos disponibles"
            },
            fnDrawCallback: function () {
                $("#dataTables-example thead").remove();
            },
            columns: [
                { "width": "10%" },
                { "width": "50%" },
                null
                
            ]

        });

        $("#cmbprograma").change(function () {
            
            getActivities();
            getProgramMembers();

        });
        $("#cmbprograma").trigger("change");

        //$('li.dropdown.mega-dropdown a').on('click', function (event) {
        //    $(this).parent().toggleClass('open');
        //});

        $("#btnpresente").click(function () {
            if (tas.data().count() > 0) {
                tas.rows().iterator('row', function (context, index) {
                    var node = $(this.row(index).node());
                    $(node).find("input[data-id-type='1']").each(function () {
                        
                        $(this).parent().button('toggle');

                    });

                });
            }
        });
        $("#btnausente").click(function () {
            if (tas.data().count() > 0) {
                tas.rows().iterator('row', function (context, index) {
                    var node = $(this.row(index).node());
                    $(node).find("input[data-id-type='2']").each(function () {

                        $(this).parent().button('toggle');

                    });

                });
            }
        });

        $("#savepage").click(function () {

            var allSelected= AllStatesSelected();
            if (tas.data().length > 0 && allSelected) {
                var screenmode = $("#screenmode").val();
                 saveRecords(screenmode);
            }
            else {
                var error = "Por Favor ingrese la informacion"
                displayErrorMessage(error);
            }

        });

        
    }

    function setMembersTable(responseData) {
        $("#dataTables-example").append(responseData);
        if (tas == null || tas == undefined) {

            tas = $('#dataTables-example').DataTable({
                "bPaginate": true,
                "bFilter": true,
                "bInfo": false,
                "bLengthChange": false,
                "pagingType": "numbers",
                "fnCreatedRow": function (nRow, aData, iDataIndex) {
                    //$('td:not(.select-checkbox)', nRow).append("<textarea rows='4' cols='40'></textarea>");
                    //$('td:eq(0)', nRow).append("<label >" + aData[0] + "</label>");

                },
                fnDrawCallback: function () {
                    $("#dataTables-example thead").remove();
                },
                language: {
                    searchPlaceholder: "Búsqueda",
                    "search": "",
                    "emptyTable": "No hay datos encontrados",
                    "zeroRecords": "No hay datos disponibles"
                },
                columns: [
                    { "width": "10%" },
                    { "width": "50%" },
                    null
                ]

            });

        }
        else {
            tas.clear();
            $("#dataTables-example > tbody").remove();
            tas.destroy();
            $("#dataTables-example").append(responseData);
            tas = $('#dataTables-example').DataTable({
                "bPaginate": true,
                "bFilter": true,
                "bInfo": false,
                "bLengthChange": false,
                "pagingType": "numbers",
                "fnCreatedRow": function (nRow, aData, iDataIndex) {
                    //$('td:not(.select-checkbox)', nRow).append("<textarea rows='4' cols='40'></textarea>");
                    //$('td:eq(0)', nRow).append("<label >" + aData[0] + "</label>");

                },
                language: {
                    searchPlaceholder: "Búsqueda",
                    "search": "",
                    "emptyTable": "No hay datos encontrados",
                    "zeroRecords": "No hay datos disponibles"
                },
                fnDrawCallback: function () {
                    $("#dataTables-example thead").remove();
                },
                columns: [
                    { "width": "10%" },
                    { "width": "50%" },
                    null
                ]

            });
        }

    }

    function showLoadingIndicator() {
        // $("#backgroundCover").show();
        var message = "Cargando Información..."
        $("#messagecontainer").html(message);
        $("#myMessageDialog").modal('show');
        $("#pagebtndelete").unbind();
        $("#tabdetails").unbind();
        //displayMessage(message);
    }

    function hideLoadingIndicator() {

            //$("#backgroundCover").hide();
        $('#myMessageDialog').modal('hide');
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

        $("#pagebtndelete").unbind();
        $("#tabdetails").unbind();
    }

    function getProgramsCombo() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/AttendanceTracking.ashx?method=getprograms',
            async:false,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {

                    $("#cmbprograma").html(response.ResponseData);

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

    function getActivities() {

        var dataToSend =
            {
                ID_Programa: parseInt($("#cmbprograma > option:selected").attr("data-id-programs"))
            };

        $.ajax({
            type: 'POST',
            data: dataToSend,
            beforeSend: showLoadingIndicator,
            complete: hideLoadingIndicator,
            url: '/Handlers/AttendanceTracking.ashx?method=getactivities',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {

               SetEventsToCalendar(response.ResponseData);

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

    function VerifyDay(ActivityDate) {

        var dataToSend =
            {
                ID_Programa: parseInt($("#cmbprograma > option:selected").attr("data-id-programs")),
                FechaActividad: ActivityDate
            };

        $.ajax({
            type: 'POST',
            data: dataToSend,
            url: '/Handlers/AttendanceTracking.ashx?method=exists',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {

                    if (response.ResponseData.Exists != true) {
                        $("#savepage").prop("disabled", true);
                        
                        var message = "La fecha Seleccionada no tiene Actividad Registrada"
                        displayMessage(message);
                    }
                    else
                    {
                        $("#idactividad").val(response.ResponseData.ID_Actividad);
                        $("#screenmode").val(response.ResponseData.Mode);
                        if (response.ResponseData.Mode == "add") {
                            resetToggle();
                        }
                        else if (response.ResponseData.Mode == "edit") {
                            getStatesFromActivities();
                        }
                        $("#savepage").prop("disabled", false);
                       
                    }

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

    function getProgramMembers() {

        var dataToSend =
            {
                ID_Programa: parseInt($("#cmbprograma > option:selected").attr("data-id-programs"))
            };

        $.ajax({
            type: 'POST',
            data: dataToSend,
            url: '/Handlers/AttendanceTracking.ashx?method=getmembers',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {

                    setMembersTable(response.ResponseData);

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

    function SetEventsToCalendar(dates)
    {
        $('#calendar').fullCalendar('removeEvents');
       
        for (var i = 0; i < dates.length; i++) {

            var newEvent = new Object();
            newEvent.title = "Actividad";
            newEvent.start = new Date(dates[i]);
            newEvent.start = new Date(newEvent.start.getTime() + newEvent.start.getTimezoneOffset() * 60000);
            newEvent.allDay = false;
            newEvent.id = i;
            $('#calendar').fullCalendar('renderEvent', newEvent, true);

        }

        
    }

    function saveRecords(screenmode) {

        if (screenmode === "add") {

            //var alldata = tac.rows().data();

            var ID_Programa = parseInt($("#cmbprograma > option:selected").attr("data-id-programs"));
            var ID_Actividad = parseInt($("#idactividad").val());
            var Asistencias = [];
            var contenido = [];
            if (tas.data().count() > 0) {

                tas.rows().iterator('row', function (context, index) {
                    var node = $(this.row(index).node());
                    $(node).find("td:not(:first-child)").each(function () {

                        if ($(this).is("[data-id-beneficiario]")) {
                            contenido.push(parseInt($(this).attr("data-id-beneficiario")));
                        }
                        else if ($(this).children().length > 0) {
                            $(this).find("label.btn.btn-primary.active").each(function () {
                                if ($(this).find("input").attr('data-id-type') == "1") {
                                    contenido.push("Presente");
                                }
                                else if ($(this).find("input").attr('data-id-type') == "2") {
                                    contenido.push("Ausente");
                                }
                            });
                        }


                    });
                    Asistencias.push({ "ID_Asistencia": 0, "ID_Beneficiario": contenido[0], "ID_Actividad": ID_Actividad, "Estado": contenido[1] });
                    contenido.length = 0;

                });
            }

            var dataToSend =
                {
                    ID_Programa: ID_Programa,
                    ID_Actividad: ID_Actividad,
                    Asistencias: Asistencias,
                    ToDelete: []
                };

            var data = JSON.stringify(dataToSend);

            makeAjaxCall(data, screenmode);
            //$("#tabtable").tab("show");
        }
        else if (screenmode === "edit") {

            var ID_Programa = parseInt($("#cmbprograma > option:selected").attr("data-id-programs"));
            var ID_Actividad = parseInt($("#idactividad").val());
            var Asistencias = [];
            var contenido = [];
            if (tas.data().count() > 0) {

                tas.rows().iterator('row', function (context, index) {
                    var node = $(this.row(index).node());
                    $(node).find("td:not(:first-child)").each(function () {

                        if ($(this).is("[data-id-beneficiario]")) {
                            contenido.push(parseInt($(this).attr("data-id-beneficiario")));
                        }
                        else if ($(this).children().length > 0) {
                            $(this).find("label.btn.btn-primary.active").each(function () {
                                if ($(this).find("input").attr('data-id-type') == "1") {
                                    contenido.push("Presente");
                                }
                                else if ($(this).find("input").attr('data-id-type') == "2") {
                                    contenido.push("Ausente");
                                }
                            });
                        }


                    });
                    Asistencias.push({ "ID_Asistencia": 0, "ID_Beneficiario": contenido[0], "ID_Actividad": ID_Actividad, "Estado": contenido[1] });
                    contenido.length = 0;

                });
            }

            var dataToSend =
                {
                    ID_Programa: ID_Programa,
                    ID_Actividad: ID_Actividad,
                    Asistencias: Asistencias,
                    ToDelete: []
                };

            var data = JSON.stringify(dataToSend);

            makeAjaxCall(data, screenmode);
        }
    }

    function makeAjaxCall(dataToSend, action) {
        $.ajax({
            type: 'POST',
            url: '/Handlers/AttendanceTracking.ashx?method=' + action,
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    //getPrograms();
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

    function resetToggle()
    {
        if (tas.data().count() > 0) {

            tas.rows().iterator('row', function (context, index) {
                var node = $(this.row(index).node());
                $(node).find("td > div").each(function () {

                    if ($(this).children().length > 0) {
                        $(this).find("label.btn.btn-primary.active").each(function () {
                            
                            $(this).removeClass("active");
                        });
                    }


                });
               

            });
        }
    }

    function AllStatesSelected()
    {
        var isOK = false;
        var selected = [];
        var totalRows = tas.data().length;
        if (totalRows > 0) {
            
            tas.rows().iterator('row', function (context, index) {
                var count = 0;
                var node = $(this.row(index).node());
                $(node).find("td > div").each(function () {

                    if ($(this).children().length > 0) {
                        $(this).find("label.btn.btn-primary.active").each(function () {

                            
                            selected.push(true);
                            
                            
                        });
                    }


                });

         

            });

            if (selected.length == totalRows) {
                isOK=true;
            }
        }

        return isOK;

    }

    function setAttendanceTable(responseData) {
        $("#dataTables-example").append(responseData);
        if (tas == null || tas == undefined) {

            tas = $('#dataTables-example').DataTable({
                "bPaginate": true,
                "bFilter": true,
                "bInfo": false,
                "bLengthChange": false,
                "pagingType": "numbers",
                "fnCreatedRow": function (nRow, aData, iDataIndex) {
                    //$('td:not(.select-checkbox)', nRow).append("<textarea rows='4' cols='40'></textarea>");
                    //$('td:eq(0)', nRow).append("<label >" + aData[0] + "</label>");

                },
                language: {
                    searchPlaceholder: "Búsqueda",
                    "search": "",
                    "emptyTable": "No hay datos encontrados",
                    "zeroRecords": "No hay datos disponibles"
                },
                fnDrawCallback: function () {
                    $("#dataTables-example thead").remove();
                },
                columns: [
                    { "width": "10%" },
                    { "width": "50%" },
                    null

                ]

            });

        }
        else {
            tas.clear();
            $("#dataTables-example > tbody").remove();
            tas.destroy();
            $("#dataTables-example").append(responseData);
            tas = $('#dataTables-example').DataTable({
                "bPaginate": true,
                "bFilter": true,
                "bInfo": false,
                "bLengthChange": false,
                "pagingType": "numbers",
                "fnCreatedRow": function (nRow, aData, iDataIndex) {
                    //$('td:not(.select-checkbox)', nRow).append("<textarea rows='4' cols='40'></textarea>");
                    //$('td:eq(0)', nRow).append("<label >" + aData[0] + "</label>");

                },
                language: {
                    searchPlaceholder: "Búsqueda",
                    "search": "",
                    "emptyTable": "No hay datos encontrados",
                    "zeroRecords": "No hay datos disponibles"
                },
                fnDrawCallback: function () {
                    $("#dataTables-example thead").remove();
                },
                columns: [
                    { "width": "10%" },
                    { "width": "50%" },
                    null

                ]

            });
        }

    }

    function getStatesFromActivities() {

        var dataToSend =
            {
                ID_Programa: parseInt($("#cmbprograma > option:selected").attr("data-id-programs")),
                ID_Actividad: parseInt($("#idactividad").val())
            };

        $.ajax({
            type: 'POST',
            data: dataToSend,
            url: '/Handlers/AttendanceTracking.ashx?method=states',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {

                    setAttendanceTable(response.ResponseData);

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


});