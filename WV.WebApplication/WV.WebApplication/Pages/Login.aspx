<%@Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WV.WebApplication.Pages.Login"  ClientIDMode="Static"%>

<!DOCTYPE html>
<html >
  <head runat="server">
    <title runat="server">Visión Mundial - Iniciar Sesión</title>
    <meta charset="UTF-8">
      <link href="/Content/assets/img/favicon.ico" type="image/x-icon" rel="shortcut icon"/>
      
    <link rel="stylesheet" href="/Content/assets/css/charisma-app.css">
    <link rel="stylesheet" href="/Content/assets/css/bootstrap.css">
    <%--<link rel="stylesheet" href="/Content/assets/css/normalize.css">--%>
    <link rel="stylesheet" href="/Content/assets/css/style.css">
    <script src="/Content/assets/js/prefixfree.min.js"></script>
    <script src="../Content/assets/js/jquery-1.10.2.js"></script> 
    <script type="text/javascript" src="/Content/assets/js/bootstrap.min.js"></script>
  </head>
  <body>
   <%-- <div class="login">
	<h1>Login</h1>
    <form id="form1" runat="server">
    	<input id="nombreusuario" type="text" name="u" placeholder="Username" required="required" />
        <input id="contrasenia" type="password" name="p" placeholder="Password" required="required" />
        <div id="errormessage" class=""></div>
        <button id="login" type="button" class="btn btn-primary btn-block btn-large">Log In</button>
    </form>
</div>--%>
      
    
        
    <div class="row">
        <div class="col-md-12 center login-header">
            <h2 style="color:white;">SAP Vision Mundial El Salvador</h2>
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
