$(window).load(function () {

	var tas;
	//Function calling
	getSubjectsCombo();
	InitializeControls();



	function InitializeControls() {

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
				"zeroRecords": "No hay datos disponibles",
				"paginate": {
				    "previous": "Anterior",
				    "next": "Siguiente"
				}
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

		$("#cmbmateria").change(function () {


		    getExamsBySubject();
		    $("#cmbexamen").trigger("change");
		    //VerifyExistence();
		    $("#notescontrol").html($("#cmbmateria > option:selected").html() + " " + $("#cmbexamen > option:selected").html());

		});

		$("#cmbexamen").change(function () {

		    VerifyExistence();
		    $("#notescontrol").html($("#cmbmateria > option:selected").html() + " " + $("#cmbexamen > option:selected").html());
		});

		$("#cmbmateria").trigger("change");
		$("#savepage").click(function () {

			var allEntered = AllGradesEntered();

			if (tas.data().length && allEntered) {
			    var screenmode = $("#screenmode").val();
			    saveRecords(screenmode);
			}
			else {
			    var error = "Por Favor ingrese la informacion requerida"
			    displayErrorMessage(error);
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
					"zeroRecords": "No hay datos disponibles",
					"paginate": {
					    "previous": "Anterior",
					    "next": "Siguiente"
					}
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
					"zeroRecords": "No hay datos disponibles",
					"paginate": {
					    "previous": "Anterior",
					    "next": "Siguiente"
					}
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
	    var message = "Cargando Información..."
	    $("#messagecontainer").html(message);
	    $("#myMessageDialog").modal('show');
	    $("#pagebtndelete").unbind();
	    $("#tabdetails").unbind();
	    //displayMessage(message);
	}

	function hideLoadingIndicator() {

	    //$("#backgroundCover").hide();
	    $('#myMessageDialog').modal('hide');
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

	    $("#pagebtndelete").unbind();
	    $("#tabdetails").unbind();
	}

	function getSubjectsCombo() {
		$.ajax({
			type: 'POST',
			url: '/Handlers/RecordGrades.ashx?method=getsubjects',
			async: false,
			success: function (data) {

				var response = JSON.parse(data);
				if (response.IsSucess) {

					$("#cmbmateria").html(response.ResponseData);

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

	function getExamsBySubject() {

		var dataToSend =
			{
				ID_Materia: parseInt($("#cmbmateria > option:selected").attr("data-id-subject"))
			};

		$.ajax({
			type: 'POST',
			data: dataToSend,
			async: false,
			beforeSend: showLoadingIndicator,
			complete: hideLoadingIndicator,
			url: '/Handlers/RecordGrades.ashx?method=getexams',
			success: function (data) {

				var response = JSON.parse(data);
				if (response.IsSucess) {

					$("#cmbexamen").html(response.ResponseData);

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

	function VerifyExistence() {

		var dataToSend =
			{
				ID_Examen: parseInt($("#cmbexamen > option:selected").attr("data-id-exam")),
				//FechaActividad: ActivityDate
			};
		var x = 0;
		$.ajax({
			type: 'POST',
			data: dataToSend,
			async: false,
			url: '/Handlers/RecordGrades.ashx?method=exists',
			success: function (data) {

				var response = JSON.parse(data);
				if (response.IsSucess) {

					if (response.ResponseData.Exists != true) {
						
						$("#idexamen").val(response.ResponseData.ID_Examen);
						$("#screenmode").val(response.ResponseData.Mode);
					   
						//make call to load data
						getMembersofNonExistenceGrades();
						$("#savepage").prop("disabled", false);
					}
					else {
						$("#idexamen").val(response.ResponseData.ID_Examen);
						$("#screenmode").val(response.ResponseData.Mode);
						
						//make call to load data
						getMembersofExistingGrades();

						$("#savepage").prop("disabled", false);
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

	function getMembersofNonExistenceGrades() {

		var dataToSend =
			{
				ID_Materia: parseInt($("#cmbmateria > option:selected").attr("data-id-subject"))
				
			};

		$.ajax({
			type: 'POST',
			data: dataToSend,
			async:false,
			url: '/Handlers/RecordGrades.ashx?method=getnonexistinggrades',
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

	function getMembersofExistingGrades() {

		var dataToSend =
		   {
			   ID_Examen: parseInt($("#cmbexamen > option:selected").attr("data-id-exam"))

		   };

		$.ajax({
			type: 'POST',
			data: dataToSend,
			async:false,
			url: '/Handlers/RecordGrades.ashx?method=getexistinggrades',
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

	function AllGradesEntered() {
		var isOK = false;
		var selected = [];
		var totalRows = tas.data().length;
		if (totalRows > 0) {

			tas.rows().iterator('row', function (context, index) {
				var count = 0;
				var node = $(this.row(index).node());
				$(node).find("td > input").each(function () {

				    if ($(this).val() != "")
				    {
						 selected.push(true);
					 }
				});
			});

			if (selected.length == totalRows) {
				isOK = true;
			}
		}

		return isOK;

	}

	//Pending to implement

	function saveRecords(screenmode) {

		if (screenmode === "add") {

			//var alldata = tac.rows().data();

		    var ID_Examen = parseInt($("#idexamen").val());
			var Resultados = [];
			var contenido = [];
			if (tas.data().count() > 0) {

				tas.rows().iterator('row', function (context, index) {
					var node = $(this.row(index).node());
					$(node).find("td:not(:first-child)").each(function () {

						if ($(this).is("[data-id-beneficiario]")) {
							contenido.push(parseInt($(this).attr("data-id-beneficiario")));
						}
						else if ($(this).children().length > 0) {
							$(this).find("input").each(function () {
								
									contenido.push($(this).val());
							});
						}


					});
					Resultados.push({ "ID_Beneficiario": contenido[0], "ID_Examen": ID_Examen, "Nota": contenido[1] });
					contenido.length = 0;

				});
			}

			var dataToSend =
				{
					
					ID_Examen: ID_Examen,
					Resultados: Resultados,
					ToDelete: []
				};

			var data = JSON.stringify(dataToSend);

			makeAjaxCall(data, screenmode);
			//$("#tabtable").tab("show");
		}
		else if (screenmode === "edit") {

		    //var alldata = tac.rows().data();

		    var ID_Examen = parseInt($("#idexamen").val());
		    var Resultados = [];
		    var contenido = [];
		    if (tas.data().count() > 0) {

		        tas.rows().iterator('row', function (context, index) {
		            var node = $(this.row(index).node());
		            $(node).find("td:not(:first-child)").each(function () {

		                if ($(this).is("[data-id-beneficiario]")) {
		                    contenido.push(parseInt($(this).attr("data-id-beneficiario")));
		                }
		                else if ($(this).children().length > 0) {
		                    $(this).find("input").each(function () {

		                        contenido.push($(this).val());
		                    });
		                }


		            });
		            Resultados.push({ "ID_Beneficiario": contenido[0], "ID_Examen": ID_Examen, "Nota": contenido[1] });
		            contenido.length = 0;

		        });
		    }

		    var dataToSend =
				{

				    ID_Examen: ID_Examen,
				    Resultados: Resultados,
				    ToDelete: []
				};

		    var data = JSON.stringify(dataToSend);

			makeAjaxCall(data, screenmode);
		}
	}

	function makeAjaxCall(dataToSend, action) {
		$.ajax({
			type: 'POST',
			url: '/Handlers/RecordGrades.ashx?method=' + action,
			data: dataToSend,
			success: function (data) {

				var response = JSON.parse(data);
				if (response.IsSucess) {
					displayMessage(response.Message);
				   
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