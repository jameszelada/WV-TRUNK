<%@Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WV.WebApplication.Pages.Login"  ClientIDMode="Static"%>

<!DOCTYPE html>
<html >
  <head runat="server">
    <title runat="server">Visión Mundial - Iniciar Sesión</title>
    <meta charset="UTF-8">
    <link rel="stylesheet" href="/Content/assets/css/normalize.css">
    <link rel="stylesheet" href="/Content/assets/css/style.css">
    <script src="/Content/assets/js/prefixfree.min.js"></script>
    <script src="../Content/assets/js/jquery-1.10.2.js"></script> 
  </head>
  <body>
    <div class="login">
	<h1>Login</h1>
    <form id="form1" runat="server">
    	<input id="nombreusuario" type="text" name="u" placeholder="Username" required="required" />
        <input id="contrasenia" type="password" name="p" placeholder="Password" required="required" />
        <div id="errormessage" class=""></div>
        <button id="login" type="button" class="btn btn-primary btn-block btn-large">Log In</button>
    </form>
</div>
      <script src="../Content/assets/js/login.js"></script> 
  </body>
</html>
