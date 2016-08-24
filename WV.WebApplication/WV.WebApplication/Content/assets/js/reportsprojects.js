$(window).load(function () {

    //Execution


    getAllPersons();

    getAllProjects();

    //getAllRoles();

    $("#cmbpersonas").change(function () {
        getLogBooks();
    });

    $("#cmbpersonas").trigger("change");

    $("#cmbpersonasplan").change(function () {
        getWeeklyPlans();
    });

    $("#cmbpersonasplan").trigger("change"); 

    $("#cmbproyecto").change(function () {
        generateURL();
    });

    $("#cmbproyecto").trigger("change");

    $("#cmbfechabitacora").change(function () {
        generateURLLogbook();
    });

    $("#cmbfechaplan").change(function () {
        generateURLWeeklyPlan();
    });

    


    function getAllPersons() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/GeneralReportsProject.ashx?method=getallpersons',
            async: false,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    $("#cmbpersonas").html(response.ResponseData);
                    $("#cmbpersonasplan").html(response.ResponseData);

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
        var projectID = $("#cmbproyecto > option:selected").attr("data-id-project");
        $("#btnreporteasignacion").attr("href", "/Handlers/GeneralReportsProject.ashx?method=getassignreport&ID_Proyecto=" + projectID);
    }

    function generateURLLogbook() {
        var personID = $("#cmbpersonas > option:selected").attr("data-id-person");
        var logbookID = $("#cmbfechabitacora > option:selected").attr("data-id-logbook");

        if (logbookID != undefined)
        {
            $("#btnreportebitacora").attr("href", "/Handlers/GeneralReportsProject.ashx?method=getlogbookreport&ID_Persona=" + personID + "&ID_Bitacora=" + logbookID);
        }
        else
        {
            $("#btnreportebitacora").attr("href", "javascript:void(0)");
        }

    }

    function generateURLWeeklyPlan() {
        var personID = $("#cmbpersonasplan > option:selected").attr("data-id-person");
        var weeklyplanID = $("#cmbfechaplan > option:selected").attr("data-id-weeklyplan");

        if (weeklyplanID != undefined) {
            $("#btnreporteplan").attr("href", "/Handlers/GeneralReportsProject.ashx?method=getweeklyplanreport&ID_Persona=" + personID + "&ID_PlanSemanal=" + weeklyplanID);
        }
        else {
            $("#btnreporteplan").attr("href", "javascript:void(0)");
        }

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