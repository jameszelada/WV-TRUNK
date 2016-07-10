
$(window).load(function () {

    var intro  =null;

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

    function AddHelpInfo()
    {
        if ($("#pagename").html() == "Usuarios") {

            var hints = [
            {
                element: document.querySelector('#tabtable'),
                hint: "This is a tooltip.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('#tabdetails'),
                hint: "This is a tooltip.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.assign'),
                hint: "This is a tooltip.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.detail'),
                hint: "This is a tooltip.",
                hintPosition: 'top'
            },
            {
                element: document.querySelector('.edit'),
                hint: "This is a tooltip.",
                hintPosition: 'top'
            }
            ,
            {
                element: document.querySelector('.delete'),
                hint: "This is a tooltip.",
                hintPosition: 'top'
            }
            ];
            

            addHints(hints);
        }
        else if ($("#pagename").html() == "Roles") {

        }
        else if ($("#pagename").html() == "Inicio") {

        }
        else if ($("#pagename").html() == "Recursos") {

        }
        else if ($("#pagename").html() == "Tipo_Programa") {

        }
        else if ($("#pagename").html() == "Comunidades") {

        }
        else if ($("#pagename").html() == "Control_Personal") {

        }
        else if ($("#pagename").html() == "Bitacora") {

        }
        else if ($("#pagename").html() == "Plan_Semanal") {

        }
        else if ($("#pagename").html() == "Puesto") {

        }
        else if ($("#pagename").html() == "Asignar_RRHH") {

        }
        else if ($("#pagename").html() == "Programa") {

        }
        else if ($("#pagename").html() == "Inscripcion") {

        }
        else if ($("#pagename").html() == "Actividades") {

        }
        else if ($("#pagename").html() == "Control_Asistencia") {

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



    

});


