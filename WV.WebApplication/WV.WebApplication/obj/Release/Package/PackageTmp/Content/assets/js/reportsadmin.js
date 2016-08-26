﻿$(window).load(function () {

    //Execution


    getAllUsers();

    getAllRoles();

    $("#cmbusuarios").change(function () {
        generateURL();
    });

    $("#cmbusuarios").trigger("change");

    $("#cmbroles").change(function () {
        generateURRole();
    });

    $("#cmbroles").trigger("change");


    function getAllUsers() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/GeneralReporstAdmin.ashx?method=getallusers',
            async:false,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    $("#cmbusuarios").html(response.ResponseData);

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

    function getAllRoles() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/GeneralReporstAdmin.ashx?method=getallroles',
            async: false,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {


                    $("#cmbroles").html(response.ResponseData);
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

    function generateURL()
    {
        var userID = $("#cmbusuarios > option:selected").attr("data-id-user");
        $("#btnreporteusuario").attr("href", "/Handlers/GeneralReporstAdmin.ashx?method=getuserinfo&ID_Usuario="+userID);
    }

    function generateURRole() {
        var roleID = $("#cmbroles > option:selected").attr("data-id-role");
        $("#btnreporterol").attr("href", "/Handlers/GeneralReporstAdmin.ashx?method=getroleinfo&ID_Rol=" + roleID);
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