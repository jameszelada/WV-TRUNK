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
    /// Summary description for Job
    /// </summary>
    public class Job : ActionTemplate, IHttpHandler, IRequiresSessionState
    {
        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IDataRepository<Puesto> _puesto;
        IAWContext _context1;
        IDataRepository<TipoPuesto> _tipoPuesto;
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
                case "jobtypes":
                    context.Response.Write(getJobTypes(context));
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
            _puesto = new DataRepository<IAWContext, Puesto>(_context);
            _context1= new AWContext(Connection);
            _tipoPuesto = new DataRepository<IAWContext, TipoPuesto>(_context1);
        }

        public override string GetAllRecords()
        {
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader = "<div class='table-responsive'>";
            tableHeader += "<table class='table table-striped'>";
            tableHeader += "<thead><tr><th>No</th><th>Nombre de Puesto</th><th>Descripción</th></tr></thead>";
            tableHeader += "<tbody>";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var puesto = _puesto.GetAll().Select((x, index) => new
                {
                    index,
                    x.ID_Puesto,
                    x.Puesto1,
                    x.PuestoDescripcion

                });
                foreach (var puesto1 in puesto)
                {
                    int index = puesto1.index + 1;

                    string showButton = "<a data-id-job='" + puesto1.ID_Puesto + "'class='btn btn-primary btn-sm detail'>Mostrar</a>";
                    string editButton = "<a data-id-job='" + puesto1.ID_Puesto + "' class='btn btn-primary btn-sm edit'>Editar</a>";
                    string deleteButton = "<a data-id-job='" + puesto1.ID_Puesto + "' class='btn btn-primary btn-sm delete' data-toggle='modal' data-target='#modalmessage'>Eliminar</a>";
                    tableBody += "<tr><td>" + index + "</td><td>" + puesto1.Puesto1 + "</td><td>" + puesto1.PuestoDescripcion + "</td><td>" + showButton + "</td><td>" + editButton + "</td><td>" + deleteButton + "</td></tr>";
                }
                tableFooter += "</tbody></table></div>";
                table = tableHeader + tableBody + tableFooter;

                if (puesto.ToList().Count > 0)
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
            int ID_Puesto = Int32.Parse(context.Request.Params["Id_Puesto"].ToString());

            try
            {
                var puesto = _puesto.GetFirst(u => u.ID_Puesto == ID_Puesto);

                response.IsSucess = true;
                response.ResponseData = puesto;
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
            int ID_Puesto = Int32.Parse(context.Request.Params["Id_Puesto"].ToString());
            try
            {
                var puesto = _puesto.GetFirst(u => u.ID_Puesto == ID_Puesto);
                _puesto.Delete(puesto);
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

        public string getJobTypes(HttpContext context)
        {

             JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string optionsJobType = "";
            try
            {
                var jobTypes = _tipoPuesto.Select();
               
                foreach (var jobType in jobTypes)
                {
                    optionsJobType += "<option data-id-jobtype='" + jobType.ID_TipoPuesto + "'>" + jobType.TipoPuesto1 + "</option>";
                }

                response.IsSucess = true;
                response.ResponseData = optionsJobType;
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

        public override string AddRecord(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string puesto1 = "", puestoDescripcion = "";
            int ID_TipoPuesto;

            puesto1 = context.Request.Params["puesto"];
            puestoDescripcion = context.Request.Params["puestoDescripcion"];
            ID_TipoPuesto = Int32.Parse( context.Request.Params["ID_TipoPuesto"]);


            try
            {
                Puesto puesto = new Puesto();
                puesto.Puesto1 = puesto1;
                puesto.PuestoDescripcion = puestoDescripcion;
                puesto.ID_TipoPuesto = ID_TipoPuesto;
                puesto.CreadoPor = SystemUsername;


                _puesto.Add(puesto);
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
            int ID_Puesto = Int32.Parse(context.Request.Params["Id_Puesto"].ToString());

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string puesto1 = "", puestoDescripcion = "";
            int ID_TipoPuesto;

            puesto1 = context.Request.Params["puesto"];
            puestoDescripcion = context.Request.Params["puestoDescripcion"];
            ID_TipoPuesto = Int32.Parse(context.Request.Params["ID_TipoPuesto"]);


            try
            {
                var puesto = _puesto.GetFirst(u => u.ID_Puesto == ID_Puesto);
                puesto.Puesto1 = puesto1;
                puesto.PuestoDescripcion = puestoDescripcion;
                puesto.ID_TipoPuesto = ID_TipoPuesto;
                puesto.ModificadoPor = SystemUsername;
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