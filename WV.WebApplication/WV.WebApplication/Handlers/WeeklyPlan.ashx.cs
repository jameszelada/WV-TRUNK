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
    /// Summary description for WeeklyPlan
    /// </summary>
    public class WeeklyPlan : ActionTemplate, IHttpHandler, IRequiresSessionState
    {
        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IAWContext _context1;
        IDataRepository<PlanSemanal> _planSemanal;
        IDataRepository<PlanSemanalDetalle> _planSemanalDetalle;
        IAWContext _context2;
        IDataRepository<Persona> _persona;
        IAWContext _lazyContext;
        public void ProcessRequest(HttpContext context)
        {
            InitializeObjects();
            MethodName = context.Request.Params["method"];
            CallBackMethodName = context.Request.Params["callbackmethod"];
            Parameter = context.Request.Params["parameter"];

            switch (MethodName.ToLower())
            {
                case "getall":
                    context.Response.Write(GetAllWeeklyPlans(context));
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

            _planSemanal = new DataRepository<IAWContext, PlanSemanal>(_context);

            _context1 = new AWContext(Connection);

            _planSemanalDetalle = new DataRepository<IAWContext, PlanSemanalDetalle>(_context1);

            _context2 = new AWContext(Connection);

            _persona = new DataRepository<IAWContext, Persona>(_context2);

            _lazyContext = new AWContext();

        }

        public override string GetAllRecords()
        {
            throw new NotImplementedException();
        }

        public string GetAllWeeklyPlans(HttpContext context)
        {
            int ID_Persona = Int32.Parse(context.Request.Params["ID_Persona"].ToString());

            string htmlElements = "";
            string nombre = "";
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var planesSemanales = _planSemanal.Where(p => p.ID_Persona == ID_Persona);
                var persona = _persona.GetFirst(p => p.ID_Persona == ID_Persona);
                nombre = persona.Nombre + " " + persona.Apellido;
                foreach (var plan in planesSemanales)
                {
                    htmlElements += "<li data-weeklyplan-header='" + plan.ID_PlanSemanal + "'><a>" + plan.FechaInicio.ToShortDateString() + "<span class='sub_icon glyphicon glyphicon-link'></span></a>";
                }

                if (planesSemanales.ToList().Count > 0)
                {
                    response.IsSucess = true;
                    response.ResponseData = htmlElements;
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

        public override string GetSingleRecord(HttpContext context)
        {
            int ID_PlanSemanal = Int32.Parse(context.Request.Params["ID_PlanSemanal"].ToString());


            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var planSemanal = _planSemanal.GetFirst(p => p.ID_PlanSemanal == ID_PlanSemanal);
                int idParent = planSemanal.ID_PlanSemanal;
                var planSemanalDetalle = _planSemanalDetalle.Where(bd => bd.ID_PlanSemanal == idParent);

                var finalObject = new { ObjetoPlanSemanal = planSemanal, ObjetoPlanSemanalDetalle = planSemanalDetalle };

                response.IsSucess = true;
                response.ResponseData = finalObject;
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
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_PlanSemanal = Int32.Parse(context.Request.Params["ID_PlanSemanal"].ToString());
            try
            {
                var planSemanal = _planSemanal.GetFirst(u => u.ID_PlanSemanal == ID_PlanSemanal);
                _planSemanal.Delete(planSemanal);
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
            DateTime FechaInicio;
            DateTime FechaFinal;
            try
            {
                var data = context.Request;
                var sr = new StreamReader(data.InputStream);
                var stream = sr.ReadToEnd();
                var javaScriptSerializer = new JavaScriptSerializer();
                var planSemanal = javaScriptSerializer.Deserialize<PlanesTemp>(stream);

                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                FechaInicio = epoch.AddMilliseconds(Convert.ToInt64(planSemanal.FechaInicio));
                FechaFinal = epoch.AddMilliseconds(Convert.ToInt64(planSemanal.FechaFinal));

                PlanSemanal planEncabezado = new PlanSemanal();
                planEncabezado.ID_Persona = planSemanal.ID_Persona;
                planEncabezado.FechaInicio = FechaInicio;
                planEncabezado.FechaFinal = FechaFinal;
                planEncabezado.CreadoPor = SystemUsername;

                foreach (var detalle in planSemanal.PlanSemanalDetalle)
                {
                    PlanSemanalDetalle planSemanalDetalle = new PlanSemanalDetalle();
                    planSemanalDetalle.Actividad = detalle.Actividad;
                    planSemanalDetalle.Observaciones = detalle.Observaciones;
                    planSemanalDetalle.Recurso = detalle.Recurso;
                    planSemanalDetalle.CreadoPor = SystemUsername;
                    planEncabezado.PlanSemanalDetalle.Add(planSemanalDetalle);

                }

                _planSemanal.Add(planEncabezado);
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

            try
            {
                var data = context.Request;
                var sr = new StreamReader(data.InputStream);
                var stream = sr.ReadToEnd();
                var javaScriptSerializer = new JavaScriptSerializer();
                var planEncabezado = javaScriptSerializer.Deserialize<PlanesTemp>(stream);

                int ID_PlanSemanal = planEncabezado.ID_Persona; // uso el id pero no es necesariamente es de la persona sino del plan semanal
                _planSemanal = new DataRepository<IAWContext, PlanSemanal>(_lazyContext);

                var planSemanal = _planSemanal.GetFirst(b => b.ID_PlanSemanal == ID_PlanSemanal);
                planSemanal.ModificadoPor = SystemUsername;
                //Forma menos complicada borrar todos los detalles e insertar los nuevos


                foreach (var detalle in planSemanal.PlanSemanalDetalle.ToList())
                {
                    var planDetalle = _lazyContext.Set<PlanSemanalDetalle>().Single(c => c.ID_PlanSemanalDetalle == detalle.ID_PlanSemanalDetalle);
                    _lazyContext.Set<PlanSemanalDetalle>().Remove(planDetalle);
                    _lazyContext.SaveChanges();
                }



                //Insercion de nuevos detalles
                foreach (var detalle in planEncabezado.PlanSemanalDetalle)
                {
                    PlanSemanalDetalle planDetalle = new PlanSemanalDetalle();
                    planDetalle.Actividad = detalle.Actividad;
                    planDetalle.Observaciones = detalle.Observaciones;
                    planDetalle.Recurso = detalle.Recurso;
                    planDetalle.CreadoPor = SystemUsername;
                    planSemanal.PlanSemanalDetalle.Add(planDetalle);

                }

                _lazyContext.SaveChanges();

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

        [Serializable]
        public class PlanesTemp
        {
            public int ID_Persona { get; set; }
            public string FechaInicio { get; set; }
            public string FechaFinal { get; set; }
            public List<PlanTemp> PlanSemanalDetalle { get; set; }
            public List<int> ToDelete { get; set; }
        }
        [Serializable]
        public class PlanTemp
        {
            public int ID_PlanSemanalDetalle { get; set; }
            public string Actividad { get; set; }
            public string Observaciones { get; set; }
            public string Recurso { get; set; }
        }
    }
}