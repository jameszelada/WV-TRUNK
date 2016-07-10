$(window).load(function ()
{
    var t;// reference to table object
    var projects, jobs, people;
    
    getAllProjects();
    getAllPersons();
    getAllJobs();
    initializeControls();
    attachClickToModal();
    


    function initializeControls()
    {


        t = $('#example').DataTable({
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "fnCreatedRow": function (nRow, aData, iDataIndex) {
                //$('td:not(.select-checkbox)', nRow).append("<textarea rows='4' cols='40'></textarea>");
                $('td:eq(1)', nRow).append("<select class='nombre'>" + aData[1] + "</select>");
                $('td:eq(2)', nRow).append("<label class='telefono'>" + aData[2] + "</label>");
                $('td:eq(3)', nRow).append("<select class='puesto'>" + aData[3] + "</select>");
            },
            columnDefs: [{
                orderable: false,
                className: 'select-checkbox',
                targets: 0
            }
            ],
            select: {
                style: 'os',
                selector: 'td:first-child'
            },
            order: [[1, 'asc']]
        });

        $('#addRow').on('click', function () {
            
            t.row.add(['', people.ResponseData, '', jobs.ResponseData]).draw(false);
            $("tbody > tr > td > select").each(function (value, index) {

                $(this).parent().html($(this));

            });

            attachEventsToSelect();

        });

        $('#deleterow').click(function () {
            t.row('.selected').remove().draw(false);
            //disableElements();
        });

        $('#savepage').click(function () {
            OnSave();
        });

    }

    function attachEventsToSelect()
    {
        $(".nombre").each(function (index, value) {

            $(this).change(function (event) {
                var select = event.target;

                var value = $(select).find("option:selected").attr("data-tel-person");

                $(select).parent().next().find("label").html(value);
            })
           


        });
    }

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
    //    //$("#pagebtndelete").unbind();
    //    //$("#tabdetails").unbind();

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

    function getAllProjects() {

        $.ajax({
            type: 'POST',
            url: '/Handlers/AssignRRHH.ashx?method=getprojects',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    $("#cmbproyecto").html(response.ResponseData);

                    if ($('#cmbproyecto option').length != 0) {
                        $("#savepage").prop('disabled', false);
                    }

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

    function getAllJobs() {

        $.ajax({
            type: 'POST',
            url: '/Handlers/AssignRRHH.ashx?method=getjobs',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    jobs = response;

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
    function getAllPersons() {

        $.ajax({
            type: 'POST',
            url: '/Handlers/AssignRRHH.ashx?method=getpersons',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    people = response;

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

    function OnSave() {


            var AsignacionRecursoHumano = [];
            if (t.data().count() > 0) {
                $("table#example > tbody > tr").each(function (index, value) {


                    var ID_Puesto = parseInt($(this).find("select.puesto > option:selected").attr("data-id-job"));
                    var ID_Persona = parseInt($(this).find("select.nombre > option:selected").attr("data-id-person"));
                    var ID_Proyecto = parseInt($("#cmbproyecto > option:selected").attr("data-id-project"));

                    AsignacionRecursoHumano.push({ "ID_AsignacionRecursoHumano": 0, "ID_Puesto": ID_Puesto, "ID_Persona": ID_Persona, "ID_Proyecto": ID_Proyecto });

                });

                var persons = [];
                $("select.nombre > option:selected").each(function (index, value)
                {
                    persons.push(parseInt($(this).attr("data-id-person")));
                });

                if (!hasDuplicated(persons)) {

                    var dataToSend =
                {

                    AsignacionRecursoHumano: AsignacionRecursoHumano
                };

                    var data = JSON.stringify(dataToSend);

                    $.ajax({
                        type: 'POST',
                        url: '/Handlers/AssignRRHH.ashx?method=add',
                        data: data,
                        success: function (data) {

                            var response = JSON.parse(data);
                            if (response.IsSucess) {
                                displayMessage(response.Message);
                                disableControls();
                                returnToProjects();
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
                else
                {
                    displayErrorMessage("Existen Registros duplicados");
                }

                
            }
            else
            {
                displayErrorMessage("Se deben agregar recursos humanos al proyecto");
            }
    }

    function attachClickToModal() {
        $("#pagebtndelete").click(function () {
            window.location.href = "/AssignRRHH";
        });
    }

    function hasDuplicated(array)
    {
     for(var i = 0; i <= array.length; i++) {
         for(var j = i; j <= array.length; j++) {
             if(i != j && array[i] == array[j]) {
                 return true;
             }
         }
     }
     return false;
    }

    function disableControls()
    {
        $('#addRow').prop('disabled', true);
        $('#deleterow').prop('disabled', true);
        $("select").prop('disabled', true);
        $("#cancelpage").prop('disabled', true);
        $("#savepage").prop('disabled', true);

        
    }

    function returnToProjects()
    {
        setTimeout(function () {
            window.location.href = "/Project";;
        }, 3000);
    }
});