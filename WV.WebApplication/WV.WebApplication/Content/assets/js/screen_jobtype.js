﻿
$(window).load(function () {

    //Execution
    CleanTabState();

    getJobTypes();

    

    // End of Execution

    /*CRUD Functions*/

    function getJobTypes() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/JobType.ashx?method=getall',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    setJobTypeTable(response.ResponseData);
                    attachClickToDeleteButtons();
                    attachClickToShowButtons();
                    attachClickToEditButtons();
                    attachClickToListButton();
                    if (Security.agregar) {
                        attachClickToNewButton();
                    }
                   
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


    function deleteJobType() {
        var idToDelete = $("#pagetodelete").val();
        var dataToSend =
            {
                ID_TipoPuesto: idToDelete
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/JobType.ashx?method=delete',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getJobTypes();
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

    function showJobType(ID_JobType) {
        var dataToSend =
            {
                Id_TipoPuesto: ID_JobType
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/JobType.ashx?method=getsingle',
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




    function showJobTypeToEdit(ID_JobType) {
        var dataToSend =
            {
                Id_TipoPuesto: ID_JobType
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/JobType.ashx?method=getsingle',
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

    function saveJobType(screenmode) {

        if (screenmode === "add") {
            var dataToSend =
                {
                    tipoPuesto: $("#in_tipo_puesto").val(),
                    tipoPuestoDescipcion: $("#in_tipo_puesto_descripcion").val()
                   

                };

            makeAjaxCall(dataToSend, screenmode);
            $("#tabtable").tab("show");
            $("#cancelpage").unbind();
            $("#savepage").unbind();
        }
        else if (screenmode === "edit") {
            // To implement****************
            var id = $('#pagetoedit').val();
            var dataToSend =
                {
                    Id_TipoPuesto: id,
                    tipoPuesto: $("#in_tipo_puesto").val(),
                    tipoPuestoDescipcion: $("#in_tipo_puesto_descripcion").val()

                };

            makeAjaxCall(dataToSend, screenmode);
            //$("#tabtable").tab("show");
            $("#tabtable").tab("show");
            $("#cancelpage").unbind();
            $("#savepage").unbind();
        }
    }

    function setJobTypeTable(responseData) {
        $("#page_table").html(responseData);
        applyOptionPermissions();
        validation();
    }


    /*Attaching Functions to Events*/
    function attachClickToDeleteButtons() {
        $("#page_table").find("[class='btn btn-primary btn-sm delete']").each(function (index, value) {
            $(value).click(function () {

                $('#pagetodelete').val($(this).attr("data-id-jobtype"));

            });
        });
    }

    function attachClickToShowButtons() {
        $("#page_table").find("[class='btn btn-primary btn-sm detail']").each(function (index, value) {
            $(value).click(function () {
                showJobType($(this).attr("data-id-jobtype"));
            });
        });
        //

    }

    function attachClickToEditButtons() {
        $("#page_table").find("[class='btn btn-primary btn-sm edit']").each(function (index, value) {
            $(value).click(function () {

                $('#pagetoedit').val($(this).attr("data-id-jobtype"));
                var idJobType = $('#pagetoedit').val();
                $("#tabdetails").tab("show");
                $("#screenmode").val("edit");
                setTabInEditMode();

                attachActionButtons();
                showJobTypeToEdit(idJobType);
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
            deleteJobType();
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
                saveJobType(screenmode);
                clearControls();
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
        $("#in_tipo_puesto_descripcion").val("");
        
    }
    //*********************************
    function fillLblFields(jobtype) {
        $("#lbl_tipo_puesto").html(jobtype.TipoPuesto1);
        $("#lbl_tipo_puesto_descripcion").html(jobtype.TipoPuestoDescripcion);

        $('#tabdetails').tab('show');
    }
    //***********************************
    function fillInputFields(jobtype) {
        $("#in_tipo_puesto").val(jobtype.TipoPuesto1);
        $("#in_tipo_puesto_descripcion").val(jobtype.TipoPuestoDescripcion);
        //$("#in_dui").val(project.Estado);




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
                in_tipo_puesto: {
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
                in_tipo_puesto_descripcion: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        },
                        stringLength: {
                            message: 'El campo debe tener mas de 5 caracteres',
                            min: 5
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
            url: '/Handlers/JobType.ashx?method=' + action,
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getJobTypes();
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
        var htmlToAppend = "<div class='col-md-2 col-sm-2'></div><div id='sidebaroptions' class='col-md-4 col-sm-4'><div class='activity_box activity_box2'><h3 style='color:#999'>Accesos rápidos</h3><div class='scrollbar' id='style-2'> <div class='activity-row activity-row1'><div class='single-bottom'><ul><li><a id='showjobreport' href='/Handlers/GeneralReportsProject.ashx?method=getjobreport'>Reporte General de Puestos</a></li><li><a id='showjobtypes' href='Job'>Pantalla de Puestos</a></li></ul></div></div></div></div></div>";
        $(htmlToAppend).insertAfter("div[class='col-md-6 col-sm-6']");
    }

    function applyOptionPermissions() {
        if (!Security.editar) {
            $("#page_table").find("[class='btn btn-primary btn-sm edit']").each(function (index, value) {
                $(this).parent().attr("hidden", "hidden");
            });
        }
        if (!Security.eliminar) {
            $("#page_table").find("[class='btn btn-primary btn-sm delete']").each(function (index, value) {
                $(this).parent().attr("hidden", "hidden");
            });
        }
    }

    function CleanTabState() {
        $('#tabtable').on('shown.bs.tab', function (e) {
            setTabInDetailsMode();
        });
    }

});
