
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
                "info": "Mostrando pagina _PAGE_ de _PAGES_"

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

   

});
