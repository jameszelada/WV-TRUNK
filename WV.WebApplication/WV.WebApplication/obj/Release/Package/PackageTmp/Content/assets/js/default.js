$(window).load(function () {

    setSelectedModule();

    //if ($("#contenido").children().children().hasClass("col-md-12"))
    //{
    //    $("#contenido").children().children().attr("style", "margin:0;padding:0;");
    //}

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