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
    /// Summary description for Exam
    /// </summary>
    public class Exam : ActionTemplate, IHttpHandler, IRequiresSessionState
    {

        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;

        IDataRepository<Examen> _examen;
        IDataRepository<Materia> _materia;
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
                case "subjects":
                    context.Response.Write(getSubjects(context));
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
            _examen = new DataRepository<IAWContext, Examen>(_context);
            _materia = new DataRepository<IAWContext, Materia>(_context);
        }

        public override string GetAllRecords()
        {
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader += "<tbody>";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var examenes = _examen.GetAll().Select((x, index) => new
                {
                    index,
                    x.ID_Examen,
                    x.Materia.Grado,
                    x.NumeroExamen,
                    x.Materia.Nombre
                    

                });
                foreach (var examen in examenes)
                {
                    int index = examen.index + 1;

                    string showButton = "<a data-id-exam='" + examen.ID_Examen + "'class='btn btn-primary btn-sm detail'>Mostrar</a>";
                    string editButton = "<a data-id-exam='" + examen.ID_Examen + "' class='btn btn-primary btn-sm edit'>Editar</a>";
                    string deleteButton = "<a data-id-exam='" + examen.ID_Examen + "' class='btn btn-primary btn-sm delete' data-toggle='modal' data-target='#modalmessage'>Eliminar</a>";
                    tableBody += "<tr><td>" + index + "</td><td>" + examen.NumeroExamen + "</td><td>" + examen.Nombre + "</td><td>" + examen.Grado + "</td><td>" + showButton + "</td><td>" + editButton + "</td><td>" + deleteButton + "</td></tr>";
                }
                tableFooter += "</tbody>";
                table = tableHeader + tableBody + tableFooter;

                if (examenes.ToList().Count > 0)
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
            int ID_Examen = Int32.Parse(context.Request.Params["ID_Examen"].ToString());

            try
            {
                var examen = _examen.GetFirst(u => u.ID_Examen == ID_Examen);
                var fullObject = new { NumeroExamen= examen.NumeroExamen,Archivo=examen.Archivo, ID_Materia= examen.ID_Materia , NombreMateria=examen.Materia.Nombre +" - "+ examen.Materia.Grado}; 

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

        public override string DeleteRecord(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Examen = Int32.Parse(context.Request.Params["ID_Examen"].ToString());
            try
            {
                var examen = _examen.GetFirst(u => u.ID_Examen == ID_Examen);
                _examen.Delete(examen);
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
            string numeroExamen = "", archivo = "";
            int ID_Materia = 0;

            numeroExamen = context.Request.Params["numeroExamen"];
            archivo = context.Request.Params["archivo"];
            ID_Materia = Int32.Parse(context.Request.Params["ID_Materia"]);


            try
            {
                Examen examen = new Examen();
                examen.NumeroExamen = numeroExamen;
                examen.Archivo = archivo;
                examen.ID_Materia = ID_Materia;


                _examen.Add(examen);
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
            int ID_Examen = Int32.Parse(context.Request.Params["ID_Examen"].ToString());

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string numeroExamen = "", archivo = "";
            int ID_Materia = 0;

            numeroExamen = context.Request.Params["numeroExamen"];
            archivo = context.Request.Params["archivo"];
            ID_Materia = Int32.Parse(context.Request.Params["ID_Materia"]);



            try
            {
                var examen = _examen.GetFirst(u => u.ID_Examen == ID_Examen);
               examen.NumeroExamen = numeroExamen;
                examen.Archivo = archivo;
                examen.ID_Materia = ID_Materia;

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

        public string getSubjects(HttpContext context)
        {

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string optionsSubject = "";
            try
            {
                var materias = _materia.Select();

                foreach (var materia in materias)
                {
                    optionsSubject += "<option data-id-subject='" + materia.ID_Materia + "'>" + materia.Nombre + " - "+materia.Grado+ "</option>";
                }

                response.IsSucess = true;
                response.ResponseData = optionsSubject;
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
        
    }
}