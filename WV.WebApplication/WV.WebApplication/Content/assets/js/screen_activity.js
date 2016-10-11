$(window).load(function () {
    CleanTabState();
    var tac;
    var tpr;
    getPrograms();
    InitializeControls();

    function getPrograms() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/Activity.ashx?method=getall',
            async: false,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    setProgramsTable(response.ResponseData);
                    attachClickToView();
                    attachClickToEdit();
                    attachClickToListButton();
                    if (Security.agregar) {
                        attachClickToNewButton();
                    }
                    else
                    {
                        $("#tabdetails").html("Detalles");
                    }
                    attachClickToAddRecord();

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

    function setProgramsTable(responseData) {
        $("#dataTables-example").append(responseData);
        if (tpr == null || tpr == undefined) {
           
            tpr = $('#dataTables-example').DataTable({
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
                columns: [
                    { "width": "5%" },
                    { "width": "50%" },
                    { "width": "15%" },
                    { "width": "15%" },
                    { "width": "15%" }

                ]

            });

        }
        else {
            tpr.clear();
            $("#dataTables-example > tbody").remove();
            tpr.destroy();
            $("#dataTables-example").append(responseData);
            tpr = $('#dataTables-example').DataTable({
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
                columns: [
                    { "width": "5%" },
                    { "width": "50%" },
                    { "width": "15%" },
                    { "width": "15%" },
                    { "width": "15%" }

                ]

            });
        }
        //Permissions
        applyOptionPermissions(tpr);
    }

    function attachClickToView() {
        $("#table-responsive").find("[class='btn btn-primary btn-sm details']").each(function (index, value) {
            $(value).click(function () {
               
                showActivitiesInProgram($(this).attr("data-id-program"));
               
               
            });
        });
    }

    function attachClickToEdit() {
        $("#table-responsive").find("[class='btn btn-primary btn-sm edit']").each(function (index, value) {
            $(value).click(function () {
              
                $('#pagetoedit').val($(this).attr("data-id-program"));
                $("#tabdetails").tab("show");
                $("#screenmode").val("edit");
                setTabInEditMode();
                showActivitiesInProgramToEdit($(this).attr("data-id-program"));

            });
        });
    }

    function InitializeControls()
    {
        var calendar=$('#calendar').fullCalendar({
            lang: 'es',
            editable: true,
            selectable: true,
            eventLimit: true,
            dayClick: function (date, jsEvent, view) {
                    
                $("#in_fecha").val(date.format("DD/MM/YYYY"));
                $('#modalmessage').modal('show');
                $('#in_fecha').attr("data-fecha",date._d.getTime());
            },
            header: {
                left: 'prev,next',
                center: 'title',
                right: 'today'
            }
        });

        $(document).on('shown.bs.tab','a[data-toggle="tab"]', function (e) {
            $('#calendar').fullCalendar('render');
           
            if (tac.data().count() > 0)
            {
                $('#calendar').fullCalendar('removeEvents');
                tac.rows().iterator('row', function (context, index) {
                    var node = $(this.row(index).node());
                    $(node).find("td:not(:first-child)").each(function () {
                        if ($(this).children().length > 0) {
                           
                            var newEvent = new Object();
                            newEvent.title = "Actividad";
                            newEvent.start = new Date(parseInt($(this).find('label').html()));
                            newEvent.start = new Date(newEvent.start.getTime() +newEvent.start.getTimezoneOffset() * 60000);
                            newEvent.allDay = false;
                            newEvent.id = parseInt($(this).find('label').html());
                            $('#calendar').fullCalendar('renderEvent', newEvent,true);
                        }
                        

                    });

                });
            }

            

        });


        

        validation();

        //Temporarily
        $("#savepage").click(function () {
            
            if (tac.data().length > 0) {
                var screenmode = $("#screenmode").val();
                saveActivities(screenmode);
            }
            else {
                var error= "Por Favor ingrese la informacion"
                displayErrorMessage(error);
            }
                
        });

        $("#cancelpage").click(function () {
            $("#tabtable").tab("show");
            clearControls();

        });

        $('#deleterow').click(function () {
            var val = tac.row('.selected').node();
            var td = $(val).find("td label").text();
            $('#calendar').fullCalendar('removeEvents',parseInt($(val).find("td label").text()));
            tac.row('.selected').remove().draw(false);
            
        });

        
    }

    function displayErrorMessage(message) {
  
        $("#errorcontainer").html(message);
        $("#myErrorDialog").modal('show');
    }

    function displayMessage(message) {
      

        $("#messagecontainer").html(message);
        $("#myMessageDialog").modal('show');
        $("#pagebtndelete").unbind();
        $("#tabdetails").unbind();
    }

    function attachClickToNewButton() {

       

        $("#tabdetails").click(function ()
        {

            $('#calendar').fullCalendar('removeEvents');

            setTimeout(function () {
                $('#calendar').fullCalendar('removeEvents');
            }, 1000);
           
            getProgramsCombo();

            $("#screenmode").val("add");
            setTabInAddMode();
            if (tac == null || tac == undefined) {
                tac = $('#actividades').DataTable({
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
                    order: [[1, 'asc']],
                    columns: [
                        { "width": "10%" },
                        { "width": "15%" },
                        { "width": "15%" },
                        { "width": "15%" },
                        { "width": "15%" },
                        { "width": "30%" }
                    ]

                });

            } else {
                tac.clear();
                $("#actividades > tbody").remove();
                tac.destroy();
                tac = $('#actividades').DataTable({
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
                    order: [[1, 'asc']],
                    columns: [
                        { "width": "10%" },
                        { "width": "15%" },
                        { "width": "15%" },
                        { "width": "15%" },
                        { "width": "15%" },
                        { "width": "30%" }
                    ]

                });

            }
           
            //attachActionButtons();

        });
    }

    function attachClickToListButton() {
        $("#tabtable").click(function () {
            //$("#cancelpage").unbind();
            //$("#savepage").unbind();
            clearControls();
           // $("#form1").data('bootstrapValidator').resetForm();
        });
    }

    function clearControls() {
        $(".in-controls").find("input").each(function (index, value) {
            $(value).val("");
        });
        $("#cancelpage").unbind();
        $("#savepage").unbind();
    }

    /*Mode Functions*/

    function setTabInAddMode() {

        $(".txt-controls").each(function (index, value) {
            if (!$(value).hasClass("hidden")) {

                $(value).addClass("hidden");
            }
        });

        $(".in-controls").each(function (index, value) {
            if ($(value).hasClass("hidden")) {

                $(value).removeClass("hidden");
            }
        });


    }

    function setTabInEditMode() {
        $(".txt-controls").each(function (index, value) {
            if (!$(value).hasClass("hidden")) {

                $(value).addClass("hidden");
            }
        });

        $(".in-controls").each(function (index, value) {
            if ($(value).hasClass("hidden")) {

                $(value).removeClass("hidden");
            }
        });
    }

    function setTabInDetailsMode() {
        $(".txt-controls").each(function (index, value) {
            if ($(value).hasClass("hidden")) {

                $(value).removeClass("hidden");
            }
        });

        $(".in-controls").each(function (index, value) {
            if (!$(value).hasClass("hidden")) {

                $(value).addClass("hidden");
            }
        });
    }

    function showActivitiesInProgram(ID_Program) {
        var dataToSend =
            {
                ID_Programa: ID_Program
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Activity.ashx?method=getsingle',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    //to implement
                    fillLblFields(response.ResponseData);
                    setTabInDetailsMode();
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

    function showActivitiesInProgramToEdit(ID_Program) {
        var dataToSend =
            {
                ID_Programa: ID_Program
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Activity.ashx?method=getsingle',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    //to implement
                    fillInputFields(response.ResponseData);

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

    function fillInputFields(programa) {
       
        $("#cmbprograma").html(programa.ComboPrograma);
        //$("#cmbprograma").prop("disabled", true); hay que hacerlo mas pequeño
        if (tac == null || tac == undefined) {
            $("#actividades").append(programa.Actividades);
            tac = $('#actividades').DataTable({
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
                order: [[1, 'asc']],
                columns: [
                    { "width": "10%" },
                    { "width": "15%" },
                    { "width": "15%" },
                    { "width": "15%" },
                    { "width": "15%" },
                    { "width": "30%" }
                ]

            });

        } else {
            tac.clear();
            $("#actividades > tbody").remove();
            tac.destroy();
            $("#actividades").append(programa.Actividades);
            tac = $('#actividades').DataTable({
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
                order: [[1, 'asc']],
                columns: [
                    { "width": "10%" },
                    { "width": "15%" },
                    { "width": "15%" },
                    { "width": "15%" },
                    { "width": "15%" },
                    { "width": "30%" }
                ]

            });

        }
       
    }

    function fillLblFields(programa) {
        $("#lbl_programa").html(programa.Programa);
       

        if (tac == null || tac == undefined)
        {
            $("#actividades").append(programa.Actividades);
            tac=$('#actividades').DataTable({
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
                order: [[1, 'asc']],
                columns: [
                    { "width": "10%" },
                    { "width": "15%" },
                    { "width": "15%" },
                    { "width": "15%" },
                    { "width": "15%" },
                    { "width": "30%" }
                ]

            });

        } else {
            tac.clear();
            $("#actividades > tbody").remove();
            tac.destroy();
            $("#actividades").append(programa.Actividades);
            tac = $('#actividades').DataTable({
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
                order: [[1, 'asc']],
                columns: [
                    { "width": "10%" },
                    { "width": "15%" },
                    { "width": "15%" },
                    { "width": "15%" },
                    { "width": "15%" },
                    { "width": "30%" }
                ]

            });

        }

       

        $('#tabdetails').tab('show');
    }

    function getProgramsCombo() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/Activity.ashx?method=getprograms',
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

    function attachClickToAddRecord()
    {
        $("#addRecord").click(function () {

            var formValidation = $("#form1").data('bootstrapValidator').isValid();

            if (formValidation ) {

                var exists=dateExists($("#in_fecha").attr("data-fecha"));
                if (exists) {
                    $('#modalmessage').modal('hide');
                    var error = "-El Registro de Actividad ya existe." + "</br></br>" +
                    "-Para EDITAR un registro existente Remuevalo de " +
                    "la tabla y luego REDEFINA la informacion.";
                        
                    displayMessage(error);
                    $("#form1").data('bootstrapValidator').resetForm();

                }
                else
                {
                    tac.row.add(['', $("#in_codigo").val(), $("#in_descripcion").val(), $("#in_estado").val(), "<label class='hidden'>" + $("#in_fecha").attr("data-fecha") + "</label>" + $("#in_fecha").val(), $("#in_observacion").val()]).draw(false);
                    $("#form1").data('bootstrapValidator').resetForm();
                    clearControls();
                    $('#modalmessage').modal('hide');
                    var newEvent = new Object();
                    newEvent.title = "Actividad";
                    newEvent.start = new Date(parseInt($("#in_fecha").attr("data-fecha")));
                    newEvent.start = new Date(newEvent.start.getTime() + newEvent.start.getTimezoneOffset() * 60000);
                    newEvent.allDay = false;
                    newEvent.id = parseInt($("#in_fecha").attr("data-fecha"));
                    $('#calendar').fullCalendar('renderEvent', newEvent, true);
                }

                
            }

            


        });
    }

    function dateExists(date)
    {
        var exists = false;
        tac.rows().iterator('row', function (context, index) {
            var node = $(this.row(index).node());
            $(node).find("td label").each(function () {
               
                if ($(this).text() == date) {

                    exists = true;
                }

            });
           
        });

        return exists;
    }

    function validation() {
        $('#form1').bootstrapValidator({
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            excluded: [':disabled'],
            fields: {
                in_codigo: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        },
                        stringLength: {
                            message: 'Mínimo 4 caracteres, Máximo 20 ',
                            max: 20,
                            min: 4
                        },
                        regexp: {
                            regexp: /^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ-\s]+$/i,
                            message: 'Solo caracteres alfanuméricos'
                        }
                    },

                },
                in_descripcion: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        },
                        stringLength: {
                            message: 'Mínimo 15 caracteres, Máximo 1000',
                            min: 15,
                            max: 1000
                        }
                    }

                }
              
            }

        }).on('bv-form', function () {
            $(this).validate();
        });
    }

    function clearControls() {
        $(".in-controls").find("input[type='text']").each(function (index, value) {
            $(value).val("").removeAttr('checked').removeAttr('selected').removeAttr('disabled');
        });

        $("#in_codigo").val("");
        $("#in_descripcion").val("");
        $("#in_observacion").val("");

        if ($("#screenmode").val() == "add") {
            $("p[class='form-control-static']").each(function (index, value) {
                $(value).html("");

            });
        }
        else {
            $("p[class='form-control-static']").each(function (index, value) {
                $(value).html("");

            });
        }


        //$("#screenmode").val("");
        //$("#pagetoedit").val("");



    }

    function saveActivities(screenmode) {

        if (screenmode === "add") {

            var alldata = tac.rows().data();

            var ID_Programa =parseInt($("#cmbprograma > option:selected").attr("data-id-programs"));
            var Actividades = [];
            var contenido = [];
            if (tac.data().count() > 0) {

                tac.rows().iterator('row', function (context, index) {
                    var node = $(this.row(index).node());
                    $(node).find("td:not(:first-child)").each(function () {
                        if ($(this).children().length > 0) {
                            contenido.push($(this).find('label').html());

                        }
                        else {
                            contenido.push($(this).html());
                        }

                    });
                    Actividades.push({ "ID_Actividad": 0, "Codigo": contenido[0], "Descripcion": contenido[1], "Estado": contenido[2], "Fecha": contenido[3], "Observacion": contenido[4], "ID_Programa": ID_Programa });
                    contenido.length = 0;

                });
            }

            var dataToSend =
                {
                    ID_Programa: ID_Programa,
                    Actividades: Actividades,
                    ToDelete: []
                };

            var data = JSON.stringify(dataToSend);

           makeAjaxCall(data, screenmode);
            $("#tabtable").tab("show");
        }
        else if (screenmode === "edit") {
            
            var ID_Programa = parseInt($("#cmbprograma > option:selected").attr("data-id-programs"));
            var Actividades = [];
            var contenido = [];
            if (tac.data().count() > 0) {
               
                tac.rows().iterator('row', function (context, index) {
                    var node = $(this.row(index).node());
                        $(node).find("td:not(:first-child)").each(function () {
                            if ($(this).children().length > 0) {
                                contenido.push($(this).find('label').html());

                            }
                            else {
                                contenido.push($(this).html());
                            }

                        });
                        Actividades.push({ "ID_Actividad": 0, "Codigo": contenido[0], "Descripcion": contenido[1], "Estado": contenido[2], "Fecha": contenido[3], "Observacion": contenido[4], "ID_Programa": ID_Programa });
                        contenido.length = 0;
                    
                });
            }

            var dataToSend =
                {
                    ID_Programa: ID_Programa,
                    Actividades: Actividades,
                    ToDelete: []
                };

            var data = JSON.stringify(dataToSend);

            makeAjaxCall(data, screenmode);
            $("#tabtable").tab("show");
        }
    }

    function makeAjaxCall(dataToSend, action) {
        $.ajax({
            type: 'POST',
            url: '/Handlers/Activity.ashx?method=' + action,
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getPrograms();
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

    function applyOptionPermissions(table) {
        if (!Security.editar) {
            table.column(4).visible(false);
        }
        //if (!Security.eliminar) {
        //    table.column(6).visible(false);
        //}
    }

    function CleanTabState() {
        $('#tabtable').on('shown.bs.tab', function (e) {
            setTabInDetailsMode();
            $('#calendar').fullCalendar('removeEvents');
        });
    }
});