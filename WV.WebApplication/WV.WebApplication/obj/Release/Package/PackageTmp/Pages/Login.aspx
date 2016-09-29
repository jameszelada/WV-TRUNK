<%@Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WV.WebApplication.Pages.Login"  ClientIDMode="Static"%>

<!DOCTYPE html>
<html >
  <head runat="server">
    <title runat="server">Visión Mundial - Iniciar Sesión</title>
    <meta charset="UTF-8">
      <meta name="viewport" content="width=device-width, initial-scale=1.0">
      <link href="/Content/assets/img/favicon.ico" type="image/x-icon" rel="shortcut icon"/>
      
    <link rel="stylesheet" href="/Content/assets/css/charisma-app.css">
    <link rel="stylesheet" href="/Content/assets/css/bootstrap.css">
    <%--<link rel="stylesheet" href="/Content/assets/css/normalize.css">--%>
    <link rel="stylesheet" href="/Content/assets/css/style.css">
    <script src="/Content/assets/js/prefixfree.min.js"></script>
    <script src="../Content/assets/js/jquery-1.10.2.js"></script> 
    <script type="text/javascript" src="/Content/assets/js/bootstrap.min.js"></script>

      <style type="text/css">

          h2 
          {
              font-size :33px;
          }

          @media(max-width:480px) 
          {
              h2 
              {
                  font-size: 16px;
              }

              .login-header 
              {
                 height:80px;
              }

              #cornervm 
              {
                  display:none;
              }
          }

          @media(max-width:600px) 
          {
              h2 
              {
                  font-size: 16px;
              }  

              .login-header 
              {
                  height:80px;
              }

              #cornervm 
              {
                  display:none;
              }
          }

          @media(max-width:700px) 
          {
              h2 
              {
                   font-size: 16px;
              }  

              .login-header 
              {
                  height:80px;
              }

              #cornervm 
              {
                  display:none;
              }
          }

      </style>

  </head>
  <body>

      
    <img id="cornervm" src="../Content/assets/css/images/vm_transparent.png"  />
        
    <div class="row" style="margin-top: 50px;margin-bottom:20px;">
        <div class="col-md-12 center login-header">
            <h2 style="color:white;font-weight: bold;">Sistema de Administración de Proyectos <br>Visión Mundial El Salvador, Guaymango</h2>
        </div>
        <!--/span-->
    </div><!--/row-->

    <div class="row">
        <div class="well col-md-5 center login-box">
            <div id="errormessage" class="alert alert-info" title="Ingrese su usuario" data-toggle="tooltip">
                Inicie sesion con su usuario y contraseña.
            </div>
            <form class="form-horizontal" id="form1" runat="server">
                <fieldset>
                    <div class="input-group input-group-lg">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user blue"></i></span>
                        <input id="nombreusuario" type="text" name="u" class="form-control" placeholder="Usuario">
                    </div>
                    <div class="clearfix"></div><br>

                    <div class="input-group input-group-lg">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-lock blue"></i></span>
                        <input id="contrasenia" type="password" name="p" class="form-control" placeholder="Contraseña">
                    </div>
                    <div class="clearfix"></div>

                    <div class="input-prepend">
                        <label class="remember" for="remember"><input type="checkbox" id="remember"> Recordar</label>
                    </div>
                    <div class="clearfix"></div>

                    <p class="center col-md-5">
                        <button id="login" type="button" class="btn btn-primary btn-block btn-large">Login</button>
                    </p>
                </fieldset>
            </form>
        </div>
        <!--/span-->
    </div><!--/row-->
      <script src="../Content/assets/js/login.js"></script> 
  </body>
</html>
