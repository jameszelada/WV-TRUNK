
$(window).load(function () {

    //Execution


    getStaff();

    //loadSidebarOptions();



    // End of Execution

    /*CRUD Functions*/

    function getStaff() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/Staff.ashx?method=getall',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    setStaffTable(response.ResponseData);
                    attachClickToDeleteButtons();
                    attachClickToShowButtons();
                    attachClickToEditButtons();
                    attachClickToListButton();
                    attachClickToNewButton();
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


    function deletePerson() {
        var idToDelete = $("#pagetodelete").val();
        var dataToSend =
            {
                Id_Persona: idToDelete
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Staff.ashx?method=delete',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getStaff();
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

    function showPerson(ID_Persona) {
        var dataToSend =
            {
                Id_Persona: ID_Persona
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Staff.ashx?method=getsingle',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    //to implement
                    fillLblFields(response.ResponseData);
                    setTabInDetailsMode();
                    if (!$("#sidebaroptions").length) {
                        loadSidebarOptions();
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

   


    function showPersonToEdit(ID_Persona) {
        var dataToSend =
            {
                Id_Persona: ID_Persona
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Staff.ashx?method=getsingle',
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

    function savePerson(screenmode) {

        if (screenmode === "add") {
            var dataToSend =
                {
                    nombre: $("#in_nombre").val(),
                    apellido: $("#in_apellido").val(),
                    dui: $("#in_dui").val(),
                    direccion: $("#in_direccion").val(),
                    telefono: $("#in_telefono").val(),
                    email: $("#in_email").val(),
                    fechaNacimiento: $("#in_fecha_nacimiento").val(),
                    sexo: $("#radio_masculino").is(':checked') ? "M" : "F"
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
                    Id_Persona: id,
                    nombre: $("#in_nombre").val(),
                    apellido: $("#in_apellido").val(),
                    dui: $("#in_dui").val(),
                    direccion: $("#in_direccion").val(),
                    telefono: $("#in_telefono").val(),
                    email: $("#in_email").val(),
                    fechaNacimiento: $("#in_fecha_nacimiento").val(),
                    sexo: $("#radio_masculino").is(':checked') ? "M" : "F"


                };

            makeAjaxCall(dataToSend, screenmode);
            //$("#tabtable").tab("show");
            $("#tabtable").tab("show");
            $("#cancelpage").unbind();
            $("#savepage").unbind();
        }
    }

    function setStaffTable(responseData) {
        $("#page_table").html(responseData);
        validation();
    }


    /*Attaching Functions to Events*/
    function attachClickToDeleteButtons() {
        $("#page_table").find("[class='btn btn-primary btn-sm delete']").each(function (index, value) {
            $(value).click(function () {

                $('#pagetodelete').val($(this).attr("data-id-person"));

            });
        });
    }

    function attachClickToShowButtons() {
        $("#page_table").find("[class='btn btn-primary btn-sm detail']").each(function (index, value) {
            $(value).click(function () {
                showPerson($(this).attr("data-id-person"));
            });
        });
        //

    }

    function attachClickToEditButtons() {
        $("#page_table").find("[class='btn btn-primary btn-sm edit']").each(function (index, value) {
            $(value).click(function () {

                $('#pagetoedit').val($(this).attr("data-id-person"));
                var idPersona = $('#pagetoedit').val();
                $("#tabdetails").tab("show");
                $("#screenmode").val("edit");
                setTabInEditMode();

                attachActionButtons();
                showPersonToEdit(idPersona);
                validation();
            });
        });
    }

    function attachClickToNewButton() {
        $("#tabdetails").click(function () {
            validation();
            $("#screenmode").val("add");
            setTabInAddMode();
            attachActionButtons();

        });
    }

    function attachClickToListButton() {
        $("#tabtable").click(function () {
            
            clearControls();
            $("#form1").data('bootstrapValidator').resetForm();
        });
    }

    function attachClickToModal() {
        $("#pagebtndelete").click(function () {
            deletePerson();
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
            var formValidation = $("#form1").data('bootstrapValidator').isValid();

            if (formValidation) {
                var screenmode = $("#screenmode").val();
                savePerson(screenmode);
                $("#form1").data('bootstrapValidator').resetForm();
            }


        });
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

    /*Utility Functions*/

    function clearControls() {
        $(".in-controls").find("input").each(function (index, value) {
            $(value).val("");
        });
 
        
    }
    //*********************************
    function fillLblFields(person) {
        $("#lbl_nombre").html(person.Nombre);
        $("#lbl_apellido").html(person.Apellido);
        $("#lbl_dui").html(person.Dui);
        $("#lbl_telefono").html(person.Telefono);
        $("#lbl_direccion").html(person.Direccion);
        $("#lbl_email").html(person.Email);
        var date = new Date(parseInt(person.FechaNacimiento.substr(6)));
        var anio = date.getFullYear();
        var dia = date.getDate();
        var mes = date.getMonth() + 1;
        var fechaCompleta = dia +"-"+ mes +"-"+ anio;
        $("#lbl_fecha_nacimiento").html(fechaCompleta);
        person.Sexo == "M" ? $("#lbl_sexo").html("Masculino") : $("#lbl_sexo").html("Femenino");
       
        $('#tabdetails').tab('show');
    }
    //***********************************
    function fillInputFields(person) {
        $("#in_nombre").val(person.Nombre);
        $("#in_apellido").val(person.Apellido);
        $("#in_dui").val(person.Dui);
        $("#in_telefono").val(person.Telefono);
        $("#in_direccion").val(person.Direccion);
        $("#in_email").val(person.Email);
        var date = new Date(parseInt(person.FechaNacimiento.substr(6)));
        var anio = date.getFullYear();
        var dia = (date.getDate()).toString();
        dia = dia.length == 1 ? "0" + dia : dia;
        var mes = (date.getMonth() + 1).toString();
        mes = mes.length == 1 ? "0" + mes : mes;
        var fechaCompleta = anio + "-" + mes + "-" + dia;
        $("#in_fecha_nacimiento").val(fechaCompleta);
        person.Sexo == "M" ? $("#radio_masculino").prop("checked", true) : $("#radio_femenino").prop("checked", true);
        
        $("#form1").data('bootstrapValidator').validate();
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
                in_nombre: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        },
                        stringLength: {
                            message: 'El campo debe tener mas de 5 caracteres',
                            min: 5
                        }
                    },

                },
                in_apellido: {
            validators: {
                    notEmpty: {
                        message: 'Este campo es requerido.'
                    },
                stringLength: {
                    message: 'El campo debe tener mas de 5 caracteres',
                    min: 5
                }
            },

                },
                in_dui: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        },
                        stringLength: {
                            message: 'El campo debe tener mas de 5 caracteres',
                            min: 5
                        }
                    },

                },
                in_fecha_nacimiento: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        }
                    },

                },
                in_email: {
            validators: {
                    notEmpty: {
                        message: 'Este campo es requerido.'
                    }
            },

                },
                in_direccion: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        }
                    },

                },
                in_telefono: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        }
                    },

                }
            }

        }).on('bv-form', function () {
            $(this).validate();
        });
    }

    function makeAjaxCall(dataToSend, action) {
        $.ajax({
            type: 'POST',
            url: '/Handlers/Staff.ashx?method=' + action,
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getStaff();
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

    function loadSidebarOptions() {
        var htmlToAppend = "<div class='col-md-2 col-sm-2'></div><div id='sidebaroptions' class='col-md-4 col-sm-4'><div class='activity_box activity_box2'><h3 style='color:#999'>Opciones</h3><div class='scrollbar' id='style-2'> <div class='activity-row activity-row1'><div class='single-bottom'><ul><li> </li></ul></div></div></div></div></div>";
        $(htmlToAppend).insertAfter("div[class='col-md-6 col-sm-6']");
    }

});
