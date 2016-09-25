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
    /// Summary description for Program
    /// </summary>
    public class Program : ActionTemplate, IHttpHandler, IRequiresSessionState
    {
        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;

        IAWContext _context;
        IDataRepository<Programa> _programa;
        IAWContext _context1;
        IDataRepository<Comunidad> _comunidad;
        IAWContext _context2;
        IDataRepository<TipoPrograma> _tipoPrograma;
        IAWContext _context3;
        IDataRepository<Proyecto> _proyecto;
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
                case "getinfo":
                    context.Response.Write(GetInfo(context));
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
            _context = new AWContext();
            _programa = new DataRepository<IAWContext, Programa>(_context);

            _context1 = new AWContext();
            _comunidad = new DataRepository<IAWContext, Comunidad>(_context);

            _context2 = new AWContext();
            _tipoPrograma = new DataRepository<IAWContext, TipoPrograma>(_context);

            _context3 = new AWContext();
            _proyecto = new DataRepository<IAWContext, Proyecto>(_context);


        }

        public override string GetAllRecords()
        {
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader += "<tbody>";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var programas = _programa.GetAll().Select((x, index) => new
                {
                    index,
                    x.Codigo,
                    x.ID_Programa,
                    x.Estado,
                    x.TipoPrograma

                });

                foreach (var programa in programas)
                {
                    int index = programa.index + 1;
                    string estado = "";
                    switch (programa.Estado)
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

                    string showProgram = "<a data-id-program='" + programa.ID_Programa + "'class='btn btn-primary btn-sm detail'>Mostrar</a>";
                    string editProgram = "<a data-id-program='" + programa.ID_Programa + "' class='btn btn-primary btn-sm edit'>Editar</a>";
                    tableBody += "<tr><td>" + index + "</td><td>" + programa.Codigo + "</td><td>" + estado + "</td><td>" + programa.TipoPrograma.TipoPrograma1 + "</td><td>" + showProgram + "</td><td>" + editProgram + "</td></tr>";
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

        public string GetInfo(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string optionsComunidades = "", optionsTipoPrograma = "",optionsProyecto="";
            try
            {
                //var comunidad = _comunidad.GetFirst(u => u.ID_Comunidad == ID_Comunidad);
                var comunidades = _comunidad.Select();
                var tiposProgramas = _tipoPrograma.Select();
                var proyectos = _proyecto.Select();

                foreach (var comunidad in comunidades)
                {
                    optionsComunidades += "<option data-id-community='" + comunidad.ID_Comunidad + "'>" + comunidad.Comunidad1 + "</option>";
                }

                foreach (var tipoPrograma in tiposProgramas)
                {
                    optionsTipoPrograma += "<option data-id-programtype='" + tipoPrograma.ID_TipoPrograma + "'>" + tipoPrograma.TipoPrograma1 + "</option>";
                }

                foreach (var proyecto in proyectos)
                {
                    optionsProyecto += "<option data-id-project='" + proyecto.ID_Proyecto + "'>" + proyecto.Codigo + "</option>";
                }
                var fullObject = new
                {
                    Proyecto=optionsProyecto,
                    Comunidad = optionsComunidades,
                    TipoPrograma = optionsTipoPrograma
                };

                response.IsSucess = true;
                response.ResponseData = fullObject;
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
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Programa = Int32.Parse(context.Request.Params["ID_Programa"].ToString());

            try
            {
                var programa = _programa.GetFirst(u => u.ID_Programa == ID_Programa);
                
                var fakeProgram = new {
                    ID_Proyecto= programa.ID_Proyecto,
                    ID_Comunidad = programa.ID_Comunidad,
                    ID_TipoPrograma = programa.ID_TipoPrograma,
                    Estado= programa.Estado,
                    Codigo= programa.Codigo,
                    ProgramaDescripcion = programa.ProgramaDescripcion,
                    FechaInicio = programa.FechaInicio.ToShortDateString(),
                    FechaFinal = programa.FechaFinal.ToShortDateString(),
                    Proyecto= programa.Proyecto.Codigo,
                    Comunidad = programa.Comunidad.Comunidad1,
                    TipoPrograma= programa.TipoPrograma.TipoPrograma1,
                    FechaInicioD= programa.FechaInicio,
                    FechaFinalD = programa.FechaFinal
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

        public override string DeleteRecord(HttpContext context)
        {
            throw new NotImplementedException();
        }

        public override string AddRecord(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string codigo = "", proyectoDescripcion = "", estado = "";
            int ID_Proyecto, ID_Comunidad, ID_TipoPrograma;
            DateTime FechaInicio, FechaFinal;

            codigo = context.Request.Params["Codigo"];
            proyectoDescripcion = context.Request.Params["ProgramaDescripcion"];
            estado = context.Request.Params["Estado"];
            ID_TipoPrograma = Int32.Parse(context.Request.Params["ID_TipoPrograma"]);
            ID_Comunidad = Int32.Parse(context.Request.Params["ID_Comunidad"]);
            ID_Proyecto = Int32.Parse(context.Request.Params["ID_Proyecto"]);

            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            FechaInicio = epoch.AddMilliseconds(Convert.ToInt64(context.Request.Params["FechaInicio"]));
            FechaFinal = epoch.AddMilliseconds(Convert.ToInt64(context.Request.Params["FechaFinal"]));

            try
            {
                Programa programa = new Programa();
                programa.ID_Comunidad = ID_Comunidad;
                programa.ID_Proyecto = ID_Proyecto;
                programa.ID_TipoPrograma = ID_TipoPrograma;
                programa.Codigo = codigo;
                programa.ProgramaDescripcion = proyectoDescripcion;
                programa.Estado = estado;
                programa.FechaFinal = FechaFinal;
                programa.FechaInicio = FechaInicio;
                programa.CreadoPor = SystemUsername;


                _programa.Add(programa); 
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
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Programa = Int32.Parse(context.Request.Params["ID_Programa"].ToString());

            string codigo = "", proyectoDescripcion = "", estado = "";
            int ID_Proyecto, ID_Comunidad, ID_TipoPrograma;
            DateTime FechaInicio, FechaFinal;

            codigo = context.Request.Params["Codigo"];
            proyectoDescripcion = context.Request.Params["ProgramaDescripcion"];
            estado = context.Request.Params["Estado"];
            ID_TipoPrograma = Int32.Parse(context.Request.Params["ID_TipoPrograma"]);
            ID_Comunidad = Int32.Parse(context.Request.Params["ID_Comunidad"]);
            ID_Proyecto = Int32.Parse(context.Request.Params["ID_Proyecto"]);

            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            FechaInicio = epoch.AddMilliseconds(Convert.ToInt64(context.Request.Params["FechaInicio"]));
            FechaFinal = epoch.AddMilliseconds(Convert.ToInt64(context.Request.Params["FechaFinal"]));

            try
            {
                var programa = _programa.GetFirst(p=>p.ID_Programa == ID_Programa);
                programa.ID_Comunidad = ID_Comunidad;
                programa.ID_Proyecto = ID_Proyecto;
                programa.ID_TipoPrograma = ID_TipoPrograma;
                programa.Codigo = codigo;
                programa.ProgramaDescripcion = proyectoDescripcion;
                programa.Estado = estado;
                programa.FechaFinal = FechaFinal;
                programa.FechaInicio = FechaInicio;
                programa.ModificadoPor = SystemUsername;

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