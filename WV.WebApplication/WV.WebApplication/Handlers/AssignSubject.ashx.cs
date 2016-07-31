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
    /// Summary description for AssignSubject
    /// </summary>
    public class AssignSubject : ActionTemplate, IHttpHandler, IRequiresSessionState
    {

        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IDataRepository<TipoPrograma> _tipoPrograma;
        IAWContext _context1;
        IDataRepository<Programa> _programa;
        IDataRepository<Materia> _materia;
        IDataRepository<AsignacionMateria> _asignacion;
        IDataRepository<Beneficiario> _beneficiario;

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
                case "getmembers":
                    context.Response.Write(GetAllMembersInProgram(context));
                    break;
                case "add":
                    context.Response.Write(AssignSubjectMass(context));
                    break;
                case "delete":
                    context.Response.Write(DeleteAssignation(context));
                    break;

                case "getassignations":
                    context.Response.Write(GetAssignationsBySubject(context));
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
            _tipoPrograma = new DataRepository<IAWContext, TipoPrograma>(_context);
            _context1 = new AWContext();
            _programa = new DataRepository<IAWContext, Programa>(_context);
            _materia = new DataRepository<IAWContext, Materia>(_context);
            _asignacion = new DataRepository<IAWContext, AsignacionMateria>(_context);
            _beneficiario = new DataRepository<IAWContext, Beneficiario>(_context);
        }

        public string GetAllMembersInProgram(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader += "<tbody>";

            try
            {
                var cic = _tipoPrograma.Select().First(tp => tp.TipoPrograma1 == "CIC");
                var programas = _programa.Select().Where(p => p.ID_TipoPrograma == cic.ID_TipoPrograma);

                int index = 1;
                foreach (var programa in programas)
                {
                    foreach (var beneficiario in programa.Beneficiario)
                    {
        
                        tableBody += "<tr data-id-beneficiario='" + beneficiario.ID_Beneficiario + "'><td class='bs-checkbox'><input data-index='" + index + "' name='btSelectItem' type='checkbox'></td><td>" + beneficiario.Nombre +" "+beneficiario.Apellido + "</td></tr>";
                        index++;
                    }

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

        public string DeleteAssignation(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Materia = Int32.Parse(context.Request.Params["ID_Materia"].ToString());
            int ID_Beneficiario = Int32.Parse(context.Request.Params["ID_Beneficiario"].ToString());
            try
            {
                var asignacion = _asignacion.GetFirst(asi => asi.ID_Beneficiario == ID_Beneficiario && asi.ID_Materia == ID_Materia);
                _asignacion.Delete(asignacion);

                _context.SaveChanges();
                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Asignacion Eliminada Satisfactoriamente";
                response.CallBack = string.Empty;

            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public override string GetAllRecords()
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string optionsMaterias = "";
            try
            {
                var materias = _materia.Select();

                foreach (var materia in materias)
                {
                    optionsMaterias += "<option data-id-subject='" + materia.ID_Materia + "'>" + materia.Nombre + "-" + materia.Grado + "</option>";

                }
                response.IsSucess = true;
                response.ResponseData = optionsMaterias;
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

        public string AssignSubjectMass(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Materia = Int32.Parse(context.Request.Params["ID_Materia"].ToString());
            string[] assignations = context.Request.Form["BeneficiariosArray[]"].Split(',');

            try
            {
                foreach (string resource in assignations)
                {

                    int ID_Beneficiario = Int32.Parse(resource);
                    AsignacionMateria asignacion = new AsignacionMateria();
                    asignacion.ID_Materia = ID_Materia;
                    asignacion.ID_Beneficiario = ID_Beneficiario;
                    _asignacion.Add(asignacion);
                }

                _context.SaveChanges();
                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Asignacion Realizada satisfactoriamente";
                response.CallBack = string.Empty;

            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public string GetAssignationsBySubject(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Materia = Int32.Parse(context.Request.Params["ID_Materia"].ToString());

            try
            {
                var materia = _materia.GetFirst(m => m.ID_Materia == ID_Materia);
                string tableHeader = "", tableBody = "", tableFooter = "", table = "";
                tableHeader += "<tbody>";
                int index = 0;

                foreach (var asignacion in materia.AsignacionMateria)
                {
                    Beneficiario bene = _beneficiario.GetFirst(b => b.ID_Beneficiario == asignacion.ID_Beneficiario);

                        index++;
                        string deleteButton = "<a data-id-beneficiario='" + bene.ID_Beneficiario + "' class='btn btn-primary btn-sm delete' >Eliminar</a>";
                        tableBody += "<tr data-id-beneficiario='" + bene.ID_Beneficiario + "'><td>" + index + "</td><td>" + bene.Nombre +" "+bene.Apellido + "</td><td>" + deleteButton + "</td></tr>";

                }
                tableFooter += "</tbody>";

                table = tableHeader + tableBody + tableFooter;
                response.IsSucess = true;
                response.ResponseData = table;
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

        #region Not implemented functions

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

        #endregion
    }
}