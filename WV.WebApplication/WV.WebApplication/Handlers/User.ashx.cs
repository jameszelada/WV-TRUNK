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
    /// Summary description for User
    /// </summary>
    public class User :ActionTemplate, IHttpHandler, IRequiresSessionState
    {
        string connection = ConfigurationManager.ConnectionStrings["VISIONMUNDIALEntities"].ConnectionString;
        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IDataRepository<Usuario> _usuario;
        IDataRepository<Rol> _rol;
        IDataRepository<UsuarioRol> _usuarioRol;
        public void ProcessRequest(HttpContext context)
        {
            InitializeObjects();
            MethodName = context.Request.Params["method"];
            CallBackMethodName = context.Request.Params["callbackmethod"];
            Parameter = context.Request.Params["parameter"];

            switch (MethodName.ToLower())
            {
                case "getall":
                    context.Response.Write(GetAllUsers());
                    break;
                case "getallroles":
                    context.Response.Write(GetAllRoles());
                    break;
                case "delete":
                    context.Response.Write(DeleteUser(context));
                    break;
                case "getsingle":
                    context.Response.Write(GetSingleUser(context));
                    break;
                case "add":
                    context.Response.Write(AddUser(context));
                    break;
                case "edit":
                    context.Response.Write(EditUser(context));
                    break;
                case "saveuserrole":
                    context.Response.Write(AssignUserRol(context));
                    break;
            }
        }

        public override void InitializeObjects()
        {

            _context = new AWContext(connection);
            
            _usuario = new DataRepository<IAWContext, Usuario>(_context);
            _rol = new DataRepository<IAWContext, Rol>(_context);
            _usuarioRol = new DataRepository<IAWContext, UsuarioRol>(_context);
        }

        public string GetAllRoles()
        {
            string options = "";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var roles = _rol.GetAll().Select((x, index) => new
                {
                    index,
                    x.ID_Rol,
                   x.Rol1
                });

                foreach (var rol in roles)
                {
                    int index = rol.index + 1;
                    options += "<option data-id-role='"+rol.ID_Rol+"'>"+rol.Rol1+"</option>";
                }

                

                if (roles.ToList().Count > 0)
                {
                    response.IsSucess = true;
                    response.ResponseData = options;
                    response.Message = string.Empty;
                    response.CallBack = string.Empty;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public string GetAllUsers()
        {
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader = "<div class='table-responsive'>";
            tableHeader += "<table class='table table-striped'>";
            tableHeader += "<thead><tr><th>No</th><th>Username</th><th>Nombre</th><th>Apellido</th><th>Email</th><th></th><th></th></tr></thead>";
            tableHeader += "<tbody>";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var usuarios = _usuario.GetAll().Select((x, index) => new {
                    index,
                    x.NombreUsuario,
                    x.Nombre,
                    x.Apellido,
                    x.Email,
                    x.ID_Usuario,
                   
                   
                });


                foreach (var usuario in usuarios)
                {
                    int index =usuario.index +1;
                    string assignRoleButton = "<a data-id-user='" + usuario.ID_Usuario + "' data-toggle='modal' data-target='#modalrole' class='btn btn-primary btn-sm assign'>Asignar Rol</a>";
                    string showButton= "<a data-id-user='"+usuario.ID_Usuario+"'class='btn btn-primary btn-sm detail'>Mostrar</a>";
                    string editButton = "<a data-id-user='" + usuario.ID_Usuario + "' class='btn btn-primary btn-sm edit'>Editar</a>";
                    string deleteButton = "<a data-id-user='" + usuario.ID_Usuario + "' class='btn btn-primary btn-sm delete' data-toggle='modal' data-target='#modalmessage'>Eliminar</a>";
                    tableBody += "<tr><td>" + index + "</td><td>" + usuario.NombreUsuario + "</td><td>" + usuario.Nombre + "</td><td>" + usuario.Apellido + "</td><td>" + usuario.Email + "</td><td>" + assignRoleButton + "</td><td>" + showButton + "</td><td>" + editButton + "</td><td>" + deleteButton + "</td></tr>";
                }

                tableFooter += "</tbody></table></div>";
                table = tableHeader + tableBody + tableFooter;

                if (usuarios.ToList().Count > 0)
                {
                    response.IsSucess = true;
                    response.ResponseData = table;
                    response.Message = string.Empty;
                    response.CallBack = string.Empty;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public string DeleteUser(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Usuario = Int32.Parse(context.Request.Params["Id_Usuario"].ToString());
            try
            {
                var usuario = _usuario.GetFirst(u=> u.ID_Usuario==ID_Usuario);
                _usuario.Delete(usuario);
                _context.SaveChanges();
                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Usuario Eliminado Satisfactoriamente";
                response.CallBack = string.Empty;

            }
            catch (Exception ex)
            {
                
                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public string GetSingleUser(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Usuario = Int32.Parse(context.Request.Params["Id_Usuario"].ToString());
            try
            {
                var usuario = _usuario.GetFirst(u => u.ID_Usuario == ID_Usuario);
                response.IsSucess = true;
                response.ResponseData = usuario;
                response.Message = string.Empty;
                response.CallBack = string.Empty;

            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        
        }

        public string AddUser(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string username = "", nombre = "", apellido = "", contrasenia = "", email = "";
            username = context.Request.Params["username"];
            nombre = context.Request.Params["nombre"];
            apellido = context.Request.Params["apellido"];
            contrasenia = context.Request.Params["contrasenia"];
            email = context.Request.Params["email"];
            try 
	        {
                Usuario usuario = new Usuario();
                usuario.NombreUsuario = username;
                usuario.Nombre = nombre;
                usuario.Apellido = apellido;
                usuario.Contrasenia = contrasenia;
                usuario.Email = email;
                usuario.CreadoPor = SystemUsername;
                _usuario.Add(usuario);
                _context.SaveChanges();
                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Usuario Creado Satisfactoriamente";
                response.CallBack = string.Empty;
                
	        }
	        catch (Exception ex)
	        {
		
		        response.Message = ex.Message;
                response.IsSucess = false;
	        }
        
            return serializer.Serialize(response);
        }

        public string EditUser(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Usuario = Int32.Parse(context.Request.Params["Id_Usuario"].ToString());
            string username = "", nombre = "", apellido = "", contrasenia = "", email = "";
            username = context.Request.Params["username"];
            nombre = context.Request.Params["nombre"];
            apellido = context.Request.Params["apellido"];
            contrasenia = context.Request.Params["contrasenia"];
            email = context.Request.Params["email"];

            try
            {
                var usuario = _usuario.GetFirst(u => u.ID_Usuario == ID_Usuario);
                usuario.NombreUsuario = username;
                usuario.Nombre = nombre;
                usuario.Apellido = apellido;
                usuario.Contrasenia = contrasenia;
                usuario.Email = email;
                usuario.ModificadoPor = SystemUsername;

                _context.SaveChanges();
                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Edicion realizada Satisfactoriamente";
                response.CallBack = string.Empty;

            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);


        }

        public string AssignUserRol(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_usuario, ID_rol;
            ID_usuario = Int32.Parse(context.Request.Params["ID_Usuario"].ToString());
            ID_rol = Int32.Parse(context.Request.Params["ID_Rol"].ToString());



            try
            {

                bool UserExists = _usuarioRol.GetAll().Where(ur=>ur.ID_Usuario == ID_usuario).ToList().Count > 0 ? true :false;

                if (UserExists)
                {
                    var UsuarioRol = _usuarioRol.GetFirst(ur=> ur.ID_Usuario == ID_usuario);
                    UsuarioRol.ID_Rol = ID_rol;
                    UsuarioRol.ModificadoPor = SystemUsername;
                    _context.SaveChanges();
                    response.IsSucess = true;
                    response.ResponseData = string.Empty;
                    response.Message = "Rol Modificado Satisfactoriamente";
                    response.CallBack = string.Empty;

                }
                else 
                {
                    UsuarioRol usuariorol = new UsuarioRol();
                    usuariorol.ID_Rol = ID_rol;
                    usuariorol.ID_Usuario = ID_usuario;
                    usuariorol.CreadoPor = SystemUsername;
                    _usuarioRol.Add(usuariorol);
                    _context.SaveChanges();
                    response.IsSucess = true;
                    response.ResponseData = string.Empty;
                    response.Message = "Rol Asignado Satisfactoriamente";
                    response.CallBack = string.Empty;
                }

                

            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public override string GetAllRecords()
        {
            throw new NotImplementedException();
        }

        public override string GetSingleRecord(HttpContext context)
        {
            throw new NotImplementedException();
        }

        public override string DeleteRecord(HttpContext context)
        {
            throw new NotImplementedException();
        }

        public override string AddRecord(HttpContext context)
        {
            throw new NotImplementedException();
        }

        public override string EditRecord(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}