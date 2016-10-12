
$(window).load(function () {

    //Execution


   
    getStaff();

    



    // End of Execution

    /*CRUD Functions*/

    function getStaff() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/ControlStaff.ashx?method=getstaff',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    setStaffTable(response.ResponseData);
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


  

    function setStaffTable(responseData) {
        $("#dataTables-example").append(responseData);
        attachClickSeeLogBookButtons();
        attachClickToSeeScheduleButtons();
        $('#dataTables-example').dataTable({
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
                    "next":"Siguiente"
                }
            }


        });
        
    }


   

    function attachClickSeeLogBookButtons() {
        $("#table-responsive").find("[class='btn btn-primary btn-sm logbook']").each(function (index, value) {
            $(value).click(function () {
                window.location.href = "/LogBook?id=" + $(this).attr("data-id-person");
            });
        });
    }

    function attachClickToSeeScheduleButtons() {
        $("#table-responsive").find("[class='btn btn-primary btn-sm schedule']").each(function (index, value) {
            $(value).click(function () {
                window.location.href = "/WeeklyPlan?id=" + $(this).attr("data-id-person");
                
            });
        });
    }


    /*Message Functions*/

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
