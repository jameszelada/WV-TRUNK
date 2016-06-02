$(window).load(function () {

    var t;// reference to table object
    //Execution

    initializeControls();
    attachEventsToToolbar();
    getAllLogBooks();

    function initializeControls()
    {
        t = $('#example').DataTable({
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "fnCreatedRow": function( nRow, aData, iDataIndex ) {
                //$('td:not(.select-checkbox)', nRow).append("<textarea rows='4' cols='40'></textarea>");
                $('td:eq(1)', nRow).append("<textarea class='actividad' rows='4' cols='40'>" + aData[1] + "</textarea>");
                $('td:eq(2)', nRow).append("<textarea class='observaciones'rows='4' cols='40'>" + aData[2] + "</textarea>");
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
            t.row.add(['','','']).draw(false);

        });

        $('#deleterow').click(function () {
            t.row('.selected').remove().draw(false);
            //disableElements();
        });

        $("#datepicker").datepicker();

        $("#btnshowside").click(displaySidebar);

        $("#menu-toggle").click(function () {
            //e.preventDefault();
            $("#sidebar-wrapper").hide(90);
            $("#wrapperinside").toggleClass("active");
            $("#btnshowside").click(displaySidebar);
        });


        
    }

    function displaySidebar() {
        $("#sidebar-wrapper").show();
        $("#wrapperinside").toggleClass("active");
        $(this).unbind();
    }

    function disableControls(mode)
    {
        var table = $('#example').DataTable();
        if (mode=="edit") {
            $("#datepicker").prop('disabled', true);
            $('#addRow').prop('disabled', true);
            $('#deleterow').prop('disabled', true);

            var rowcount = $('#example tbody tr').length;

            if (rowcount > 0) {
                $("textarea").prop('disabled', true);
            }
        } else {
            //Deberia de hacer lo mismo y cargar de nuevo el registro**************Falta*****************
            $("#datepicker").prop('disabled', true).val("");
            $('#addRow').prop('disabled', true);
            $('#deleterow').prop('disabled', true);

            table.clear().draw();
        }
        
    }

    function enableControls(mode)
    {
        
        $('#addRow').prop('disabled', false);
        $('#deleterow').prop('disabled', false);

        var table = $('#example').DataTable();

        if (mode == "edit") {
            var rowcount = $('#example tbody tr').length;

            if (rowcount > 0) {
                $("textarea").prop('disabled', false);
            }
        } else {
            table.clear().draw();
            $("#datepicker").prop('disabled', false).val("");
        }
       
    }

    function onAdd()
    {
        $("#screenmode").html("add");
        
        enableControls("add");
        $("#action-add").prop('disabled', true);
        $("#action-delete").prop('disabled', true);
        $("#action-edit").prop('disabled', true);
        $("#action-cancel").prop('disabled', false);
        $("#action-save").prop('disabled', false);

    }

    function onCancel()
    {
        //var mode = $("#screenmode").html();
        //if (mode == "add")
        //{
            disableControls("add");
            $("#action-add").prop('disabled', false);
            $("#action-delete").prop('disabled', false);
            $("#action-edit").prop('disabled', true);
            $("#action-cancel").prop('disabled', true);
            $("#action-save").prop('disabled', true);

        //}
        //else
        //{
        //    disableControls(mode);
        //    $("#action-add").prop('disabled', false);
        //    //****
        //    $("#action-delete").prop('disabled', false);
        //    $("#action-edit").prop('disabled', false);
        //    $("#action-cancel").prop('disabled', true);
        //    $("#action-save").prop('disabled', true);

        //}
    }

    function onload() {
        var mode = $("#screenmode").html();
        
      
            disableControls(mode);
            $("#action-add").prop('disabled', false);
            //****
            $("#action-delete").prop('disabled', true);
            $("#action-edit").prop('disabled', true);
            $("#action-cancel").prop('disabled', true);
            $("#action-save").prop('disabled', true);

        
    }

    function onEdit()
    {
        $("#screenmode").html("edit");
        enableControls("edit");
        $("#action-add").prop('disabled', true);
        $("#action-delete").prop('disabled', true);
        $("#action-edit").prop('disabled', true);
        $("#action-cancel").prop('disabled', false);
        $("#action-save").prop('disabled', false);
    }

    function onSave()
    {
        var date = $("#datepicker").val();
        
        if(date == null || date == undefined || date=="")
        {
            displayErrorMessage("Debe Ingresar Fecha de Bitacora");
        }
        else
        {
            var screenmode = $("#screenmode").html();
            saveLogBook(screenmode);

            disableControls("add");
            $("#action-add").prop('disabled', false);
            $("#action-delete").prop('disabled', false);
            $("#action-edit").prop('disabled', false);
            $("#action-cancel").prop('disabled', true);
            $("#action-save").prop('disabled', true);
        }
 
    }

    function onDelete()
    {
        disableControls("add");
        $("#action-add").prop('disabled', false);
        $("#action-delete").prop('disabled', false);
        $("#action-edit").prop('disabled', true);
        $("#action-cancel").prop('disabled', true);
        $("#action-save").prop('disabled', true);
    }

    function onView()
    {
        disableControls("edit");
        $("#action-add").prop('disabled', false);
        $("#action-delete").prop('disabled', false);
        $("#action-edit").prop('disabled', false);
        $("#action-cancel").prop('disabled', true);
        $("#action-save").prop('disabled', true);
    }

    function attachEventsToToolbar()
    {
        $("#action-add").click(onAdd);
        $("#action-edit").click(onEdit);
        $("#action-cancel").click(onCancel);
        $("#action-save").click(onSave);
    }

    function attachClickToDates() {
        $("#logbookdates").find("li").each(function (index, value) {
            $(value).click(function () {

                $('#pagetoedit').val($(this).attr("data-logbook-header"));
                $('#pagetodelete').val($(this).attr("data-logbook-header"));
                getSingleLogBook($(this).attr("data-logbook-header"));

            });
        });
    }

    function getAllLogBooks() {
        var id= $("#idperson").html();
        var dataToSend =
            {
                ID_Persona: id
            };

        $.ajax({
            type: 'POST',
            url: '/Handlers/LogBook.ashx?method=getall',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    setLogBookSidebar(response);
                    attachClickToDates();
                    attachClickToModal();
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

    function getSingleLogBook(id) {
        var dataToSend =
            {
                ID_Bitacora: id
            };

        $.ajax({
            type: 'POST',
            url: '/Handlers/LogBook.ashx?method=getsingle',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    fillData(response.ResponseData);
                    onView();


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

    function setLogBookSidebar(response) {
        $("#logbookdates").html(response.ResponseData);
        $("#lbl_nombre").html(response.ResponseAdditional);
        onload();

    }

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

    function fillData(logbook)
    {
        var date = new Date(parseInt(logbook.ObjetoBitacora.FechaBitacora.substr(6)));
        $("#datepicker").datepicker("setDate", date);
        var detalle = logbook.ObjetoBitacoraDetalle;
        t.clear().draw();
        for (var i = 0; i < detalle.length; i++) {

            t.row.add(["", detalle[i]["Actividad"], detalle[i]["Observaciones"]]).draw(false);

        }

        $("tbody > tr > td > textarea").each(function (value, index) {

            $(this).parent().html($(this));

        });
 
    }

    function attachClickToModal() {
        $("#pagebtndelete").click(function () {
            deleteLogBook();
            onDelete();
        });
    }

    function deleteLogBook() {
        var idToDelete = $("#pagetodelete").val();
        var dataToSend =
            {
                ID_Bitacora: idToDelete
            };
        $.ajax({
            type: 'POST',
            url: '/Handlers/LogBook.ashx?method=delete',
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getAllLogBooks();
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

    function saveLogBook(screenmode) {
        var id = $("#idperson").html();

        //Here I create the array of detail logbooks

        if (screenmode === "add") {

            var bitacoraDetalle = [];
            if (t.data().count() > 0) {
                $("table#example > tbody > tr").each(function (index, value) {


                    var actividad = $(value).find('textarea.actividad').val();
                    var observaciones = $(value).find('textarea.observaciones').val();

                    bitacoraDetalle.push({ "ID_BitacoraDetalle": 0, "Actividad": actividad, "Observaciones": observaciones });

                });
            }

            var dataToSend =
                {
                    ID_Persona : parseInt(id),
                    FechaBitacora: $("#datepicker").datepicker( 'getDate' ).getTime(),
                    BitacoraDetalle: bitacoraDetalle,
                    ToDelete:[]
                };

            var data = JSON.stringify(dataToSend);

            makeAjaxCall(data, screenmode);
            //$("#tabtable").tab("show");
            //$("#cancelpage").unbind();
            //$("#savepage").unbind();
            //clearControls();
        }
        else if (screenmode === "edit") {
            // To implement****************
            var id = $('#pagetoedit').val();

            var bitacoraDetalle = [];
            if (t.data().count() > 0) {
                $("table#example > tbody > tr").each(function (index, value) {


                    var actividad = $(value).find('textarea.actividad').val();
                    var observaciones = $(value).find('textarea.observaciones').val();

                    bitacoraDetalle.push({ "ID_BitacoraDetalle": 0, "Actividad": actividad, "Observaciones": observaciones });

                });
            }

            var dataToSend =
                {
                    ID_Persona: parseInt(id),
                    FechaBitacora: "",
                    BitacoraDetalle: bitacoraDetalle,
                    ToDelete:[]

                };

            var data = JSON.stringify(dataToSend);
            makeAjaxCall(data, screenmode);
            //$("#tabtable").tab("show");
            //$("#tabtable").tab("show");
            //$("#cancelpage").unbind();
            //$("#savepage").unbind();
        }
    }

    function makeAjaxCall(dataToSend, action) {
        $.ajax({
            type: 'POST',
            url: '/Handlers/LogBook.ashx?method=' + action,
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    displayMessage(response.Message);
                    getAllLogBooks();
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
});