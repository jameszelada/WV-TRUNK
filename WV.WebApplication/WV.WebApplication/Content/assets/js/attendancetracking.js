$(window).load(function () {

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

    $('#dataTables-example').DataTable({
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
        columns: [
            { "width": "10%" },
            { "width": "50%" },
            null,
            null
        ]

    });

});