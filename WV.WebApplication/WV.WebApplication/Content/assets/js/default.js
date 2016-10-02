
var Security = (function () {

    var _userName = $('meta[name=username]').attr("content"),
        _resourceName = $('#pagename').html(),
         _ADD, _EDIT, _DELETE;

    var dataToSend = {
        UserName: _userName,
        ResourceName : _resourceName
    }

    getOptionPermissions(dataToSend);

    var optionPermissions =
    {
        agregar: _ADD,
        editar: _EDIT,
        eliminar:_DELETE
    };


    //Functions declaration
    function getOptionPermissions(dataToSend)
    {
        $.ajax({
            type: 'POST',
            url: '/Handlers/RoleConfiguration.ashx?method=getoptionpermissions',
            async:false,
            data: dataToSend,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess) {
                    _ADD = response.ResponseData.Agregar;
                    _EDIT = response.ResponseData.Modificar;
                    _DELETE = response.ResponseData.Eliminar;

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

    function displayErrorMessage(message) {
        
        $("#errorcontainer").html(message);
        $("#myErrorDialog").modal('show');
    }

    function displayMessage(message) {

        $("#messagecontainer").html(message);
        $("#myMessageDialog").modal('show');
    }

    return optionPermissions;
})();



$(window).load(function () {

   

    $(window).scroll(function () {
        if ($(this).scrollTop() > 50) {
            $('#back-to-top').fadeIn();
        } else {
            $('#back-to-top').fadeOut();
        }
    });
    // scroll body to 0px on click
    $('#back-to-top').click(function () {
        //$('#back-to-top').tooltip('hide');
        $('body,html').animate({
            scrollTop: 0
        }, 800);
        return false;
    });


    setSelectedModule();

    if (sessionStorage.getItem("currentmenu") != undefined)
    {
        idMenu = sessionStorage.getItem("currentmenu");
        $("#" + idMenu).addClass("active").find("ul").addClass("in");
    }


    if ($("#pagename").html() == "Inicio") {
        var introHtml = "<li><a href=''><div><strong>Habilitar Intro</strong><span class='pull-right text-muted'><em>Intro</em></span></div><div><div id='toggleintro' class='btn-group' data-toggle='buttons'><label class='btn btn-primary'><input type='radio' name='options1' id='' data-id-type='1'>On</label><label class='btn btn-primary active'><input type='radio' name='options1' id='' data-id-type=''>Off</label></div></div></a></li>";
        var divider = "<li class='divider'></li>";

        $("#togglehelp").parent().parent().parent().remove();

        var elements = introHtml;

        $("ul.dropdown-messages").append(elements);
        $("#Inicio").next().addClass("active").find("ul").addClass("in");

    }


    var intro = null;
    var realintro = null;

    $("#master_username").append($('meta[name=username]').attr("content"));

    $("#togglehelp").on('change', 'input:radio', function (event) {
        if ($(this).attr("data-id-type")==1) {
            
            if (intro !== null) {
                $("div.introjs-hints > a").each(function () {

                    if ($(this).hasClass("introjs-hidehint")) {
                        $(this).removeClass("introjs-hidehint");
                    }

                    ;
                });
            }
            else
            {
                AddHelpInfo();
            }
           
        }
        else
        {
            if (intro !== null) {
                intro.hideHints();
            }
            
        }
    });

    $("#toggleintro").on('change', 'input:radio', function (event) {
        if ($(this).attr("data-id-type") == 1) {

            if (intro !== null) {
            //    $("div.introjs-hints > a").each(function () {

            //        if ($(this).hasClass("introjs-hidehint")) {
            //            $(this).removeClass("introjs-hidehint");
            //        }

            //        ;
            //    });
            }
            else {
                var stepsArray = [
                    
                    {
                        element: document.querySelector('.navbar-brand'),
                        intro: "Bienvenido al sistema de administracion de proyectos de Vision Mundial El Salvador.",
                        position: 'right'
                    },
                     {
                         element: document.querySelector('#main-menu'),
                         intro: "Esta barra lateral le permite elegir las opciones disponibles para su usuario, muestra los modulos del sistema.",
                         position: 'right'
                     },
                     {
                         element: document.querySelector('li > a > i + span'),
                         intro: 'Al hacer clic sobre cualquiera de los modulos visualizara las opciones respectivas.',
                         position: 'right'
                     },
                     {
                         element: document.querySelector('ul.nav.nav-second-level.collapse.in'),
                         intro: 'Para acceder a dichas opciones debe tener los permisos adecuados.',
                            position: 'right'
                     },
                     {
                         element: document.querySelector('#contenido > div[class="row"]'),
                         intro: 'El Panel principal muestra las opciones mas importantes del sistema, puede acceder a ella haciendo clic',
                         position: 'center'
                     }
                     ,
                     {
                         element: document.querySelector('ul.nav.navbar-top-links.navbar-right'),
                         intro: 'Por ultimo se muestran algunas opciones de ayuda para el usuario.',
                         position: 'center'
                     }
                    
                       ];

                       startIntro(stepsArray);
                   }

        }
        else {
            //if (intro !== null) {
            //    intro.hideHints();
            //}

        }
    });

    function AddHelpInfo()
    {
        if ($("#pagename").html() == "Usuarios") {

            var hints = [
            {
                element: document.querySelector('#tabtable'),
                hint: "Muestra el todos los registro de la pantalla actual.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#tabdetails'),
                hint: "Habilita los campos de ingreso de informacion, coloca la pantalla en modo Agregar.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.assign'),
                hint: "Despliega un dialogo modal en el cual puede elegir el rol a asignar al usuario del sistema",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.detail'),
                hint: "Muestra el registro seleccionado y coloca la pantalla en modo Vista",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.edit'),
                hint: "Habilita la edicion de la informacion para el registro seleccionado y coloca la pantalla en modo Edicion",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('.delete'),
                hint: "Despliega un dialogo modal el cual permite confirmar si se desea eliminar el registro seleccionado",
                hintPosition: 'top'
            }
            ];
            

            addHints(hints);
        }
        else if ($("#pagename").html() == "Roles") {
            var hints = [
            {
                element: document.querySelector('#tabtable'),
                hint: "Muestra el todos los registro de la pantalla actual.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#tabdetails'),
                hint: "Habilita los campos de ingreso de informacion, coloca la pantalla en modo Agregar.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.detail'),
                hint: "Muestra el registro seleccionado y coloca la pantalla en modo Vista",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.edit'),
                hint: "Habilita la edicion de la informacion para el registro seleccionado y coloca la pantalla en modo Edicion",
                hintPosition: 'top'
            }
            ];


            addHints(hints);
        }
        else if ($("#pagename").html() == "Materia") {

            var hints = [
            {
                element: document.querySelector('#tabtable'),
                hint: "Muestra el todos los registro de la pantalla actual.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#tabdetails'),
                hint: "Habilita los campos de ingreso de informacion, coloca la pantalla en modo Agregar.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.detail'),
                hint: "Muestra el registro seleccionado y coloca la pantalla en modo Vista",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.edit'),
                hint: "Habilita la edicion de la informacion para el registro seleccionado y coloca la pantalla en modo Edicion",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('.delete'),
                hint: "Despliega un dialogo modal el cual permite confirmar si se desea eliminar el registro seleccionado",
                hintPosition: 'top'
            }
            ];


            addHints(hints);

        }
        else if ($("#pagename").html() == "Examen") {

            var hints = [
            {
                element: document.querySelector('#tabtable'),
                hint: "Muestra el todos los registro de la pantalla actual.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#tabdetails'),
                hint: "Habilita los campos de ingreso de informacion, coloca la pantalla en modo Agregar.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.detail'),
                hint: "Muestra el registro seleccionado y coloca la pantalla en modo Vista",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.edit'),
                hint: "Habilita la edicion de la informacion para el registro seleccionado y coloca la pantalla en modo Edicion",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('.delete'),
                hint: "Despliega un dialogo modal el cual permite confirmar si se desea eliminar el registro seleccionado",
                hintPosition: 'top'
            }
            ];


            addHints(hints);

        }
        else if ($("#pagename").html() == "Acciones_Mantenimiento") {

            var hints = [
            {
                element: document.querySelector('[href="#roles"]'),
                hint: "Muestra los registros correspondientes a roles para ejecutar una accion.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('[href="#beneficiarios"]'),
                hint: "Muestra los registros correspondientes a beneficiarios para ejecutar una accion.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('[href="#proyectos"]'),
                hint: "Muestra los registros correspondientes a Proyectos para ejecutar una accion.",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('[href="#programas"]'),
                hint: "Muestra los registros correspondientes a Programas para ejecutar una accion.",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('.btn.btn-primary.dropdown-toggle'),
                hint: "Despliega las acciones disponibles para ejecutar a los registros seleccionados.",
                hintPosition: 'top'
            }
            ];


            addHints(hints);

        }
        else if ($("#pagename").html() == "Recursos") {
            var hints = [
            {
                element: document.querySelector('#menuroles'),
                hint: "Se despliegan los roles del sistema, al hacer clic sobre alguno de ellos se carga la informaicon en el panel principal.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#btnagregaropcion'),
                hint: "Despliega un cuadro modal que contiene todas las opciones del sistema y que se eligen para agregar al rol seleccionado",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.delete'),
                hint: "Al hacer clic en este boton elimina el recurso seleccionado del rol y se actualiza la tabla.",
                hintPosition: 'top'
            }
            ];


            addHints(hints);

        }
        else if ($("#pagename").html() == "Asignar_Materia") {
            var hints = [
            {
                element: document.querySelector('#menuroles'),
                hint: "Se despliegan los roles del sistema, al hacer clic sobre alguno de ellos se carga la informaicon en el panel principal.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#btnagregarbeneficiario'),
                hint: "Despliega un cuadro modal que contiene todas los beneficiarios inscritos en ese programa",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#cmbmateria'),
                hint: "Despliega una lista en la cual se elige a que materia asignar al beneficiario",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.delete'),
                hint: "Al hacer clic en este boton elimina la asignacion del beneficiario seleccionado y se actualiza el contenido de la pantalla.",
                hintPosition: 'top'
            }
            ];


            addHints(hints);

        }
        else if ($("#pagename").html() == "Control_Notas") {
            var hints = [
            {
                element: document.querySelector('td>input'),
                hint: "Permite introducir la calificacion para el beneficiario",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#cmbmateria'),
                hint: "Despliega una lista en la cual se elige la materia en la que se registraran las calificaciones",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#cmbexamen'),
                hint: "Despliega una lista en la cual se elige el examen al cual se registraran las calificaciones.",
                hintPosition: 'top'
            }
            ];


            addHints(hints);

        }
        else if ($("#pagename").html() == "Tipo_Programa") {
            var hints = [
            {
                element: document.querySelector('#tabtable'),
                hint: "Muestra el todos los registro de la pantalla actual.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#tabdetails'),
                hint: "Habilita los campos de ingreso de informacion, coloca la pantalla en modo Agregar.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.detail'),
                hint: "Muestra el registro seleccionado y coloca la pantalla en modo Vista",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.edit'),
                hint: "Habilita la edicion de la informacion para el registro seleccionado y coloca la pantalla en modo Edicion",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('.delete'),
                hint: "Despliega un dialogo modal el cual permite confirmar si se desea eliminar el registro seleccionado",
                hintPosition: 'top'
            }
            ];


            addHints(hints);
        }
        else if ($("#pagename").html() == "Comunidades") {
            var hints = [
            {
                element: document.querySelector('#tabtable'),
                hint: "Muestra el todos los registro de la pantalla actual.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#tabdetails'),
                hint: "Habilita los campos de ingreso de informacion, coloca la pantalla en modo Agregar.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.detail'),
                hint: "Muestra el registro seleccionado y coloca la pantalla en modo Vista",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.edit'),
                hint: "Habilita la edicion de la informacion para el registro seleccionado y coloca la pantalla en modo Edicion",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('.delete'),
                hint: "Despliega un dialogo modal el cual permite confirmar si se desea eliminar el registro seleccionado",
                hintPosition: 'top'
            }
            ];


            addHints(hints);
        }
        else if ($("#pagename").html() == "Control_Personal") {
            var hints = [
            {
                element: document.querySelector('.schedule'),
                hint: "Abre la pantalla para plan semanal para el colaborador seleccionado.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.logbook'),
                hint: "Abre la pantalla para la bitacora del colaborador seleccionado.",
                hintPosition: 'top'
            }
            ];

            addHints(hints);
        }
        else if ($("#pagename").html() == "Personal") {
            var hints = [
            {
                element: document.querySelector('#tabtable'),
                hint: "Muestra el todos los registro de la pantalla actual.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#tabdetails'),
                hint: "Habilita los campos de ingreso de informacion, coloca la pantalla en modo Agregar.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.detail'),
                hint: "Muestra el registro seleccionado y coloca la pantalla en modo Vista",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.edit'),
                hint: "Habilita la edicion de la informacion para el registro seleccionado y coloca la pantalla en modo Edicion",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('.delete'),
                hint: "Despliega un dialogo modal el cual permite confirmar si se desea eliminar el registro seleccionado",
                hintPosition: 'top'
            }
            ];
            addHints(hints);
        }
        else if ($("#pagename").html() == "Puesto") {
            var hints = [
            {
                element: document.querySelector('#tabtable'),
                hint: "Muestra el todos los registro de la pantalla actual.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#tabdetails'),
                hint: "Habilita los campos de ingreso de informacion, coloca la pantalla en modo Agregar.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.detail'),
                hint: "Muestra el registro seleccionado y coloca la pantalla en modo Vista",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.edit'),
                hint: "Habilita la edicion de la informacion para el registro seleccionado y coloca la pantalla en modo Edicion",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('.delete'),
                hint: "Despliega un dialogo modal el cual permite confirmar si se desea eliminar el registro seleccionado",
                hintPosition: 'top'
            }
            ];


            addHints(hints);
        }
        else if ($("#pagename").html() == "Tipo_Puesto") {
            var hints = [
            {
                element: document.querySelector('#tabtable'),
                hint: "Muestra el todos los registro de la pantalla actual.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#tabdetails'),
                hint: "Habilita los campos de ingreso de informacion, coloca la pantalla en modo Agregar.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.detail'),
                hint: "Muestra el registro seleccionado y coloca la pantalla en modo Vista",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.edit'),
                hint: "Habilita la edicion de la informacion para el registro seleccionado y coloca la pantalla en modo Edicion",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('.delete'),
                hint: "Despliega un dialogo modal el cual permite confirmar si se desea eliminar el registro seleccionado",
                hintPosition: 'top'
            }
            ];


            addHints(hints);
        }
        else if ($("#pagename").html() == "Asignar_RRHH") {
            var hints = [
            {
                element: document.querySelector('#cmbproyecto'),
                hint: "Muesta los proyectos que aun no tienen asignacion de personal.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#addRow'),
                hint: "Agrega un nuevo registro a la tabla de detalle en la cual se permite elegir el personal y definir su puesto",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#deleterow'),
                hint: "Elimina un registro existente de la tabla de detalle en la cual se ha definido el personal",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#cancelpage'),
                hint: "Limpia la informacion ingresada en la pantalla",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('#savepage'),
                hint: "Guarda la informacion en la pantalla, si se ha cumplido las validaciones respectivas de lo contrario muestra mensajes de advertencia",
                hintPosition: 'top'
            }
            ];


            addHints(hints);

        }
        else if ($("#pagename").html() == "Bitacora") {
            var hints = [
            {
                element: document.querySelector('#btnshowside'),
                hint: "Muestra un sidebar con el detalle de las bitacoras por dia para el colaborador.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#addRow'),
                hint: "Agrega un nuevo registro a la tabla de detalle en la cual se permite agregar observaciones y actividades",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#deleterow'),
                hint: "Elimina un registro existente de la tabla de detalle en la cual se ha definido la bitacora",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#datepicker'),
                hint: "Permite seleccionar la fecha de la bitacora",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('#action-add'),
                hint: "La pantalla entra en modo Agregar",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#action-edit'),
                hint: "La pantalla entra en modo Edicion",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#action-cancel'),
                hint: "Limpia el contenido de la pantalla y entra en modo Detalle",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#action-save'),
                hint: "Guarda la informacion ingresada en pantalla",
                hintPosition: 'top'
            }
            , {
                element: document.querySelector('#action-delete'),
                hint: "Elimina la bitacora seleccionada",
                hintPosition: 'top'
            }
            ];


            addHints(hints);

        }
        else if ($("#pagename").html() == "Plan_Semanal") {
            var hints = [
            {
                element: document.querySelector('#btnshowside'),
                hint: "Muestra un sidebar con el detalle del plan semanal para el colaborador.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#addRow'),
                hint: "Agrega un nuevo registro a la tabla de detalle en la cual se permite agregar observaciones, actividades y recursos",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#deleterow'),
                hint: "Elimina un registro existente de la tabla de detalle en la cual se ha definido el plan semanal",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#datepicker'),
                hint: "Permite seleccionar una fecha, puede elegirse cualquier dia de la semana y calcula el inicio y el final.",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('#action-add'),
                hint: "La pantalla entra en modo Agregar",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#action-edit'),
                hint: "La pantalla entra en modo Edicion",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#action-cancel'),
                hint: "Limpia el contenido de la pantalla y entra en modo Detalle",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#action-save'),
                hint: "Guarda la informacion ingresada en pantalla",
                hintPosition: 'top'
            }
            , {
                element: document.querySelector('#action-delete'),
                hint: "Elimina El Plan Semanal seleccionado",
                hintPosition: 'top'
            }
            ];


            addHints(hints);
        }
        else if ($("#pagename").html() == "Proyectos") {
            var hints = [
            {
                element: document.querySelector('#tabtable'),
                hint: "Muestra el todos los registro de la pantalla actual.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#tabdetails'),
                hint: "Habilita los campos de ingreso de informacion, coloca la pantalla en modo Agregar.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.detail'),
                hint: "Muestra el registro seleccionado y coloca la pantalla en modo Vista",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.edit'),
                hint: "Habilita la edicion de la informacion para el registro seleccionado y coloca la pantalla en modo Edicion",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('.delete'),
                hint: "Despliega un dialogo modal el cual permite confirmar si se desea eliminar el registro seleccionado",
                hintPosition: 'top'
            }
            ];


            addHints(hints);
        }
        else if ($("#pagename").html() == "Programa") {
            var hints = [
            {
                element: document.querySelector('#tabtable'),
                hint: "Muestra el todos los registro de la pantalla actual.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#tabdetails'),
                hint: "Habilita los campos de ingreso de informacion, coloca la pantalla en modo Agregar.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.detail'),
                hint: "Muestra el registro seleccionado y coloca la pantalla en modo Vista",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.edit'),
                hint: "Habilita la edicion de la informacion para el registro seleccionado y coloca la pantalla en modo Edicion",
                hintPosition: 'top'
            }
            ];


            addHints(hints);
        }
        else if ($("#pagename").html() == "Reportes_Administracion") {
                var hints = [
                {
                    element: document.querySelector('#listausuarios'),
                    hint: "Este reporte permite mostrar el rol al que el usuario pertenece y las opciones respectivas.",
                    hintPosition: 'top'
                },
                {
                    element: document.querySelector('#listaroles'),
                    hint: "Este reporte muestra los roles registrados en el sistema.",
                    hintPosition: 'top'
                },
                {
                    element: document.querySelector('#rolesopciones'),
                    hint: "Este Reporte permite elegir el rol para mostrar los detalles de sus opciones registradas",
                    hintPosition: 'top'
                }
                ];


                addHints(hints);
        }
        else if ($("#pagename").html() == "Reportes_Notas") {
            var hints = [
            {
                element: document.querySelector('#listainscritos'),
                hint: "Este reporte permite mostrar la lista de beneficiarios inscritos por materia.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#listanotas'),
                hint: "Este reporte abre un dialogo que permite elegir la materia y el examen respectivo y muestra los resultados para dicha prueba.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#consolidadomaterias'),
                hint: "Este Reporte permite mostrar un consolidado de notas por materia, incluye informacion de beneficiarios inscritos y sus respectivas calificaciones.",
                hintPosition: 'top'
            }
            ];


            addHints(hints);
        }
        else if ($("#pagename").html() == "Reportes_Proyectos") {
            var hints = [
            {
                element: document.querySelector('#bitacoradiaria'),
                hint: "Este reporte permite mostrar el detalle de la bitacora diaria registrada para un colaborador.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#plansemanal'),
                hint: "Este reporte permite elegir la fecha correspondiente al plan semanal que se desea obtener.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#asignacionpersonal'),
                hint: "Este Reporte permite mostrar la asignacion de personal (Recurso Humano) en un proyecto seleccionado.",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('#detallepuestos'),
                hint: "Este Reporte permite mostrar los registros detallados de los puestos actuales registrados en el sistema.",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('#consolidadoproyecto'),
                hint: "Este Reporte permite mostrar un resumen general con gráficas de los beneficiarios por programa y datos estadísticos",
                hintPosition: 'top'
            }
            ];


            addHints(hints);
        }
        else if ($("#pagename").html() == "Reportes_Programas") {
            var hints = [
            {
                element: document.querySelector('#detalleprogramas'),
                hint: "Este reporte permite elegir un programa en el cual muestra un resumen de la informacion mas relevante del mismo.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#programasproyecto'),
                hint: "Este reporte muestra la informacion de todos los programas pertenecientes al proyecto seleccionado.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#Actividadesprograma'),
                hint: "Este Reporte permite mostrar las distintas actividades calendarizadas por programa seleccionado.",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('#beneficiarioprograma'),
                hint: "Este Reporte permite mostrar la lista de beneficiarios en el programa con su informacion personal.",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('#beneficiariocomunidad'),
                hint: "Este Reporte permite mostrar los distintos beneficiarios filtrados por comunidad mostrando informacion de los programas a los cuales pertenecen.",
                hintPosition: 'top'
            }
            ,
            
            {
                element: document.querySelector('#programaAsistencia'),
                hint: "Este Reporte permite mostrar la lista de asistencias de los beneficiarios por actividad seleccionada.",
                hintPosition: 'top'
            }
            ];


            addHints(hints);
        }
        else if ($("#pagename").html() == "Inscripcion") {
            var hints = [
            {
                element: document.querySelector('#example'),
                hint: "Muestra la vista preliminar de los beneficiarios de todos los programas.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#nuevobeneficiario'),
                hint: "Habilita los campos de ingreso de informacion, coloca la pantalla en modo Agregar.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#editarbeneficiario'),
                hint: "Habilita los campos de informacion del beneficiairio seleccionado y coloca la pantalla en modo Edicion",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('fa.fa-fw.fa-user'),
                hint: "Al hacer clic sobre cualqiuera de los beneficiarios se muestra la informacion en el panel principal",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('.nav.nav-tabs'),
                hint: "Contenedor principal de informacion separado por tabs",
                hintPosition: 'top'
            }
            ];


            addHints(hints);
        }
        else if ($("#pagename").html() == "Actividades") {
            var hints = [
           {
               element: document.querySelector('#tabtable'),
               hint: "Muestra el todos los registro de la pantalla actual.",
               hintPosition: 'top'
           },
           {
               element: document.querySelector('#tabdetails'),
               hint: "Habilita los campos de ingreso de informacion, coloca la pantalla en modo Agregar.",
               hintPosition: 'top'
           },
           {
               element: document.querySelector('.details'),
               hint: "Muestra luna tabla con el detalle de actividades para un programa en la pantalla entra en modo Vista",
               hintPosition: 'top'
           },
           {
               element: document.querySelector('.edit'),
               hint: "Habilita la edicion de la informacion para el registro seleccionado.",
               hintPosition: 'top'
           }
           
            ];


            addHints(hints);
        }
        else if ($("#pagename").html() == "Control_Asistencia") {
            var hints = [
           {
               element: document.querySelector('#cmbprograma'),
               hint: "Muestra los programas que contienen actividades, al seleccionar uno de ellos se cargan las actividades en el calendario.",
               hintPosition: 'top'
           },
           {
               element: document.querySelector('.btn.btn-primary.dropdown-toggle'),
               hint: "Permite hacer una seleccion masiva del estado de la asistencia de los Beneficiario",
               hintPosition: 'top'
           },
           {
               element: document.querySelector('#calendar'),
               hint: "Muestra los dias del mes que contienen actividades, los dias que poseen un rectangulo azul contienen actividades registradas para el proyecto.",
               hintPosition: 'top'
           },
           {
               element: document.querySelector('#savepage'),
               hint: "Guarda la informacion de asistencia para la actividad seleccionada, debe estar completa la informacion ingresada.",
               hintPosition: 'top'
           }

            ];


            addHints(hints);
        }
        else if ($("#pagename").html() == "Administracion_Notas") {
            var hints = [
           {
               element: document.querySelector('#materiadialogo'),
               hint: "Al acceder a esta opcion podra crear asignaturas individuales.",
               hintPosition: 'top'
           },
           {
               element: document.querySelector('#examendialogo'),
               hint: "Al acceder a esta opcion prodra crear examenes y adjuntar archivos a este.",
               hintPosition: 'top'
           },
           {
               element: document.querySelector('#asignarmateriadialogo'),
               hint: "Al acceder a esta opcion podra asignar beneficiarios a una materia en especifico.",
               hintPosition: 'top'
           },
           {
               element: document.querySelector('#registrarnotasdialogo'),
               hint: "Esta opcion permite acceder al registro de calificaciones para los distintos examenes de la asignatura.",
               hintPosition: 'top'
           }

            ];


            addHints(hints);
        }


    }

    function addHints(hintarray) {

        if (intro != null) {
            intro.refresh();
            intro.addHints();
        }
        else
        {
            intro = introJs();
            intro.setOptions({
            hints: hintarray
            });
            intro.onhintsadded(function () {
                // console.log('all hints added');
            });
            intro.onhintclick(function (hintElement, item, stepId) {
                // console.log('hint clicked', hintElement, item, stepId);
            });
            intro.onhintclose(function (stepId) {
                // console.log('hint closed', stepId);
            });
            intro.addHints();
        }

        
    }

    function startIntro(stepsarray)
    {
        if (realintro != null) {

        }
        else
        {
            var realintro = introJs();
            realintro.setOptions({
                steps: stepsarray
            });
            realintro.start();
        }
    }

    function setSelectedModule()
    {
        $(".lastselected").click(function ()
        {
            sessionStorage.setItem("currentmenu",$(this).attr("id"));
        });
    }


    

});