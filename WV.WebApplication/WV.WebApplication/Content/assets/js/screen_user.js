
$(window).load(function () {

    //Execution
   

    getAllUsers();

    getAllRoles();

    loadSidebarOptions();


    // End of Execution

    /*CRUD Functions*/

    function getAllUsers()
    {
        $.ajax({
            type: 'POST',
            url: '/Handlers/User.ashx?method=getall',
            success: function (data)
            {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    setUsersTable(response.ResponseData);
                    attachClickToAssignButtons()
                    attachClickToDeleteButtons();
                    attachClickToShowButtons();
                    attachClickToEditButtons();
                    attachClickToListButton();
                    attachClickToNewButton();
                    attachClickToModal();

                }
                else
                {
                    var error = "Error de Conexión, Intente nuevamente  ";
                    displayErrorMessage(error);
                }
            },
            error: function ()
            {
                var error = "Error de Conexión, Intente nuevamente";
                displayErrorMessage(error);
            }
        });
    }

    function getAllRoles()
    {
        $.ajax({
            type: 'POST',
            url: '/Handlers/User.ashx?method=getallroles',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    

                    $("#cmbroles").html(response.ResponseData);
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

        $("#btnassignrole").click(function () {
            //llamar funcion asignar role
            var ID_user= $("#usertoassign").val();
            $("#usertoassign").val($("#cmbroles option:selected").attr("data-id-role"));
            var ID_role = $("#usertoassign").val();

            var dataToSend =
            {
                ID_Usuario: ID_user,
                ID_Rol: ID_role
            };
            var action = "saveuserrole";
            saveUserRole(dataToSend, action);

        });

    }

    function deleteUser()
    {
        var idToDelete = $("#usertodelete").val();
        var dataToSend =
            {
                Id_Usuario: idToDelete
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/User.ashx?method=delete',
            data: dataToSend,
            success: function (data)
            {

                var response = JSON.parse(data);
                if (response.IsSucess)
                {
                    displayMessage(response.Message);
                    getAllUsers();
                }
                else
                {
                    displayErrorMessage(response.Message);
                }
            },
            error: function ()
            {
                var error = "Error de Conexión, Intente nuevamente";
                displayErrorMessage(error);
            }
        });
    }

    function showUser(Id_User)
    {
        var dataToSend =
            {
                Id_Usuario: Id_User
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/User.ashx?method=getsingle',
            data: dataToSend,
            success: function (data)
            {

                var response = JSON.parse(data);
                if (response.IsSucess)
                {
                    //to implement
                    fillLblFields(response.ResponseData);
                    setTabInDetailsMode();
                }
                else {
                    displayErrorMessage(response.Message);
                }
            },
            error: function ()
            {
                var error = "Error de Conexión, Intente nuevamente";
                displayErrorMessage(error);
            }
        });
    }

    function showUserToEdit(Id_User) {
        var dataToSend =
            {
                Id_Usuario: Id_User
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/User.ashx?method=getsingle',
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

    function saveUser(screenmode)
    {

        if (screenmode === "add")
        {
            var dataToSend =
                {
                username: $("#in_username").val(),
                nombre: $("#in_nombre").val(),
                apellido: $("#in_apellido").val(),
                contrasenia: $("#in_contrasenia").val(),
                email: $("#in_email").val()
            };

            makeAjaxCall(dataToSend, screenmode);
            $("#tabtable").tab("show");
            $("#canceluser").unbind();
            $("#saveuser").unbind();
            clearControls();
        }
        else if (screenmode === "edit")
        {
            // To implement****************
            var id=$('#usertoedit').val();
            var dataToSend =
                {
                    Id_Usuario: id,
                    username: $("#in_username").val(),
                    nombre: $("#in_nombre").val(),
                    apellido: $("#in_apellido").val(),
                    contrasenia: $("#in_contrasenia").val(),
                    email: $("#in_email").val()
                };

            makeAjaxCall(dataToSend, screenmode);
            //$("#tabtable").tab("show");
            $("#tabtable").tab("show");
            $("#canceluser").unbind();
            $("#saveuser").unbind();
        }
    }

    function setUsersTable(responseData)
    {
        $("#users_table").html(responseData);
        
    }

    function saveUserRole(dataToSend,action)
    {
        $.ajax({
            type: 'POST',
            url: '/Handlers/User.ashx?method=' + action,
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    //getAllUsers();
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

    /*Attaching Functions to Events*/
    function attachClickToDeleteButtons()
    {
        $("#users_table").find("[class='btn btn-primary btn-sm delete']").each(function (index, value)
        {
            $(value).click(function ()
            {

                $('#usertodelete').val($(this).attr("data-id-user"));

            });
        });
    }

    function attachClickToAssignButtons()
    {
        $("#users_table").find("[class='btn btn-primary btn-sm assign']").each(function (index, value) {
            $(value).click(function () {
               
                $("#usertoassign").val($(this).attr("data-id-user"));
                
                
            });
        });
    }

    function attachClickToShowButtons()
    {
        $("#users_table").find("[class='btn btn-primary btn-sm detail']").each(function (index, value)
        {
            $(value).click(function ()
            {
                showUser($(this).attr("data-id-user"));
            });
        });
    }

    function attachClickToEditButtons() {
        $("#users_table").find("[class='btn btn-primary btn-sm edit']").each(function (index, value) {
            $(value).click(function () {

                $('#usertoedit').val($(this).attr("data-id-user"));
                var idUserToEdit = $('#usertoedit').val();
                $("#tabdetails").tab("show");
                $("#screenmode").val("edit");
                setTabInEditMode();
               
                attachActionButtons();
                showUserToEdit(idUserToEdit);
                validation();
            });
        });
    }

    function attachClickToNewButton()
    {
        $("#tabdetails").click(function ()
        {
            validation();
            $("#screenmode").val("add");
            setTabInAddMode();
            attachActionButtons();
           
        });
    }

    function attachClickToListButton()
    {
        $("#tabtable").click(function ()
        {
            $("#canceluser").unbind();
            $("#saveuser").unbind();
            clearControls();
            $("#form1").data('bootstrapValidator').resetForm();
        });
    }

    function attachClickToModal()
    {
        $("#btndeleteuser").click(function ()
        {
            deleteUser();
        });
    }

    function attachClickToAssignModal() {
        $("#btnassignrole").click(function () {
            //llamar funcion asignar role
        });
    }

    function attachActionButtons()
    {
        $("#canceluser").click(function ()
        {
            $("#tabtable").tab("show");
            clearControls();
            $("#form1").data('bootstrapValidator').resetForm();

        });

        $("#saveuser").click(function ()
        {
            var formValidation = $("#form1").data('bootstrapValidator').isValid();
           
            if (formValidation)
            {
                var screenmode = $("#screenmode").val();
                saveUser(screenmode);
                $("#form1").data('bootstrapValidator').resetForm();
            }

           
        });
    }

    /*Mode Functions*/

    function setTabInAddMode() {
        
        $(".txt-controls").each(function (index, value)
        {
            if (!$(value).hasClass("hidden"))
            {
                
                $(value).addClass("hidden");
            }
        });

        $(".in-controls").each(function (index, value)
        {
            if ($(value).hasClass("hidden"))
            {

                $(value).removeClass("hidden");
            }
        });

       
    }

    function setTabInEditMode()
    {
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

    function setTabInDetailsMode()
    {
        $(".txt-controls").each(function (index, value)
        {
            if ($(value).hasClass("hidden"))
            {

                $(value).removeClass("hidden");
            }
        });

        $(".in-controls").each(function (index, value)
        {
            if (!$(value).hasClass("hidden"))
            {

                $(value).addClass("hidden");
            }
        });
    }

    /*Utility Functions*/

    function clearControls()
    {
        $(".in-controls").find("input").each(function (index, value) 
        {
            $(value).val("");
        });
        $("#canceluser").unbind();
        $("#saveuser").unbind();
    }

    function fillLblFields(usuario)
    {
        $("#lbl_username").html(usuario.NombreUsuario);
        $("#lbl_nombre").html(usuario.Nombre);
        $("#lbl_apellido").html(usuario.Apellido);
        $("#lbl_contrasenia").html('*'.repeat(usuario.Contrasenia.length));
        $("#lbl_email").html(usuario.Email);
        $('#tabdetails').tab('show');

    }

    function fillInputFields(usuario)
    {
        $("#in_username").val(usuario.NombreUsuario);
        $("#in_nombre").val(usuario.Nombre);
        $("#in_apellido").val(usuario.Apellido);
        $("#in_contrasenia").val(usuario.Contrasenia);
        $("#in_contrasenia_re").val(usuario.Contrasenia);
        $("#in_email").val(usuario.Email);
        $("#form1").data('bootstrapValidator').validate();
    }

    function validation()
    {
        $('#form1').bootstrapValidator({
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            excluded: [':disabled'],
            fields: {
                in_username: {
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
                in_nombre: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        }
                    }
                },
                in_apellido: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        }
                    }
                },
                in_contrasenia: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        }

                    }
                },
                in_contrasenia_re: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        },
                        identical: {
                            field: 'in_contrasenia',
                            message: 'La contraseña debe coincidir'
                        }
                    }
                },
                in_email: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        },
                        emailAddress: {
                            message: 'Ingrese una direccion de correo válida'
                        }
                    }
                }
            }

        }).on('bv-form', function () {
            $(this).validate();
        });
    }

    function makeAjaxCall(dataToSend, action)
    {
        $.ajax({
            type: 'POST',
            url: '/Handlers/User.ashx?method=' + action,
            data: dataToSend,
            success: function (data)
            {

                var response = JSON.parse(data);
                if (response.IsSucess)
                {
                    displayMessage(response.Message);
                    getAllUsers();
                } else
                {
                    displayErrorMessage(response.Message);
                }
            },
            error: function ()
            {
                var error = "Error de Conexión, Intente nuevamente";
                displayErrorMessage(error);
            }
        });
    }

    /*Message Functions*/

    function displayErrorMessage(message)
    {
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
        $("#btndeleteuser").unbind();
        $("#tabdetails").unbind();

    }

    function loadSidebarOptions()
    {
        var htmlToAppend = "<div class='col-md-2 col-sm-2'></div><div class='col-md-4 col-sm-4'><div class='activity_box activity_box2'><h3>Opciones</h3><div class='scrollbar' id='style-2'> <div class='activity-row activity-row1'><div class='single-bottom'><ul><li><a href='#' id='brand'> Andrew Jos</a></li><li><a href='#' id='brand1'> Action #2 Some description</a></li><li><a href='#' id='brand2'> Action #2 Some description</a> </li><li><a href='#' id='brand3'> Action #2 Some description</a></li></ul></div></div></div></div></div>";
        $(htmlToAppend).insertAfter("div[class='col-md-6 col-sm-6']");
    }

});
