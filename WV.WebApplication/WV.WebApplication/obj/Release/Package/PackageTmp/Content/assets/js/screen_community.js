
$(window).load(function () {

    //Execution
    CleanTabState();

    getAllCommunities();

   

    // End of Execution

    /*CRUD Functions*/

    function getAllCommunities() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/Community.ashx?method=getall',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    setCommunityTable(response.ResponseData);
                    attachLocations();
                    attachClickToDeleteButtons();
                    attachClickToShowButtons();
                    attachClickToEditButtons();
                    attachClickToListButton();
                    if (Security.agregar) {
                        attachClickToNewButton();
                    }
                    else
                    {
                        $("#tabdetails").html("Detalles");
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


    function deleteCommunity() {
        var idToDelete = $("#pagetodelete").val();
        var dataToSend =
            {
                Id_Comunidad: idToDelete
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Community.ashx?method=delete',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getAllCommunities();
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

    function showCommunity(ID_Comunidad) {
        var dataToSend =
            {
                Id_Comunidad: ID_Comunidad
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Community.ashx?method=getsingle',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    //to implement
                    fillLblFields(response.ResponseData);
                    setTabInDetailsMode();
                    if (!$("#sidebaroptions").length) {
                        loadSidebarOptions(ID_Comunidad);
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

    function attachLocations()
    {
        
        $.ajax({
            type: 'POST',
            url: '/Handlers/Community.ashx?method=locations',
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

    function loadSelects(fullObject)
    {
        $("#cmbdepartamento").html(fullObject.Departamentos).prop("disabled", false);
        $("#cmbmunicipio").html(fullObject.Municipios).prop("disabled", false);
        var id_municipio = 2;
        var id_deptoComunidad;
        $("#cmbmunicipio > option").each(function (index, value) {

            if ($(this).attr("data-id-municipio") == id_municipio) {
                $(this).attr("selected", "selected");
                id_deptoComunidad = $(this).attr("data-id-municipio-departamento")
            }

        });

        $("#cmbdepartamento > option").each(function (index, value) {

            if ($(this).attr("data-id-department") == id_deptoComunidad) {
                $(this).attr("selected", "selected");
            }

        });

        $("#cmbmunicipio > option").each(function (index, value) {

            if ($(this).attr("data-id-municipio-departamento") != id_deptoComunidad) {

                $(this).addClass("hidden");

            }

        });
    }

    function showCommunityToEdit(ID_Comunidad) {
        var dataToSend =
            {
                Id_Comunidad: ID_Comunidad
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Community.ashx?method=getsingle',
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

    function saveCommunity(screenmode) {

        if (screenmode === "add") {
            var dataToSend =
                {
                    Id_Municipio: $("#cmbmunicipio > option:selected").attr("data-id-municipio"),
                    comunidad: $("#in_comunidad").val()
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
                    Id_Comunidad: id,
                    Id_Municipio: $("#cmbmunicipio > option:selected").attr("data-id-municipio"),
                    comunidad: $("#in_comunidad").val()
                    

                };

            makeAjaxCall(dataToSend, screenmode);
            //$("#tabtable").tab("show");
            $("#tabtable").tab("show");
            $("#cancelpage").unbind();
            $("#savepage").unbind();
        }
    }

    function setCommunityTable(responseData) {
        $("#page_table").html(responseData);
        applyOptionPermissions();
        validation();
    }


    /*Attaching Functions to Events*/
    function attachClickToDeleteButtons() {
        $("#page_table").find("[class='btn btn-primary btn-sm delete']").each(function (index, value) {
            $(value).click(function () {

                $('#pagetodelete').val($(this).attr("data-id-community"));

            });
        });
    }

    function attachClickToShowButtons() {
        $("#page_table").find("[class='btn btn-primary btn-sm detail']").each(function (index, value) {
            $(value).click(function () {
                showCommunity($(this).attr("data-id-community"));
            });
        });

        $("#cmbdepartamento").unbind();
        $("#cmbmunicipio").unbind();
        //Attach Event to Select Departament

        $("#cmbdepartamento").change(function ()
        {
            var optionSelected = $("option:selected");
            var departmentOption = optionSelected.attr("data-id-department");
            $("#cmbmunicipio > option").each(function (index, value) {

                if ($(value).attr("data-id-municipio-departamento") == departmentOption) {
                    $(value).removeClass("hidden");
                }
                else
                {
                    $(value).addClass("hidden");
                }
                
                $(value).removeAttr("selected");
            });
            //$("#cmbmunicipio option[class='']").prop("selectedIndex", 0);
           $("#cmbmunicipio option[class='']:first").attr('selected', 'selected');
        });

      //

    }

    function attachClickToEditButtons() {
        $("#page_table").find("[class='btn btn-primary btn-sm edit']").each(function (index, value) {
            $(value).click(function () {

                $('#pagetoedit').val($(this).attr("data-id-community"));
                var idComunidad = $('#pagetoedit').val();
                $("#tabdetails").tab("show");
                $("#screenmode").val("edit");
                setTabInEditMode();

                attachActionButtons();
                showCommunityToEdit(idComunidad);
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

    function attachClickToModal() {
        $("#pagebtndelete").click(function () {
            deleteCommunity();
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
                saveCommunity(screenmode);
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
        $("#cmbdepartamento").prop("disabled", false);
        $("#cmbmunicipio").prop("disabled", false);
        
    }
    //*********************************
    function fillLblFields(comunidad) {
        $("#lbl_comunidad").html(comunidad.Comunidad.Comunidad1);
        $("#cmbdepartamento").html(comunidad.Departamentos).prop("disabled",true);
        $("#cmbmunicipio").html(comunidad.Municipios).prop("disabled",true);
        var id_municipio = comunidad.Comunidad.ID_Municipio;
        var id_deptoComunidad;
        $("#cmbmunicipio > option").each(function (index, value) {
            
            if ($(this).attr("data-id-municipio") == id_municipio)
            {
                $(this).attr("selected","selected");
                id_deptoComunidad = $(this).attr("data-id-municipio-departamento")
            }
            
        });

        $("#cmbdepartamento > option").each(function (index, value) {

            if ($(this).attr("data-id-department") == id_deptoComunidad) {
                $(this).attr("selected", "selected");
            }

        });

        $("#cmbmunicipio > option").each(function (index, value) {

            if ($(this).attr("data-id-municipio-departamento") != id_deptoComunidad) {
               
                $(this).addClass("hidden");

            }

        });


        $('#tabdetails').tab('show');
    }
    //***********************************
    function fillInputFields(comunidad) {
        $("#in_comunidad").val(comunidad.Comunidad.Comunidad1);
        $("#cmbdepartamento").html(comunidad.Departamentos).prop("disabled", false);
        $("#cmbmunicipio").html(comunidad.Municipios).prop("disabled", false);
        var id_municipio = comunidad.Comunidad.ID_Municipio;
        var id_deptoComunidad;
        $("#cmbmunicipio > option").each(function (index, value) {

            if ($(this).attr("data-id-municipio") == id_municipio) {
                $(this).attr("selected", "selected");
                id_deptoComunidad = $(this).attr("data-id-municipio-departamento")
            }

        });

        $("#cmbdepartamento > option").each(function (index, value) {

            if ($(this).attr("data-id-department") == id_deptoComunidad) {
                $(this).attr("selected", "selected");
            }

        });

        $("#cmbmunicipio > option").each(function (index, value) {

            if ($(this).attr("data-id-municipio-departamento") != id_deptoComunidad) {

                $(this).addClass("hidden");

            }

        });
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
                in_comunidad: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        },
                        stringLength: {
                            message: 'Mínimo 4 caracteres, Máximo 30',
                            max: 30,
                            min: 4
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
            url: '/Handlers/Community.ashx?method=' + action,
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getAllCommunities();
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
    function loadSidebarOptions(idComunidad) {
        var htmlToAppend = "<div class='col-md-2 col-sm-2'></div><div id='sidebaroptions' class='col-md-4 col-sm-4'><div class='activity_box activity_box2'><h3 style='color:#999'>Accesos Rápidos</h3><div class='scrollbar' id='style-2'> <div class='activity-row activity-row1'><div class='single-bottom'><ul><li><a id='showrolereport' href='/Handlers/GeneralReportsProgram.ashx?method=getbeneficiarycommunityreport&ID_Comunidad=" + idComunidad + "'>Reporte de Beneficiarios</a></li></ul></div></div></div></div></div>";
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
