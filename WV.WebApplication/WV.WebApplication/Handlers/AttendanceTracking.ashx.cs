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
                //case "delete":
                //    context.Response.Write(DeleteRecord(context));
                //    break;
                case "getmembers":
                    context.Response.Write(GetAllMembersInProgram(context));
                    break;
                case "add":
                    context.Response.Write(AddRecord(context));
                    break;
                case "edit":
                    context.Response.Write(EditRecord(context));
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
                    tableBody += "<tr><td>"+index+"</td><td>" + beneficiario.Nombre +" "+beneficiario.Apellido + "</td><td>" +groupContainer + presenteButton + ausenteButton+ "</div></td></tr>";
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