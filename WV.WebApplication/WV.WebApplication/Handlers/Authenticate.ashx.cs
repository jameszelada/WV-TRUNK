using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using Repository;
using System.Configuration;
using WV.WebApplication.Utils;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace WV.WebApplication.Handlers
{
    /// <summary>
    /// Summary description for Authenticate
    /// </summary>
    public class Authenticate : IHttpHandler, IRequiresSessionState
    {
        string connection = ConfigurationManager.ConnectionStrings["VISIONMUNDIALEntities"].ConnectionString;
        AWContext _context;
        IDataRepository<Usuario> _usuario;
        string _username = "";
        string _password = "";

        public void ProcessRequest(HttpContext context)
        {
            InitializeObjects();
            _username = context.Request.Params["user"];
            _password = context.Request.Params["pass"];

            context.Response.ContentType = "application/*";
            context.Response.Write(ValidateUser(context));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void InitializeObjects() 
        {

            _context = new AWContext(connection);
            _usuario = new DataRepository<IAWContext, Usuario>(_context);
        }

        private bool VerifyUser() 
        {
            bool success = false;
            Usuario user = new Usuario();
            user = _usuario.GetSingle(u => u.NombreUsuario==_username && u.Contrasenia==_password);
            if (user != null)
            {
                success = true;
            }
            return success;
        }

        private string ValidateUser(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            response.ResponseData = string.Empty;
            response.CallBack = string.Empty;
            try
            {
                if (VerifyUser())
                {
                    response.IsSucess = true;
                    response.Message = "Exitosamente Autorizado";
                    context.Session["isActive"] = true;
                }
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Message = "Error en usuario o contraseña, Intente de nuevo.";
            }
            return serializer.Serialize(response);
        }

        private string GetValidOptions()
        {
            var statement = @"
                SELECT RE.Recurso FROM Usuario AS US
                LEFT JOIN UsuarioRol AS UR
                ON US.ID_Usuario = UR.ID_Usuario
                LEFT JOIN  ROL AS RL
                ON RL.ID_Rol = UR.ID_Rol
                LEFT JOIN RolRecurso AS RR 
                ON RL.ID_Rol = RR.ID_Rol
                LEFT JOIN Recurso AS RE
                ON RE.ID_Recurso = RR.ID_Recurso
                WHERE NombreUsuario = "+_username;
            
            //var options = _context.Database. 

           
            return "";
        }


    }
}