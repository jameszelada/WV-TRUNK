using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using Repository;
using System.Configuration;
using WV.WebApplication.Utils;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace WV.WebApplication.Handlers
{
    /// <summary>
    /// Summary description for ProgramType
    /// </summary>
    public class ProgramType : IHttpHandler, IRequiresSessionState
    {

        string connection = ConfigurationManager.ConnectionStrings["VISIONMUNDIALEntities"].ConnectionString;
        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IDataRepository<TipoPrograma> _tipoPrograma;
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

        private void InitializeObjects()
        {

            _context = new AWContext(connection);

            _tipoPrograma = new DataRepository<IAWContext, TipoPrograma>(_context);
            
        }

        

        public string GetAllRecords()
        {
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader = "<div class='table-responsive'>";
            tableHeader += "<table class='table'>";
            tableHeader += "<thead><tr><th>No</th><th>Tipo de Programa</th><th>Descripcion de Programa</th></tr></thead>";
            tableHeader += "<tbody>";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var tiposdeprogramas = _tipoPrograma.GetAll().Select((x, index) => new
                {
                    index,
                    x.TipoPrograma1,
                    x.TipoProgramaDescripcion,
                    x.ID_TipoPrograma
                });

                foreach (var tipos in tiposdeprogramas)
                {
                    int index = tipos.index + 1;
                    
                    string showButton = "<a data-id-program='" + tipos.ID_TipoPrograma+ "'class='btn btn-primary btn-sm detail'>Mostrar</a>";
                    string editButton = "<a data-id-program='" + tipos.ID_TipoPrograma + "' class='btn btn-primary btn-sm edit'>Editar</a>";
                    string deleteButton = "<a data-id-program='" + tipos.ID_TipoPrograma + "' class='btn btn-primary btn-sm delete' data-toggle='modal' data-target='#modalmessage'>Eliminar</a>";
                    tableBody += "<tr><td>" + index + "</td><td>" + tipos.TipoPrograma1 + "</td><td>" + tipos.TipoProgramaDescripcion + "</td><td>" + showButton + "</td><td>" + editButton + "</td><td>" + deleteButton + "</td></tr>";
                }

                tableFooter += "</tbody></table></div>";
                table = tableHeader + tableBody + tableFooter;

                if (tiposdeprogramas.ToList().Count > 0)
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

        public string DeleteRecord(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_TipoPrograma = Int32.Parse(context.Request.Params["Id_TipoPrograma"].ToString());
            try
            {
                var tipoPrograma = _tipoPrograma.GetFirst(u => u.ID_TipoPrograma == ID_TipoPrograma);
                _tipoPrograma.Delete(tipoPrograma);
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

        public string GetSingleRecord(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_TipoPrograma = Int32.Parse(context.Request.Params["Id_TipoPrograma"].ToString());
            try
            {
                var tipoPrograma = _tipoPrograma.GetFirst(u => u.ID_TipoPrograma == ID_TipoPrograma);
                response.IsSucess = true;
                response.ResponseData = tipoPrograma;
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

        public string AddRecord(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string tipoPrograma1 = "", tipoProgramaDescripcion = "";
            tipoPrograma1 = context.Request.Params["tipoPrograma"];
            tipoProgramaDescripcion = context.Request.Params["tipoProgramaDescripcion"];
           
            try
            {
                TipoPrograma tipoPrograma = new TipoPrograma();
                tipoPrograma.TipoPrograma1 = tipoPrograma1;
                tipoPrograma.TipoProgramaDescripcion = tipoProgramaDescripcion;
                
                _tipoPrograma.Add(tipoPrograma);
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

        public string EditRecord(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_TipoPrograma = Int32.Parse(context.Request.Params["Id_TipoPrograma"].ToString());
            string tipoPrograma1 = "", tipoProgramaDescripcion = "";
            tipoPrograma1 = context.Request.Params["tipoPrograma"];
            tipoProgramaDescripcion = context.Request.Params["tipoProgramaDescripcion"];
            

            try
            {
                var tipoPrograma = _tipoPrograma.GetFirst(u => u.ID_TipoPrograma == ID_TipoPrograma);
                tipoPrograma.TipoPrograma1 = tipoPrograma1;
                tipoPrograma.TipoProgramaDescripcion = tipoProgramaDescripcion;
                

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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}