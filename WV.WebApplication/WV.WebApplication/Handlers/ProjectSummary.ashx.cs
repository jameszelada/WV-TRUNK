using DataLayer;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using WV.WebApplication.Reports;
using WV.WebApplication.Utils;

namespace WV.WebApplication.Handlers
{
    /// <summary>
    /// Summary description for ProjectSummary
    /// </summary>
    public class ProjectSummary : DataAccess, IHttpHandler, IRequiresSessionState
    {

        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IDataRepository<Persona> _persona;
        IDataRepository<Proyecto> _proyecto;
        IDataRepository<Programa> _programa;
        IDataRepository<Comunidad> _comunidad;
        IDataRepository<Actividad> _actividad;
        IDataRepository<Beneficiario> _beneficiario;
        AWContext _context1;
        public void ProcessRequest(HttpContext context)
        {
            InitializeObjects();
            MethodName = context.Request.Params["method"];
            switch (MethodName.ToLower())
            {
                case "getprogramsummary":
                    context.Response.Write(GetProgramSummary(context));
                    break;
                case "getprojectsummary":
                    context.Response.Write(GetProjectSummary(context));
                    break;
                //case "getalllogbooks":
                //    context.Response.Write(GetLogBooks(context));
                //    break;
            }
        }


        public string GetProgramSummary(HttpContext context)
        {
            int ID_Programa = Int32.Parse(context.Request.Params["ID_Programa"].ToString());

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string assignedRRHH = "",activities="",additionalInfo="";
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader = "<div class='table-responsive'>";
            tableHeader += "<table class='table table-striped table-hover'>";
            tableHeader += "<thead class='thead-inverse'><tr><th>Nombre Completo</th><th>DUI</th><th>Email</th><th>Puesto</th><th>Tipo de Puesto</th></tr></thead>";
            tableHeader += "<tbody >";
            try
            {
                var programa = _programa.GetFirst(pro=> pro.ID_Programa==ID_Programa);

                Dictionary<string, Object> param = new Dictionary<string, Object>();
                param.Add("projectIdentity", programa.ID_Proyecto);
                string query = "spGetAsignation";
                DataSet myDataset = GetDataSet(query, CommandType.StoredProcedure, param);

                foreach (DataRow row in myDataset.Tables[0].Rows)
                {
                    tableBody += "<tr><td>" + row["NombreCompleto"].ToString() + "</td><td>" + row["Dui"].ToString() + "</td><td>" + row["Email"].ToString() + "</td><td>" + row["Puesto"].ToString() + "</td><td>" + row["TipoPuesto"].ToString() + "</td></tr>";
                }

                tableFooter += "</tbody></table></div>";
                assignedRRHH = tableHeader + tableBody + tableFooter;


                //2nd Table
                tableBody = "";
                tableHeader = "<div class='table-responsive'>";
                tableHeader += "<table class='table table-striped table-hover'>";
                tableHeader += "<thead class='thead-inverse'><tr><th>Fecha</th><th>Codigo Actividad</th><th>Actividad</th><th>Estado</th><th>Observación</th></tr></thead>";
                foreach (var actividad in programa.Actividad)
                {
                    tableBody += "<tr><td>" + actividad.Fecha.ToShortDateString() + "</td><td>" + actividad.Codigo + "</td><td>" + actividad.ActividadDescripcion + "</td><td>" + actividad.Estado + "</td><td>" + actividad.Observacion + "</td></tr>";
                }
                tableFooter += "</tbody></table></div>";
                activities = tableHeader + tableBody + tableFooter;

                //3rd Table
                tableBody = "";
                tableHeader = "<div class='table-responsive'>";
                tableHeader += "<table class='table table-striped table-hover'>";
                tableHeader += "<thead class='thead-inverse'><tr><th></th><th></th></tr></thead>";

                tableBody += "<tr><td>" + "Número de Beneficiarios en el Programa:" + "</td><td>" + programa.Beneficiario.Count().ToString() + "</td></tr>";
                tableBody += "<tr><td>" + "Número de Beneficiarios de Sexo Masculino:" + "</td><td>" + programa.Beneficiario.Count(ben => ben.Sexo == "M").ToString() + "</td></tr>";
                tableBody += "<tr><td>" + "Número de Beneficiarios de Sexo Femenino:" + "</td><td>" + programa.Beneficiario.Count(ben => ben.Sexo == "F").ToString() + "</td></tr>";
                tableBody += "<tr><td>" + "Número de Beneficiarios Patrocinados" + "</td><td>" + programa.Beneficiario.Count(ben => !string.IsNullOrEmpty(ben.Codigo)).ToString() + "</td></tr>";
                
                tableFooter += "</tbody></table></div>";
                additionalInfo = tableHeader + tableBody + tableFooter;

                //Data Gráfico1
                Dictionary<string, object> Data = new Dictionary<string, object>();
                Data.Add("Masculino", programa.Beneficiario.Count(ben => ben.Sexo == "M"));
                Data.Add("Femenino", programa.Beneficiario.Count(ben => ben.Sexo == "F"));

                //Data Gráfico2

                ChartingHelper chartAges = new ChartingHelper();
                Dictionary<string, object> DataAges = new Dictionary<string, object>();
                List<int> Ranges = chartAges.GetAgeRanges(programa.TipoPrograma.TipoPrograma1);
                int menor1 = 0, menor2 = 0, menor3 = 0;


                foreach (var beneficiario in programa.Beneficiario)
                {
                    string[] personAge = beneficiario.Edad.Split('|');
                    int years = Convert.ToInt32(personAge[0]);
                    if (years <= Ranges[0] && years >= Ranges[1])
                    {
                        menor1++;
                    }
                    else if (years <= Ranges[1] && years >= Ranges[2])
                    {
                        menor2++;
                    }
                    else if (years <= Ranges[2])
                    {
                        menor3++;
                    }
                }

                DataAges.Add("Menor a " + Ranges[0], menor1);
                DataAges.Add("Menor a " + Ranges[1], menor2);
                DataAges.Add("Menor a " + Ranges[2], menor3);

                var fakeObject = new { Staff=assignedRRHH,Activities=activities,AdditionalInfo=additionalInfo, Chart1=Data,Chart2=DataAges};
                response.IsSucess = true;
                response.ResponseData = fakeObject;
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

        public string GetProjectSummary(HttpContext context)
        {
            int ID_Proyecto = Int32.Parse(context.Request.Params["ID_Proyecto"].ToString());

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string programType = "",beneficiariesInfo="";
            string tableHeader = "", tableBody = "", tableFooter = "";
            
            try
            {
                var proyecto = _proyecto.GetFirst(pro => pro.ID_Proyecto == ID_Proyecto);

                //1st Table
                tableBody = "";
                tableHeader = "<div class='table-responsive'>";
                tableHeader += "<table class='table table-striped table-hover'>";
                tableHeader += "<thead class='thead-inverse'><tr><th></th><th></th></tr></thead>";

                tableBody += "<tr><td>" + "Número de programas en proyecto:" + "</td><td>" + proyecto.Programa.Count().ToString() + "</td></tr>";
                tableFooter += "</tbody></table></div>";
                programType = tableHeader + tableBody + tableFooter;

                //2nd Table
                int total = 0;

                proyecto.Programa.ToList().ForEach(prog => total += prog.Beneficiario.Count());
                tableBody = "";
                tableHeader = "<div class='table-responsive'>";
                tableHeader += "<table class='table table-striped table-hover'>";
                tableHeader += "<thead class='thead-inverse'><tr><th></th><th></th></tr></thead>";

                tableBody += "<tr><td>" + "Número de Beneficiarios en el proyecto:" + "</td><td>" + total + "</td></tr>";
                
                tableFooter += "</tbody></table></div>";
                beneficiariesInfo = tableHeader + tableBody + tableFooter;

                //Data Gráfico1
                Dictionary<string, object> Data = new Dictionary<string, object>();

                List<string> programTypesId = _context1.Database.SqlQuery<string>("SELECT CONVERT(varchar(10), ID_TipoPrograma) +'-'+ TipoPrograma as result FROM TipoPrograma").ToList();

                foreach (var tipoPrograma in programTypesId)
                {
                    string[] result = tipoPrograma.Split('-');
                    int ID_TipoPrograma = Convert.ToInt32(result[0]);
                    string tipoPrograma1 = result[1];

                    Data.Add(tipoPrograma1, proyecto.Programa.Count(programa => programa.ID_TipoPrograma == ID_TipoPrograma));

                }

                //Data Gráfico2

                Dictionary<string, object> DataChartGenderCommunity = new Dictionary<string, object>();

                //loading data

                int male = 0;
                int female = 0;

                proyecto.Programa.ToList().ForEach((pro) =>
                {
                    male += pro.Beneficiario.Count(ben => ben.Sexo == "M");
                    female += pro.Beneficiario.Count(ben => ben.Sexo == "F");
                });
                DataChartGenderCommunity.Add("Masculino", male);
                DataChartGenderCommunity.Add("Femenino", female);

                //Data Gráfico 3

                Dictionary<string, object> DataChartNCR = new Dictionary<string, object>();

                //loading data

                int NCR = 0;
                int RC = 0;

                proyecto.Programa.ToList().ForEach((pro) =>
                {
                    NCR += pro.Beneficiario.Count(ben => string.IsNullOrEmpty(ben.Codigo));
                    RC += pro.Beneficiario.Count(ben => !string.IsNullOrEmpty(ben.Codigo));
                });
                DataChartNCR.Add("No Patrocinados", NCR);
                DataChartNCR.Add("Patrocinados", RC);

                //Data Gráfico 4
                ChartingHelper chartAges = new ChartingHelper();
                Dictionary<string, object> DataAges = new Dictionary<string, object>();
                List<int> Ranges = chartAges.GetAgeRanges("other");
                int menor1 = 0, menor2 = 0, menor3 = 0;

                proyecto.Programa.ToList().ForEach(
                    (pro) =>
                    {
                        foreach (var ben in pro.Beneficiario)
                        {
                            string[] personAge = ben.Edad.Split('|');
                            int years = Convert.ToInt32(personAge[0]);
                            if (years <= Ranges[0] && years >= Ranges[1])
                            {
                                menor1++;
                            }
                            else if (years <= Ranges[1] && years >= Ranges[2])
                            {
                                menor2++;
                            }
                            else if (years <= Ranges[2])
                            {
                                menor3++;
                            }
                        }

                    }

                    );

                DataAges.Add("Menor a " + Ranges[0], menor1);
                DataAges.Add("Menor a " + Ranges[1], menor2);
                DataAges.Add("Menor a " + Ranges[2], menor3);

                var fakeObject = new { Beneficiaries = beneficiariesInfo, ProgramType = programType, Chart1 = Data, Chart2 = DataChartGenderCommunity, Chart3 = DataChartNCR,Chart4=DataAges };
                response.IsSucess = true;
                response.ResponseData = fakeObject;
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


        private void InitializeObjects()
        {

            _context = new AWContext();
            _context1 = new AWContext();
            _programa = new DataRepository<IAWContext, Programa>(_context);
            _proyecto = new DataRepository<IAWContext, Proyecto>(_context);
            _comunidad = new DataRepository<IAWContext, Comunidad>(_context);
            _actividad = new DataRepository<IAWContext, Actividad>(_context);
            _beneficiario = new DataRepository<IAWContext, Beneficiario>(_context);
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