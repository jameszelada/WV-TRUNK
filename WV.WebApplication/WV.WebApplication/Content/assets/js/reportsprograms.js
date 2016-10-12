$(window).load(function () {

    //Execution


    getAllPrograms();

    getAllProjects();

    getAllCommunities();

    getAttendancePrograms();

    ////getAllRoles();

    $("#cmbprograma").change(function () {
        generateURL();
    });

    $("#cmbproyecto").change(function () {
        generateURLProject();
    });

    $("#cmbproyecto").trigger("change");


    $("#cmbprogramaactividad").change(function () {
        generateURLActivity();
    });

    $("#cmbprogramabeneficiario").change(function () {
        generateURLBeneficiary();
    });

    $("#cmbcomunidadbeneficiario").change(function () {
        generateURLCommunity();
    });

    //$("#cmbpersonasplan").trigger("change");

    $("#cmbprogramaasistencia").change(function () {
        getActivities();
    });

    $("#cmbprogramaasistencia").trigger("change");

    $("#cmbactividad").change(function () {
        generateURLAttendance();
    });

    //$("#cmbproyecto").trigger("change");

    //$("#cmbfechabitacora").change(function () {
    //    generateURLLogbook();
    //});

    //$("#cmbfechaplan").change(function () {
    //    generateURLWeeklyPlan();
    //});




    function getAllPrograms() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/GeneralReportsProgram.ashx?method=getallprograms',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    $("#cmbprograma").html(response.ResponseData); 
                    $("#cmbprogramaactividad").html(response.ResponseData);
                    $("#cmbprogramabeneficiario").html(response.ResponseData); 
                    $("#cmbprograma").trigger("change"); 
                    $("#cmbprogramaactividad").trigger("change");
                    $("#cmbprogramabeneficiario").trigger("change");
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

    function getActivities() {
        var ID_Programa = $("#cmbprogramaasistencia > option:selected").attr("data-id-programs");
        var dataToSend =
            {
                ID_Programa: ID_Programa
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/GeneralReportsProgram.ashx?method=getallactivities',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    $("#cmbactividad").html(response.ResponseData);
                    $("#cmbactividad").trigger("change");
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

    function getAttendancePrograms() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/GeneralReportsProgram.ashx?method=getallprograms',
            async:false,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    $("#cmbprogramaasistencia").html(response.ResponseData);
                    
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

    function getAllProjects() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/GeneralReportsProject.ashx?method=getallprojects',
            async: false,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    $("#cmbproyecto").html(response.ResponseData);
                    //$("#cmbpersonasplan").html(response.ResponseData);

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

    function getAllCommunities() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/GeneralReportsProgram.ashx?method=getallcommunities',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    $("#cmbcomunidadbeneficiario").html(response.ResponseData);
                    $("#cmbcomunidadbeneficiario").trigger("change");
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

    function getLogBooks() {
        var ID_Persona = $("#cmbpersonas > option:selected").attr("data-id-person");
        var dataToSend =
            {
                ID_Persona: ID_Persona
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/GeneralReportsProject.ashx?method=getalllogbooks',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    $("#cmbfechabitacora").html(response.ResponseData);
                    $("#cmbfechabitacora").trigger("change");
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

    function getWeeklyPlans() {
        var ID_Persona = $("#cmbpersonasplan > option:selected").attr("data-id-person");
        var dataToSend =
            {
                ID_Persona: ID_Persona
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/GeneralReportsProject.ashx?method=getallweeklyplans',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    $("#cmbfechaplan").html(response.ResponseData);
                    $("#cmbfechaplan").trigger("change");
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

    function generateURL() {
        var programID = $("#cmbprograma > option:selected").attr("data-id-programs");
        $("#btnreporteprograma").attr("href", "/Handlers/GeneralReportsProgram.ashx?method=getprogramreport&ID_Programa=" + programID);
    }

    function generateURLProject() {
        var projectID = $("#cmbproyecto > option:selected").attr("data-id-project");
        $("#btnreporteproyecto").attr("href", "/Handlers/GeneralReportsProgram.ashx?method=getprojectreport&ID_Proyecto=" + projectID);
    }

    function generateURLActivity() {
        var programID = $("#cmbprogramaactividad > option:selected").attr("data-id-programs");
        $("#btnreporteprogramaactividad").attr("href", "/Handlers/GeneralReportsProgram.ashx?method=getactivityreport&ID_Programa=" + programID);
    }

    function generateURLBeneficiary() {
        var programID = $("#cmbprogramabeneficiario > option:selected").attr("data-id-programs");
        $("#btnreportebeneficiarioprograma").attr("href", "/Handlers/GeneralReportsProgram.ashx?method=getbeneficiaryprogramreport&ID_Programa=" + programID);
    }

    function generateURLCommunity() {
        var communityID  = $("#cmbcomunidadbeneficiario > option:selected").attr("data-id-community");
        $("#btnreportebeneficiariopcomunidad").attr("href", "/Handlers/GeneralReportsProgram.ashx?method=getbeneficiarycommunityreport&ID_Comunidad=" + communityID);
    }

    function generateURLAttendance() {
        var programID = $("#cmbprogramaasistencia > option:selected").attr("data-id-programs");
        var activityID = $("#cmbactividad > option:selected").attr("data-id-activity");

        if (activityID != undefined) {
            $("#btngenerarreporte").attr("href", "/Handlers/GeneralReportsProgram.ashx?method=getattendancereport&ID_Programa=" + programID + "&ID_Actividad=" + activityID);
        }
        else {
            $("#btngenerarreporte").attr("href", "javascript:void(0)");
        }

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
});