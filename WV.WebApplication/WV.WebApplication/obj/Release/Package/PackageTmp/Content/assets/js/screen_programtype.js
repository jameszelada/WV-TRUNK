
$(window).load(function () {

    //Execution


    getAllProgramTypes();

    


    // End of Execution

    /*CRUD Functions*/

    function getAllProgramTypes() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/ProgramType.ashx?method=getall',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    setProgramTypeTable(response.ResponseData);
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


    function deleteProgramType() {
        var idToDelete = $("#pagetodelete").val();
        var dataToSend =
            {
                Id_TipoPrograma: idToDelete
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/ProgramType.ashx?method=delete',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getAllProgramTypes();
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

    function showProgramType(ID_TipoPrograma) {
        var dataToSend =
            {
                Id_TipoPrograma: ID_TipoPrograma
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/ProgramType.ashx?method=getsingle',
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

    function showProgramTypeToEdit(ID_TipoPrograma) {
        var dataToSend =
            {
                Id_TipoPrograma: ID_TipoPrograma
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/ProgramType.ashx?method=getsingle',
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

    function saveProgramType(screenmode) {

        if (screenmode === "add") {
            var dataToSend =
                {
                    tipoPrograma: $("#in_tipo_programa").val(),
                    tipoProgramaDescripcion: $("#in_tipo_programa_descripcion").val()
                    
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
                    Id_TipoPrograma: id,
                    tipoPrograma: $("#in_tipo_programa").val(),
                    tipoProgramaDescripcion: $("#in_tipo_programa_descripcion").val()
                   
                };

            makeAjaxCall(dataToSend, screenmode);
            //$("#tabtable").tab("show");
            $("#tabtable").tab("show");
            $("#cancelpage").unbind();
            $("#savepage").unbind();
        }
    }

    function setProgramTypeTable(responseData) {
        $("#page_table").html(responseData);
        validation();
    }


    /*Attaching Functions to Events*/
    function attachClickToDeleteButtons() {
        $("#page_table").find("[class='btn btn-primary btn-sm delete']").each(function (index, value) {
            $(value).click(function () {

                $('#pagetodelete').val($(this).attr("data-id-program"));

            });
        });
    }

    function attachClickToShowButtons() {
        $("#page_table").find("[class='btn btn-primary btn-sm detail']").each(function (index, value) {
            $(value).click(function () {
                showProgramType($(this).attr("data-id-program"));
            });
        });
    }

    function attachClickToEditButtons() {
        $("#page_table").find("[class='btn btn-primary btn-sm edit']").each(function (index, value) {
            $(value).click(function () {

                $('#pagetoedit').val($(this).attr("data-id-program"));
                var idTipoPrograma = $('#pagetoedit').val();
                $("#tabdetails").tab("show");
                $("#screenmode").val("edit");
                setTabInEditMode();

                attachActionButtons();
                showProgramTypeToEdit(idTipoPrograma);
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
            deleteProgramType();
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
                saveProgramType(screenmode);
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
    function fillLblFields(tipoPrograma) {
        $("#lbl_tipo_programa").html(tipoPrograma.TipoPrograma1);
        $("#lbl_tipo_programa_descripcion").html(tipoPrograma.TipoProgramaDescripcion);
       
        $('#tabdetails').tab('show');
    }
    //***********************************
    function fillInputFields(tipoPrograma) {
        $("#in_tipo_programa").val(tipoPrograma.TipoPrograma1);
        $("#in_tipo_programa_descripcion").val(tipoPrograma.TipoProgramaDescripcion);
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
                in_tipo_programa: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        },
                        stringLength: {
                            message: 'El campo debe tener mas de 5 caracteres',
                            min: 5
                        }
                    }
                },
                in_tipo_programa_descripcion: {
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

    function makeAjaxCall(dataToSend, action) {
        $.ajax({
            type: 'POST',
            url: '/Handlers/ProgramType.ashx?method=' + action,
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getAllProgramTypes();
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
        var htmlToAppend = "<div class='col-md-2 col-sm-2'></div><div id='sidebaroptions' class='col-md-4 col-sm-4'><div class='activity_box activity_box2'><h3 style='color:#999'>Accesos rápidos</h3><div class='scrollbar' id='style-2'> <div class='activity-row activity-row1'><div class='single-bottom'><ul><li><a id='showjobreport' href='Project'>Modificar Proyectos</a></li><li><a id='showprogram' href='Program'>Modificar Programas</a></li></ul></div></div></div></div></div>";
        $(htmlToAppend).insertAfter("div[class='col-md-6 col-sm-6']");
    }

});
