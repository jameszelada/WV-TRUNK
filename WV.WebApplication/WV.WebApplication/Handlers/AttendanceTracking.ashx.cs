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
    /// Summary description for AttendanceTracking
    /// </summary>
    public class AttendanceTracking : ActionTemplate, IHttpHandler, IRequiresSessionState
    {
        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IDataRepository<Actividad> _actividad;
        IAWContext _context1;
        IDataRepository<Programa> _programa;

        public void ProcessRequest(HttpContext context)
        {
            InitializeObjects();
            MethodName = context.Request.Params["method"];
            CallBackMethodName = context.Request.Params["callbackmethod"];
            Parameter = context.Request.Params["parameter"];

            switch (MethodName.ToLower())
            {

                case "getactivities":
                    context.Response.Write(GetAllActivitiesInProgram(context));
                    break;
                case "getprograms":
                    context.Response.Write(GetAllProgramsWithoutActivities());
                    break;
                case "exists":
                    context.Response.Write(ExistsActivityInProgram(context));
                    break;
                case "getmembers":
                    context.Response.Write(GetAllMembersInProgram(context));
                    break;
                case "add":
                    context.Response.Write(AddRecord(context));
                    break;
                case "edit":
                    context.Response.Write(EditRecord(context));
                    break;
                case "states":
                    context.Response.Write(GetStatesFromActivity(context));
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
            _actividad = new DataRepository<IAWContext, Actividad>(_context);
            _context1 = new AWContext();
            _programa = new DataRepository<IAWContext, Programa>(_context);
        }

        public string GetAllProgramsWithoutActivities()
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string optionsProgramas = "";
            try
            {
                var programas = _programa.Select();


                foreach (var programa in programas)
                {
                    if (programa.Actividad.Count > 0)
                    {
                        optionsProgramas += "<option data-id-programs='" + programa.ID_Programa + "'>" + programa.Codigo + "-" + programa.TipoPrograma.TipoPrograma1 + "-" + programa.Comunidad.Comunidad1 + "</option>";
                    }
      
                }
                response.IsSucess = true;
                response.ResponseData = optionsProgramas;
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

        public string GetAllActivitiesInProgram(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Programa = Int32.Parse(context.Request.Params["ID_Programa"].ToString());
            List<long> Fechas = new List<long>();
            try
            {
                var programa = _programa.GetFirst(p => p.ID_Programa == ID_Programa);

                foreach (var actividad in programa.Actividad)
                {
                    long mili = Convert.ToInt64(actividad.Fecha.Subtract(
                    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                        ).TotalMilliseconds);

                    Fechas.Add(mili);
                }
               
                response.IsSucess = true;
                response.ResponseData = Fechas;
                response.Message = string.Empty;
                response.CallBack = string.Empty;

            }
            catch (Exception msg)
            {
                response.Message = msg.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public string GetAllMembersInProgram(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Programa = Int32.Parse(context.Request.Params["ID_Programa"].ToString());
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader += "<tbody>";
            string groupContainer = "";
            string presenteButton = "", ausenteButton = "";
            List<long> Fechas = new List<long>();
            try
            {
                var programa = _programa.GetFirst(p => p.ID_Programa == ID_Programa);
                int index = 1;
                foreach (var beneficiario in programa.Beneficiario)
                {
                    groupContainer = "<div class='btn-group' data-toggle='buttons'>";
                    presenteButton = "<label class='btn btn-primary'><input type='radio' name='options"+index+"' data-id-type='1' >Presente</input></label>";
                    ausenteButton = "<label class='btn btn-primary'><input type='radio' name='options" + index + "' data-id-type='2'>Ausente</input></label>";
                    tableBody += "<tr><td>" + index + "</td><td data-id-beneficiario='" + beneficiario.ID_Beneficiario + "'>" + beneficiario.Nombre + " " + beneficiario.Apellido + "</td><td>" + groupContainer + presenteButton + ausenteButton + "</div></td></tr>";
                    index++;
                }

                tableFooter += "</tbody>";
                table = tableHeader + tableBody + tableFooter;

                response.IsSucess = true;
                response.ResponseData = table;
                response.Message = string.Empty;
                response.CallBack = string.Empty;

            }
            catch (Exception msg)
            {
                response.Message = msg.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public string ExistsActivityInProgram(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Programa = Int32.Parse(context.Request.Params["ID_Programa"].ToString());
            long FechaActividad = Int64.Parse(context.Request.Params["FechaActividad"].ToString());
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            try
            {
                var programa = _programa.GetFirst(p => p.ID_Programa == ID_Programa);
                DateTime Fecha = epoch.AddMilliseconds(FechaActividad);
                bool exists = programa.Actividad.Any(act=> act.Fecha.ToShortDateString() == Fecha.ToShortDateString());
                int id = 0;
                string mode = "";
                if (exists)
                {
                    id = programa.Actividad.First(act => act.Fecha.ToShortDateString() == Fecha.ToShortDateString()).ID_Actividad;
                    mode = programa.Actividad.First(act=> act.ID_Actividad==id).Asistencia.Count == 0 ? "add":"edit";
                }

                var fullObject = new { Exists=exists,ID_Actividad=id , Mode=mode};

                response.IsSucess = true;
                response.ResponseData = fullObject;
                response.Message = string.Empty;
                response.CallBack = string.Empty;

            }
            catch (Exception msg)
            {
                response.Message = msg.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
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
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            
            try
            {
                var data = context.Request;
                var sr = new StreamReader(data.InputStream);
                var stream = sr.ReadToEnd();
                var javaScriptSerializer = new JavaScriptSerializer();
                var actividadTemp = javaScriptSerializer.Deserialize<ActividadTemp>(stream);

                var actividad = _actividad.GetFirst(a => ((a.ID_Programa == actividadTemp.ID_Programa) && (a.ID_Actividad == actividadTemp.ID_Actividad)));

                if (actividad.Asistencia.Count==0)
                {
                    foreach (var asistenciaTemp in actividadTemp.Asistencias)
                    {
                        Asistencia asistencia = new Asistencia();
                        asistencia.ID_Actividad = asistenciaTemp.ID_Actividad;
                        asistencia.ID_Beneficiario = asistenciaTemp.ID_Beneficiario;
                        asistencia.Estado = asistenciaTemp.Estado;

                        actividad.Asistencia.Add(asistencia);
                    }
                }
                _context.SaveChanges();

                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Registros Creados Satisfactoriamente";
                response.CallBack = string.Empty;

            }
            catch (Exception msg)
            {
                response.Message = msg.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public string GetStatesFromActivity(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Programa = Int32.Parse(context.Request.Params["ID_Programa"].ToString());
            int ID_Actividad = Int32.Parse(context.Request.Params["ID_Actividad"].ToString());
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader += "<tbody>";
            string groupContainer = "";
            string presenteButton = "", ausenteButton = "";
            List<long> Fechas = new List<long>();
            try
            {
                var programa = _programa.GetFirst(p => p.ID_Programa == ID_Programa);
                var actividad = _actividad.GetFirst(a=> a.ID_Actividad == ID_Actividad);
                int index = 1;
                
                var Asistencias = programa.Actividad.First(a => a.ID_Actividad == ID_Actividad).Asistencia;
                foreach (var asistencia in Asistencias)
                {
                    groupContainer = "<div class='btn-group' data-toggle='buttons'>";
                    if (asistencia.Estado == "Presente")
                    {
                        presenteButton = "<label class='btn btn-primary active'><input type='radio' name='options" + index + "' data-id-type='1' >Presente</input></label>";
                        ausenteButton = "<label class='btn btn-primary'><input type='radio' name='options" + index + "' data-id-type='2'>Ausente</input></label>";
                    }
                    else if (asistencia.Estado == "Ausente")
                    {
                        presenteButton = "<label class='btn btn-primary'><input type='radio' name='options" + index + "' data-id-type='1' >Presente</input></label>";
                        ausenteButton = "<label class='btn btn-primary active'><input type='radio' name='options" + index + "' data-id-type='2'>Ausente</input></label>";
                    }
                    tableBody += "<tr><td>" + index + "</td><td data-id-beneficiario='" + asistencia.Beneficiario.ID_Beneficiario + "'>" + asistencia.Beneficiario.Nombre + " " + asistencia.Beneficiario.Apellido + "</td><td>" + groupContainer + presenteButton + ausenteButton + "</div></td></tr>";
                    index++;
                }

                tableFooter += "</tbody>";
                table = tableHeader + tableBody + tableFooter;

                response.IsSucess = true;
                response.ResponseData = table;
                response.Message = string.Empty;
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
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                var data = context.Request;
                var sr = new StreamReader(data.InputStream);
                var stream = sr.ReadToEnd();
                var javaScriptSerializer = new JavaScriptSerializer();
                var actividadTemp = javaScriptSerializer.Deserialize<ActividadTemp>(stream);

                var actividad = _actividad.GetFirst(a => ((a.ID_Programa == actividadTemp.ID_Programa) && (a.ID_Actividad == actividadTemp.ID_Actividad)));

                if (actividad.Asistencia.Count > 0)
                {
                    foreach (var asistenciaTemp in actividadTemp.Asistencias)
                    {

                        foreach (var asistencia in actividad.Asistencia)
                        {
                            if (asistenciaTemp.ID_Beneficiario == asistencia.ID_Beneficiario && asistenciaTemp.ID_Actividad == asistencia.ID_Actividad)
                            {
                                asistencia.Estado = asistenciaTemp.Estado;
                                break;
                            }
                        }
                    }
                }
                _context.SaveChanges();

                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Registros Modificados Satisfactoriamente";
                response.CallBack = string.Empty;

            }
            catch (Exception msg)
            {
                response.Message = msg.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        [Serializable]
        public class ActividadTemp
        {
            public int ID_Actividad { get; set; }
            public int ID_Programa { get; set; }
            public List<Asistencias> Asistencias { get; set; }
            public List<int> ToDelete { get; set; }
        }
        [Serializable]
        public class Asistencias
        {
            public int ID_Actividad { get; set; }
            public int ID_Beneficiario { get; set; }
            public string Estado { get; set; }
            public int ID_Asistencia { get; set; }
        }
    }
}