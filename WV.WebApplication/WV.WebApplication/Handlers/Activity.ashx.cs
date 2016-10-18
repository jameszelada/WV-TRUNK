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
    /// Summary description for Activity
    /// </summary>
    public class Activity : ActionTemplate, IHttpHandler, IRequiresSessionState
    {

        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IDataRepository<Actividad> _actividad;
        IAWContext _context1;
        IDataRepository<Programa> _programa;
        IAWContext _fakeContext;
        IDataRepository<Actividad> _fakeActividad;

        IAWContext _context3;
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
                case "getprograms":
                    context.Response.Write(GetAllProgramsWithoutActivities());
                    break;
                //case "delete":
                //    context.Response.Write(DeleteRecord(context));
                //    break;
                case "getsingle":
                    context.Response.Write(GetSingleRecord(context));
                    break;
                case "add":
                    context.Response.Write(AddRecord(context));
                    break;
                case "edit":
                    context.Response.Write(EditRecord(context));
                    break;
                case "exists":
                    context.Response.Write(ExistsActivityInProgram(context));
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
            _context3 = new AWContext(Connection);
            _actividad = new DataRepository<IAWContext, Actividad>(_context);
            _context1 = new AWContext();
            _programa = new DataRepository<IAWContext, Programa>(_context);
            _fakeContext = new AWContext();
            _fakeActividad = new DataRepository<IAWContext, Actividad>(_context3);
        }

        public override string GetAllRecords()
        {
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader += "<tbody>";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var programas = _actividad.GetAll().GroupBy(act=> act.ID_Programa).Select(group=>group.First()).Select((x, index) => new
                {
                    index,
                    x.Programa
                    
                });


                foreach (var programa in programas)
                {
                    int index = programa.index + 1;
                    string estado = "";
                    switch (programa.Programa.Estado)
                    {
                        case "A":
                            estado = "Activo";
                            break;
                        case "I":
                            estado = "Inactivo";
                            break;
                        case "S":
                            estado = "Suspendido";
                            break;
                    }
                    string showButton = "<a data-id-program='" + programa.Programa.ID_Programa + "'class='btn btn-primary btn-sm details'>Mostrar</a>";
                    string editButton = "<a data-id-program='" + programa.Programa.ID_Programa + "' class='btn btn-primary btn-sm edit'>Editar</a>";
                    tableBody += "<tr><td>" + index + "</td><td>" +programa.Programa.Codigo+"-"+ programa.Programa.TipoPrograma.TipoPrograma1 +"-"+programa.Programa.Comunidad.Comunidad1+ "</td><td>" + estado + "</td><td>" + showButton + "</td><td>" + editButton + "</td></tr>";
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

        public override string GetSingleRecord(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Programa = Int32.Parse(context.Request.Params["ID_Programa"].ToString());
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader += "<tbody>";
            string optionsProgramas = "";

            try
            {
                var programa = _programa.GetFirst(p=> p.ID_Programa == ID_Programa);
                optionsProgramas += "<option data-id-programs='" + programa.ID_Programa + "'>" + programa.Codigo + "-" + programa.TipoPrograma.TipoPrograma1 + "-" + programa.Comunidad.Comunidad1 + "</option>";
                foreach (var actividad in programa.Actividad.OrderByDescending(act=>act.Fecha))
                {
                    long mili = Convert.ToInt64( actividad.Fecha.Subtract(
                    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                        ).TotalMilliseconds);

                    string estado = "";
                    switch (actividad.Estado)
                    {
                        case "A":
                            estado = "Activo";
                            break;
                        case "I":
                            estado = "Inactivo";
                            break;
                        case "S":
                            estado = "Suspendido";
                            break;
                    }

                    tableBody += "<tr><td></td><td>" + actividad.Codigo + "</td><td>" + actividad.ActividadDescripcion + "</td><td>" + estado + "</td><td>" + actividad.Fecha.ToShortDateString() + "<label class='hidden'>"+mili+"</label></td><td>" + actividad.Observacion + "</td></tr>";
                }
                tableFooter += "</tbody>";
                table = tableHeader + tableBody + tableFooter;

                string NombrePrograma= programa.Codigo+"-"+programa.TipoPrograma.TipoPrograma1+"-"+programa.Comunidad.Comunidad1;
                var fakeProgram = new
                {
                    ID_Programa = programa.ID_Programa,
                    Programa=NombrePrograma,
                    Actividades=table,
                    ComboPrograma=optionsProgramas
                };

                response.IsSucess = true;
                response.ResponseData = fakeProgram;
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
                    if (programa.Actividad.Count == 0)
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

        public override string DeleteRecord(HttpContext context)
        {
            throw new NotImplementedException();
        }

        public override string AddRecord(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            DateTime Fecha;
            try
            {
                var data = context.Request;
                var sr = new StreamReader(data.InputStream);
                var stream = sr.ReadToEnd();
                var javaScriptSerializer = new JavaScriptSerializer();
                var programaTemp = javaScriptSerializer.Deserialize<ProgramaTemp>(stream);

                var programa = _programa.GetFirst(p=> p.ID_Programa == programaTemp.ID_Programa);

                if (programa.Actividad.Count == 0)
                {
                    var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    foreach (var actividadTemp in programaTemp.Actividades)
                    {
                        Actividad actividad = new Actividad();
                        actividad.ID_Programa = actividadTemp.ID_Programa;
                        actividad.Codigo = actividadTemp.Codigo;
                        actividad.ActividadDescripcion = actividadTemp.Descripcion;
                        actividad.Estado = actividadTemp.Estado[0].ToString();
                        actividad.Observacion = actividadTemp.Observacion;
                        Fecha = epoch.AddMilliseconds(Convert.ToInt64(actividadTemp.Fecha));
                        actividad.Fecha = Fecha;
                        actividad.CreadoPor = SystemUsername;

                        programa.Actividad.Add(actividad);
                    }
                }

                _context.SaveChanges();

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
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            DateTime Fecha;
            try
            {
                var data = context.Request;
                var sr = new StreamReader(data.InputStream);
                var stream = sr.ReadToEnd();
                var javaScriptSerializer = new JavaScriptSerializer();
                var programaTemp = javaScriptSerializer.Deserialize<ProgramaTemp>(stream);

                var programa = _programa.GetFirst(p => p.ID_Programa == programaTemp.ID_Programa);
                var OriginalActivities = new List<DateTime>();
                var CurrentActivties = new List<DateTime>();
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var ID_Programa = programa.Actividad.First().ID_Programa;
                programa.Actividad.ToList().ForEach((act) => { OriginalActivities.Add(act.Fecha); });
                programaTemp.Actividades.ForEach((act) => 
                { 
                    Fecha = epoch.AddMilliseconds(Convert.ToInt64(act.Fecha));
                    CurrentActivties.Add(Fecha);
                });

                var ToDelete = OriginalActivities.Except(CurrentActivties);

                foreach (var activityToDelete in ToDelete.ToList())
                {
                   // var actividad = _actividad.GetFirst();
                    var actividad =_fakeContext.Set<Actividad>().Single(act => act.Fecha == activityToDelete && act.ID_Programa == ID_Programa );
                    _fakeContext.Set<Actividad>().Remove(actividad);
                    _fakeContext.SaveChanges();
                }

                if (programa.Actividad.Count > 0)
                {
                    foreach (var actividadTemp in programaTemp.Actividades)
                    {
                        Fecha = epoch.AddMilliseconds(Convert.ToInt64(actividadTemp.Fecha));

                        if (programa.Actividad.Any(act => act.Fecha == Fecha))
                        {
                            var actividad = _actividad.GetFirst(act => (act.Fecha == Fecha && act.ID_Programa == actividadTemp.ID_Programa));
                            actividad.ID_Programa = actividadTemp.ID_Programa;
                            actividad.Codigo = actividadTemp.Codigo;
                            actividad.ActividadDescripcion = actividadTemp.Descripcion;
                            actividad.Estado = actividadTemp.Estado[0].ToString();
                            actividad.Observacion = actividadTemp.Observacion;
                            actividad.ModificadoPor = SystemUsername;

                        }
                        else
                        {
                            Actividad actividad = new Actividad();
                            actividad.ID_Programa = actividadTemp.ID_Programa;
                            actividad.Codigo = actividadTemp.Codigo;
                            actividad.ActividadDescripcion = actividadTemp.Descripcion;
                            actividad.Estado = actividadTemp.Estado[0].ToString();
                            actividad.Observacion = actividadTemp.Observacion;
                            Fecha = epoch.AddMilliseconds(Convert.ToInt64(actividadTemp.Fecha));
                            actividad.Fecha = Fecha;
                            actividad.CreadoPor = SystemUsername;

                            programa.Actividad.Add(actividad);
                        }
                    }
                }

                var programaOriginal = _fakeContext.Set<Programa>().First(p => p.ID_Programa == programaTemp.ID_Programa);



                _context.SaveChanges();

                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Registro Modificado Satisfactoriamente";
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
                bool exists = programa.Actividad.Any(act => act.Fecha.ToShortDateString() == Fecha.ToShortDateString());
                int id = 0;
                string mode = "";
                string codigo = "", description = "", observations = "";
                if (exists)
                {
                    id = programa.Actividad.First(act => act.Fecha.ToShortDateString() == Fecha.ToShortDateString()).ID_Actividad;
                    mode = programa.Actividad.First(act => act.ID_Actividad == id).Asistencia.Count == 0 ? "add" : "edit";
                    var actividad = _actividad.GetFirst(act=>act.ID_Actividad == id);
                    codigo = actividad.Codigo;
                    description = actividad.ActividadDescripcion;
                    observations= actividad.Observacion;

                }

                var fullObject = new { Exists = exists, ID_Actividad = id, Mode = mode ,Codigo=codigo,Descripcion=description,Observacion=observations};

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

        [Serializable]
        public class ProgramaTemp
        {
            public int ID_Programa { get; set; }
            public List<Actividades> Actividades { get; set; }
            public List<int> ToDelete { get; set; }
        }
        [Serializable]
        public class Actividades
        {
            public int ID_Actividad { get; set; }
            public string Codigo { get; set; }
            public string Descripcion { get; set; }
            public string Estado { get; set; }
            public string Fecha { get; set; }
            public string Observacion { get; set; }
            public int ID_Programa { get; set; }
        }
    }
}