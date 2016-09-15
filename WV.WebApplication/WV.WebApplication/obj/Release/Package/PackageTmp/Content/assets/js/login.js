$(document).ready(function () {
    
    $('[data-toggle="tooltip"]').tooltip({
        placement:'rigth'
    });

    $('#contrasenia').keypress(function (event) {
        if (event.keyCode == 13) {
            $('#login').click();
        }
    });

    $('#nombreusuario').focus();

    $('#login').click(function () {

        var username = $("#nombreusuario").val();
        var password = $("#contrasenia").val();

        var dataToSend = { user: username, pass:password };

        $.ajax({
            type: 'POST',
            url: '/Handlers/Authenticate.ashx',
            data: dataToSend ,
            success: function (data) {

                var response = JSON.parse(data);
                if (response.IsSucess)
                {
                    submitDataToHome();
                } else
                {
                    setErrorMessage(response.Message);
                }
            },
            error: function () {
                alert("error Authentication");
            }
        });

    });

    function setErrorMessage(error)
    {
        $("#errormessage").html(error).addClass("errormessage alert-danger");
    }

    function submitDataToHome()
    {
        $("#form1").attr({
            action: "Home"
        }).submit();
    }
});