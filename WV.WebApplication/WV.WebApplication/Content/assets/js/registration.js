$(window).load(function () {

    var t;
    applyOptionPermissions();
    getAll();
    getPrograms();
    initialization();

    function initialization()
    {

        $("#ins_curva").datepicker();
        $("#ins_vacuna").datepicker();
        validation();
        attachClickToAdd();
        attachClickToCancel();
        attachClickToSave();
        attachClickToEdit();
        limitEntry();

        
    }

    function clearControls() {
        $(".in-controls").find("input[type='text']").each(function (index, value) {
            $(value).val("").removeAttr('checked').removeAttr('selected').removeAttr('disabled');
        });

        $(".in-controls").find("input[type='radio']").each(function (index, value) {
            $(value).removeAttr('checked').removeAttr('selected');
        });

        if ( $("#screenmode").val()=="add") {
            $("p[class='form-control-static']").each(function (index, value) {
                $(value).html("");

            });
        }
        else
        {
            $("p[class='form-control-static']").each(function (index, value) {
                $(value).html("");

            });
        }


        $("#screenmode").val("");
        $("#pagetoedit").val("");
        


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
            fields: {
                inp_nombre: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        },
                        stringLength: {
                            message: 'Mínimo 4 caracteres, Máximo 30',
                            max: 30,
                            min: 4
                        },
                        regexp: {
                            regexp: /^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$/i,
                            message: 'Solo caracteres alfabéticos'
                        }
                    },

                },
                inp_apellido: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        },
                        stringLength: {
                            message: 'Mínimo 4 caracteres, Máximo 50',
                            max: 50,
                            min: 4
                        },
                        regexp: {
                            regexp: /^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$/i,
                            message: 'Solo caracteres alfabéticos'
                        }
                    }

                },
                inp_direccion: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        },
                        stringLength: {
                            message: 'Mínimo 4 caracteres, Máximo 100',
                            max: 100,
                            min: 4
                        }
                    }

                },
                inp_dui: {
                    validators: {
                        regexp: {
                            regexp: /^\d{8}-\d$/i,
                            message: 'Ingrese el campo en el formato correcto'
                        }
                    }

                },
                inp_codigo: {
                    validators: {
      
                        regexp: {
                            regexp: /^\d{5}$/i,
                            message: 'Ingrese el campo en el formato correcto'
                        }
                    }

                },
                optionpatrocinio: {
                    validators: {
                        notEmpty: {
                            message: 'Por Favor seleccione alguna de las opciones'
                        }
                    }

                },
                optradiogenero: {
                    validators: {
                        notEmpty: {
                            message: 'Por Favor seleccione alguna de las opciones'
                        }
                    }

                },
                //optradioregistro: {
                //    validators: {
                //        notEmpty: {
                //            message: 'Por Favor seleccione alguna de las opciones'
                //        }
                //    }^\d{4}-\d{4}$

                //},
                //optradioreconoce: {
                //    validators: {
                //        notEmpty: {
                //            message: 'Por Favor seleccione alguna de las opciones'
                //        }
                //    }

                //},
                //radioestadosalud: {
                //    validators: {
                //        notEmpty: {
                //            message: 'Por Favor seleccione alguna de las opciones'
                //        }
                //    }

                //},
                //optradiotarjeta: {
                //    validators: {
                //        notEmpty: {
                //            message: 'Por Favor seleccione alguna de las opciones'
                //        }
                //    }

                //},
                //optradioestudia: {
                //    validators: {
                //        notEmpty: {
                //            message: 'Por Favor seleccione alguna de las opciones'
                //        }
                //    }

                //},
                
                datepickerinicio: {
                    validators: {
                        notEmpty: {
                            message: 'campo es requerido.'
                        }
                    }

                }


            }

        }).on('bv-form', function () {
            $(this).validate();
        });
    }

    function showRecord(ID_Beneficiario) {
        var dataToSend =
            {
                ID_Beneficiario: ID_Beneficiario
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Registration.ashx?method=getsingle',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                   
                    fillLabelFields(response.ResponseData);
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

    function showRecordToEdit(ID_Beneficiario) {
        var dataToSend =
            {
                ID_Beneficiario: ID_Beneficiario
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Registration.ashx?method=getsingle',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    //to implement
                    //fillLblFields(response.ResponseData);
                    //setTabInDetailsMode();
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

    function getPrograms() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/Registration.ashx?method=getprograms',
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

    function getAll() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/Registration.ashx?method=getall',
            async: false,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    if (t == null || t == undefined) {
                        $("#example").append(response.ResponseData);
                        attachClickToRecords();
                        //validation();

                        t = $('#example').DataTable({
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
                                $("#example thead").remove();
                            }, language: {
                                searchPlaceholder: "Búsqueda",
                                "search": "",
                                "emptyTable": "No hay datos encontrados",
                                "zeroRecords": "No hay datos disponibles"
                            }

                        });
                    }
                    else {
                        t.clear();
                        $("#example > tbody").remove();
                        t.destroy();
                        $("#example").append(response.ResponseData);
                        attachClickToRecords();
                        t = $('#example').DataTable({
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
                                $("#example thead").remove();
                            }, language: {
                                searchPlaceholder: "Búsqueda",
                                "search": "",
                                "emptyTable": "No hay datos encontrados",
                                "zeroRecords": "No hay datos disponibles"
                            }

                        });
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
        $("#example_paginate").parent().attr('class', 'col-sm-12').prev().remove();
        $("#example_filter").parent().prev().attr('class', 'col-sm-3');
    }


    /*Message Functions*/

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
    }

    function attachClickToAdd() {
        $("#nuevobeneficiario").click(function () {

            setTabInAddMode();
            $("#screenmode").val("add");
            $('.nav-tabs li:eq(0) a').tab('show');

            if (!$('#nuevobeneficiario').is('[disabled=disabled]') && !$('#editarbeneficiario').is('[disabled=disabled]')) {
                $('#nuevobeneficiario').prop("disabled", true);
                $('#editarbeneficiario').prop("disabled", true);
            }

        });
    }

    function attachClickToEdit() {
        $("#editarbeneficiario").click(function () {
            if ($('#pagetoedit').val() != "") {
                showRecordToEdit($('#pagetoedit').val());
                setTabInEditMode();
                $("#screenmode").val("edit");
                $('.nav-tabs li:eq(0) a').tab('show');
                validation();
                if (!$('#nuevobeneficiario').is('[disabled=disabled]') && !$('#editarbeneficiario').is('[disabled=disabled]')) {
                    $('#nuevobeneficiario').prop("disabled", true);
                    $('#editarbeneficiario').prop("disabled", true);
                }
            } else
            {
                displayMessage("Por Favor seleccione un registro");
            }
            
        });
    }
    

    function attachClickToCancel() {
        $("#cancelpage").click(function () {
            setTabInDetailsMode();
            clearControls();
            $('.nav-tabs li:eq(0) a').tab('show');
            $("#form1").data('bootstrapValidator').resetForm();
            $('#nuevobeneficiario').prop("disabled", false);
            $('#editarbeneficiario').prop("disabled", false);
        });
       
    }

    function attachClickToSave() {
        $("#savepage").click(function () {
            $("#form1").data('bootstrapValidator').validate();
            var formValidation = $("#form1").data('bootstrapValidator').isValid();

            if (formValidation) {

                var screenmode = $("#screenmode").val();
                saveRecord(screenmode);
                $("#form1").data('bootstrapValidator').resetForm();
            }
            $('#nuevobeneficiario').prop("disabled", false);
            $('#editarbeneficiario').prop("disabled", false);
        });

       

    }

    function attachClickToRecords() {
        $("#example").find("tr").each(function (index, value) {
            $(value).click(function () {

                $('#pagetoedit').val($(this).attr("data-id-beneficiario"));
                $('#pagetodelete').val($(this).attr("data-id-beneficiario"));
                showRecord($(this).attr("data-id-beneficiario"));
                //alert($(this).attr("data-id-beneficiario"));

            });
        });
    }


    function limitEntry()
    {
        $("input[name='optradioiglesia']").on('change', function () {
           $("input[name='optradioiglesia']:checked").val() == "no" ? $("#inc_iglesia").prop("disabled", true).val("") : $("#inc_iglesia").prop("disabled", false).focus();
        });
        
        $("input[name='optradiodiscapacidad']").on('change', function () {
            $("input[name='optradiodiscapacidad']:checked").val() == "no" ? $("#ins_discapacidad").prop("disabled", true).val("") : $("#ins_discapacidad").prop("disabled", false).focus();
        });

        $("input[name='optradiopadece']").on('change', function () {
            $("input[name='optradiopadece']:checked").val() == "no" ? $("#ins_enfermedad").prop("disabled", true).val("") : $("#ins_enfermedad").prop("disabled", false).focus();
        });

        $("input[name='optradiovacunas']").on('change', function () {
            $("input[name='optradiovacunas']:checked").val() == "no" ? $("#ins_vacuna").prop("disabled", true).val("") : $("#ins_vacuna").prop("disabled", false).focus();
        });

        $("input[name='optradiocurvacrecimiento']").on('change', function () {
            $("input[name='optradiocurvacrecimiento']:checked").val() == "no" ? $("#ins_curva").prop("disabled", true).val("") : $("#ins_curva").prop("disabled", false).focus();
        });

        $("input[name='optradioestudia']").on('change', function () {
            $("input[name='optradioestudia']:checked").val() == "no" ? $("#cmbgrado").prop("disabled", true) && $("#ine_motivo").prop("disabled", false).focus() : $("#cmbgrado").prop("disabled", false).focus() && $("#ine_motivo").prop("disabled", true).val("");
        });

        $("input[name='optionpatrocinio']").on('change', function () {
            $("input[name='optionpatrocinio']:checked").val() == "NRC" ? $("#inp_codigo").prop("disabled", true).val("") : $("#inp_codigo").prop("disabled", false).focus();
        });
        
    }

    function fillInputFields(beneficiario)
    {
       
        $("#cmbprograma > option").each(function (index, value) { $(value).removeAttr("selected") });
        $("#cmbanio > option").each(function (index, value) { $(value).removeAttr("selected") });
        $("#cmbgrado > option").each(function (index, value) { $(value).removeAttr("selected") });
        $("#cmbultimogrado > option").each(function (index, value) { $(value).removeAttr("selected") });
        $("#cmbultimoaño > option").each(function (index, value) { $(value).removeAttr("selected") });
        
        $("#cmbprograma > option[data-id-program='" + beneficiario.ID_Program + "']").attr("selected", "selected");
        $("#inp_nombre").val(beneficiario.Nombre);
        $("#inp_apellido").val(beneficiario.Apellido);
        beneficiario.Codigo == "N/A" ? $("input[value='NRC']").prop('checked', true) && $("#inp_codigo").prop("disabled", true) : $("input[value='RC']").prop('checked', true) && $("#inp_codigo").val(beneficiario.Codigo).prop("disabled", false);
        
        var edad = beneficiario.Edad.split("|");

        if (edad[0] == 0) 
        {
            $("#cmbanio > option:first").prop('selected', true);
        }
        else
        {
            $("#cmbanio > option").each(function (index, value) {

                if ($(value).html() == edad[0]) {
                    $(value).prop('selected', true);
                    return false;

                }

            });
        }

        if (edad[1] == 0) {
            $("#cmbmeses > option:first").prop('selected', true);
        }
        else {
            $("#cmbmeses > option").each(function (index, value) {

                if ($(value).html() == edad[1]) {
                    $(value).prop('selected', true);
                    return false;

                }

            });
        }

        beneficiario.Sexo == "Masculino" ? $("input[value='Masculino']").prop('checked', true) : $("input[value='Femenino']").prop('checked', true);
        $("#inp_dui").val(beneficiario.Dui);
        $("#inp_direccion").val(beneficiario.Direccion);

        //Adicional
        
        if (beneficiario.BeneficiarioAdicional !== null) {
            beneficiario.BeneficiarioAdicional.NombreEmergencia == "N/A" ? $("#ina_emergenciacontacto").val("") : $("#ina_emergenciacontacto").val(beneficiario.BeneficiarioAdicional.NombreEmergencia);
            beneficiario.BeneficiarioAdicional.NumeroEmergencia == "N/A" ? $("#ina_emergencianumero").val("") : $("#ina_emergencianumero").val(beneficiario.BeneficiarioAdicional.NumeroEmergencia);
            beneficiario.BeneficiarioAdicional.TieneRegistroNacimiento === false ? $("input[value='no'][name='optradioregistro']").prop('checked', true) : $("input[value='si'][name='optradioregistro']").prop('checked', true);

        }
        if (beneficiario.BeneficiarioCompromiso !== null) {
            //Compromiso
            beneficiario.BeneficiarioCompromiso.AceptaCompromiso === false ? $("input[value='no'][name='optradioreconoce']").prop('checked', true) : $("input[value='si'][name='optradioreconoce']").prop('checked', true);
            beneficiario.BeneficiarioCompromiso.ExistioProblema === false ? $("input[value='no'][name='optradioproblema']").prop('checked', true) : $("input[value='si'][name='optradioproblema']").prop('checked', true);
            beneficiario.BeneficiarioCompromiso.SeCongrega === false ? $("input[value='no'][name='optradioiglesia']").prop('checked', true) && $("#inc_iglesia").prop("disabled", true) : $("input[value='si'][name='optradioiglesia']").prop('checked', true) && $("#inc_iglesia").prop("disabled", false);
            beneficiario.BeneficiarioCompromiso.Comentario == "N/A" ? $("#inc_comentario").val("") : $("#inc_comentario").val(beneficiario.BeneficiarioCompromiso.Comentario);
            beneficiario.BeneficiarioCompromiso.NombreIglesia == "N/A" ? $("#inc_iglesia").val("") : $("#inc_iglesia").val(beneficiario.BeneficiarioCompromiso.NombreIglesia);
        }
        if (beneficiario.BeneficiarioSalud !== null) {
            //Salud
            beneficiario.BeneficiarioSalud.EstadoSalud == "No Satisfactoria" ? $("input[value='No Satisfactoria'][name='radioestadosalud']").prop('checked', true) : $("input[value='Satisfactoria'][name='radioestadosalud']").prop('checked', true);
            beneficiario.BeneficiarioSalud.TieneTarjeta = false ? $("input[value='no'][name='optradiotarjeta']").prop('checked', true) : $("input[value='si'][name='optradiotarjeta']").prop('checked', true);
            beneficiario.BeneficiarioSalud.Enfermedad == "N/A" ? $("input[value='no'][name='optradiopadece']").prop('checked', true) && $("#ins_enfermedad").prop("disabled", true) : $("input[value='si'][name='optradiopadece']").prop('checked', true) && $("#ins_enfermedad").val(beneficiario.BeneficiarioSalud.Enfermedad);
            beneficiario.BeneficiarioSalud.Discapacidad == "N/A" ? $("input[value='no'][name='optradiodiscapacidad']").prop('checked', true) && $("#ins_discapacidad").prop("disabled", true) : $("input[value='si'][name='optradiodiscapacidad']").prop('checked', true) && $("#ins_discapacidad").val(beneficiario.BeneficiarioSalud.Discapacidad);

            var date = new Date(parseInt(beneficiario.BeneficiarioSalud.FechaCurvaCrecimiento.substr(6)));
            var anio = date.getFullYear();
            var dia = date.getDate();
            var mes = date.getMonth() + 1;
            var fechaCompleta = dia + "-" + mes + "-" + anio;

            if (fechaCompleta == "1-1-1900") {
                $("input[value='no'][name='optradiocurvacrecimiento']").prop('checked', true);
                $("#ins_curva").prop("disabled", true);
            }
            else {
                $("input[value='si'][name='optradiocurvacrecimiento']").prop('checked', true);
                $("#ins_curva").datepicker("setDate", date);
            }

            date = new Date(parseInt(beneficiario.BeneficiarioSalud.FechaInmunizacion.substr(6)));
            anio = date.getFullYear();
            dia = date.getDate();
            mes = date.getMonth() + 1;
            fechaCompleta = dia + "-" + mes + "-" + anio;

            if (fechaCompleta == "1-1-1900") {
                $("input[value='no'][name='optradiovacunas']").prop('checked', true);
                $("#ins_vacuna").prop("disabled", true);
            }
            else {
                $("input[value='si'][name='optradiovacunas']").prop('checked', true);
                $("#ins_vacuna").datepicker("setDate", date);
            }
        }
        if (beneficiario.BeneficiarioEducacion !== null) {

            //Educacion

            if (beneficiario.BeneficiarioEducacion.Estudia == true) {
                $("input[value='si'][name='optradioestudia']").prop('checked', true);
                $("#cmbgrado").prop("disabled", false);

                if (beneficiario.BeneficiarioEducacion.GradoEducacion != "N/A") {
                    $("#cmbgrado > option").each(function (index, value) {

                        if ($(value).html() == beneficiario.BeneficiarioEducacion.GradoEducacion) {
                            $(value).prop('selected', true);
                            return false;

                        }

                    });
                    $("#ine_motivo").val("").prop("disabled", true);
                } else {
                    $("#cmbgrado > option:first").prop('selected', true);
                }


            }
            else {
                $("input[value='no'][name='optradioestudia']").prop('checked', true);
                $("#cmbgrado").prop("disabled", true);
                $("#ine_motivo").val(beneficiario.BeneficiarioEducacion.Motivo == "N/A" ? "" : beneficiario.BeneficiarioEducacion.Motivo).prop("disabled", false);

            }

            beneficiario.BeneficiarioEducacion.NombreCentroEscolar == "N/A" ? $("#ine_centroescolar").val("") : $("#ine_centroescolar").val(beneficiario.BeneficiarioEducacion.NombreCentroEscolar);
            if (beneficiario.BeneficiarioEducacion.Turno != "N/A") {
                if (beneficiario.BeneficiarioEducacion.Turno == "Mañana") {
                    $("input[value='Mañana'][name='optionradioturno']").prop('checked', true);
                }
                else if (beneficiario.BeneficiarioEducacion.Turno == "Tarde") {
                    $("input[value='Tarde'][name='optionradioturno']").prop('checked', true)
                }
                else {
                    $("input[value='Otro'][name='optionradioturno']").prop('checked', true)
                }
            }

            if (beneficiario.BeneficiarioEducacion.UltimoGrado == "N/A") 
            {
                $("#cmbultimogrado > option:first").prop('selected', true);
            }
            else
            {
                $("#cmbultimogrado > option").each(function (index, value) {

                    if ($(value).html() == beneficiario.BeneficiarioEducacion.UltimoGrado) {
                        $(value).prop('selected', true);
                        return false;

                    }

                });
            }

            if (beneficiario.BeneficiarioEducacion.UltimoAño == "N/A") {
                $("#cmbultimoaño > option:first").prop('selected', true);
            }
            else {
                $("#cmbultimoaño > option").each(function (index, value) {

                    if ($(value).html() == beneficiario.BeneficiarioEducacion.UltimoAño) {
                        $(value).prop('selected', true);
                        return false;

                    }

                });
            }


        }
        $("#form1").data('bootstrapValidator').validate();
    }


    function fillLabelFields(beneficiario)
    {
        $('#lbl_programa').html(beneficiario.Programa);
        $('#lblp_nombre').html(beneficiario.Nombre);
        $('#lblp_apellido').html(beneficiario.Apellido);
        $('#lblp_codigo').html(beneficiario.Codigo);
        var edad = beneficiario.Edad.split("|");
        var fieldEdad = edad[0] + " años y " + edad[1] + " meses";
        $('#lblp_edad').html(fieldEdad);
        $('#lblp_genero').html(beneficiario.Sexo);
        $('#lblp_dui').html(beneficiario.Dui);
        $('#lblp_direccion').html(beneficiario.Direccion);


        if (beneficiario.BeneficiarioAdicional === null) {
            $('#lbla_emergenciacontacto').html("N/A");
            $('#lbla_emergencianumero').html("N/A"); 
            $('#lbla_registronacimiento').html("N/A");
            
        }
        else
        {
            $('#lbla_emergenciacontacto').html(beneficiario.BeneficiarioAdicional.NombreEmergencia);
            $('#lbla_emergencianumero').html(beneficiario.BeneficiarioAdicional.NumeroEmergencia);
            $('#lbla_registronacimiento').html(beneficiario.BeneficiarioAdicional.TieneRegistroNacimiento===true ? "Si": "No");
        }
        if (beneficiario.BeneficiarioCompromiso === null) {
            $('#lblc_reconoce').html("N/A");
            $('#lblc_problema').html("N/A");
            $('#lblc_congrega').html("N/A");
            $('#lblc_iglesia').html("N/A");
            $('#lblc_comentario').html("N/A");

        }
        else
        {
            $('#lblc_reconoce').html(beneficiario.BeneficiarioCompromiso.AceptaCompromiso === true ? "Si" : "No");
            $('#lblc_problema').html(beneficiario.BeneficiarioCompromiso.ExistioProblema === true ? "Si" : "No");
            $('#lblc_congrega').html(beneficiario.BeneficiarioCompromiso.SeCongrega === true ? "Si" : "No");
            $('#lblc_iglesia').html(beneficiario.BeneficiarioCompromiso.NombreIglesia);
            $('#lblc_comentario').html(beneficiario.BeneficiarioCompromiso.Comentario);
        }
        if (beneficiario.BeneficiarioSalud===null) {
            $('#lbls_estadosalud').html("N/A");
            $('#lbls_tarjeta').html("N/A");
            $('#lbls_curva').html("N/A");
            $('#lbls_vacuna').html("N/A");
            $('#lbls_enfermedad').html("N/A");
            $('#lbls_discapacidad').html("N/A");

        }
        else
        {
            $('#lbls_estadosalud').html(beneficiario.BeneficiarioSalud.EstadoSalud);
            $('#lbls_tarjeta').html(beneficiario.BeneficiarioSalud.TieneTarjeta === true ? "Si" : "No");
            var date = new Date(parseInt(beneficiario.BeneficiarioSalud.FechaCurvaCrecimiento.substr(6)));
            var anio = date.getFullYear();
            var dia = date.getDate();
            var mes = date.getMonth() + 1;
            var fechaCompleta = dia + "-" + mes + "-" + anio;
            $('#lbls_curva').html(fechaCompleta == "1-1-1900" ? "N/A" : fechaCompleta);
            date = new Date(parseInt(beneficiario.BeneficiarioSalud.FechaInmunizacion.substr(6)));
           anio = date.getFullYear();
             dia = date.getDate();
            mes = date.getMonth() + 1;
            fechaCompleta = dia + "-" + mes + "-" + anio;

            $('#lbls_vacuna').html(fechaCompleta == "1-1-1900"?"N/A":fechaCompleta);
            $('#lbls_enfermedad').html(beneficiario.BeneficiarioSalud.Enfermedad);
            $('#lbls_discapacidad').html(beneficiario.BeneficiarioSalud.Discapacidad);
        }
        if (beneficiario.BeneficiarioEducacion===null) {
            $('#lble_estudia').html("N/A");
            $('#lble_gradoactual').html("N/A");
            $('#lble_ultimogrado').html("N/A");
            $('#lble_ultimoaño').html("N/A");
            $('#lble_centroescolar').html("N/A");
            $('#lble_turno').html("N/A");
        }
        else
        {
            $('#lble_estudia').html(beneficiario.BeneficiarioEducacion.Estudia === true ? "Si" : "No");
            $('#lble_gradoactual').html(beneficiario.BeneficiarioEducacion.GradoEducacion);
            $('#lble_ultimogrado').html(beneficiario.BeneficiarioEducacion.UltimoGrado);
            $('#lble_ultimoaño').html(beneficiario.BeneficiarioEducacion.UltimoAño);
            $('#lble_centroescolar').html(beneficiario.BeneficiarioEducacion.NombreCentroEscolar);
            $('#lble_turno').html(beneficiario.BeneficiarioEducacion.Turno);
        }

    }


    function saveRecord(screenmode) {
        var id = $("#idperson").html();

        //Here I create the array of detail logbooks

        if (screenmode === "add") {
            
            var edad= $("#cmbanio > option:selected").html();
            edad += "|" + $("#cmbmeses > option:selected").html();

            var turno = "";
            if ($("input[value='Mañana'][name='optionradioturno']").is(":checked")) {
                turno = "Mañana";
            } else if ($("input[value='Tarde'][name='optionradioturno']").is(":checked")) {
                turno = "Tarde";
            } else if ($("input[value='Mañana'][name='optionradioturno']").is(":checked")) {
                turno = "Otro";
            }

            var registroNacimiento;
            if ($("input[value='no'][name='optradioregistro']").is(":checked")) {
                registroNacimiento = false;
            } else if ($("input[value='si'][name='optradioregistro']").is(":checked")) {
                registroNacimiento = true;
            } else  {
                registroNacimiento = null;
            }

            var beneficiarioTempAdicional = {
                ID_BeneficiarioAdicional: 0,
                NombreEmergencia: $("#ina_emergenciacontacto").val(),
                NumeroEmergencia: $("#ina_emergencianumero").val(),
                TieneRegistroNacimiento: registroNacimiento
            };

            var aceptaCompromiso;
            if ($("input[value='no'][name='optradioreconoce']").is(":checked")) {
                aceptaCompromiso = false;
            } else if ($("input[value='si'][name='optradioreconoce']").is(":checked")) {
                aceptaCompromiso = true;
            } else {
                aceptaCompromiso = null;
            }

            var existioProblema;
            if ($("input[value='no'][name='optradioproblema']").is(":checked")) {
                existioProblema = false;
            } else if ($("input[value='si'][name='optradioproblema']").is(":checked")) {
                existioProblema = true;
            } else {
                existioProblema = null;
            }

            var seCongrega;
            if ($("input[value='no'][name='optradioiglesia']").is(":checked")) {
                seCongrega = false;
            } else if ($("input[value='si'][name='optradioiglesia']").is(":checked")) {
                seCongrega = true;
            } else {
                seCongrega = null;
            }
            var beneficiarioTempCompromiso = {
                ID_BeneficiarioCompromiso: 0,
                AceptaCompromiso: aceptaCompromiso,
                ExistioProblema: existioProblema,
                SeCongrega: seCongrega,
                NombreIglesia: $("#inc_iglesia").val(),
                Comentario: $("#inc_comentario").val()    
            };
            var estadoSalud;
            if ($("input[value='No Satisfactoria'][name='radioestadosalud']").is(":checked")) {
                estadoSalud = "No Satisfactoria";
            } else if ($("input[value='Satisfactoria'][name='radioestadosalud']").is(":checked")) {
                estadoSalud = "Satisfactoria";
            } else {
                estadoSalud = "";
            }

            var tieneTarjeta;
            if ($("input[value='no'][name='optradiotarjeta']").is(":checked")) {
                tieneTarjeta = false;
            } else if ($("input[value='si'][name='optradiotarjeta']").is(":checked")) {
                tieneTarjeta = true;
            } else {
                tieneTarjeta = null;
            }
            var beneficiarioTempSalud = {
                ID_BeneficiarioSalud: 0,
                EstadoSalud: estadoSalud,
                TieneTarjeta: tieneTarjeta,
                Enfermedad:  $("#ins_enfermedad").val(),
                Discapacidad: $("#ins_discapacidad").val(),
                FechaCurvaCrecimiento: $("#ins_curva").datepicker('getDate'),
                FechaInmunizacion: $("#ins_vacuna").datepicker('getDate')
            };

            var estudia;
            if ($("input[value='si'][name='optradioestudia']").is(":checked")) {
                estudia = true;
            } else if ($("input[value='no'][name='optradioestudia']").is(":checked")) {
                estudia = false;
            } else {
                estudia = null;
            }
            var beneficiarioTempEducacion = {
                ID_BeneficiarioEducacion: 0,
                Estudia: estudia,
                GradoEducacion: $("#cmbgrado > option:selected").html(),
                Motivo: $("#ine_motivo").val(),
                UltimoGrado: $("#cmbultimogrado > option:selected").html(),
                UltimoAño: $("#cmbultimoaño > option:selected").html(),
                NombreCentroEscolar: $("#ine_centroescolar").val(),
                Turno:turno
            };
            

            var dataToSend =
                {
                    ID_Programa: parseInt($("#cmbprograma > option:selected").attr("data-id-program")),
                    Nombre: $("#inp_nombre").val(),
                    Apellido: $("#inp_apellido").val(),
                    Codigo: $("#inp_codigo").val(),
                    Dui: $("#inp_dui").val(),
                    Direccion: $("#inp_direccion").val(),
                    Sexo: $("input[value='Masculino']").is(":checked") ? "M" : "F",
                    Edad: edad,
                    ID_Beneficiario: 0,
                    BeneficiarioAdicional: beneficiarioTempAdicional,
                    BeneficiarioCompromiso: beneficiarioTempCompromiso,
                    BeneficiarioSalud:beneficiarioTempSalud,
                    BeneficiarioEducacion: beneficiarioTempEducacion
                };

            var data = JSON.stringify(dataToSend);

            makeAjaxCall(data, screenmode);
            setTabInDetailsMode();
            clearControls();
            
        }
        else if (screenmode === "edit") {
            var edad = $("#cmbanio > option:selected").html();
            edad += "|" + $("#cmbmeses > option:selected").html();

            var turno = "";
            if ($("input[value='Mañana'][name='optionradioturno']").is(":checked")) {
                turno = "Mañana";
            } else if ($("input[value='Tarde'][name='optionradioturno']").is(":checked")) {
                turno = "Tarde";
            } else if ($("input[value='Mañana'][name='optionradioturno']").is(":checked")) {
                turno = "Otro";
            }

            var registroNacimiento;
            if ($("input[value='no'][name='optradioregistro']").is(":checked")) {
                registroNacimiento = false;
            } else if ($("input[value='si'][name='optradioregistro']").is(":checked")) {
                registroNacimiento = true;
            } else {
                registroNacimiento = null;
            }

            var beneficiarioTempAdicional = {
                ID_BeneficiarioAdicional: 0,
                NombreEmergencia: $("#ina_emergenciacontacto").val(),
                NumeroEmergencia: $("#ina_emergencianumero").val(),
                TieneRegistroNacimiento: registroNacimiento
            };

            var aceptaCompromiso;
            if ($("input[value='no'][name='optradioreconoce']").is(":checked")) {
                aceptaCompromiso = false;
            } else if ($("input[value='si'][name='optradioreconoce']").is(":checked")) {
                aceptaCompromiso = true;
            } else {
                aceptaCompromiso = null;
            }

            var existioProblema;
            if ($("input[value='no'][name='optradioproblema']").is(":checked")) {
                existioProblema = false;
            } else if ($("input[value='si'][name='optradioproblema']").is(":checked")) {
                existioProblema = true;
            } else {
                existioProblema = null;
            }

            var seCongrega;
            if ($("input[value='no'][name='optradioiglesia']").is(":checked")) {
                seCongrega = false;
            } else if ($("input[value='si'][name='optradioiglesia']").is(":checked")) {
                seCongrega = true;
            } else {
                seCongrega = null;
            }
            var beneficiarioTempCompromiso = {
                ID_BeneficiarioCompromiso: 0,
                AceptaCompromiso: aceptaCompromiso,
                ExistioProblema: existioProblema,
                SeCongrega: seCongrega,
                NombreIglesia: $("#inc_iglesia").val(),
                Comentario: $("#inc_comentario").val()
            };
            var estadoSalud;
            if ($("input[value='No Satisfactoria'][name='radioestadosalud']").is(":checked")) {
                estadoSalud = "No Satisfactoria";
            } else if ($("input[value='Satisfactoria'][name='radioestadosalud']").is(":checked")) {
                estadoSalud = "Satisfactoria";
            } else {
                estadoSalud = "";
            }

            var tieneTarjeta;
            if ($("input[value='no'][name='optradiotarjeta']").is(":checked")) {
                tieneTarjeta = false;
            } else if ($("input[value='si'][name='optradiotarjeta']").is(":checked")) {
                tieneTarjeta = true;
            } else {
                tieneTarjeta = null;
            }
            var beneficiarioTempSalud = {
                ID_BeneficiarioSalud: 0,
                EstadoSalud: estadoSalud,
                TieneTarjeta: tieneTarjeta,
                Enfermedad: $("#ins_enfermedad").val(),
                Discapacidad: $("#ins_discapacidad").val(),
                FechaCurvaCrecimiento: $("#ins_curva").datepicker('getDate'),
                FechaInmunizacion: $("#ins_vacuna").datepicker('getDate')
            };

            var estudia;
            if ($("input[value='si'][name='optradioestudia']").is(":checked")) {
                estudia = true;
            } else if ($("input[value='no'][name='optradioestudia']").is(":checked")) {
                estudia = false;
            } else {
                estudia = null;
            }
            var beneficiarioTempEducacion = {
                ID_BeneficiarioEducacion: 0,
                Estudia: estudia,
                GradoEducacion: $("#cmbgrado > option:selected").html(),
                Motivo: $("#ine_motivo").val(),
                UltimoGrado: $("#cmbultimogrado > option:selected").html(),
                UltimoAño: $("#cmbultimoaño > option:selected").html(),
                NombreCentroEscolar: $("#ine_centroescolar").val(),
                Turno: turno
            };

            var idBeneficiario=parseInt( $('#pagetoedit').val());
            var dataToSend =
                {
                    ID_Beneficiario : idBeneficiario,
                    ID_Programa: parseInt($("#cmbprograma > option:selected").attr("data-id-program")),
                    Nombre: $("#inp_nombre").val(),
                    Apellido: $("#inp_apellido").val(),
                    Codigo: $("#inp_codigo").val(),
                    Dui: $("#inp_dui").val(),
                    Direccion: $("#inp_direccion").val(),
                    Sexo: $("input[value='Masculino']").is(":checked") ? "M" : "F",
                    Edad: edad,
                    BeneficiarioAdicional: beneficiarioTempAdicional,
                    BeneficiarioCompromiso: beneficiarioTempCompromiso,
                    BeneficiarioSalud: beneficiarioTempSalud,
                    BeneficiarioEducacion: beneficiarioTempEducacion
                };

            var data = JSON.stringify(dataToSend);

            makeAjaxCall(data, screenmode);
            setTabInDetailsMode();
            clearControls();
           
        }
    }

    function makeAjaxCall(dataToSend, action) {
        $.ajax({
            type: 'POST',
            url: '/Handlers/Registration.ashx?method=' + action,
            async: false,
            data: dataToSend,
            
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getAll();
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
            $("#editarbeneficiario").addClass("hidden");
        }
        //if (!Security.eliminar) {
        //    $("#action-delete").addClass("hidden");
        //}
        if (!Security.agregar) {
            $("#nuevobeneficiario").addClass("hidden");

        }
    }
});