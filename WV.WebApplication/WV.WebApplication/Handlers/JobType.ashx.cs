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
    /// Summary description for JobType
    /// </summary>
    public class JobType : ActionTemplate, IHttpHandler, IRequiresSessionState
    {
        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
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
            _tipoPuesto = new DataRepository<IAWContext, TipoPuesto>(_context);
        }

        public override string GetAllRecords()
        {
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader = "<div class='table-responsive'>";
            tableHeader += "<table class='table table-striped'>";
            tableHeader += "<thead><tr><th>No</th><th>Tipo de Puesto</th><th>Descripción</th></tr></thead>";
            tableHeader += "<tbody>";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var tipoPuesto = _tipoPuesto.GetAll().Select((x, index) => new
                {
                    index,
                    x.ID_TipoPuesto,
                    x.TipoPuesto1,
                    x.TipoPuestoDescripcion

                });
                foreach (var tipoPuesto1 in tipoPuesto)
                {
                    int index = tipoPuesto1.index + 1;
                   
                    string showButton = "<a data-id-jobtype='" + tipoPuesto1.ID_TipoPuesto + "'class='btn btn-primary btn-sm detail'>Mostrar</a>";
                    string editButton = "<a data-id-jobtype='" + tipoPuesto1.ID_TipoPuesto + "' class='btn btn-primary btn-sm edit'>Editar</a>";
                    string deleteButton = "<a data-id-jobtype='" + tipoPuesto1.ID_TipoPuesto + "' class='btn btn-primary btn-sm delete' data-toggle='modal' data-target='#modalmessage'>Eliminar</a>";
                    tableBody += "<tr><td>" + index + "</td><td>" + tipoPuesto1.TipoPuesto1 + "</td><td>" + tipoPuesto1.TipoPuestoDescripcion+ "</td><td>" + showButton + "</td><td>" + editButton + "</td><td>" + deleteButton + "</td></tr>";
                }
                tableFooter += "</tbody></table></div>";
                table = tableHeader + tableBody + tableFooter;

                if (tipoPuesto.ToList().Count > 0)
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
            int ID_TipoPuesto = Int32.Parse(context.Request.Params["Id_TipoPuesto"].ToString());

            try
            {
                var tipoPuesto = _tipoPuesto.GetFirst(u => u.ID_TipoPuesto == ID_TipoPuesto);

                response.IsSucess = true;
                response.ResponseData = tipoPuesto;
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
            int ID_TipoPuesto = Int32.Parse(context.Request.Params["Id_TipoPuesto"].ToString());
            try
            {
                var tipoPuesto = _tipoPuesto.GetFirst(u => u.ID_TipoPuesto == ID_TipoPuesto);
                _tipoPuesto.Delete(tipoPuesto);
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
            string tipoPuesto1 = "", tipoPuestoDescripcion = "";

            tipoPuesto1 = context.Request.Params["tipoPuesto"];
            tipoPuestoDescripcion = context.Request.Params["tipoPuestoDescipcion"];
          

            try
            {
                TipoPuesto tipoPuesto = new TipoPuesto();
                tipoPuesto.TipoPuesto1 = tipoPuesto1;
                tipoPuesto.TipoPuestoDescripcion = tipoPuestoDescripcion;
                tipoPuesto.CreadoPor = SystemUsername;
                

                _tipoPuesto.Add(tipoPuesto);
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
            int ID_TipoPuesto = Int32.Parse(context.Request.Params["Id_TipoPuesto"].ToString());

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string tipoPuesto1 = "", tipoPuestoDescripcion = "";

            tipoPuesto1 = context.Request.Params["tipoPuesto"];
            tipoPuestoDescripcion = context.Request.Params["tipoPuestoDescipcion"];

            

            try
            {
                var tipoPuesto = _tipoPuesto.GetFirst(u => u.ID_TipoPuesto == ID_TipoPuesto);
                tipoPuesto.TipoPuesto1 = tipoPuesto1;
                tipoPuesto.TipoPuestoDescripcion = tipoPuestoDescripcion;
                tipoPuesto.ModificadoPor = SystemUsername;
                
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