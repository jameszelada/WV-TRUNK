$(window).load(function () {
    CleanTabState();
    var t;
    getPrograms();

    // End of Execution

    /*CRUD Functions*/

    function getPrograms() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/Program.ashx?method=getall',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {

                    setStaffTable(response.ResponseData);
                    
                   // attachClickToModal();

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


    function attachSelects() {

        $.ajax({
            type: 'POST',
            url: '/Handlers/Program.ashx?method=getinfo',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    //to implement
                    loadSelects(response.ResponseData);

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

    function loadSelects(fullObject) {
        $("#cmbproyecto").html(fullObject.Proyecto).prop("disabled", false);
        $("#cmbtipoprograma").html(fullObject.TipoPrograma).prop("disabled", false);
        $("#cmbcomunidad").html(fullObject.Comunidad).prop("disabled", false);
        
    }

    function saveProgram(screenmode) {

        if (screenmode === "add") {
            var dataToSend =
                {
                    ID_Proyecto:  $("#cmbproyecto > option:selected").attr("data-id-project"),
                    ID_TipoPrograma : $("#cmbtipoprograma > option:selected").attr("data-id-programtype"),
                    ID_Comunidad: $("#cmbcomunidad > option:selected").attr("data-id-community"),

                    Codigo: $("#in_codigo").val(),
                    ProgramaDescripcion: $("#in_descripcion").val(),
                    Estado: $("#cmbestado > option:selected").attr("data-id-estado"),
                    FechaInicio: $("#datepickerinicio").datepicker('getDate').getTime(),
                    FechaFinal: $("#datepickerfinal").datepicker('getDate').getTime()
                    
                };

            makeAjaxCall(dataToSend, screenmode);
            $("#tabtable").tab("show");
            $("#cancelpage").unbind();
            $("#savepage").unbind();
            clearControls();
        }
        else if (screenmode === "edit") {
            // To implement****************
            var id = $('#pagetoedit').val();
            var dataToSend =
                {
                    ID_Programa: id,
                    ID_Proyecto: $("#cmbproyecto > option:selected").attr("data-id-project"),
                    ID_TipoPrograma: $("#cmbtipoprograma > option:selected").attr("data-id-programtype"),
                    ID_Comunidad: $("#cmbcomunidad > option:selected").attr("data-id-community"),

                    Codigo: $("#in_codigo").val(),
                    ProgramaDescripcion: $("#in_descripcion").val(),
                    Estado: $("#cmbestado > option:selected").attr("data-id-estado"),
                    FechaInicio: $("#datepickerinicio").datepicker('getDate').getTime(),
                    FechaFinal: $("#datepickerfinal").datepicker('getDate').getTime()


                };

            makeAjaxCall(dataToSend, screenmode);
            //$("#tabtable").tab("show");
            $("#tabtable").tab("show");
            $("#cancelpage").unbind();
            $("#savepage").unbind();
        }
    }

    function makeAjaxCall(dataToSend, action) {
        $.ajax({
            type: 'POST',
            url: '/Handlers/Program.ashx?method=' + action,
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




    function setStaffTable(responseData) {

        if (t == null || t == undefined) {
            $("#dataTables-example").append(responseData);
            attachSelects();
            attachClickToShowProgramButtons();
            attachClickToEditProgramButtons();
            attachClickToListButton();
            if (Security.agregar) {
                attachClickToNewButton();
            } else
            {
                $("#tabdetails").html("Detalles");
            }
            t = $('#dataTables-example').DataTable({
                "bFilter": true,
                "bPaginate": true,
                "bLengthChange": true,
                "bInfo": true,
                "pageLength": 5,
                "aLengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
                language: {
                    searchPlaceholder: "Búsqueda",
                    "search": "Buscar",
                    "emptyTable": "No hay datos encontrados",
                    "zeroRecords": "No hay datos disponibles",
                    "lengthMenu": "_MENU_ registros por página",
                    "info": "Mostrando pagina _PAGE_ de _PAGES_"

                }


            });

        }
        else {
            t.clear();
            $("#dataTables-example > tbody").remove();
            t.destroy();
            $("#dataTables-example").append(responseData);
            attachSelects();
            attachClickToShowProgramButtons();
            attachClickToEditProgramButtons();
            attachClickToListButton();
            if (Security.agregar) {
                attachClickToNewButton();
            }
            else {
                $("#tabdetails").html("Detalles");
            }
            t = $('#dataTables-example').DataTable({
                "bFilter": true,
                "bPaginate": true,
                "bLengthChange": true,
                "bInfo": true,
                "pageLength": 5,
                "aLengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
                language: {
                    searchPlaceholder: "Búsqueda",
                    "search": "Buscar",
                    "emptyTable": "No hay datos encontrados",
                    "zeroRecords": "No hay datos disponibles",
                    "lengthMenu": "_MENU_ registros por página",
                    "info": "Mostrando pagina _PAGE_ de _PAGES_"

                }


            });
        }
        //permissions
        $("#datepickerinicio").datepicker();
        $("#datepickerfinal").datepicker();
        applyOptionPermissions(t);
        validation();
    }

    function fillLblFields(program) {
        $("#lbl_proyecto").html(program.Proyecto);
        $("#lbl_comunidad").html(program.Comunidad);
        $("#lbl_tipoprograma").html(program.TipoPrograma);
        $("#lbl_codigo").html(program.Codigo);
        $("#lbl_descripcion").html(program.ProgramaDescripcion);

        var estado = "";
        if (program.Estado == "A") {
            estado = "Activo";
        }
        else if (program.Estado == "I") {
            estado = "Inactivo";
        }
        else if (program.Estado == "S") {
            estado = "Suspendido";
        }
        $("#lbl_estado").html(estado);

        $("#lbl_datepickerinicio").html(program.FechaInicio);
        $("#lbl_datepickerfinal").html(program.FechaFinal);

        $('#tabdetails').tab('show');
    }

    function fillInputFields(program) {

        $("#cmbproyecto > option").each(function (index, value) { $(value).removeAttr("selected") });
        $("#cmbtipoprograma > option").each(function (index, value) { $(value).removeAttr("selected") });
        $("#cmbcomunidad > option").each(function (index, value) { $(value).removeAttr("selected") });
        $("#cmbestado > option").each(function (index, value) { $(value).removeAttr("selected") });

        $("#in_codigo").val(program.Codigo);
        $("#in_descripcion").val(program.ProgramaDescripcion);

        var estado = "";
        if (program.Estado == "A") {
            $("#cmbestado > option[data-id-estado='A']").attr("selected", "selected");
        }
        else if (program.Estado == "I") {
            var g = $("#cmbestado > option[data-id-estado='I']").attr("selected", "selected");
        }
        else if (program.Estado == "S") {
            $("#cmbestado > option[data-id-estado='S']").attr("selected", "selected");
        }

        $("#cmbproyecto > option[data-id-project='"+program.ID_Proyecto+"']").attr("selected", "selected");
        $("#cmbtipoprograma > option[data-id-programtype='" + program.ID_TipoPrograma + "']").attr("selected", "selected");
        $("#cmbcomunidad > option[data-id-community='" + program.ID_Comunidad + "']").attr("selected", "selected");

        var date = new Date(parseInt(program.FechaInicioD.substr(6)));
        $("#datepickerinicio").datepicker("setDate", date);

        var date2 = new Date(parseInt(program.FechaFinalD.substr(6)));
        $("#datepickerfinal").datepicker("setDate", date2);


        $("#form1").data('bootstrapValidator').validate();
    }


    function showProgram(ID_Programa) {
        var idProgram = ID_Programa;
        var dataToSend =
            {
                ID_Programa: ID_Programa
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Program.ashx?method=getsingle',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {

                    
                    fillLblFields(response.ResponseData);
                    setTabInDetailsMode();
                    
                    if (!$("#sidebaroptions").length) {
                        
                        loadSidebarOptions(response.ResponseData.ID_Proyecto, parseInt(idProgram));
                        attachClickToStatisticsLink(idProgram);
                        attachClickToChartsLink(idProgram);
                    }

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

    function showProgramToEdit(ID_Programa) {
        var dataToSend =
            {
                ID_Programa: ID_Programa
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Program.ashx?method=getsingle',
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

    function attachClickToShowProgramButtons() {
        $("#table-responsive").find("[class='btn btn-primary btn-sm detail']").each(function (index, value) {
            $(value).click(function () {
              
                $('#pagetoshow').val($(this).attr("data-id-program"));
                showProgram($(this).attr("data-id-program"));
                
            });
        });
    }

    function attachClickToEditProgramButtons() {
        $("#table-responsive").find("[class='btn btn-primary btn-sm edit']").each(function (index, value) {
            $(value).click(function () {
               // window.location.href = "/WeeklyPlan?id=" + $(this).attr("data-id-person");
                $('#pagetoedit').val($(this).attr("data-id-program"));
                //var idProyecto = $('#pagetoedit').val();
                $("#tabdetails").tab("show");
                $("#screenmode").val("edit");
                setTabInEditMode();
                clearControls();
                attachActionButtons();
                showProgramToEdit($(this).attr("data-id-program"));
                validation();
            });
        });
    }

    function attachClickToNewButton() {
        $("#tabdetails").click(function () {
            validation();
            $("#screenmode").val("add");
            setTabInAddMode();
            clearControls();
            attachActionButtons();

        });
    }

    function attachClickToListButton() {
        
        $("#tabtable").click(function () {
            $("#sidebaroptions").remove();
            $("#cancelpage").unbind();
            $("#savepage").unbind();
            clearControls();
            $("#form1").data('bootstrapValidator').resetForm();
        });
    }

    function attachActionButtons() {
        $("#sidebaroptions").remove();
        $("#cancelpage").click(function () {
            $("#tabtable").tab("show");
            clearControls();
            $("#form1").data('bootstrapValidator').resetForm();

        });

        $("#savepage").click(function () {
           $("#form1").data('bootstrapValidator').validate();
            var formValidation = $("#form1").data('bootstrapValidator').isValid();

            if (formValidation) {
                var screenmode = $("#screenmode").val();
                saveProgram(screenmode);
                clearControls();
                $("#form1").data('bootstrapValidator').resetForm();
            }


        });
    }

    function attachClickToStatisticsLink(ID_Programa) {

        $("#showstatistics").click(function () {

            var dataToSend =
            {
                ID_Programa: ID_Programa
            };

            $.ajax({
                type: 'POST',
                url: '/Handlers/ProjectSummary.ashx?method=getprogramsummary',
                async:false,
                data: dataToSend,
                success: function (data) {

                    var response = JSON.parse(data);
                    if (response.IsSucess) {

                        $("#assignedcontainer").html(response.ResponseData.Staff);
                        $("#activitiescontainer").html(response.ResponseData.Activities);
                        $("#additionalinfocontainer").html(response.ResponseData.AdditionalInfo);
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

            $('#modalstatistics').modal({
                keyboard: false
            })

        });
    }

    function attachClickToChartsLink(ID_Programa) {

        $("#showcharts").click(function () {

            var dataToSend =
            {
                ID_Programa: ID_Programa
            };

            $.ajax({
                type: 'POST',
                url: '/Handlers/ProjectSummary.ashx?method=getprogramsummary',
                async: false,
                data: dataToSend,
                success: function (data) {

                    var options = { 
                        responsive: true,
                        maintainAspectRatio: true
                    }

                    var response = JSON.parse(data);
                    if (response.IsSucess) {
                        var xValues1 = Object.getOwnPropertyNames(response.ResponseData.Chart1);
                        var yValues1 = $.map(response.ResponseData.Chart1, function (val, key) { return val; });
                        var xValues2 = Object.getOwnPropertyNames(response.ResponseData.Chart2);
                        var yValues2 = $.map(response.ResponseData.Chart2, function (val, key) { return val; });

                        var dataPie = {
                            labels: xValues1,
                            datasets: [
                                {
                                    data: yValues1,
                                    backgroundColor: [
                                        "#FF6384",
                                        "#36A2EB"
                                    ],
                                    hoverBackgroundColor: [
                                        "#FF6384",
                                        "#36A2EB"
                                    ]
                                }]
                        };

                        var context = $("#chart1container");
                        context.get(0).getContext('2d');
                        var Chart1 = new Chart(context, {
                            type: "pie",
                            data: dataPie,
                            options: options
                        });

                        var dataDoughnut = {
                            labels: xValues2,
                            datasets: [
                                {
                                    data: yValues2,
                                    backgroundColor: [
                                        "#FF6384",
                                        "#36A2EB",
                                        "#FFCE56"
                                    ],
                                    hoverBackgroundColor: [
                                        "#FF6384",
                                        "#36A2EB",
                                        "#FFCE56"

                                    ]
                                }]
                        };

                        var context2 = $("#chart2container");
                        context2.get(0).getContext('2d');
                        var Chart1 = new Chart(context2, {
                            type: "doughnut",
                            data: dataDoughnut,
                            options: options
                        });

                      
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

            $('#modalcharts').modal({
                keyboard: false
            })

        });
    }


    function clearControls() {
        $(".in-controls").find("input").each(function (index, value) {
            $(value).val("");
        });
        $("#in_descripcion").val("");
        $("#cancelpage").unbind();
        $("#savepage").unbind();
    }

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

    function validation() {
        $('#form1').bootstrapValidator({
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            excluded: [':disabled'],
            trigger: 'change keyup',
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
                    }
                    
                },
                in_descripcion: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        },
                        stringLength: {
                            message: 'Mínimo 15 caracteres, Máximo 1000',
                            min: 15,
                            max:1000
                        }
                    }

                },
                datepickerfinal: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        }
                    }

                },
                datepickerinicio: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        }
                    }

                }
               
                
            }

        }).on('bv-form', function () {
            $(this).validate();
        });
    }

    /*Message Functions*/

    //function displayErrorMessage(message) {
    //    $("#errorcontainer").css({
    //        'position': 'absolute',
    //        'zIndex': '0',
    //        'right': '30%'

    //    }).html(message).toggleClass("hidden").fadeToggle(2000, "linear", function () { $("#errorcontainer").toggleClass("hidden").empty(); });
    //}

    //function displayMessage(message) {
    //    $("#messagecontainer").css({
    //        'position': 'absolute',
    //        'zIndex': '0',
    //        'right': '30%'

    //    }).html(message).toggleClass("hidden").fadeToggle(2000, "linear", function () { $("#messagecontainer").toggleClass("hidden").empty(); });
    //    $("#pagebtndelete").unbind();
    //    $("#tabdetails").unbind();

    //}

    function displayErrorMessage(message) {
        //message += "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>";
        //$("#errorcontainer").css({
        //    'position': 'absolute',
        //    'zIndex': '0',
        //    'right': '30%'

        //}).html(message).toggleClass("hidden").fadeToggle(8000, "linear", function () { $("#errorcontainer").toggleClass("hidden").empty(); });
        $("#errorcontainer").html(message);
        $("#myErrorDialog").modal('show');
    }

    function displayMessage(message) {
        // message += "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>";
        //$("#messagecontainer").css({
        //    'position': 'absolute',
        //    'zIndex': '0',
        //    'right': '30%'

        //}).html(message).toggleClass("hidden").fadeToggle(8000,"linear" , function () { $("#messagecontainer").toggleClass("hidden").empty(); });
        //$("#pagebtndelete").unbind();
        //$("#tabdetails").unbind();

        $("#messagecontainer").html(message);
        $("#myMessageDialog").modal('show');
        $("#pagebtndelete").unbind();
        $("#tabdetails").unbind();
    }

    function loadSidebarOptions(ID_Project,ID_Program) {
        var htmlToAppend = "<div class='col-md-2 col-sm-2'></div><div id='sidebaroptions' class='col-md-4 col-sm-4'><div class='activity_box activity_box2'><h3 style='color:#999'>Accesos rápidos</h3><div class='scrollbar' id='style-2'> <div class='activity-row activity-row1'><div class='single-bottom'><ul><li><a id='showuserreport' href='/Handlers/GeneralReportsProject.ashx?method=getassignreport&ID_Proyecto=" + ID_Project + "'> Reporte de Asignación de Personal</a></li><li><a id='showasignreport' href='/Handlers/GeneralReportsProgram.ashx?method=getprogramreport&ID_Programa=" + ID_Program + "'> Reporte General de Programa</a></li><li><a id='showstatistics' href='#'>Estadísticas</a></li><li><a id='showcharts' href='#'>Gráficos</a></li></ul></div></div></div></div></div>";
        $(htmlToAppend).insertAfter("div[class='col-md-6 col-sm-6']");
    }

    function applyOptionPermissions(table) {
        if (!Security.editar) {
            table.column(5).visible(false);
        }
        //if (!Security.eliminar) {
        //    table.column(6).visible(false);
        //}
    }

    function CleanTabState() {
        $('#tabtable').on('shown.bs.tab', function (e) {
            setTabInDetailsMode();
        });
    }

});