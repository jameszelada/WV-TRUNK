$(window).load(function () {

    var t;

    getAllSubjects();

    getAllPeople();


    function getAllSubjects() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/AssignSubject.ashx?method=getall',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    $("#cmbmateria").html(response.ResponseData);
                    attachClickToDeleteButtons();
                    attachClickSubjectOptions();
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

    function getAllPeople() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/AssignSubject.ashx?method=getmembers',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    $("#tblbeneficiarios").append(response.ResponseData);
                    $("#tblbeneficiarios").DataTable({
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
                            "info": "Mostrando pagina _PAGE_ de _PAGES_"

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

    

    function showMembersInSubject(ID_Materia) {
        var dataToSend =
            {
                ID_Materia: ID_Materia
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/AssignSubject.ashx?method=getassignations',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);

                if (response.IsSucess) {

                    if (t == null || t == undefined) {
                        $("#dataTables-example").append(response.ResponseData);
                        t = $('#dataTables-example').DataTable({
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
                                "info": "Mostrando pagina _PAGE_ de _PAGES_"

                            }

                        });



                    }
                    else {
                        t.clear();
                        $("#dataTables-example > tbody").remove();
                        t.destroy();
                        $("#dataTables-example").append(response.ResponseData);
                        attachClickToDeleteButtons();
                        t = $('#dataTables-example').DataTable({
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
                                "info": "Mostrando pagina _PAGE_ de _PAGES_"

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

    function attachClickSubjectOptions() {

        $("#cmbmateria").change(function () {
          
            var option = $('option:selected', this).attr('data-id-subject');
            $("#subjecttoassign").val(option);
            showMembersInSubject(option);

        });

        $("#btnguardarasignacion").click(function () {
            processMembersToAdd();
        });

        $("#cmbmateria").trigger("change");
    }

    function attachClickToDeleteButtons() {
        $("#dataTables-example").find("[class='btn btn-primary btn-sm delete']").each(function (index, value) {
            $(value).click(function () {

                var ID_Materia = $("#subjecttoassign").val();
                var ID_Beneficiario = $(this).attr("data-id-beneficiario");

                var dataToSend = {
                    ID_Materia: ID_Materia,
                    ID_Beneficiario: ID_Beneficiario
                };

                deleteMemberInSubject(dataToSend);

            });
        });
    }

    function fillLblFields(rol) {
        $("#lbl_rol").html(rol.Rol1);
        $("#lbl_descripcion").html(rol.Descripcion);
        $("#btnagregaropcion").removeClass("disabled");

    }

    function processMembersToAdd() {
        var selectedRows = $("#tblbeneficiarios tr:has(:checkbox:checked)");
        var membersInsubject = $("#dataTables-example tr[data-id-beneficiario]");
        var toAdd = [];
        var toCompare = [];
        var toSave = [];
        selectedRows.each(function (index, value) {
            toAdd[index] = parseInt($(value).attr("data-id-beneficiario"));
        });
        membersInsubject.each(function (index, value) {
            toCompare[index] = parseInt($(value).attr("data-id-beneficiario"));
        });

        for (var i = 0; i < toAdd.length; i++) {
            if ($.inArray(toAdd[i], toCompare) == -1) {
                toSave.push(toAdd[i]);
            }
        }

        if (toSave.length > 0) {
            // code to save the records
            var ID_Materia = $("#subjecttoassign").val();
            var dataToSend =
            {
                ID_Materia: ID_Materia,
                BeneficiariosArray: toSave
            };

            addMemberToSubject(dataToSend);
        }


    }

    function deleteMemberInSubject(dataToSend) {
        $.ajax({
            type: 'POST',
            url: '/Handlers/AssignSubject.ashx?method=delete',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    showMembersInSubject($('#subjecttoassign').val());

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

    function addMemberToSubject(dataToSend) {
        $.ajax({
            type: 'POST',
            url: '/Handlers/AssignSubject.ashx?method=add',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    showMembersInSubject($('#subjecttoassign').val());

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


});