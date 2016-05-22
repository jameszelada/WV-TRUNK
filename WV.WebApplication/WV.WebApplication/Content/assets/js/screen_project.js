
$(window).load(function () {

    //Execution


    getProjects();



    // End of Execution

    /*CRUD Functions*/

    function getProjects() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/Project.ashx?method=getall',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    setProjectTable(response.ResponseData);
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


    function deleteProject() {
        var idToDelete = $("#pagetodelete").val();
        var dataToSend =
            {
                Id_Proyecto: idToDelete
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Project.ashx?method=delete',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getProjects();
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

    function showProject(ID_Project) {
        var dataToSend =
            {
                Id_Proyecto: ID_Project
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Project.ashx?method=getsingle',
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




    function showProjectToEdit(ID_Proyecto) {
        var dataToSend =
            {
                Id_Proyecto: ID_Proyecto
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Project.ashx?method=getsingle',
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

    function saveProject(screenmode) {

        if (screenmode === "add") {
            var dataToSend =
                {
                    codigo: $("#in_codigo_proyecto").val(),
                    descripcionProyecto: $("#in_descripcion").val(),
                    estado: $("#cmbestado > option:selected").attr("data-id-estado")
                    
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
                    Id_Proyecto: id,
                    codigo: $("#in_codigo_proyecto").val(),
                    descripcionProyecto: $("#in_descripcion").val(),
                    estado: $("#cmbestado > option:selected").attr("data-id-estado")
 
                };

            makeAjaxCall(dataToSend, screenmode);
            //$("#tabtable").tab("show");
            $("#tabtable").tab("show");
            $("#cancelpage").unbind();
            $("#savepage").unbind();
        }
    }

    function setProjectTable(responseData) {
        $("#page_table").html(responseData);

    }


    /*Attaching Functions to Events*/
    function attachClickToDeleteButtons() {
        $("#page_table").find("[class='btn btn-primary btn-sm delete']").each(function (index, value) {
            $(value).click(function () {

                $('#pagetodelete').val($(this).attr("data-id-project"));

            });
        });
    }

    function attachClickToShowButtons() {
        $("#page_table").find("[class='btn btn-primary btn-sm detail']").each(function (index, value) {
            $(value).click(function () {
                showProject($(this).attr("data-id-project"));
            });
        });
        //

    }

    function attachClickToEditButtons() {
        $("#page_table").find("[class='btn btn-primary btn-sm edit']").each(function (index, value) {
            $(value).click(function () {

                $('#pagetoedit').val($(this).attr("data-id-project"));
                var idProyecto = $('#pagetoedit').val();
                $("#tabdetails").tab("show");
                $("#screenmode").val("edit");
                setTabInEditMode();

                attachActionButtons();
                showProjectToEdit(idProyecto);
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
            $("#cancelpage").unbind();
            $("#savepage").unbind();
            clearControls();
            $("#form1").data('bootstrapValidator').resetForm();
        });
    }

    function attachClickToModal() {
        $("#pagebtndelete").click(function () {
            deleteProject();
        });
    }


    function attachActionButtons() {
        $("#cancelpage").click(function () {
            $("#tabtable").tab("show");
            clearControls();
            $("#form1").data('bootstrapValidator').resetForm();

        });

        $("#savepage").click(function () {
            var formValidation = $("#form1").data('bootstrapValidator').isValid();

            if (formValidation) {
                var screenmode = $("#screenmode").val();
                saveProject(screenmode);
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
        $("#in_descripcion").val("");
        $("#cancelpage").unbind();
        $("#savepage").unbind();
    }
    //*********************************
    function fillLblFields(project) {
        $("#lbl_codigo_proyecto").html(project.Codigo);
        $("#lbl_descripcion").html(project.ProyectoDescripcion);
        var estado = "";
        if (project.Estado == "A") {
            estado = "Activo";
        }
        else if (project.Estado == "I") {
            estado = "Inactivo";
        }
        else if (project.Estado == "S") {
            estado = "Suspendido";
        }
        $("#lbl_estado").html(estado);

        $('#tabdetails').tab('show');
    }
    //***********************************
    function fillInputFields(project) {
        $("#in_codigo_proyecto").val(project.Codigo);
        $("#in_descripcion").val(project.ProyectoDescripcion);
        //$("#in_dui").val(project.Estado);

        var estado = "";
        if (project.Estado == "A") {
            $("#cmbestado > option[data-id-estado='A']").attr("selected","selected");
        }
        else if (project.Estado == "I") {
            var g=$("#cmbestado > option[data-id-estado='I']").attr("selected", "selected");
        }
        else if (project.Estado == "S") {
            $("#cmbestado > option[data-id-estado='S']").attr("selected", "selected");
        }

        

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
                in_codigo_proyecto: {
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
                in_descripcion: {
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
            url: '/Handlers/Project.ashx?method=' + action,
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getProjects();
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
        $("#errorcontainer").css({
            'position': 'absolute',
            'zIndex': '0',
            'right': '30%'

        }).html(message).toggleClass("hidden").fadeToggle(2000, "linear", function () { $("#errorcontainer").toggleClass("hidden").empty(); });
    }

    function displayMessage(message) {
        $("#messagecontainer").css({
            'position': 'absolute',
            'zIndex': '0',
            'right': '30%'

        }).html(message).toggleClass("hidden").fadeToggle(2000, "linear", function () { $("#messagecontainer").toggleClass("hidden").empty(); });
        $("#pagebtndelete").unbind();
        $("#tabdetails").unbind();

    }

});
