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
    /// Summary description for RecordGrades
    /// </summary>
    public class RecordGrades : ActionTemplate, IHttpHandler, IRequiresSessionState
    {
        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IDataRepository<ExamenResultado> _examenResultado;
        IAWContext _context1;
        IDataRepository<Examen> _examen;
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

                case "getsubjects":
                    context.Response.Write(GetSubjects());
                    break;
                case "getexams":
                    context.Response.Write(getExamsBySubject(context));
                    break;
                case "exists":
                    context.Response.Write(ExistsGradeRecord(context));
                    break;
                case "getnonexistinggrades":
                    context.Response.Write(GetNonExistingGrades(context));
                    break;

                case "getexistinggrades":
                    context.Response.Write(GetExistingGrades(context));
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
            _examenResultado = new DataRepository<IAWContext, ExamenResultado>(_context);
            _materia = new DataRepository<IAWContext, Materia>(_context);
            _beneficiario = new DataRepository<IAWContext, Beneficiario>(_context);
            _asignacion = new DataRepository<IAWContext, AsignacionMateria>(_context);
            _examen = new DataRepository<IAWContext, Examen>(_context);
        }

        public string GetSubjects()
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

        public string getExamsBySubject(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Materia= Int32.Parse(context.Request.Params["ID_Materia"]);
            string optionsExams = "";
            try
            {
                var materias = _materia.GetFirst(m=> m.ID_Materia == ID_Materia) ;

                foreach (var examen in materias.Examen)
                {
                    optionsExams += "<option data-id-exam='" + examen.ID_Examen + "'>" + examen.NumeroExamen  + "</option>";

                }
                response.IsSucess = true;
                response.ResponseData = optionsExams;
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

        public string ExistsGradeRecord(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Examen = Int32.Parse(context.Request.Params["ID_Examen"].ToString());
            try
            {
               var examen = _examen.GetFirst(e=> e.ID_Examen == ID_Examen);
               bool exists = examen.ExamenResultado.Count > 0 ? true : false;
               string mode="";
                
               if (exists)
               {
                   mode = "edit";
               }
               else
               {
                   mode ="add";
               }

               var fullObject = new { Exists=exists, ID_Examen= ID_Examen,Mode=mode};
               response.IsSucess = true;
               response.ResponseData = fullObject;
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

        public string GetNonExistingGrades(HttpContext context)
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
                    string grade = "<input type='number' name='quantity' min='1' max='10' step='any' value=''>";
                    tableBody += "<tr data-id-beneficiario='" + bene.ID_Beneficiario + "'><td>" + index + "</td><td data-id-beneficiario='" + bene.ID_Beneficiario + "' >" + bene.Nombre + " " + bene.Apellido + "</td><td>" + grade + "</td></tr>";

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

        public string GetExistingGrades(HttpContext context) 
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Examen = Int32.Parse(context.Request.Params["ID_Examen"].ToString());

            try
            {
                var examen = _examen.GetFirst(m => m.ID_Examen == ID_Examen);
                string tableHeader = "", tableBody = "", tableFooter = "", table = "";
                tableHeader += "<tbody>";
                int index = 0;

                foreach (var examenResultado in examen.ExamenResultado)
                {
                    Beneficiario bene = _beneficiario.GetFirst(b => b.ID_Beneficiario == examenResultado.ID_Beneficiario);

                    index++;
                    string grade = "<input type='number' name='quantity' min='1' max='10' step='any' value='"+examenResultado.Nota+"'>";
                    tableBody += "<tr data-id-beneficiario='" + bene.ID_Beneficiario + "'><td>" + index + "</td><td data-id-beneficiario='" + bene.ID_Beneficiario + "' >" + bene.Nombre + " " + bene.Apellido + "</td><td>" + grade + "</td></tr>";
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
        
        public override string AddRecord(HttpContext context)
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                var data = context.Request;
                var sr = new StreamReader(data.InputStream);
                var stream = sr.ReadToEnd();
                var javaScriptSerializer = new JavaScriptSerializer();
                var examenTemporal = javaScriptSerializer.Deserialize<ExamenTemp>(stream);

                var examen = _examen.GetFirst(a => a.ID_Examen == examenTemporal.ID_Examen);

                if (examen.ExamenResultado.Count == 0)
                {
                    foreach (var examenTemp in examenTemporal.Resultados)
                    {
                        ExamenResultado examenResultado = new ExamenResultado();
                        examenResultado.ID_Beneficiario = examenTemp.ID_Beneficiario;
                        examenResultado.ID_Examen = examenTemp.ID_Examen;
                        examenResultado.Nota = examenTemp.Nota;

                        examen.ExamenResultado.Add(examenResultado);
                    }
                }
                _context.SaveChanges();

                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Registros Creados Satisfactoriamente";
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
                var examenTemp = javaScriptSerializer.Deserialize<ExamenTemp>(stream);

                var examen = _examen.GetFirst(a => a.ID_Examen == examenTemp.ID_Examen);

                if (examen.ExamenResultado.Count > 0)
                {
                    foreach (var examenResultadoTemp in examenTemp.Resultados)
                    {

                        foreach (var resultado in examen.ExamenResultado)
                        {
                            if (examenResultadoTemp.ID_Beneficiario == resultado.ID_Beneficiario && examenResultadoTemp.ID_Examen == resultado.ID_Examen)
                            {
                                resultado.Nota = examenResultadoTemp.Nota;
                                break;
                            }
                        }
                    }
                }
                _context.SaveChanges();

                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Registros Modificados Satisfactoriamente";
                response.CallBack = string.Empty;

            }
            catch (Exception msg)
            {
                response.Message = msg.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        #region Not implemented functions

        public override string GetAllRecords()
        {
            throw new NotImplementedException();
        }

        public override string GetSingleRecord(HttpContext context)
        {
            throw new NotImplementedException();
        }

        public override string DeleteRecord(HttpContext context)
        {
            throw new NotImplementedException();
        }
        #endregion

        [Serializable]
        public class ExamenTemp
        {
            public int ID_Examen { get; set; }
            public List<Resultado> Resultados { get; set; }
            public List<int> ToDelete { get; set; }
        }
        [Serializable]
        public class Resultado
        {
            public int ID_Examen { get; set; }
            public int ID_Beneficiario { get; set; }
            public string Nota { get; set; }
            
        }
    }
}