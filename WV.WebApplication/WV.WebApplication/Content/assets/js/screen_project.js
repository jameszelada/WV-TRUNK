
$(window).load(function () {

    //Execution
    CleanTabState();

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

    function getRRHH(ID_Project) {
        
        var dataToSend =
            {
                Id_Proyecto: ID_Project
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Project.ashx?method=getrrhh',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    $("#rrhh").html(response.ResponseData);
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

    function deleteRRHH(ID_Project) {

        var dataToSend =
            {
                Id_Proyecto: ID_Project
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/Project.ashx?method=deleterrhh',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getRRHH($('#pagetoshow').val());
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
        var idProject = ID_Project;
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
                    if (!$("#sidebaroptions").length)
                    {
                        loadSidebarOptions(idProject);
                        attachClickToStatisticsLink(idProject);
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

    function attachClickToStatisticsLink(ID_Proyecto) {

        $("#showstatistics").click(function () {

            var dataToSend =
            {
                ID_Proyecto: ID_Proyecto
            };

            $.ajax({
                type: 'POST',
                url: '/Handlers/ProjectSummary.ashx?method=getprojectsummary',
                async: false,
                data: dataToSend,
                success: function (data) {

                    var response = JSON.parse(data);
                    if (response.IsSucess) {

                        var options = {
                            responsive: true,
                            maintainAspectRatio: true
                        }

                        $("#numberofmechanismscontainer").html(response.ResponseData.ProgramType);
                        $("#numberofbeneficiariescontainer").html(response.ResponseData.Beneficiaries);

                        var xValues1 = Object.getOwnPropertyNames(response.ResponseData.Chart2);
                        var yValues1 = $.map(response.ResponseData.Chart2, function (val, key) { return val; });
                        var xValues2 = Object.getOwnPropertyNames(response.ResponseData.Chart4);
                        var yValues2 = $.map(response.ResponseData.Chart4, function (val, key) { return val; });
                        var xValues3 = Object.getOwnPropertyNames(response.ResponseData.Chart1);
                        var yValues3 = $.map(response.ResponseData.Chart1, function (val, key) { return val; });
                        var xValues4 = Object.getOwnPropertyNames(response.ResponseData.Chart3);
                        var yValues4 = $.map(response.ResponseData.Chart3, function (val, key) { return val; });

                        var dataPie = {
                            labels: xValues1,
                            datasets: [
                                {
                                    data: yValues1,
                                    backgroundColor: [
                                        "#FF6384",
                                        "#36A2EB"
                                    ],
                                    hoverBackgroundColor: [
                                        "#FF6384",
                                        "#36A2EB"
                                    ]
                                }]
                        };

                        var context = $("#chart2container");
                        context.get(0).getContext('2d');
                        var Chart1 = new Chart(context, {
                            type: "pie",
                            data: dataPie,
                            options: options
                        });

                        var dataDoughnut = {
                            labels: xValues2,
                            datasets: [
                                {
                                    data: yValues2,
                                    backgroundColor: [
                                        "#FF6384",
                                        "#36A2EB",
                                        "#FFCE56"
                                    ],
                                    hoverBackgroundColor: [
                                        "#FF6384",
                                        "#36A2EB",
                                        "#FFCE56"

                                    ]
                                }]
                        };

                        var context2 = $("#chart4container");
                        context2.get(0).getContext('2d');
                        var Chart1 = new Chart(context2, {
                            type: "doughnut",
                            data: dataDoughnut,
                            options: options
                        });

                        var backgroundColors = [];
                        var borderColors = [];

                        for (var i = 0; i < yValues3.length; i++) {
                            backgroundColors.push('#' + (Math.random() * 0xFFFFFF << 0).toString(16));
                            borderColors.push('rgba(255,99,132,1)');
                        }

                        var dataBar = {
                            labels: xValues3,
                            datasets: [
                                {
                                    label: "Programas por tipo de Mecanismo",
                                    backgroundColor: backgroundColors,
                                    borderColor: borderColors,
                                    borderWidth: 1,
                                    data: yValues3,
                                }
                            ]
                        };

                        var context3 = $("#chart1container");
                        context3.get(0).getContext('2d');
                        var Chart3 = new Chart(context3, {
                            type: "horizontalBar",
                            data: dataBar,
                            options: options
                        });

                        var dataBar1 = {
                            labels: xValues4,
                            datasets: [
                                {
                                    label: "",
                                    backgroundColor: backgroundColors,
                                    borderColor: borderColors,
                                    borderWidth: 1,
                                    data: yValues4,
                                }
                            ]
                        };

                        var context4 = $("#chart3container");
                        context4.get(0).getContext('2d');
                        var Chart4 = new Chart(context4, {
                            type: "horizontalBar",
                            data: dataBar1,
                            options: options
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

            $('#modalcharts').modal({
                keyboard: false
            })

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
        applyOptionPermissions();
        validation();
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
                var Id_Project = $(this).attr("data-id-project");
                $('#pagetoshow').val(Id_Project);
                showProject(Id_Project);
                getRRHH(Id_Project);
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
            $("#sidebaroptions").remove();
            clearControls();
            $("#form1").data('bootstrapValidator').resetForm();
        });
    }

    function attachClickToModal() {
        $("#pagebtndelete").click(function () {
            deleteProject();
        });

        $("#pagebtndeleteassign").click(function () {
            deleteRRHH($('#pagetoshow').val());
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
                saveProject(screenmode);
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
        $("#in_descripcion").val("");
        
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
                            message: 'Mínimo 4 caracteres, Máximo 20',
                            max: 20,
                            min: 4
                        },
                        regexp: {
                            regexp: /^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s]+$/i,
                            message: 'Solo caracteres alfanuméricos'
                        }
                    },

                },
                in_descripcion: {
                    validators: {
                        notEmpty: {
                            message: 'Este campo es requerido.'
                        },
                        stringLength: {
                            message: 'Mínimo 4 caracteres, Máximo 1000',
                            max: 1000,
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

    function loadSidebarOptions(idProject) {
        var htmlToAppend = "<div class='col-md-2 col-sm-2'></div><div id='sidebaroptions' class='col-md-4 col-sm-4'><div class='activity_box activity_box2'><h3 style='color:#999'>Accesos rápidos</h3><div class='scrollbar' id='style-2'> <div class='activity-row activity-row1'><div class='single-bottom'><ul><li><a id='deleterrhhasign' href='#gridSystemModal'  data-toggle='modal'> Eliminar Asignación de RRHH</a></li><li><a id='showprojectreport' href='/Handlers/GeneralReportsProject.ashx?method=getsummaryseport&ID_Proyecto=" + idProject + "'> Reporte General de Proyecto</a></li><li><a id='showstatistics' href='#'>Estadísticas</a></li></ul></div></div></div></div></div>";
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
