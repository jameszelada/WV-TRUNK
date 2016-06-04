
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
                    attachClickSeeLogBookButtons();
                    attachClickToSeeScheduleButtons();
                    //attachClickToShowButtons();
                    //attachClickToEditButtons();
                    //attachClickToListButton();
                    //attachClickToNewButton();
                    //attachClickToModal();

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
        $('#dataTables-example').dataTable();
        
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
        $("#errorcontainer").css({
            'position': 'absolute',
            'zIndex': '0',
            'right': '30%'

        }).html(message).toggleClass("hidden").fadeToggle(2000, "linear", function () { $("#errorcontainer").toggleClass("hidden").empty(); });
    }

    function displayMessage(message) {
        $("#messagecontainer").css({
            'position': 'absolute',
            'zIndex': '0',
            'right': '30%'

        }).html(message).toggleClass("hidden").fadeToggle(2000, "linear", function () { $("#messagecontainer").toggleClass("hidden").empty(); });
        $("#pagebtndelete").unbind();
        $("#tabdetails").unbind();

    }

   

});
