$(window).load(function () {

    CleanTabState();

    /*CRUD Functions*/
    getAllRoles();

    function getAllRoles() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/Role.ashx?method=getall',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    setRolesTable(response.ResponseData);
                    if (Security.agregar)
                    {
                        attachClickToNewButton();
                    }
                    else
                    {
                        $("#tabdetails").html("Detalles");
                    }
                    attachClickToShowButtons();
                    attachClickToEditButtons();
                    attachClickToListButton();
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

    function showRole(IdRole) {
        var idRole = IdRole;
        var dataToSend =
            {
                Id_Role: IdRole
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Role.ashx?method=getsingle',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    //to implement
                    fillLblFields(response.ResponseData);
                    setTabInDetailsMode();
                    if (!$("#sidebaroptions").length) {
                        loadSidebarOptions(idRole);
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

    function showRoleToEdit(IdRol) {
        var dataToSend =
            {
                Id_Role: IdRol
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Role.ashx?method=getsingle',
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

    function saveRole(screenmode) {

        if (screenmode === "add") {
            var dataToSend =
                {
                    rol: $("#in_rol").val(),
                    descripcion: $("#in_descripcion").val()
                };

            makeAjaxCall(dataToSend, screenmode);
            $("#tabtable").tab("show");
            $("#cancelrole").unbind();
            $("#saverole").unbind();
            clearControls();
        }
        else if (screenmode === "edit") {
            // To implement****************
            var id = $('#roletoedit').val();
            var dataToSend =
                {
                    Id_Rol: id,
                    rol: $("#in_rol").val(),
                    descripcion: $("#in_descripcion").val()
                   
                };

            makeAjaxCall(dataToSend, screenmode);
            $("#tabtable").tab("show");
            $("#cancelrole").unbind();
            $("#saverole").unbind();
            clearControls();
        }
    }

    function setRolesTable(responseData) {
        $("#roles_table").html(responseData);
        applyOptionPermissions();
        validation();
    }

    /*Attaching Functions to Events*/
    function attachClickToShowButtons() {
        $("#roles_table").find("[class='btn btn-primary btn-sm detail']").each(function (index, value) {
            $(value).click(function () {
                showRole($(this).attr("data-id-role"));
            });
        });
    }

    function attachClickToEditButtons() {
        $("#roles_table").find("[class='btn btn-primary btn-sm edit']").each(function (index, value) {
            $(value).click(function () {

                $('#roletoedit').val($(this).attr("data-id-role"));
                var idRoleToEdit = $('#roletoedit').val();
                $("#tabdetails").tab("show");
                $("#screenmode").val("edit");
                setTabInEditMode();

                attachActionButtons();
                showRoleToEdit(idRoleToEdit);
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
            $("#sidebaroptions").remove();
            clearControls();
            $("#form1").data('bootstrapValidator').resetForm();
        });
    }

    function attachActionButtons() {
        $("#sidebaroptions").remove();
        $("#cancelrole").click(function () {
            $("#tabtable").tab("show");
            clearControls();
            $("#form1").data('bootstrapValidator').resetForm();

        });

        $("#saverole").click(function () {
           $("#form1").data('bootstrapValidator').validate();
            var formValidation = $("#form1").data('bootstrapValidator').isValid();

            if (formValidation) {
                var screenmode = $("#screenmode").val();
                saveRole(screenmode);
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
        
    }

    function fillLblFields(rol) {
        $("#lbl_rol").html(rol.Rol1);
        $("#lbl_descripcion").html(rol.Descripcion);
        $('#tabdetails').tab('show');

    }

    function fillInputFields(rol) {
        $("#in_rol").val(rol.Rol1);
        $("#in_descripcion").val(rol.Descripcion);
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
                in_rol: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        },
                        stringLength: {
                            message: 'Mínimo 4 caracteres, Máximo 25 ',
                            max: 25,
                            min: 4
                        },
                        regexp: {
                            regexp: /^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s]+$/i,
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
                            message: 'Máximo 25 caracteres',
                            max: 25
                        }
                    }
                }
            }

        });
    }

    function makeAjaxCall(dataToSend, screenmode) {
        $.ajax({
            type: 'POST',
            url: '/Handlers/Role.ashx?method=' + screenmode,
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getAllRoles();
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

    function loadSidebarOptions(idRole) {
        var htmlToAppend = "<div class='col-md-2 col-sm-2'></div><div id='sidebaroptions' class='col-md-4 col-sm-4'><div class='activity_box activity_box2'><h3 style='color:#999'>Accesos rápidos</h3><div class='scrollbar' id='style-2'> <div class='activity-row activity-row1'><div class='single-bottom'><ul><li><a id='showrolereport' href='/Handlers/GeneralReporstAdmin.ashx?method=getroleinfo&ID_Rol="+idRole+"'> Reporte de Rol del Sistema</a></li><li><a id='showrolereport' href='/Handlers/GeneralReporstAdmin.ashx?method=getrolesreport'> Reporte General de Roles</a></li></ul></div></div></div></div></div>";
        $(htmlToAppend).insertAfter("div[class='col-md-6 col-sm-6']");
    }

    function applyOptionPermissions() {
        if (!Security.editar) {
            $("#users_table").find("[class='btn btn-primary btn-sm edit']").each(function (index, value) {
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