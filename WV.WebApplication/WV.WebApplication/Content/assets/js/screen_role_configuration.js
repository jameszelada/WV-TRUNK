$(window).load(function () {

    var t;

    getAllRoles();

    getAllResources();


    function getAllRoles() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/RoleConfiguration.ashx?method=getallroles',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess)
                {
                    $("#menuroles").html(response.ResponseData);
                    attachClickToRoleButtons();
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

    function getAllResources() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/RoleConfiguration.ashx?method=getallresources',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    $("#tblopciones").append(response.ResponseData);
                    $("#tblopciones").DataTable({
                        "bFilter": true,
                        "bPaginate": true,
                        "bLengthChange": true,
                        "bInfo": true,
                        "pageLength": 5,
                        "aLengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
                        "oLanguage": {
                            "sEmptyTable": '',
                            "sInfoEmpty": ''
                        }

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

    }

    function showRole(Id_rol) {
        var dataToSend =
            {
                Id_Rol: Id_rol
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/RoleConfiguration.ashx?method=getsingle',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    fillLblFields(response.ResponseData);
                    
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

    function showOptionsInRole(Id_rol)
    {
        var dataToSend =
            {
                Id_Rol: Id_rol
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/RoleConfiguration.ashx?method=getoptionsbyrole',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                
                if (response.IsSucess) {

                    if (t == null || t == undefined) {
                        $("#tblopcionesenrol").append(response.ResponseData);
                        t = $('#tblopcionesenrol').DataTable({
                            "bFilter": true,
                            "bPaginate": true,
                            "bLengthChange": true,
                            "bInfo": true,
                            "pageLength": 5,
                            "aLengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
                            "oLanguage": {
                                "sEmptyTable": '',
                                "sInfoEmpty": ''
                            }
                            
                        });

                        
                       
                    }
                    else {
                        t.clear();
                        $("#tblopcionesenrol > tbody").remove();
                        t.destroy();
                        $("#tblopcionesenrol").append(response.ResponseData);
                        t = $('#tblopcionesenrol').DataTable({
                            "bFilter": true,
                            "bPaginate": true,
                            "bLengthChange": true,
                            "bInfo": true,
                            "pageLength": 5,
                            "aLengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
                            "oLanguage": {
                                "sEmptyTable": '',
                                "sInfoEmpty": ''
                            }
                            
                        });
                    }
                    
                    
                   
                   

                    attachClickToDeleteButtons();

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

    function attachClickToRoleButtons()
    {
        $("#menuroles button[type=button]").each(function (index, value) {
            $(value).click(function ()
            {
                showRole($(this).attr("data-id-role"));
                $("#roletosave").val($(this).attr("data-id-role"));
                showOptionsInRole($(this).attr("data-id-role"));
            });
        });

         $("#btnguardaropciones").click(function () {
            processResourcesToAdd();
        });
    }

    function attachClickToDeleteButtons() {
        $("#tblopcionesenrol").find("[class='btn btn-primary btn-sm delete']").each(function (index, value) {
            $(value).click(function () {

                var Id_rol = $("#roletosave").val();
                var Id_recurso = $(this).attr("data-id-resource");

                var dataToSend = {
                    Id_Rol : Id_rol,
                    Id_Recurso : Id_recurso
                };

                deleteOptionInRole(dataToSend);
              
            });
        });
    }

    function fillLblFields(rol) {
        $("#lbl_rol").html(rol.Rol1);
        $("#lbl_descripcion").html(rol.Descripcion);
        $("#btnagregaropcion").removeClass("disabled");

    }

    function processResourcesToAdd()
    {
        var selectedRows = $("#tblopciones tr:has(:checkbox:checked)");
        var resourcesInRole = $("#tblopcionesenrol tr[data-id-resource]");
        var toAdd = [];
        var toCompare = [];
        var toSave = [];
        selectedRows.each(function (index,value) {
            toAdd[index] = parseInt($(value).attr("data-id-recurso"));
        });
        resourcesInRole.each(function (index, value) {
            toCompare[index] = parseInt($(value).attr("data-id-resource"));
        });

        for (var i = 0; i < toAdd.length; i++)
        {           
            if ($.inArray(toAdd[i],toCompare) == -1) {
                toSave.push(toAdd[i]);
            }
        }

        if (toSave.length > 0)
        {
            // code to save the records
            var ID_role = $("#roletosave").val();
            var dataToSend =
            {
                ID_Rol: ID_role,
                OptionsArray : toSave
            };

            addOptionToRole(dataToSend);
        }


    }

    function deleteOptionInRole(dataToSend)
    {
        $.ajax({
            type: 'POST',
            url: '/Handlers/RoleConfiguration.ashx?method=delete',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    showOptionsInRole($('#roletosave').val());

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

    function addOptionToRole(dataToSend)
    {
        $.ajax({
            type: 'POST',
            url: '/Handlers/RoleConfiguration.ashx?method=add',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    showOptionsInRole($('#roletosave').val());

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

    }


});