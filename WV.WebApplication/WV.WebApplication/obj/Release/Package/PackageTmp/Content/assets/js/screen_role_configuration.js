$(window).load(function () {

    var t;
    $(':checkbox').checkboxpicker();
    getAllRoles();

    getAllResources();

    AttachClickToSavePermissionsButton();


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
                        "aLengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
                        language: {
                            searchPlaceholder: "Búsqueda",
                            "search": "Buscar",
                            "emptyTable": "No hay datos encontrados",
                            "zeroRecords": "No hay datos disponibles",
                            "lengthMenu": "_MENU_ registros por página",
                            "info": "Mostrando pagina _PAGE_ de _PAGES_",
                            "paginate": {
                                "previous": "Anterior",
                                "next": "Siguiente"
                            }
                            
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
                        attachClickToDeleteButtons();
                        attachClickToPermissions();
                        t = $('#tblopcionesenrol').DataTable({
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
                                "info": "Mostrando pagina _PAGE_ de _PAGES_",
                                "paginate": {
                                    "previous": "Anterior",
                                    "next": "Siguiente"
                                }

                            }
                            
                        });

                        
                       
                    }
                    else {
                        t.clear();
                        $("#tblopcionesenrol > tbody").remove();
                        t.destroy();
                        $("#tblopcionesenrol").append(response.ResponseData);
                        attachClickToDeleteButtons();
                        attachClickToPermissions();
                        t = $('#tblopcionesenrol').DataTable({
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
                                "info": "Mostrando pagina _PAGE_ de _PAGES_",
                                "paginate": {
                                    "previous": "Anterior",
                                    "next": "Siguiente"
                                }

                            }
                            
                        });
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

    function AttachClickToSavePermissionsButton()
    {
        $("#btnguardarpermisos").click(function () {

            var ID_Role = $("#roletosave").val();
            var Id_recurso = $("#identrecurso").html();

            var permissionsArray = [];
            permissionsArray.push($('#opcionagregar').is(':checked'));
            permissionsArray.push($('#opcioneditar').is(':checked'));
            permissionsArray.push($('#opcioneliminar').is(':checked'));
            
            var dataToSend =
            {
                ID_Rol: ID_Role,
                ID_Recurso : Id_recurso,
                OptionsArray: permissionsArray
            };

            modifyPermissions(dataToSend);

        });

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

    function getSingleOption(dataToSend) {
        $.ajax({
            type: 'POST',
            url: '/Handlers/RoleConfiguration.ashx?method=getsingleoption',
            data: dataToSend,
            async:false,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    
                    $('#opcionagregar').prop('checked', response.ResponseData.Agregar);
                    $('#opcioneditar').prop('checked', response.ResponseData.Modificar);
                    $('#opcioneliminar').prop('checked', response.ResponseData.Eliminar);

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

    function attachClickToPermissions() {
        $("#tblopcionesenrol").find("[class='btn btn-primary btn-sm permissions']").each(function (index, value) {
            $(value).click(function () {

                $("#identrecurso").html($(this).parent().parent().attr("data-id-resource"));
                $("#nombreopcion").html($(this).parent().parent().find('td').eq(1).html());

                var Id_rol = $("#roletosave").val();
                var Id_recurso = $("#identrecurso").html();

                var dataToSend = {
                    Id_Rol: Id_rol,
                    Id_Recurso: Id_recurso
                };

                getSingleOption(dataToSend);
                
                $('#modalpermissions').modal({
                    keyboard: false
                })

            });
        });
    }

    function modifyPermissions(dataToSend)
    {
        $.ajax({
            type: 'POST',
            url: '/Handlers/RoleConfiguration.ashx?method=edit',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                   

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



});