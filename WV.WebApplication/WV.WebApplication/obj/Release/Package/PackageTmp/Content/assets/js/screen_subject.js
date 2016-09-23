$(window).load(function () {
    
    CleanTabState();
    var t;
    getSubjects();

    // End of Execution

    /*CRUD Functions*/

    function getSubjects() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/Subject.ashx?method=getall',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {

                    setSubjectsTable(response.ResponseData);
                    attachClickToModal();
                    
                   // attachClickToModal();

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


    function saveSubject(screenmode) {

        if (screenmode === "add") {
            var dataToSend =
                {
                    nombre: $("#in_nombre_materia").val(),
                    grado: $("#cmbgrado > option:selected").html(),
                    anio: $("#cmbanio > option:selected").html()
                    
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
                    ID_Materia: id,
                    nombre: $("#in_nombre_materia").val(),
                    grado: $("#cmbgrado > option:selected").html(),
                    anio: $("#cmbanio > option:selected").html()

                };

            makeAjaxCall(dataToSend, screenmode);
            //$("#tabtable").tab("show");
            $("#tabtable").tab("show");
            $("#cancelpage").unbind();
            $("#savepage").unbind();
        }
    }

    function makeAjaxCall(dataToSend, action) {
        $.ajax({
            type: 'POST',
            url: '/Handlers/Subject.ashx?method=' + action,
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getSubjects();
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




    function setSubjectsTable(responseData) {

        if (t == null || t == undefined) {
            $("#dataTables-example").append(responseData);
            attachClickToShowSubjectButtons();
            attachClickToEditSubjectButtons();
            attachClickToDeleteButtons();
            attachClickToListButton();
            if (Security.agregar) {
                attachClickToNewButton();
            }

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
            $("#dataTables-example").append(responseData);
            attachClickToShowSubjectButtons();
            attachClickToEditSubjectButtons();
            attachClickToDeleteButtons();
            attachClickToListButton();
            if (Security.agregar) {
                attachClickToNewButton();
            };
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
        //Permissions
        applyOptionPermissions(t);
        validation();
        
    }

    function fillLblFields(materia) {
        $("#lbl_nombre_materia").html(materia.Nombre);
        $("#lbl_anio").html(materia.Anio);
        $("#lbl_grado").html(materia.Grado);


        $('#tabdetails').tab('show');
    }

    function fillInputFields(materia) {

        $("#in_nombre_materia").val(materia.Nombre);
        $("#cmbgrado > option").each(function (index, value) {

                if ($(value).html() == materia.Grado) {
                    $(value).prop('selected', true);
                    return false;

                }

        });
        $("#cmbanio > option").each(function (index, value) {

            if ($(value).html() == materia.Anio) {
                $(value).prop('selected', true);
                return false;

            }

        });
           
        


        $("#form1").data('bootstrapValidator').validate();
    }


    function showSubject(ID_Materia) {
        var dataToSend =
            {
                ID_Materia: ID_Materia
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Subject.ashx?method=getsingle',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {

                    
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

    function showSubjectToEdit(ID_Materia) {
        var dataToSend =
            {
                ID_Materia: ID_Materia
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Subject.ashx?method=getsingle',
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

    function deleteSubject() {
        var idToDelete = $("#pagetodelete").val();
        var dataToSend =
            {
                ID_Materia: idToDelete
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Subject.ashx?method=delete',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getSubjects();
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

    function attachClickToShowSubjectButtons() {
        $("#table-responsive").find("[class='btn btn-primary btn-sm detail']").each(function (index, value) {
            $(value).click(function () {
              
                $('#pagetoshow').val($(this).attr("data-id-subject"));
                showSubject($(this).attr("data-id-subject"));
                
            });
        });
    }

    function attachClickToEditSubjectButtons() {
        $("#table-responsive").find("[class='btn btn-primary btn-sm edit']").each(function (index, value) {
            $(value).click(function () {
               // window.location.href = "/WeeklyPlan?id=" + $(this).attr("data-id-person");
                $('#pagetoedit').val($(this).attr("data-id-subject"));
                //var idProyecto = $('#pagetoedit').val();
                $("#tabdetails").tab("show");
                $("#screenmode").val("edit");
                setTabInEditMode();
                clearControls();
                attachActionButtons();
                showSubjectToEdit($(this).attr("data-id-subject"));
                validation();
            });
        });
    }

    function attachClickToDeleteButtons() {
        $("#table-responsive").find("[class='btn btn-primary btn-sm delete']").each(function (index, value) {
            $(value).click(function () {

                $('#pagetodelete').val($(this).attr("data-id-subject"));

            });
        });
    }

    function attachClickToNewButton() {
        $("#tabdetails").click(function () {
            $("#cmbanio > option").each(function (index, value) { $(value).removeAttr("selected") });
            $("#cmbgrado > option").each(function (index, value) { $(value).removeAttr("selected") });
            validation();
            $("#screenmode").val("add");
            setTabInAddMode();
            clearControls();
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
                saveSubject(screenmode);
                clearControls();
                $("#form1").data('bootstrapValidator').resetForm();
            }


        });
    }

    function attachClickToModal() {
        $("#pagebtndelete").click(function () {
            deleteSubject();
        });

    }


    function clearControls() {
        $(".in-controls").find("input").each(function (index, value) {
            $(value).val("");
        });
        $("#in_descripcion").val("");
        $("#cancelpage").unbind();
        $("#savepage").unbind();
    }

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

    function validation() {
        $('#form1').bootstrapValidator({
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            excluded: [':disabled'],
            fields: {
                in_nombre_materia: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        },
                        stringLength: {
                            message: 'El campo debe tener mas de 5 caracteres',
                            min: 5
                        }
                    },
                    
                }
               
                
            }

        }).on('bv-form', function () {
            $(this).validate();
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

    function applyOptionPermissions(table) {
        if (!Security.editar) {
            table.column(5).visible(false);
        }
        if (!Security.eliminar) {
            table.column(6).visible(false);
        }
    }

    function CleanTabState() {
        $('#tabtable').on('shown.bs.tab', function (e) {
            setTabInDetailsMode();
        });
    }

});