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
    /// Summary description for Staff
    /// </summary>
    public class Staff : ActionTemplate, IHttpHandler,IRequiresSessionState
    {
        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IDataRepository<Persona> _persona;

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
            _persona = new DataRepository<IAWContext, Persona>(_context);
        }

        public override string GetAllRecords()
        {
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader = "<div class='table-responsive'>";
            tableHeader += "<table class='table table-striped'>";
            tableHeader += "<thead><tr><th>No</th><th>Nombre</th><th>Apellido</th><th>Direccion</th></tr></thead>";
            tableHeader += "<tbody>";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var personas = _persona.GetAll().Select((x, index) => new
                {
                    index,
                    x.Apellido,
                    x.Nombre,
                    x.Direccion,
                    x.ID_Persona
                });

                foreach (var persona in personas)
                {
                    int index = persona.index + 1;

                    string showButton = "<a data-id-person='" + persona.ID_Persona+ "'class='btn btn-primary btn-sm detail'>Mostrar</a>";
                    string editButton = "<a data-id-person='" + persona.ID_Persona + "' class='btn btn-primary btn-sm edit'>Editar</a>";
                    string deleteButton = "<a data-id-person='" + persona.ID_Persona + "' class='btn btn-primary btn-sm delete' data-toggle='modal' data-target='#modalmessage'>Eliminar</a>";
                    tableBody += "<tr><td>" + index + "</td><td>" + persona.Nombre + "</td><td>" + persona.Apellido + "</td><td>" + persona.Direccion + "</td><td>" + showButton + "</td><td>" + editButton + "</td><td>" + deleteButton + "</td></tr>";
                }

                tableFooter += "</tbody></table></div>";
                table = tableHeader + tableBody + tableFooter;

                if (personas.ToList().Count > 0)
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
            int ID_Persona = Int32.Parse(context.Request.Params["Id_Persona"].ToString());
           
            try
            {
            
                var persona = _persona.GetFirst(u => u.ID_Persona == ID_Persona);
                string fecha = persona.FechaNacimiento.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                persona.FechaNacimiento = DateTime.ParseExact(fecha ,"yyyy-MM-dd", CultureInfo.InvariantCulture);
                response.IsSucess = true;
                response.ResponseData = persona;
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
            int ID_Persona = Int32.Parse(context.Request.Params["Id_Persona"].ToString());
            try
            {
                var persona = _persona.GetFirst(u => u.ID_Persona == ID_Persona);
                _persona.Delete(persona);
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
            string nombre = "",apellido="", email="",direccion="",telefono="",dui="";
            string  sexo;
            DateTime fechaNacimiento;

            
            nombre = context.Request.Params["nombre"];
            apellido = context.Request.Params["apellido"];
            direccion = context.Request.Params["direccion"];
            email = context.Request.Params["email"];
            telefono = context.Request.Params["telefono"];
            dui  = context.Request.Params["dui"];
            sexo = context.Request.Params["sexo"];
            fechaNacimiento = DateTime.ParseExact(context.Request.Params["fechaNacimiento"], "yyyy-MM-dd", CultureInfo.InvariantCulture);

            try
            {
                Persona persona = new Persona();
                persona.Nombre = nombre;
                persona.Apellido = apellido;
                persona.Direccion = direccion;
                persona.Email = email;
                persona.Dui = dui;
                persona.Telefono = telefono;
                persona.Sexo = sexo;
                persona.FechaNacimiento = fechaNacimiento;
                _persona.Add(persona);
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
            int ID_Persona = Int32.Parse(context.Request.Params["Id_Persona"].ToString());
            string nombre = "", apellido = "", email = "", direccion = "", telefono = "", dui = "";
            string sexo;
            DateTime fechaNacimiento;


            nombre = context.Request.Params["nombre"];
            apellido = context.Request.Params["apellido"];
            direccion = context.Request.Params["direccion"];
            email = context.Request.Params["email"];
            telefono = context.Request.Params["telefono"];
            dui = context.Request.Params["dui"];
            sexo = context.Request.Params["sexo"];
            fechaNacimiento = DateTime.ParseExact(context.Request.Params["fechaNacimiento"], "yyyy-MM-dd", CultureInfo.InvariantCulture);

            try
            {
                var persona = _persona.GetFirst(u => u.ID_Persona == ID_Persona);
                persona.Nombre = nombre;
                persona.Apellido = apellido;
                persona.Direccion = direccion;
                persona.Email = email;
                persona.Dui = dui;
                persona.Telefono = telefono;
                persona.Sexo = sexo;
                persona.FechaNacimiento = fechaNacimiento;
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