using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WV.WebApplication.Utils;
using DataLayer;
using Repository;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using System.Globalization;
using System.IO;

namespace WV.WebApplication.Handlers
{
    /// <summary>
    /// Summary description for MassActions
    /// </summary>
    public class MassActions : ActionTemplate, IHttpHandler, IRequiresSessionState
    {
        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IDataRepository<Rol> _rol;
        IAWContext _context1;
        IDataRepository<Programa> _programa;
       
        IDataRepository<Beneficiario> _beneficiario;

        IAWContext _context2;
        IDataRepository<Proyecto> _proyecto;
        public void ProcessRequest(HttpContext context)
        {
            InitializeObjects();
            MethodName = context.Request.Params["method"];
            CallBackMethodName = context.Request.Params["callbackmethod"];
            Parameter = context.Request.Params["parameter"];

            switch (MethodName.ToLower())
            {


                case "getroles":
                    context.Response.Write(GetAllRoles());
                    break;
                case "getprograms":
                    context.Response.Write(GetAllPrograms());
                    break;
                case "getall":
                    context.Response.Write(GetAllRecords());
                    break;
                case "getprojects":
                    context.Response.Write(GetAllProjects());
                    break;
                case "deleteroles":
                    context.Response.Write(DeleteMassRoles(context));
                    break;
                case "deletepeople":
                    context.Response.Write(DeleteMassPeople(context));
                    break;
                case "deleteprojects":
                    context.Response.Write(DeleteMassProjects(context));
                    break;
                case "deleteprograms":
                    context.Response.Write(DeleteMassPrograms(context));
                    break;
                case "edit":
                    context.Response.Write(EditRecord(context));
                    break;
            }
        }

        public override void InitializeObjects()
        {
            _context = new AWContext();
            _rol = new DataRepository<IAWContext, Rol>(_context);
            _context1 = new AWContext();
            _programa = new DataRepository<IAWContext, Programa>(_context1);
            _context2 = new AWContext();
            _proyecto = new DataRepository<IAWContext, Proyecto>(_context2);
            _beneficiario = new DataRepository<IAWContext, Beneficiario>(_context);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public string GetAllRoles() 
        {
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader += "<tbody>";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var roles = _rol.GetAll();


                foreach (var rol in roles)
                {
                    
                    tableBody += "<tr data-id-role='"+rol.ID_Rol+"'><td></td><td>" + rol.Rol1 + "</td></tr>";
                }

                tableFooter += "</tbody>";
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

        

        public string GetAllPrograms()
        {
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader += "<tbody>";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var programas = _programa.GetAll();


                foreach (var programa in programas)
                {

                    tableBody += "<tr data-id-program='" + programa.ID_Programa + "'><td></td><td>" + programa.Codigo +"-"+programa.TipoPrograma.TipoPrograma1 + "</td></tr>";
                }

                tableFooter += "</tbody>";
                table = tableHeader + tableBody + tableFooter;

                if (programas.ToList().Count > 0)
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

        public string GetAllProjects()
        {
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader += "<tbody>";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var projects = _proyecto.GetAll();


                foreach (var proyecto in projects)
                {

                    tableBody += "<tr data-id-project='" + proyecto.ID_Proyecto + "'><td></td><td>" + proyecto.Codigo  + "</td></tr>";
                }

                tableFooter += "</tbody>";
                table = tableHeader + tableBody + tableFooter;

                if (projects.ToList().Count > 0)
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

        public override string GetAllRecords()
        {
            string nombre = "";
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader += "<tbody>";
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var beneficiarios = _beneficiario.Select();


                foreach (var beneficiario in beneficiarios)
                {
                    tableBody += "<tr data-id-beneficiario='" + beneficiario.ID_Beneficiario + "'><td></td><td>" + beneficiario.Nombre + " " + beneficiario.Apellido + "</td></tr>";
                }
                tableFooter += "</tbody>";
                table = tableHeader + tableBody + tableFooter;
                if (beneficiarios.ToList().Count > 0)
                {
                    response.IsSucess = true;
                    response.ResponseData = table;
                    response.Message = string.Empty;
                    response.CallBack = string.Empty;
                    response.ResponseAdditional = nombre;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public string DeleteMassRoles(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string [] Ids  = context.Request.QueryString["ToDelete[]"].Split(','); 
            
            try
            {

                foreach (string id in Ids)
                {
                    int idToDelete= Int32.Parse(id);

                    bool exists = _rol.GetAll().ToList().Exists(r => r.ID_Rol == idToDelete);
                    if (exists)
                    {
                        Rol rol = _rol.GetFirst(r => r.ID_Rol == idToDelete);
                        _rol.Delete(rol);
                    }
                    
                    
                }

                _context.SaveChanges();


                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Registros Eliminados Satisfactoriamente";
                response.CallBack = string.Empty;

            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        
        }

        public string DeleteMassProjects(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string[] Ids = context.Request.QueryString["ToDelete[]"].Split(',');

            try
            {

                foreach (string id in Ids)
                {
                    int idToDelete = Int32.Parse(id);

                    bool exists = _proyecto.GetAll().ToList().Exists(r => r.ID_Proyecto == idToDelete);
                    if (exists)
                    {
                        Proyecto proyecto = _proyecto.GetFirst(r => r.ID_Proyecto == idToDelete);
                        _proyecto.Delete(proyecto);
                    }


                }

                _context2.SaveChanges();


                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Registros Eliminados Satisfactoriamente";
                response.CallBack = string.Empty;

            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);

        }

        public string DeleteMassPrograms(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string[] Ids = context.Request.QueryString["ToDelete[]"].Split(',');

            try
            {

                foreach (string id in Ids)
                {
                    int idToDelete = Int32.Parse(id);

                    bool exists = _programa.GetAll().ToList().Exists(r => r.ID_Programa == idToDelete);
                    if (exists)
                    {
                        Programa programa = _programa.GetFirst(r => r.ID_Programa == idToDelete);
                        _programa.Delete(programa);
                    }


                }

                _context1.SaveChanges();


                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Registros Eliminados Satisfactoriamente";
                response.CallBack = string.Empty;

            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);

        }

        public string DeleteMassPeople(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string[] Ids = context.Request.QueryString["ToDelete[]"].Split(',');

            try
            {

                foreach (string id in Ids)
                {
                    int idToDelete = Int32.Parse(id);

                    bool exists = _beneficiario.GetAll().ToList().Exists(r => r.ID_Beneficiario == idToDelete);
                    if (exists)
                    {
                        Beneficiario beneficiario = _beneficiario.GetFirst(r => r.ID_Beneficiario == idToDelete);
                        _beneficiario.Delete(beneficiario);
                    }


                }

                _context.SaveChanges();


                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Registros Eliminados Satisfactoriamente";
                response.CallBack = string.Empty;

            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);

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