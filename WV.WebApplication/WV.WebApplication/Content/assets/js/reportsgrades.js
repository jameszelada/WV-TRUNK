$(window).load(function () {

    //Execution


    getAllSubjects();



    $("#cmbmaterias").change(function () {
        generateURL();
    });

    $("#cmbmaterias").trigger("change");

    $("#cmbmateriasconsolidado").change(function () {
        generateURLSummaryReport();
    });

    $("#cmbmateriasconsolidado").trigger("change");


    $("#cmbmateriaconfigurar").change(function () {
        getExams();
    });

    $("#cmbmateriaconfigurar").trigger("change");

    $("#cmbexamenconfigurar").change(function () {
        generateURLExams();
    });



    function getAllSubjects() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/GeneralReportsGrades.ashx?method=getallsubjects',
            async: false,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    $("#cmbmaterias").html(response.ResponseData);
                    $("#cmbmateriasconsolidado").html(response.ResponseData);
                    $("#cmbmateriaconfigurar").html(response.ResponseData);

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

    function getExams() {
        var ID_Materia = parseInt($("#cmbmateriaconfigurar > option:selected").attr("data-id-subject"));
        var dataToSend =
            {
                ID_Materia: ID_Materia
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/GeneralReportsGrades.ashx?method=getallexams',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    $("#cmbexamenconfigurar").html(response.ResponseData);
                    $("#cmbexamenconfigurar").trigger("change");
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
        var subjectID = $("#cmbmaterias > option:selected").attr("data-id-subject");
        $("#btnreporteinscritos").attr("href", "/Handlers/GeneralReportsGrades.ashx?method=getinscriptionreport&ID_Materia=" + subjectID);
    }

    function generateURLSummaryReport() {
        var subjectID = $("#cmbmateriasconsolidado > option:selected").attr("data-id-subject");
        $("#btnreporteconsolidado").attr("href", "/Handlers/GeneralReportsGrades.ashx?method=getsummaryreport&ID_Materia=" + subjectID);
    }

    function generateURLExams() {
        var subjectID = $("#cmbmateriaconfigurar > option:selected").attr("data-id-subject");
        var examID = $("#cmbexamenconfigurar > option:selected").attr("data-id-exam");

        if (examID != undefined) {
            $("#btngenerarreporte").attr("href", "/Handlers/GeneralReportsGrades.ashx?method=getresultsreport&ID_Materia=" + subjectID + "&ID_Examen=" + examID);
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
        //$("#pagebtndelete").unbind();
        //$("#tabdetails").unbind();
    }
});