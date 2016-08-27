$(window).load(function () {

    //Execution


    getAllPrograms();

    getAllProjects();

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

    //$("#cmbpersonasplan").trigger("change");

    //$("#cmbproyecto").change(function () {
    //    generateURL();
    //});

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
                    $("#cmbprograma").trigger("change"); 
                    $("#cmbprogramaactividad").trigger("change");
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


    function displayErrorMessage(message) {

        $("#errorcontainer").html(message);
        $("#myErrorDialog").modal('show');
    }

    function displayMessage(message) {


        $("#messagecontainer").html(message);
        $("#myMessageDialog").modal('show');

    }
});