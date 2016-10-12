$(window).load(function () {
    CleanTabState();
    var t;
    getExams();
    initialization();


    /*CRUD Functions*/

    function getExams() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/Exam.ashx?method=getall',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {

                    setExamsTable(response.ResponseData);
                    attachSubjects();
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

    function initialization() {
        $("#fileupload").change(function () {
            $("#upload-file-info").html(this.files[0].name);
        });
    }

    function attachSubjects() {

        $.ajax({
            type: 'POST',
            url: '/Handlers/Exam.ashx?method=subjects',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    //to implement
                    loadSelect(response.ResponseData);

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

    function loadSelect(fullObject) {
        $("#cmbmateria").html(fullObject);
    }


    function saveExam(screenmode) {

        if (screenmode === "add") {
            //var data = new FormData();
            //var file = $("#fileupload").prop("files");
            //data.append('archivo',file[0]);
            var dataToSend =
                {
                    numeroExamen: $("#in_examen").val(),
                    archivo: $("#upload-file-info").html(),
                    ID_Materia: $("#cmbmateria > option:selected").attr("data-id-subject")
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
                    ID_Examen: id,
                    numeroExamen: $("#in_examen").val(),
                    archivo: $("#upload-file-info").html(),
                    ID_Materia: $("#cmbmateria > option:selected").attr("data-id-subject")
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
            url: '/Handlers/Exam.ashx?method=' + action,
            data: dataToSend,
            async:false,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {

                    postFileToServer(response.ResponseData)
                    displayMessage(response.Message);
                    getExams();
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




    function setExamsTable(responseData) {

        if (t == null || t == undefined) {
            $("#dataTables-example").append(responseData);
            attachClickToShowExamButtons();
            attachClickToEditExamButtons();
            attachClickToDeleteButtons();
            attachClickToListButton();
            if (Security.agregar) {
                attachClickToNewButton();
            }
            else
            {
                $("#tabdetails").html("Detalles");
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
            $("#dataTables-example > tbody").remove();
            t.destroy();
            $("#dataTables-example").append(responseData);
            attachClickToShowExamButtons();
            attachClickToEditExamButtons();
            attachClickToDeleteButtons();
            attachClickToListButton();
            if (Security.agregar) {
                attachClickToNewButton();
            }
            else
            {
                $("#tabdetails").html("Detalles");
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
                    "info": "Mostrando pagina _PAGE_ de _PAGES_",
                    "paginate": {
                        "previous": "Anterior",
                        "next": "Siguiente"
                    }

                }


            });
        }
        //Permissions
        applyOptionPermissions(t);
        validation();


    }

    function fillLblFields(examen) {
        $("#lbl_examen").html(examen.NumeroExamen);
        $("#lbl_materia").html(examen.NombreMateria);

        if (examen.Archivo === "") {
            $("#link_documento").html("Sin Documento");
        }

        else
        {
            $("#link_documento").html("<a href='/Handlers/Exam.ashx?method=downloadattachment&ID_Examen="+examen.ID_Examen+"'>Descargar Archivo</a>");
        }
        //$("#lbl_materia").html(examen.Archivo);


        $('#tabdetails').tab('show');
    }

    function fillInputFields(examen) {

        $("#in_examen").val(examen.NumeroExamen);
        $("#cmbmateria > option[data-id-subject='" + examen.ID_Materia + "']").attr("selected", "selected");
        //$("#cmbgrado > option").each(function (index, value) {

        //    if ($(value).html() == materia.Grado) {
        //        $(value).prop('selected', true);
        //        return false;

        //    }

        //});
        //$("#cmbanio > option").each(function (index, value) {

        //    if ($(value).html() == materia.Anio) {
        //        $(value).prop('selected', true);
        //        return false;

        //    }

        //});
        $("#upload-file-info").html(examen.Archivo);




        $("#form1").data('bootstrapValidator').validate();
    }


    function showExam(ID_Examen) {
        var dataToSend =
            {
                ID_Examen: ID_Examen
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Exam.ashx?method=getsingle',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {


                    fillLblFields(response.ResponseData);
                    setTabInDetailsMode();
                    if (!$("#sidebaroptions").length) {
                        loadSidebarOptions(response.ResponseData.ID_Materia, response.ResponseData.ID_Examen);
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

    function showExamToEdit(ID_Examen) {
        var dataToSend =
            {
                ID_Examen: ID_Examen
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Exam.ashx?method=getsingle',
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

    function deleteExam() {
        var idToDelete = $("#pagetodelete").val();
        var dataToSend =
            {
                ID_Examen: idToDelete
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Exam.ashx?method=delete',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getExams();
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

    function attachClickToShowExamButtons() {
        $("#table-responsive").find("[class='btn btn-primary btn-sm detail']").each(function (index, value) {
            $(value).click(function () {

                $('#pagetoshow').val($(this).attr("data-id-exam"));
                showExam($(this).attr("data-id-exam"));

            });
        });
    }

    function attachClickToEditExamButtons() {
        $("#table-responsive").find("[class='btn btn-primary btn-sm edit']").each(function (index, value) {
            $(value).click(function () {
                // window.location.href = "/WeeklyPlan?id=" + $(this).attr("data-id-person");
                $('#pagetoedit').val($(this).attr("data-id-exam"));
                //var idProyecto = $('#pagetoedit').val();
                $("#tabdetails").tab("show");
                $("#screenmode").val("edit");
                setTabInEditMode();
                clearControls();
                attachActionButtons();
                showExamToEdit($(this).attr("data-id-exam"));
                validation();
            });
        });
    }

    function attachClickToDeleteButtons() {
        $("#table-responsive").find("[class='btn btn-primary btn-sm delete']").each(function (index, value) {
            $(value).click(function () {

                $('#pagetodelete').val($(this).attr("data-id-exam"));

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
            $("#sidebaroptions").remove();
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
           $("#form1").data('bootstrapValidator').validate();
            var formValidation = $("#form1").data('bootstrapValidator').isValid();

            if (formValidation) {
                var screenmode = $("#screenmode").val();
                saveExam(screenmode);
                clearControls();
                $("#form1").data('bootstrapValidator').resetForm();
            }


        });
    }

    function attachClickToModal() {
        $("#pagebtndelete").click(function () {
            deleteExam();
        });

    }


    function clearControls() {
        $(".in-controls").find("input").each(function (index, value) {
            $(value).val("");
        });
        
        $("#upload-file-info").html("");
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
                in_examen: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        },
                        stringLength: {
                            message: 'Mínimo 4 caracteres, Máximo 25',
                            max: 25,
                            min: 4
                        }
                    }

                }


            }

        }).on('bv-form', function () {
            $(this).validate();
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

    function loadSidebarOptions(idMateria,idExamen) {
        var htmlToAppend = "<div class='col-md-2 col-sm-2'></div><div id='sidebaroptions' class='col-md-4 col-sm-4'><div class='activity_box activity_box2'><h3 style='color:#999'>Accesos Rápidos</h3><div class='scrollbar' id='style-2'> <div class='activity-row activity-row1'><div class='single-bottom'><ul><li><a id='showrolereport' href='/Handlers/GeneralReportsGrades.ashx?method=getresultsreport&ID_Materia="+idMateria+"&ID_Examen="+idExamen+"'>Reporte de resultados por examen</a></li></ul></div></div></div></div></div>";
        $(htmlToAppend).insertAfter("div[class='col-md-6 col-sm-6']");
    }


    function postFileToServer(ID_Exam)
    {
      
        var fileUpload = $("#fileupload").get(0);
            var files = fileUpload.files;
            var dataToSend = new FormData();
            dataToSend.append("ID_Examen",ID_Exam);
            for (var i = 0; i < files.length; i++) {
                dataToSend.append(files[i].name, files[i]);
            }
      
            if (files.length > 0)
            {
                $.ajax({
                    url: "/Handlers/Exam.ashx?method=uploadfile",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: dataToSend,
                    // dataType: "json",
                    success: function (data) {

                        var response = JSON.parse(data);
                        if (response.IsSucess) {
                           

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