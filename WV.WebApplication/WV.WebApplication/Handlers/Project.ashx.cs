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

namespace WV.WebApplication.Handlers
{
    /// <summary>
    /// Summary description for Project
    /// </summary>
    public class Project :ActionTemplate, IHttpHandler,IRequiresSessionState
    {
        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IDataRepository<Proyecto> _proyecto;
        IAWContext _context1;
        IDataRepository<AsignacionRecursoHumano> _rrhh;

        public void ProcessRequest(HttpContext context)
        {
            InitializeObjects();
            MethodName = context.Request.Params["method"];
            CallBackMethodName = context.Request.Params["callbackmethod"];
            Parameter = context.Request.Params["parameter"];

            switch (MethodName.ToLower())
            {
                case "getall":
                    context.Response.Write(GetAllRecords());
                    break;
                case "delete":
                    context.Response.Write(DeleteRecord(context));
                    break;
                case "getsingle":
                    context.Response.Write(GetSingleRecord(context));
                    break;
                case "add":
                    context.Response.Write(AddRecord(context));
                    break;
                case "edit":
                    context.Response.Write(EditRecord(context));
                    break;
                case "getrrhh":
                    context.Response.Write(GetHHRR(context));
                    break;
                case "deleterrhh":
                    context.Response.Write(DeleteHHRR(context));
                    break;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public override void InitializeObjects()
        {
            _context = new AWContext(Connection);
            _proyecto = new DataRepository<IAWContext, Proyecto>(_context);
            _context1 = new AWContext();
            _rrhh = new DataRepository<IAWContext, AsignacionRecursoHumano>(_context1);
        }

        public override string GetAllRecords()
        {
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader = "<div class='table-responsive'>";
            tableHeader += "<table class='table table-striped'>";
            tableHeader += "<thead><tr><th>No</th><th>Codigo</th><th>Estado</th></tr></thead>";
            tableHeader += "<tbody>";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var proyecto = _proyecto.GetAll().Select((x, index) => new
                {
                    index,
                    x.ID_Proyecto,
                    x.Codigo,
                    x.ProyectoDescripcion,
                    x.Estado
                   
                });

                foreach (var project in proyecto)
                {
                    int index = project.index + 1;
                    string estado = "";
                    string bgcolor = "";
                    if (project.Estado == "A")
                    {
                        estado = "Activo";
                    }
                    else if (project.Estado == "I")
                    {
                        estado = "Inactivo";
                        bgcolor = "#efef7f";
                    }
                    else if (project.Estado == "S")
                    {
                        estado = "Suspendido";
                        bgcolor = "#ffa807";
                    }
                    string showButton = "<a data-id-project='" + project.ID_Proyecto + "'class='btn btn-primary btn-sm detail'>Mostrar</a>";
                    string editButton = "<a data-id-project='" + project.ID_Proyecto + "' class='btn btn-primary btn-sm edit'>Editar</a>";
                    string deleteButton = "<a data-id-project='" + project.ID_Proyecto + "' class='btn btn-primary btn-sm delete' data-toggle='modal' data-target='#modalmessage'>Eliminar</a>";
                    tableBody += "<tr bgcolor='"+bgcolor+"'><td>" + index + "</td><td>" + project.Codigo + "</td><td>" + estado + "</td><td>" + showButton + "</td><td>" + editButton + "</td><td>" + deleteButton + "</td></tr>";
                }

                tableFooter += "</tbody></table></div>";
                table = tableHeader + tableBody + tableFooter;

                if (proyecto.ToList().Count > 0)
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

        public override string GetSingleRecord(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Proyecto = Int32.Parse(context.Request.Params["Id_Proyecto"].ToString());

            try
            {
                var proyecto = _proyecto.GetFirst(u => u.ID_Proyecto == ID_Proyecto);
                
                response.IsSucess = true;
                response.ResponseData = proyecto;
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

        public string GetHHRR(HttpContext context)
        {
            string content = "",message="";
            int ID_Proyecto = Int32.Parse(context.Request.Params["Id_Proyecto"].ToString());
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var rrhh = _rrhh.Where(p=>p.ID_Proyecto == ID_Proyecto);

                foreach (var persona in rrhh)
                {
                
                    content += "<a class='list-group-item'><i class='fa fa-fw fa-user'></i>"+persona.Persona.Nombre +" "+persona.Persona.Apellido+"</a>";
                }   

                if (content == "")
                {
                    message = "No hay Personal asignado a este proyecto";
                    response.ResponseData = message;
                    response.ResponseAdditional = "0";
                }
                else
                {
                    response.ResponseData = content;
                    response.ResponseAdditional = "1";
                }
                response.IsSucess = true;
                response.CallBack = string.Empty;
                response.Message = string.Empty;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public string DeleteHHRR(HttpContext context)
        {
           
            int ID_Proyecto = Int32.Parse(context.Request.Params["Id_Proyecto"].ToString());
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var rrhh = _rrhh.Where(p => p.ID_Proyecto == ID_Proyecto);

                foreach (var persona in rrhh)
                {
                    AsignacionRecursoHumano asignacion = _rrhh.GetFirst(asi=> asi.ID_AsignacionRecursoHumano == persona.ID_AsignacionRecursoHumano);
                    _rrhh.Delete(asignacion);
                }

                _context1.SaveChanges();

                response.ResponseData = string.Empty;
                response.IsSucess = true;
                response.Message = "Asignacion Eliminada Satisfactoriamente";
                response.CallBack = string.Empty;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public override string DeleteRecord(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Proyecto = Int32.Parse(context.Request.Params["Id_Proyecto"].ToString());
            try
            {
                var proyecto = _proyecto.GetFirst(u => u.ID_Proyecto == ID_Proyecto);
                _proyecto.Delete(proyecto);
                _context.SaveChanges();
                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Registro Eliminado Satisfactoriamente";
                response.CallBack = string.Empty;

            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public override string AddRecord(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string codigo = "", proyectoDescripcion = "", estado = "";

            codigo = context.Request.Params["codigo"];
            proyectoDescripcion = context.Request.Params["descripcionProyecto"];
            estado = context.Request.Params["estado"];

            try
            {
                Proyecto proyecto = new Proyecto();
                proyecto.Codigo = codigo;
                proyecto.ProyectoDescripcion = proyectoDescripcion;
                proyecto.Estado = estado;
                proyecto.CreadoPor = SystemUsername;
               
                _proyecto.Add(proyecto);
                _context.SaveChanges();
                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Registro Creado Satisfactoriamente";
                response.CallBack = string.Empty;

            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public override string EditRecord(HttpContext context)
        {
            int ID_Proyecto = Int32.Parse(context.Request.Params["Id_Proyecto"].ToString());

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string codigo = "", proyectoDescripcion = "", estado = "";

            codigo = context.Request.Params["codigo"];
            proyectoDescripcion = context.Request.Params["descripcionProyecto"];
            estado = context.Request.Params["estado"];
    
            try
            {
                var proyecto = _proyecto.GetFirst(u => u.ID_Proyecto == ID_Proyecto);
                proyecto.Codigo = codigo;
                proyecto.ProyectoDescripcion = proyectoDescripcion;
                proyecto.Estado = estado;
                proyecto.ModificadoPor = SystemUsername;
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
    }
}