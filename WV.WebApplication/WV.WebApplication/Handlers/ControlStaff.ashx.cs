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
    /// Summary description for ControlStaff
    /// </summary>
    public class ControlStaff : ActionTemplate, IHttpHandler, IRequiresSessionState
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
                case "getstaff":
                    context.Response.Write(GetStaff());
                break;

                //case "getall":
                //    context.Response.Write(GetAllRecords());
                //    break;
                //case "delete":
                //    context.Response.Write(DeleteRecord(context));
                //    break;
                //case "getsingle":
                //    context.Response.Write(GetSingleRecord(context));
                //    break;
                //case "add":
                //    context.Response.Write(AddRecord(context));
                //    break;
                //case "edit":
                //    context.Response.Write(EditRecord(context));
                //    break;
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
            throw new NotImplementedException();
        }

        public string GetStaff()
        {
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
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

                    string showLogBook = "<a data-id-person='" + persona.ID_Persona + "'class='btn btn-primary btn-sm logbook'>Ver Bitacoras</a>";
                    string showSchedule = "<a data-id-person='" + persona.ID_Persona + "' class='btn btn-primary btn-sm schedule'>Ver Planes</a>";
                    tableBody += "<tr><td>" + index + "</td><td>" + persona.Nombre + "</td><td>" + persona.Apellido + "</td><td>" + showLogBook + "</td><td>" + showSchedule + "</td></tr>";
                }

                tableFooter += "</tbody>";
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