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
    /// Summary description for Role
    /// </summary>
    public class Role : IHttpHandler, IRequiresSessionState
    {
        string connection = ConfigurationManager.ConnectionStrings["VISIONMUNDIALEntities"].ConnectionString;
        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;

        IAWContext _context;
        IDataRepository<Rol> _rol;
        public void ProcessRequest(HttpContext context)
        {
            InitializeObjects();
            MethodName = context.Request.Params["method"];
            CallBackMethodName = context.Request.Params["callbackmethod"];
            Parameter = context.Request.Params["parameter"];

            switch (MethodName.ToLower())
            {
                case "getall":
                    context.Response.Write(GetAllRoles());
                    break;
                case "getsingle":
                    context.Response.Write(GetSingleRole(context));
                    break;
                case "add":
                    context.Response.Write(AddRole(context));
                    break;
                case "edit":
                    context.Response.Write(EditRole(context));
                    break;
            }
        }

        private void InitializeObjects()
        {

            _context = new AWContext(connection);
            _rol = new DataRepository<IAWContext, Rol>(_context);
        }

        public string GetAllRoles()
        {
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader = "<div class='table-responsive'>";
            tableHeader += "<table class='table table-striped'>";
            tableHeader += "<thead><tr><th>No</th><th>Nombre de Rol</th><th>Descripción</th></tr></thead>";
            tableHeader += "<tbody>";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var roles = _rol.GetAll().Select((x, index) => new
                {
                    index,
                    x.ID_Rol,
                    x.Rol1,
                    x.Descripcion,
                    
                });

                foreach (var rol in roles)
                {
                    int index = rol.index + 1;
                    string showButton = "<a data-id-role='" + rol.ID_Rol + "'class='btn btn-primary btn-sm detail'>Mostrar</a>";
                    string editButton = "<a data-id-role='" + rol.ID_Rol + "' class='btn btn-primary btn-sm edit'>Editar</a>";
                    tableBody += "<tr><td>" + index + "</td><td>" + rol.Rol1 + "</td><td>" + rol.Descripcion + "</td><td>" + showButton + "</td><td>" + editButton + "</td></tr>";
                }

                tableFooter += "</tbody></table></div>";
                table = tableHeader + tableBody + tableFooter;

                if (roles.ToList().Count > 0)
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

        public string GetSingleRole(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Rol = Int32.Parse(context.Request.Params["Id_Role"].ToString());
            try
            {
                var usuario = _rol.GetFirst(u => u.ID_Rol == ID_Rol);

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

        public string AddRole(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string rol1 = "", descripcion = "";
            rol1 = context.Request.Params["rol"];
            descripcion = context.Request.Params["descripcion"];
            
            try
            {
                Rol rol = new Rol();
                rol.Rol1 = rol1;
                rol.Descripcion = descripcion;
                _rol.Add(rol);
                _context.SaveChanges();
                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Rol Creado Satisfactoriamente";
                response.CallBack = string.Empty;

            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public string EditRole(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Rol= Int32.Parse(context.Request.Params["Id_Rol"].ToString());
            string rol1 = "", descripcion = "";
            rol1 = context.Request.Params["rol"];
            descripcion = context.Request.Params["descripcion"];
            

            try
            {
                var rol= _rol.GetFirst(u => u.ID_Rol == ID_Rol);
                 rol.Rol1 = rol1;
                rol.Descripcion = descripcion;
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}