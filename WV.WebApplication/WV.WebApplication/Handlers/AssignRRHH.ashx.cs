using System;
using System.IO;
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
    /// Summary description for AssignRRHH
    /// </summary>
    public class AssignRRHH : ActionTemplate, IHttpHandler, IRequiresSessionState
    {
        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IAWContext _context1;
        IDataRepository<Proyecto> _proyecto;
        IDataRepository<Persona> _persona;
        IAWContext _context2;
        IDataRepository<Puesto> _puesto;
        IAWContext _context3;
        IDataRepository<AsignacionRecursoHumano> _asignacion;
        
        public void ProcessRequest(HttpContext context)
        {
            InitializeObjects();
            MethodName = context.Request.Params["method"];
            CallBackMethodName = context.Request.Params["callbackmethod"];
            Parameter = context.Request.Params["parameter"];

            switch (MethodName.ToLower())
            {
                //case "getall":
                //    context.Response.Write(GetAllWeeklyPlans(context));
                //    break;
                case "getpersons":
                    context.Response.Write(GetPersons());
                    break;
                case "getprojects":
                    context.Response.Write(GetProjects());
                    break;
                case "getjobs":
                    context.Response.Write(GetJobs());
                    break;
                case "add":
                    context.Response.Write(AddRecord(context));
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
            _context = new AWContext();

            _proyecto = new DataRepository<IAWContext, Proyecto>(_context);

            _context1 = new AWContext(Connection);

            _persona = new DataRepository<IAWContext, Persona>(_context1);

            _context2 = new AWContext(Connection);

            _puesto = new DataRepository<IAWContext, Puesto>(_context2);

            _context3 = new AWContext(Connection);
            
            _asignacion = new DataRepository<IAWContext, AsignacionRecursoHumano>(_context3);

            
        }

        public override string GetAllRecords()
        {
            throw new NotImplementedException();
        }

        public  string GetPersons()
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string optionsPerson = "";
            try
            {
                var personas = _persona.Select();

                foreach (var persona in personas)
                {
                    optionsPerson += "<option data-id-person='" + persona.ID_Persona + "' data-tel-person='"+persona.Telefono+"'>" + persona.Nombre +" "+persona.Apellido+ "</option>";
                }

                response.IsSucess = true;
                response.ResponseData = optionsPerson;
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
        public string GetProjects()
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string optionsProject = "";
            try
            {
                var proyectos = _proyecto.Select();

                foreach (var proyecto in proyectos)
                {
                    if (proyecto.AsignacionRecursoHumano.Count == 0)
                    {
                        optionsProject += "<option data-id-project='" + proyecto.ID_Proyecto + "'>" + proyecto.Codigo + "</option>";
                    }
                    
                }

                response.IsSucess = true;
                response.ResponseData = optionsProject;
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
        public string GetJobs()
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string optionsJob = "";
            try
            {
                var puestos = _puesto.Select();

                foreach (var puesto in puestos)
                {
                    optionsJob += "<option data-id-job='" + puesto.ID_Puesto + "'>" + puesto.Puesto1 + "</option>";
                }

                response.IsSucess = true;
                response.ResponseData = optionsJob;
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
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            
            try
            {
                var data = context.Request;
                var sr = new StreamReader(data.InputStream);
                var stream = sr.ReadToEnd();
                var javaScriptSerializer = new JavaScriptSerializer();
                var Asignaciones = javaScriptSerializer.Deserialize<Asignacion>(stream);


                foreach (var asignacion in Asignaciones.AsignacionRecursoHumano)
                {
                    AsignacionRecursoHumano asignacionRRHH = new AsignacionRecursoHumano();
                    asignacionRRHH.ID_Proyecto= asignacion.ID_Proyecto;
                    asignacionRRHH.ID_Persona= asignacion.ID_Persona;
                    asignacionRRHH.ID_Puesto = asignacion.ID_Puesto;
                    asignacionRRHH.CreadoPor = SystemUsername;
                    _asignacion.Add(asignacionRRHH);

                }

                _context3.SaveChanges();

                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Registro Creado Satisfactoriamente";
                response.CallBack = string.Empty;

            }
            catch (Exception msg)
            {

                response.Message = msg.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public override string EditRecord(HttpContext context)
        {
            throw new NotImplementedException();
        }

        [Serializable]
        public class Asignacion
        {
           
            public List<AsignacionTemp> AsignacionRecursoHumano { get; set; }
            
        }
        [Serializable]
        public class AsignacionTemp
        {
            public int ID_AsignacionRecursoHumano { get; set; }
            public int ID_Puesto { get; set; }
            public int ID_Persona { get; set; }
            public int ID_Proyecto { get; set; }
           
            
        }
    }
}