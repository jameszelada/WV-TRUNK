$(window).load(function () {

    var tben;
    var tpr;
    var tpry;
    var trl;
    getRoles();
    getPeople();
    getProjects();
    getPrograms();

    Events();
    //InitializeControls();

    function RefreshData()
    {
        getRoles();
        getPeople();
        getProjects();
        getPrograms();

    }

    function getPrograms() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/MassActions.ashx?method=getprograms',
            async: false,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    setProgramsTable(response.ResponseData);
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

    function setProgramsTable(responseData) {
        $("#dataTables-programas").append(responseData);
        if (tpr == null || tpr == undefined) {

            tpr = $('#dataTables-programas').DataTable({
                "bPaginate": true,
                "bFilter": false,
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
                    { "width": "90%" }

                ], 'columnDefs': [{
                    'targets': 0,
                    'searchable': false,
                    'orderable': false,
                    'className': 'dt-body-center',
                    'render': function (data, type, full, meta) {
                        return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
                    }
                }],
                'order': [[1, 'asc']]

            });

        }
        else {
            tpr.clear();
            $("#dataTables-programas > tbody").remove();
            tpr.destroy();
            $("#dataTables-programas").append(responseData);
            tpr = $('#dataTables-programas').DataTable({
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
                    { "width": "90%" }

                ], 'columnDefs': [{
                    'targets': 0,
                    'searchable': false,
                    'orderable': false,
                    'className': 'dt-body-center',
                    'render': function (data, type, full, meta) {
                        return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
                    }
                }],
                'order': [[1, 'asc']]

            });
        }

    }
    function getProjects() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/MassActions.ashx?method=getprojects',
            async: false,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    setProjectsTable(response.ResponseData);
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

    function setProjectsTable(responseData) {
        $("#dataTables-proyectos").append(responseData);
        if (tpry == null || tpry == undefined) {

            tpry = $('#dataTables-proyectos').DataTable({
                "bPaginate": true,
                "bFilter": false,
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
                    { "width": "90%" }

                ], 'columnDefs': [{
                    'targets': 0,
                    'searchable': false,
                    'orderable': false,
                    'className': 'dt-body-center',
                    'render': function (data, type, full, meta) {
                        return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
                    }
                }],
                'order': [[1, 'asc']]

            });

        }
        else {
            tpry.clear();
            $("#dataTables-proyectos > tbody").remove();
            tpry.destroy();
            $("#dataTables-proyectos").append(responseData);
            tpry = $('#dataTables-proyectos').DataTable({
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
                    { "width": "90%" }

                ], 'columnDefs': [{
                    'targets': 0,
                    'searchable': false,
                    'orderable': false,
                    'className': 'dt-body-center',
                    'render': function (data, type, full, meta) {
                        return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
                    }
                }],
                'order': [[1, 'asc']]

            });
        }

    }

    function getRoles() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/MassActions.ashx?method=getroles',
            async: false,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    setRolesTable(response.ResponseData);
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

    function setRolesTable(responseData) {
        $("#dataTables-roles").append(responseData);
        if (trl == null || trl == undefined) {

            trl = $('#dataTables-roles').DataTable({
                "bPaginate": true,
                "bFilter": false,
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
                    { "width": "90%" }

                ],
                'columnDefs': [{
                    'targets': 0,
                    'searchable': false,
                    'orderable': false,
                    'className': 'dt-body-center',
                    'render': function (data, type, full, meta) {
                        return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
                    }
                }],
                'order': [[1, 'asc']]

            });

        }
        else {
            trl.clear();
            $("#dataTables-roles > tbody").remove();
            trl.destroy();
            $("#dataTables-roles").append(responseData);
            trl = $('#dataTables-roles').DataTable({
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
                    { "width": "90%" }

                ], 'columnDefs': [{
                    'targets': 0,
                    'searchable': false,
                    'orderable': false,
                    'className': 'dt-body-center',
                    'render': function (data, type, full, meta) {
                        return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
                    }
                }],
                'order': [[1, 'asc']]

            });
        }

    }

    function getPeople() {
        $.ajax({
            type: 'POST',
            url: '/Handlers/MassActions.ashx?method=getall',
            async: false,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    setPeopleTable(response.ResponseData);
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

    function setPeopleTable(responseData) {
        $("#dataTables-beneficiarios").append(responseData);
        if (tben == null || tben == undefined) {

            tben = $('#dataTables-beneficiarios').DataTable({
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
                    { "width": "90%" }

                ], 'columnDefs': [{
                    'targets': 0,
                    'searchable': false,
                    'orderable': false,
                    'className': 'dt-body-center',
                    'render': function (data, type, full, meta) {
                        return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
                    }
                }],
                'order': [[1, 'asc']]

            });

        }
        else {
            tben.clear();
            $("#dataTables-beneficiarios > tbody").remove();
            tben.destroy();
            $("#dataTables-beneficiarios").append(responseData);
            tben = $('#dataTables-beneficiarios').DataTable({
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
                    { "width": "90%" }

                ], 'columnDefs': [{
                    'targets': 0,
                    'searchable': false,
                    'orderable': false,
                    'className': 'dt-body-center',
                    'render': function (data, type, full, meta) {
                        return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
                    }
                }],
                'order': [[1, 'asc']]

            });
        }

    }

    function Events()
    {
        $('#example-select-all').on('click', function () {
            // Get all rows with search applied
            var rows = trl.rows({ 'search': 'applied' }).nodes();
            // Check/uncheck checkboxes for all rows in the table
            $('input[type="checkbox"]', rows).prop('checked', this.checked);
        });

        // Handle click on checkbox to set state of "Select all" control dataTables-programas
        $('#dataTables-roles tbody').on('change', 'input[type="checkbox"]', function () {
            // If checkbox is not checked
            if (!this.checked) {
                var el = $('#example-select-all').get(0);
                // If "Select all" control is checked and has 'indeterminate' property
                if (el && el.checked && ('indeterminate' in el)) {
                    // Set visual state of "Select all" control 
                    // as 'indeterminate'
                    el.indeterminate = true;
                }
            }
        });

        $('#example1-select-all').on('click', function () {
            // Get all rows with search applied
            var rows = tben.rows({ 'search': 'applied' }).nodes();
            // Check/uncheck checkboxes for all rows in the table
            $('input[type="checkbox"]', rows).prop('checked', this.checked);
        });

        // Handle click on checkbox to set state of "Select all" control dataTables-programas
        $('#dataTables-beneficiarios tbody').on('change', 'input[type="checkbox"]', function () {
            // If checkbox is not checked
            if (!this.checked) {
                var el = $('#example1-select-all').get(0);
                // If "Select all" control is checked and has 'indeterminate' property
                if (el && el.checked && ('indeterminate' in el)) {
                    // Set visual state of "Select all" control 
                    // as 'indeterminate'
                    el.indeterminate = true;
                }
            }
        });

        $('#example2-select-all').on('click', function () {
            // Get all rows with search applied
            var rows = tpry.rows({ 'search': 'applied' }).nodes();
            // Check/uncheck checkboxes for all rows in the table
            $('input[type="checkbox"]', rows).prop('checked', this.checked);
        });

        // Handle click on checkbox to set state of "Select all" control dataTables-programas
        $('#dataTables-proyectos tbody').on('change', 'input[type="checkbox"]', function () {
            // If checkbox is not checked
            if (!this.checked) {
                var el = $('#example2-select-all').get(0);
                // If "Select all" control is checked and has 'indeterminate' property
                if (el && el.checked && ('indeterminate' in el)) {
                    // Set visual state of "Select all" control 
                    // as 'indeterminate'
                    el.indeterminate = true;
                }
            }
        });

        $('#example3-select-all').on('click', function () {
            // Get all rows with search applied
            var rows = tpr.rows({ 'search': 'applied' }).nodes();
            // Check/uncheck checkboxes for all rows in the table
            $('input[type="checkbox"]', rows).prop('checked', this.checked);
        });

        // Handle click on checkbox to set state of "Select all" control dataTables-programas
        $('#dataTables-programas tbody').on('change', 'input[type="checkbox"]', function () {
            // If checkbox is not checked
            if (!this.checked) {
                var el = $('#example3-select-all').get(0);
                // If "Select all" control is checked and has 'indeterminate' property
                if (el && el.checked && ('indeterminate' in el)) {
                    // Set visual state of "Select all" control 
                    // as 'indeterminate'
                    el.indeterminate = true;
                }
            }
        });

        //Delete Event Handlers

        $("#btneliminarrol").click(function ()
        {
            var toDelete = [];
            var contenido = [];
            if (trl.data().count() > 0) {

                trl.rows().iterator('row', function (context, index) {
                    var node = $(this.row(index).node());
                    $(node).find("td:first-child").find("input[type='checkbox']").each(function () {

                        if ($(this).is(":checked")) {

                            toDelete.push(parseInt($(this).parent().parent().attr("data-id-role")));

                        }

                    });
                   

                });
                if (toDelete.length > 0) {
                    var dataToSend =
                {
                    ToDelete: toDelete
                };

                    var data = JSON.stringify(dataToSend);
                    $.ajax({
                        type: 'GET',
                        url: '/Handlers/MassActions.ashx?method=deleteroles',
                        data: dataToSend,
                        success: function (data) {

                            var response = JSON.parse(data);
                            if (response.IsSucess) {
                                displayMessage(response.Message);
                                RefreshData();

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
                else {
                    displayErrorMessage("Por Favor seleccione los registros de la tabla");
                }
                
            }
        }
        );

        $("#btneliminabeneficiario").click(function () {
            var toDelete = [];
            var contenido = [];
            if (tben.data().count() > 0) {

                tben.rows().iterator('row', function (context, index) {
                    var node = $(this.row(index).node());
                    $(node).find("td:first-child").find("input[type='checkbox']").each(function () {

                        if ($(this).is(":checked")) {

                            toDelete.push(parseInt($(this).parent().parent().attr("data-id-beneficiario")));

                        }

                    });


                });
                if (toDelete.length > 0) {
                    var dataToSend =
                {
                    ToDelete: toDelete
                };

                    var data = JSON.stringify(dataToSend);
                    $.ajax({
                        type: 'GET',
                        url: '/Handlers/MassActions.ashx?method=deletepeople',
                        data: dataToSend,
                        success: function (data) {

                            var response = JSON.parse(data);
                            if (response.IsSucess) {
                                displayMessage(response.Message);
                                RefreshData();

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
                    displayErrorMessage("Por Favor seleccione los registros de la tabla");
                }
                
            }
        }
        );

        $("#btneliminarproyecto").click(function () {
            var toDelete = [];
            var contenido = [];
            if (tpry.data().count() > 0) {

                tpry.rows().iterator('row', function (context, index) {
                    var node = $(this.row(index).node());
                    $(node).find("td:first-child").find("input[type='checkbox']").each(function () {

                        if ($(this).is(":checked")) {

                            toDelete.push(parseInt($(this).parent().parent().attr("data-id-project")));

                        }

                    });


                });

                if (toDelete.length > 0) {
                    var dataToSend =
                {
                    ToDelete: toDelete
                };

                    var data = JSON.stringify(dataToSend);
                    $.ajax({
                        type: 'GET',
                        url: '/Handlers/MassActions.ashx?method=deleteprojects',
                        data: dataToSend,
                        success: function (data) {

                            var response = JSON.parse(data);
                            if (response.IsSucess) {
                                displayMessage(response.Message);
                                RefreshData();

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
                    displayErrorMessage("Por Favor seleccione los registros de la tabla");
                }

                
            }
        }
        );

        $("#btneliminarprograma").click(function () {
            var toDelete = [];
            var contenido = [];
            if (tpr.data().count() > 0) {

                tpr.rows().iterator('row', function (context, index) {
                    var node = $(this.row(index).node());
                    $(node).find("td:first-child").find("input[type='checkbox']").each(function () {

                        if ($(this).is(":checked")) {

                            toDelete.push(parseInt($(this).parent().parent().attr("data-id-program")));

                        }

                    });


                });

                if (toDelete.length > 0) {
                    var dataToSend =
               {
                   ToDelete: toDelete
               };

                    var data = JSON.stringify(dataToSend);
                    $.ajax({
                        type: 'GET',
                        url: '/Handlers/MassActions.ashx?method=deleteprograms',
                        data: dataToSend,
                        success: function (data) {

                            var response = JSON.parse(data);
                            if (response.IsSucess) {
                                displayMessage(response.Message);
                                RefreshData();

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
                else {
                    displayErrorMessage("Por Favor seleccione los registros de la tabla");
                }

               
            }
        }
        );
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
        $("#pagebtndelete").unbind();
        $("#tabdetails").unbind();
    }

    function showLoadingIndicator() {
        // $("#backgroundCover").show();
        var message = "Cargando Información..."
        displayMessage(message);
    }

    function hideLoadingIndicator() {

        //$("#backgroundCover").hide();
        $('#myMessageDialog').modal('hide');
    }
    /*Mode Functions*/

});