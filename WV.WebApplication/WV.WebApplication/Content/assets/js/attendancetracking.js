$(window).load(function () {

    var tas;
    //Function calling
    getProgramsCombo();
    InitializeControls();



    function InitializeControls()
    {
        $('#calendar').fullCalendar({
            lang: 'es',
            defaultDate: '2016-06-12',
            editable: true,
            selectable: true,
            eventLimit: true,
            dayClick: function (date, jsEvent, view) {

                $("#attendance_date").html(date.format());
            },
            header: {
                left: 'prev,next',
                center: 'title',
                right: 'today'
            }
        });

        tas = $('#dataTables-example').DataTable({
            "bPaginate": true,
            "bFilter": true,
            "bInfo": false,
            "bLengthChange": false,
            "pagingType": "numbers",
            "fnCreatedRow": function (nRow, aData, iDataIndex) {
                //$('td:not(.select-checkbox)', nRow).append("<textarea rows='4' cols='40'></textarea>");
                //$('td:eq(0)', nRow).append("<label >" + aData[0] + "</label>");

            },
            language: {
                searchPlaceholder: "Búsqueda",
                "search": "",
                "emptyTable": "No hay datos encontrados",
                "zeroRecords": "No hay datos disponibles"
            },
            fnDrawCallback: function () {
                $("#dataTables-example thead").remove();
            },
            columns: [
                { "width": "10%" },
                { "width": "50%" },
                null
                
            ]

        });

        $("#cmbprograma").change(function () {
            
            getActivities();
            getProgramMembers();

        });
        $("#cmbprograma").trigger("change");

        //$('li.dropdown.mega-dropdown a').on('click', function (event) {
        //    $(this).parent().toggleClass('open');
        //});

        $("#btnpresente").click(function () {
            if (tas.data().count() > 0) {
                tas.rows().iterator('row', function (context, index) {
                    var node = $(this.row(index).node());
                    $(node).find("input[data-id-type='1']").each(function () {
                        
                        $(this).parent().button('toggle');

                    });

                });
            }
        });
        $("#btnausente").click(function () {
            if (tas.data().count() > 0) {
                tas.rows().iterator('row', function (context, index) {
                    var node = $(this.row(index).node());
                    $(node).find("input[data-id-type='2']").each(function () {

                        $(this).parent().button('toggle');

                    });

                });
            }
        });

        
    }

    function setMembersTable(responseData) {
        $("#dataTables-example").append(responseData);
        if (tas == null || tas == undefined) {

            tas = $('#dataTables-example').DataTable({
                "bPaginate": true,
                "bFilter": true,
                "bInfo": false,
                "bLengthChange": false,
                "pagingType": "numbers",
                "fnCreatedRow": function (nRow, aData, iDataIndex) {
                    //$('td:not(.select-checkbox)', nRow).append("<textarea rows='4' cols='40'></textarea>");
                    //$('td:eq(0)', nRow).append("<label >" + aData[0] + "</label>");

                },
                fnDrawCallback: function () {
                    $("#dataTables-example thead").remove();
                },
                language: {
                    searchPlaceholder: "Búsqueda",
                    "search": "",
                    "emptyTable": "No hay datos encontrados",
                    "zeroRecords": "No hay datos disponibles"
                },
                columns: [
                    { "width": "10%" },
                    { "width": "50%" },
                    null
                ]

            });

        }
        else {
            tas.clear();
            $("#dataTables-example > tbody").remove();
            tas.destroy();
            $("#dataTables-example").append(responseData);
            tas = $('#dataTables-example').DataTable({
                "bPaginate": true,
                "bFilter": true,
                "bInfo": false,
                "bLengthChange": false,
                "pagingType": "numbers",
                "fnCreatedRow": function (nRow, aData, iDataIndex) {
                    //$('td:not(.select-checkbox)', nRow).append("<textarea rows='4' cols='40'></textarea>");
                    //$('td:eq(0)', nRow).append("<label >" + aData[0] + "</label>");

                },
                language: {
                    searchPlaceholder: "Búsqueda",
                    "search": "",
                    "emptyTable": "No hay datos encontrados",
                    "zeroRecords": "No hay datos disponibles"
                },
                fnDrawCallback: function () {
                    $("#dataTables-example thead").remove();
                },
                columns: [
                    { "width": "10%" },
                    { "width": "50%" },
                    null
                ]

            });
        }

    }

    function showLoadingIndicator() {
        // $("#backgroundCover").show();
        var message="Cargando Información..."
        displayMessage(message);
    }

    function hideLoadingIndicator() {

            //$("#backgroundCover").hide();
        $('#myMessageDialog').modal('hide');
    }

    function displayErrorMessage(message) {

        $("#errorcontainer").html(message);
        $("#myErrorDialog").modal('show');
    }

    function displayMessage(message) {


        $("#messagecontainer").html(message);
        $("#myMessageDialog").modal('show');
        $("#pagebtndelete").unbind();
        $("#tabdetails").unbind();
    }

    function getProgramsCombo() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/AttendanceTracking.ashx?method=getprograms',
            async:false,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {

                    $("#cmbprograma").html(response.ResponseData);

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

        var dataToSend =
            {
                ID_Programa: parseInt($("#cmbprograma > option:selected").attr("data-id-programs"))
            };

        $.ajax({
            type: 'POST',
            data: dataToSend,
            beforeSend: showLoadingIndicator,
            complete: hideLoadingIndicator,
            url: '/Handlers/AttendanceTracking.ashx?method=getactivities',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {

               SetEventsToCalendar(response.ResponseData);

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

    function getProgramMembers() {

        var dataToSend =
            {
                ID_Programa: parseInt($("#cmbprograma > option:selected").attr("data-id-programs"))
            };

        $.ajax({
            type: 'POST',
            data: dataToSend,
            url: '/Handlers/AttendanceTracking.ashx?method=getmembers',
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {

                    setMembersTable(response.ResponseData);

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

    function SetEventsToCalendar(dates)
    {
        $('#calendar').fullCalendar('removeEvents');
       
        for (var i = 0; i < dates.length; i++) {

            var newEvent = new Object();
            newEvent.title = "Actividad";
            newEvent.start = new Date(dates[i]);
            newEvent.start = new Date(newEvent.start.getTime() + newEvent.start.getTimezoneOffset() * 60000);
            newEvent.allDay = false;
            newEvent.id = i;
            $('#calendar').fullCalendar('renderEvent', newEvent, true);

        }

        
    }




});