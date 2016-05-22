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
    /// Summary description for Community
    /// </summary>
    public class Community : IHttpHandler, IRequiresSessionState
    {

       string connection = ConfigurationManager.ConnectionStrings["VISIONMUNDIALEntities"].ConnectionString;
        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IAWContext _context1;
        IAWContext _context2;
        IDataRepository<Comunidad> _comunidad;
        IDataRepository<Departamento> _departamento;
        IDataRepository<Municipio> _municipio;
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
                case "locations":
                    context.Response.Write(GetDepartmentsMunicipalities(context));
                    break;
                
            }
        }

        private void InitializeObjects()
        {

            _context = new AWContext(connection);
            _context1 = new AWContext(connection);
            _context2 = new AWContext(connection);

            _comunidad = new DataRepository<IAWContext, Comunidad>(_context);
            _departamento = new DataRepository<IAWContext, Departamento>(_context1);
            _municipio = new DataRepository<IAWContext, Municipio>(_context2);
            
        }

        

        public string GetAllRecords()
        {
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader = "<div class='table-responsive'>";
            tableHeader += "<table class='table'>";
            tableHeader += "<thead><tr><th>No</th><th>Nombre de la Comunidad</th></tr></thead>";
            tableHeader += "<tbody>";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var comunidades = _comunidad.GetAll().Select((x, index) => new
                {
                    index,
                    x.Comunidad1,
                    x.ID_Comunidad,
                    x.ID_Municipio
                });

                foreach (var tipos in comunidades)
                {
                    int index = tipos.index + 1;

                    string showButton = "<a data-id-community='" + tipos.ID_Comunidad + "'class='btn btn-primary btn-sm detail'>Mostrar</a>";
                    string editButton = "<a data-id-community='" + tipos.ID_Comunidad + "' class='btn btn-primary btn-sm edit'>Editar</a>";
                    string deleteButton = "<a data-id-community='" + tipos.ID_Comunidad + "' class='btn btn-primary btn-sm delete' data-toggle='modal' data-target='#modalmessage'>Eliminar</a>";
                    tableBody += "<tr><td>" + index + "</td><td>" + tipos.Comunidad1 + "</td><td>" + showButton + "</td><td>" + editButton + "</td><td>" + deleteButton + "</td></tr>";
                }

                tableFooter += "</tbody></table></div>";
                table = tableHeader + tableBody + tableFooter;

                if (comunidades.ToList().Count > 0)
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
            int ID_Comunidad = Int32.Parse(context.Request.Params["Id_Comunidad"].ToString());
            try
            {
                var comunidad = _comunidad.GetFirst(u => u.ID_Comunidad == ID_Comunidad);
                _comunidad.Delete(comunidad);
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
            int ID_Comunidad = Int32.Parse(context.Request.Params["Id_Comunidad"].ToString());
            string optionsDepartmento = "", optionsMunicipio="";
            try
            {
                //var comunidad = _comunidad.GetFirst(u => u.ID_Comunidad == ID_Comunidad);
                var departamentos = _departamento.Select();
                var municipios = _municipio.Select();

                foreach (var departamento in departamentos)
                {
                    optionsDepartmento += "<option data-id-department='" + departamento.ID_Departamento + "'>" + departamento.Departamento1 + "</option>";
                }

                foreach (var municipio in municipios)
                {
                    optionsMunicipio += "<option data-id-municipio='" + municipio.ID_Municipio + "' data-id-municipio-departamento='"+municipio.ID_Departamento+"'>" + municipio.Municipio1 + "</option>";
                }
                var comunidad = _comunidad.GetFirst(u => u.ID_Comunidad == ID_Comunidad);
                var fullObject = new
                {
                    Comunidad= comunidad,
                    Departamentos = optionsDepartmento,
                    Municipios = optionsMunicipio
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

        public string GetDepartmentsMunicipalities(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string optionsDepartmento = "", optionsMunicipio = "";
            try
            {
                //var comunidad = _comunidad.GetFirst(u => u.ID_Comunidad == ID_Comunidad);
                var departamentos = _departamento.Select();
                var municipios = _municipio.Select();

                foreach (var departamento in departamentos)
                {
                    optionsDepartmento += "<option data-id-department='" + departamento.ID_Departamento + "'>" + departamento.Departamento1 + "</option>";
                }

                foreach (var municipio in municipios)
                {
                    optionsMunicipio += "<option data-id-municipio='" + municipio.ID_Municipio + "' data-id-municipio-departamento='" + municipio.ID_Departamento + "'>" + municipio.Municipio1 + "</option>";
                }
        
                var fullObject = new
                {
                    Departamentos = optionsDepartmento,
                    Municipios = optionsMunicipio
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

        public string AddRecord(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string comunidad1 = "";
            int ID_Municipio = Int32.Parse(context.Request.Params["Id_Municipio"].ToString());
            comunidad1 = context.Request.Params["comunidad"];
           
           
            try
            {
                Comunidad comunidad = new Comunidad();
                comunidad.Comunidad1 = comunidad1;
                comunidad.ID_Municipio = ID_Municipio;
                _comunidad.Add(comunidad);
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
            int ID_Comunidad = Int32.Parse(context.Request.Params["Id_Comunidad"].ToString());
            int ID_Municipio = Int32.Parse(context.Request.Params["Id_Municipio"].ToString());
            string comunidad1 = "";
            comunidad1 = context.Request.Params["comunidad"];
            
            

            try
            {
                var comunidad = _comunidad.GetFirst(u => u.ID_Comunidad == ID_Comunidad);
                comunidad.Comunidad1 = comunidad1;
                comunidad.ID_Municipio = ID_Municipio;
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