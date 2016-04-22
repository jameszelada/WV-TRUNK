using DataLayer;
using Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using WV.WebApplication.Utils;

namespace WV.WebApplication.Handlers
{
    /// <summary>
    /// Summary description for RoleConfiguration
    /// </summary>
    public class RoleConfiguration : IHttpHandler, IRequiresSessionState
    {

        string connection = ConfigurationManager.ConnectionStrings["VISIONMUNDIALEntities"].ConnectionString;
        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IDataRepository<Usuario> _usuario;
        IDataRepository<Rol> _rol;
        IDataRepository<UsuarioRol> _usuarioRol;
        IDataRepository<RolRecurso> _rolRecurso;
        IDataRepository<Recurso> _recurso;
        public void ProcessRequest(HttpContext context)
        {
            InitializeObjects();
            MethodName = context.Request.Params["method"];
            CallBackMethodName = context.Request.Params["callbackmethod"];
            Parameter = context.Request.Params["parameter"];

            switch (MethodName.ToLower())
            {
         
                case "getallroles":
                    context.Response.Write(GetAllRoles());
                    break;
                case "getsingle":
                    context.Response.Write(GetSingleRole(context));
                    break;
                case "getallresources":
                    context.Response.Write(GetAllResources());
                    break;
                case "getoptionsbyrole":
                    context.Response.Write(GetResourcesInRole(context));
                    break;
                case "add":
                    context.Response.Write(AddResourcesToRole(context));
                    break;
                case "delete":
                    context.Response.Write(DeleteRoleResource(context));
                    break;
                
            }
        }

        private void InitializeObjects()
        {

            _context = new AWContext(connection);

            //_usuario = new DataRepository<IAWContext, Usuario>(_context);
            _rol = new DataRepository<IAWContext, Rol>(_context);
            _rolRecurso = new DataRepository<IAWContext, RolRecurso>(_context);
            _recurso = new DataRepository<IAWContext, Recurso>(_context);

            //_usuarioRol = new DataRepository<IAWContext, UsuarioRol>(_context);
        }

        public string GetAllResources()
        {
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader += "<thead><tr><th></th><th>Nombre de Opción</th><th>Descripción</th></tr></thead>";
           
            tableHeader += "<tbody>";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                    var recursos = _recurso.GetAll().Select((x, index) => new
                    {
                        index,
                        x.ID_Recurso,
                       x.Recurso1,
                       x.Pagina


                    });


                foreach (var recurso in recursos)
                {
                    
                    tableBody += "<tr data-id-recurso='"+recurso.ID_Recurso+"'><td class='bs-checkbox'><input data-index='"+recurso.index+"' name='btSelectItem' type='checkbox'></td><td>" + recurso.Recurso1 + "</td><td>" + recurso.Pagina + "</td></tr>";
                    
                }

                tableFooter += "</tbody>";
                table = tableHeader + tableBody + tableFooter;

                if (recursos.ToList().Count > 0)
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

        public string GetAllRoles()
        {
            string buttons="";

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
                    buttons += "<button type='button' data-toggle='tooltip' data-placement='left' title='Seleccionar el Rol' class='btn btn-primary' data-id-role='" + rol.ID_Rol + "'>" + rol.Rol1 + "</button></br>";
                   
                }

                if (roles.ToList().Count > 0)
                {
                    response.IsSucess = true;
                    response.ResponseData = buttons;
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
            int ID_Rol = Int32.Parse(context.Request.Params["Id_Rol"].ToString());
            try
            {
                var rol = _rol.GetFirst(r => r.ID_Rol == ID_Rol);
                response.IsSucess = true;
                response.ResponseData = rol;
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

        public string GetResourcesInRole(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Rol = Int32.Parse(context.Request.Params["Id_Rol"].ToString());
            try
            {
                var rol = _rol.GetAll();
                var rolRecurso = _rolRecurso.GetAll();
                var recurso = _recurso.GetAll();


                var resultedJoin = from rl in rol
                                   from rlrec in rolRecurso
                                   .Where(rlrec => (rlrec.ID_Rol == rl.ID_Rol))
                                   //.DefaultIfEmpty()
                                   select new { Rol_ID = rl.ID_Rol == null ? 0 : rl.ID_Rol, Rec_ID = rlrec.ID_Recurso == null ? 0 : rlrec.ID_Recurso };

                var resultsByRole = from joined in resultedJoin
                                    from rec in recurso
                                    .Where(rec => (rec.ID_Recurso == joined.Rec_ID) && (joined.Rol_ID == ID_Rol))
                                    //.DefaultIfEmpty()
                                    select new { rec.Recurso1, rec.Pagina, ID_Recurso = rec.ID_Recurso == null ? 0 : rec.ID_Recurso };

                string tableHeader = "", tableBody = "", tableFooter = "", table = "";
                tableHeader += "<thead><tr><th>No</th><th>Nombre de Opción</th><th>Descripción</th><tr><th></th></tr></thead>";

                tableHeader += "<tbody>";
                int index = 0;
                foreach (var resource in resultsByRole)
                {
                    index++;
                    string deleteButton = "<a data-id-resource='" + resource.ID_Recurso + "' class='btn btn-primary btn-sm delete' >Eliminar</a>";
                    tableBody += "<tr data-id-resource='" + resource.ID_Recurso + "'><td>" + index + "</td><td>" + resource.Recurso1 + "</td><td>" + resource.Pagina + "</td><td>" + deleteButton + "</td></tr>";
                }
                tableFooter += "</tbody>";

                table = tableHeader + tableBody + tableFooter;

                
                response.IsSucess = true;
                response.ResponseData = table;
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

        public string AddResourcesToRole(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Rol = Int32.Parse(context.Request.Params["Id_Rol"].ToString());
            string [] resourcesToAdd = context.Request.Form["OptionsArray[]"].Split(',');

            

            try
            {
                foreach (string resource in resourcesToAdd)
                {

                    int ID_Recurso = Int32.Parse(resource);
                    RolRecurso rolRecurso = new RolRecurso();
                    rolRecurso.ID_Rol = ID_Rol;
                    rolRecurso.ID_Recurso = ID_Recurso;
                    _rolRecurso.Add(rolRecurso);
                }

                _context.SaveChanges();
                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Opciones guardadas satisfactoriamente";
                response.CallBack = string.Empty;

            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public string DeleteRoleResource(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Rol = Int32.Parse(context.Request.Params["Id_Rol"].ToString());
            int ID_Recurso = Int32.Parse(context.Request.Params["Id_Recurso"].ToString());
            try
            {
                var rolRecurso = _rolRecurso.GetFirst(rr=> rr.ID_Rol==ID_Rol && rr.ID_Recurso ==ID_Recurso);
                _rolRecurso.Delete(rolRecurso);
                _context.SaveChanges();
                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Opción Eliminada Satisfactoriamente";
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