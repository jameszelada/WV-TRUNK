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
    /// Summary description for Registration
    /// </summary>
    public class Registration : ActionTemplate, IHttpHandler, IRequiresSessionState
    {
        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IDataRepository<Beneficiario> _beneficiario;
        IAWContext _context1;
        IDataRepository<Programa> _programa;

        IAWContext _context2;
        IDataRepository<BeneficiarioAdicional> _beneficiarioAdicional;
        IAWContext _context3;
        IDataRepository<BeneficiarioSalud> _beneficiarioSalud;
        IAWContext _context4;
        IDataRepository<BeneficiarioEducacion> _beneficiarioEducacion;
        IAWContext _context5;
        IDataRepository<BeneficiarioCompromiso> _beneficiarioCompromiso;

        public void ProcessRequest(HttpContext context)
        {
            InitializeObjects();
            MethodName = context.Request.Params["method"];
            CallBackMethodName = context.Request.Params["callbackmethod"];
            Parameter = context.Request.Params["parameter"];

            switch (MethodName.ToLower())
            {
                case "getprograms":
                    context.Response.Write(GetAllPrograms());
                    break;

                case "getall":
                    context.Response.Write(GetAllRecords());
                    break;
                //case "delete":
                //    context.Response.Write(DeleteRecord(context));
                //    break;
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
            _context = new AWContext();
            _beneficiario = new DataRepository<IAWContext, Beneficiario>(_context);
            _context1 = new AWContext();
            _programa = new DataRepository<IAWContext, Programa>(_context1);

            //_context2 = new AWContext(Connection);
            //_beneficiarioAdicional = new DataRepository<IAWContext, BeneficiarioAdicional>(_context1);
            //_context3 = new AWContext(Connection);
            //_programa = new DataRepository<IAWContext, Programa>(_context1);
            //_context4 = new AWContext(Connection);
            //_programa = new DataRepository<IAWContext, Programa>(_context1);
            //_context5 = new AWContext(Connection);
            //_programa = new DataRepository<IAWContext, Programa>(_context1);

        }

        public string GetAllPrograms()
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string optionsProgramas = "";
            try
            {
                var programas = _programa.Select();
                

                foreach (var programa in programas)
                {
                    optionsProgramas += "<option data-id-program='" + programa.ID_Programa + "'>" + programa.Codigo +"-"+programa.TipoPrograma.TipoPrograma1+"-"+programa.Comunidad.Comunidad1 + "</option>";
                }
                response.IsSucess = true;
                response.ResponseData = optionsProgramas;
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

        public override string GetAllRecords()
        {
            
            string nombre = "";
            string tableHeader = "", tableBody = "", tableFooter = "", table = "";
            tableHeader += "<tbody>";
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var beneficiarios = _beneficiario.Select();
                
                
                foreach (var beneficiario in beneficiarios)
                {
                    tableBody += "<tr data-id-beneficiario='"+beneficiario.ID_Beneficiario+"'><td><a class='list-group-item' style='cursor: pointer;'><i class='fa fa-fw fa-user'></i>" + beneficiario.Nombre + " " + beneficiario.Apellido + "</a></td></tr>";
                }
                tableFooter += "</tbody>";
                table = tableHeader + tableBody + tableFooter;
                if (beneficiarios.ToList().Count > 0)
                {
                    response.IsSucess = true;
                    response.ResponseData = table;
                    response.Message = string.Empty;
                    response.CallBack = string.Empty;
                    response.ResponseAdditional = nombre;
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
            int ID_Beneficiario = Int32.Parse(context.Request.Params["ID_Beneficiario"].ToString());
            BeneficiarioTemplate objBeneficiario = new BeneficiarioTemplate(); ;
            BeneficiarioTempAdicional objBeneficiarioAdicional = null;
            BeneficiarioTempSalud objBeneficiarioSalud =null;
            BeneficiarioTempEducacion objBeneficiarioEducacion = null;
            BeneficiarioTempCompromiso objBeneficiarioCompromiso = null;
            try
            {
                var beneficiario = _beneficiario.GetFirst(b => b.ID_Beneficiario == ID_Beneficiario);

                objBeneficiario.Nombre = beneficiario.Nombre;
                objBeneficiario.Apellido = beneficiario.Apellido;
                objBeneficiario.Dui = beneficiario.Dui == "" ? "N/A" : beneficiario.Dui;
                objBeneficiario.Codigo = beneficiario.Codigo == "" ? "N/A" : beneficiario.Codigo;
                objBeneficiario.Edad = beneficiario.Edad;
                objBeneficiario.Sexo = beneficiario.Sexo == "M" ? "Masculino" : "Femenino";
                objBeneficiario.Direccion = beneficiario.Direccion;
                objBeneficiario.Programa = beneficiario.Programa.Proyecto.Codigo + "-" + beneficiario.Programa.Codigo+ "-"+beneficiario.Programa.Comunidad.Comunidad1; 
                objBeneficiario.ID_Programa = beneficiario.ID_Programa;
                
                if (beneficiario.BeneficiarioAdicional.Count > 0)
                {
                    objBeneficiarioAdicional = new BeneficiarioTempAdicional();
                    BeneficiarioAdicional adicional= beneficiario.BeneficiarioAdicional.First();
                    objBeneficiarioAdicional.NombreEmergencia = adicional.NombreEmergencia == "" ? "N/A" : adicional.NombreEmergencia;
                    objBeneficiarioAdicional.NumeroEmergencia = adicional.NumeroEmergencia == "" ? "N/A" : adicional.NumeroEmergencia;
                    objBeneficiarioAdicional.TieneRegistroNacimiento = adicional.TieneRegistroNacimiento;
                }

                if (beneficiario.BeneficiarioCompromiso.Count > 0)
                {
                    objBeneficiarioCompromiso = new BeneficiarioTempCompromiso();
                    BeneficiarioCompromiso compromiso = beneficiario.BeneficiarioCompromiso.First();
                    objBeneficiarioCompromiso.AceptaCompromiso = compromiso.AceptaCompromiso;
                    objBeneficiarioCompromiso.ExistioProblema = compromiso.ExistioProblema;
                    objBeneficiarioCompromiso.SeCongrega = compromiso.SeCongrega;
                    objBeneficiarioCompromiso.NombreIglesia = compromiso.NombreIglesia == "" ? "N/A" : compromiso.NombreIglesia;
                    objBeneficiarioCompromiso.Comentario = compromiso.Comentario == "" ? "N/A" : compromiso.Comentario;

                }

                if (beneficiario.BeneficiarioEducacion.Count > 0)
                {
                    objBeneficiarioEducacion = new BeneficiarioTempEducacion();
                    BeneficiarioEducacion educacion = beneficiario.BeneficiarioEducacion.First();
                    objBeneficiarioEducacion.Estudia = educacion.Estudia;
                    objBeneficiarioEducacion.GradoEducacion = educacion.GradoEducacion == "" ? "N/A" : educacion.GradoEducacion;
                    objBeneficiarioEducacion.Motivo = educacion.Motivo == "" ? "N/A" : educacion.Motivo;
                    objBeneficiarioEducacion.UltimoGrado = educacion.UltimoGrado == "" ? "N/A" : educacion.UltimoGrado;
                    objBeneficiarioEducacion.UltimoAño = educacion.UltimoAño == "" ? "N/A" : educacion.UltimoAño;
                    objBeneficiarioEducacion.NombreCentroEscolar = educacion.NombreCentroEscolar == "" ? "N/A" : educacion.NombreCentroEscolar;
                    objBeneficiarioEducacion.GradoEducacion = educacion.GradoEducacion == "" ? "N/A" : educacion.GradoEducacion;
                    objBeneficiarioEducacion.Turno = educacion.Turno == "" ? "N/A" : educacion.Turno;
                }

                if (beneficiario.BeneficiarioSalud.Count>0)
                {
                    objBeneficiarioSalud = new BeneficiarioTempSalud();
                    BeneficiarioSalud salud= beneficiario.BeneficiarioSalud.First();
                    objBeneficiarioSalud.EstadoSalud = salud.EstadoSalud == "" ? "N/A" : salud.EstadoSalud;
                    objBeneficiarioSalud.TieneTarjeta = salud.TieneTarjeta;
                    objBeneficiarioSalud.Enfermedad = salud.Enfermedad == "" ? "N/A" : salud.Enfermedad;
                    objBeneficiarioSalud.Discapacidad = salud.Discapacidad == "" ? "N/A" : salud.Discapacidad;
                    objBeneficiarioSalud.FechaCurvaCrecimiento = salud.FechaCurvaCrecimiento;
                    objBeneficiarioSalud.FechaInmunizacion = salud.FechaInmunizacion;

                }

                objBeneficiario.BeneficiarioAdicional = objBeneficiarioAdicional;
                objBeneficiario.BeneficiarioCompromiso = objBeneficiarioCompromiso;
                objBeneficiario.BeneficiarioEducacion = objBeneficiarioEducacion;
                objBeneficiario.BeneficiarioSalud = objBeneficiarioSalud;

                response.IsSucess = true;
                response.ResponseData = objBeneficiario;
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
            throw new NotImplementedException();
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
                var beneficiario = javaScriptSerializer.Deserialize<BeneficiarioTemplate>(stream);

                Beneficiario BeneficiarioToAdd = new Beneficiario();
                BeneficiarioToAdd.Nombre = beneficiario.Nombre;
                BeneficiarioToAdd.Apellido = beneficiario.Apellido;
                BeneficiarioToAdd.Edad = beneficiario.Edad;
                BeneficiarioToAdd.ID_Programa = beneficiario.ID_Programa;
                BeneficiarioToAdd.Codigo = beneficiario.Codigo;
                BeneficiarioToAdd.Sexo = beneficiario.Sexo;
                BeneficiarioToAdd.Dui = beneficiario.Dui;
                BeneficiarioToAdd.Direccion = beneficiario.Direccion;
                BeneficiarioToAdd.CreadoPor = SystemUsername;

                BeneficiarioAdicional BeneficiarioAdicionalToAdd = new BeneficiarioAdicional();
                bool? canAddChildAdicional = null;
              
                BeneficiarioAdicionalToAdd.NombreEmergencia = !string.IsNullOrEmpty(beneficiario.BeneficiarioAdicional.NombreEmergencia)? beneficiario.BeneficiarioAdicional.NombreEmergencia:"";
                BeneficiarioAdicionalToAdd.NumeroEmergencia = !string.IsNullOrEmpty(beneficiario.BeneficiarioAdicional.NumeroEmergencia) ? beneficiario.BeneficiarioAdicional.NumeroEmergencia : "";
                if (beneficiario.BeneficiarioAdicional.TieneRegistroNacimiento.HasValue)
                {
                    BeneficiarioAdicionalToAdd.TieneRegistroNacimiento = beneficiario.BeneficiarioAdicional.TieneRegistroNacimiento.Value;
                   

                    canAddChildAdicional = true;
                }

                if (canAddChildAdicional != null)
                {
                    BeneficiarioAdicionalToAdd.CreadoPor = SystemUsername;
                    BeneficiarioToAdd.BeneficiarioAdicional.Add(BeneficiarioAdicionalToAdd);
                }

                BeneficiarioCompromiso BeneficiaroCompromisoToAdd = new BeneficiarioCompromiso();
                bool? canAddCompromiso = null;
                BeneficiaroCompromisoToAdd.Comentario = !string.IsNullOrEmpty(beneficiario.BeneficiarioCompromiso.Comentario)? beneficiario.BeneficiarioCompromiso.Comentario:"";
                BeneficiaroCompromisoToAdd.NombreIglesia = !string.IsNullOrEmpty(beneficiario.BeneficiarioCompromiso.NombreIglesia)?beneficiario.BeneficiarioCompromiso.NombreIglesia:"";
                if (beneficiario.BeneficiarioCompromiso.ExistioProblema.HasValue)
                {
                    BeneficiaroCompromisoToAdd.ExistioProblema = beneficiario.BeneficiarioCompromiso.ExistioProblema.Value;
                }
                if (beneficiario.BeneficiarioCompromiso.SeCongrega.HasValue)
                {
                    BeneficiaroCompromisoToAdd.SeCongrega = beneficiario.BeneficiarioCompromiso.SeCongrega.Value;
                }
                if (beneficiario.BeneficiarioCompromiso.AceptaCompromiso.HasValue)
                {
                    BeneficiaroCompromisoToAdd.AceptaCompromiso = beneficiario.BeneficiarioCompromiso.AceptaCompromiso.Value;
                    canAddCompromiso = true;
                }
                if (canAddCompromiso != null)
                {
                    BeneficiaroCompromisoToAdd.CreadoPor = SystemUsername;
                    BeneficiarioToAdd.BeneficiarioCompromiso.Add(BeneficiaroCompromisoToAdd);
                }

                BeneficiarioSalud BeneficiarioSaludToAdd = new BeneficiarioSalud();
                bool? canAddSalud = null;
                bool? canAddSaludTarjeta = null;
                
                BeneficiarioSaludToAdd.Discapacidad = !string.IsNullOrEmpty(beneficiario.BeneficiarioSalud.Discapacidad)?beneficiario.BeneficiarioSalud.Discapacidad:"";
                BeneficiarioSaludToAdd.Enfermedad = !string.IsNullOrEmpty(beneficiario.BeneficiarioSalud.Enfermedad)? beneficiario.BeneficiarioSalud.Enfermedad:"";
                BeneficiarioSaludToAdd.FechaCurvaCrecimiento = beneficiario.BeneficiarioSalud.FechaCurvaCrecimiento != null ? beneficiario.BeneficiarioSalud.FechaCurvaCrecimiento : new DateTime(1900,1,1);
                BeneficiarioSaludToAdd.FechaInmunizacion = beneficiario.BeneficiarioSalud.FechaInmunizacion != null ? beneficiario.BeneficiarioSalud.FechaInmunizacion : new DateTime(1900, 1, 1);
                if (!string.IsNullOrEmpty(beneficiario.BeneficiarioSalud.EstadoSalud))
                {
                    BeneficiarioSaludToAdd.EstadoSalud = beneficiario.BeneficiarioSalud.EstadoSalud;
                    canAddSalud = true;

                }
                if (beneficiario.BeneficiarioSalud.TieneTarjeta.HasValue)
                {
                    BeneficiarioSaludToAdd.TieneTarjeta = beneficiario.BeneficiarioSalud.TieneTarjeta.Value;
                    canAddSaludTarjeta = true;
                }
                if (canAddSalud != null && canAddSaludTarjeta != null)
                {
                    BeneficiarioSaludToAdd.CreadoPor = SystemUsername;
                    BeneficiarioToAdd.BeneficiarioSalud.Add(BeneficiarioSaludToAdd);
                }

                BeneficiarioEducacion BeneficiarioEducacionToAdd = new BeneficiarioEducacion();
                bool? canAddEducacion = null;
                
                BeneficiarioEducacionToAdd.GradoEducacion = !string.IsNullOrEmpty(beneficiario.BeneficiarioEducacion.GradoEducacion)?beneficiario.BeneficiarioEducacion.GradoEducacion:"";
                BeneficiarioEducacionToAdd.Motivo = !string.IsNullOrEmpty(beneficiario.BeneficiarioEducacion.Motivo) ? beneficiario.BeneficiarioEducacion.Motivo : "";
                BeneficiarioEducacionToAdd.UltimoAño = !string.IsNullOrEmpty(beneficiario.BeneficiarioEducacion.UltimoAño) ? beneficiario.BeneficiarioEducacion.UltimoAño : "";
                BeneficiarioEducacionToAdd.UltimoGrado = !string.IsNullOrEmpty(beneficiario.BeneficiarioEducacion.UltimoGrado) ? beneficiario.BeneficiarioEducacion.UltimoGrado : "";
                BeneficiarioEducacionToAdd.NombreCentroEscolar = !string.IsNullOrEmpty(beneficiario.BeneficiarioEducacion.NombreCentroEscolar) ? beneficiario.BeneficiarioEducacion.NombreCentroEscolar : "";
                BeneficiarioEducacionToAdd.Turno = !string.IsNullOrEmpty(beneficiario.BeneficiarioEducacion.Turno) ? beneficiario.BeneficiarioEducacion.Turno : "";
             
                if (beneficiario.BeneficiarioEducacion.Estudia.HasValue)
                {
                    BeneficiarioEducacionToAdd.Estudia = beneficiario.BeneficiarioEducacion.Estudia.Value;
                    canAddEducacion = true;
                }
                if (canAddEducacion != null)
                {
                    BeneficiarioEducacionToAdd.CreadoPor = SystemUsername;
                    BeneficiarioToAdd.BeneficiarioEducacion.Add(BeneficiarioEducacionToAdd);
                }



                _beneficiario.Add(BeneficiarioToAdd);
                _context.SaveChanges();


                response.IsSucess = true;
                response.ResponseData = string.Empty;
                response.Message = "Registro Creado Satisfactoriamente";
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
             
            var data = context.Request;
            var sr = new StreamReader(data.InputStream);
            var stream = sr.ReadToEnd();
            var javaScriptSerializer = new JavaScriptSerializer();
            var beneficiario = javaScriptSerializer.Deserialize<BeneficiarioTemplate>(stream);
            int ID_Beneficiario = beneficiario.ID_Beneficiario;

            try
            {

            Beneficiario BeneficiarioToAdd = _beneficiario.GetFirst(b=> b.ID_Beneficiario ==ID_Beneficiario);

            BeneficiarioToAdd.Nombre = beneficiario.Nombre;
            BeneficiarioToAdd.Apellido = beneficiario.Apellido;
            BeneficiarioToAdd.Edad = beneficiario.Edad;
            BeneficiarioToAdd.ID_Programa = beneficiario.ID_Programa;
            BeneficiarioToAdd.Codigo = beneficiario.Codigo;
            BeneficiarioToAdd.Sexo = beneficiario.Sexo;
            BeneficiarioToAdd.Dui = beneficiario.Dui;
            BeneficiarioToAdd.Direccion = beneficiario.Direccion;
            BeneficiarioToAdd.ModificadoPor = SystemUsername;

            
            //BeneficiarioToAdd.BeneficiarioAdicional.First()

            if (BeneficiarioToAdd.BeneficiarioAdicional.Count == 0)
            {
                BeneficiarioAdicional BeneficiarioAdicionalToAdd = new BeneficiarioAdicional();
                bool? canAddChildAdicional = null;

                BeneficiarioAdicionalToAdd.NombreEmergencia = !string.IsNullOrEmpty(beneficiario.BeneficiarioAdicional.NombreEmergencia) ? beneficiario.BeneficiarioAdicional.NombreEmergencia : "";
                BeneficiarioAdicionalToAdd.NumeroEmergencia = !string.IsNullOrEmpty(beneficiario.BeneficiarioAdicional.NumeroEmergencia) ? beneficiario.BeneficiarioAdicional.NumeroEmergencia : "";
                if (beneficiario.BeneficiarioAdicional.TieneRegistroNacimiento.HasValue)
                {
                    BeneficiarioAdicionalToAdd.TieneRegistroNacimiento = beneficiario.BeneficiarioAdicional.TieneRegistroNacimiento.Value;
                    canAddChildAdicional = true;
                }

                if (canAddChildAdicional != null)
                {
                    BeneficiarioAdicionalToAdd.CreadoPor = SystemUsername;
                    BeneficiarioToAdd.BeneficiarioAdicional.Add(BeneficiarioAdicionalToAdd);
                }
            }
            else
            {
                BeneficiarioToAdd.BeneficiarioAdicional.First().NombreEmergencia = !string.IsNullOrEmpty(beneficiario.BeneficiarioAdicional.NombreEmergencia) ? beneficiario.BeneficiarioAdicional.NombreEmergencia : "";
                BeneficiarioToAdd.BeneficiarioAdicional.First().NumeroEmergencia = !string.IsNullOrEmpty(beneficiario.BeneficiarioAdicional.NumeroEmergencia) ? beneficiario.BeneficiarioAdicional.NumeroEmergencia : "";
                BeneficiarioToAdd.BeneficiarioAdicional.First().TieneRegistroNacimiento = beneficiario.BeneficiarioAdicional.TieneRegistroNacimiento.Value;
                BeneficiarioToAdd.BeneficiarioAdicional.First().ModificadoPor = SystemUsername;
            }

            if (BeneficiarioToAdd.BeneficiarioCompromiso.Count == 0)
            {
                BeneficiarioCompromiso BeneficiaroCompromisoToAdd = new BeneficiarioCompromiso();
                bool? canAddCompromiso = null;
                BeneficiaroCompromisoToAdd.Comentario = !string.IsNullOrEmpty(beneficiario.BeneficiarioCompromiso.Comentario) ? beneficiario.BeneficiarioCompromiso.Comentario : "";
                BeneficiaroCompromisoToAdd.NombreIglesia = !string.IsNullOrEmpty(beneficiario.BeneficiarioCompromiso.NombreIglesia) ? beneficiario.BeneficiarioCompromiso.NombreIglesia : "";
                if (beneficiario.BeneficiarioCompromiso.ExistioProblema.HasValue)
                {
                    BeneficiaroCompromisoToAdd.ExistioProblema = beneficiario.BeneficiarioCompromiso.ExistioProblema.Value;
                }
                if (beneficiario.BeneficiarioCompromiso.SeCongrega.HasValue)
                {
                    BeneficiaroCompromisoToAdd.SeCongrega = beneficiario.BeneficiarioCompromiso.SeCongrega.Value;
                }
                if (beneficiario.BeneficiarioCompromiso.AceptaCompromiso.HasValue)
                {
                    BeneficiaroCompromisoToAdd.AceptaCompromiso = beneficiario.BeneficiarioCompromiso.AceptaCompromiso.Value;
                    canAddCompromiso = true;
                }
                if (canAddCompromiso != null)
                {
                    BeneficiaroCompromisoToAdd.CreadoPor = SystemUsername;
                    BeneficiarioToAdd.BeneficiarioCompromiso.Add(BeneficiaroCompromisoToAdd);
                }
            }
            else
            {
                BeneficiarioToAdd.BeneficiarioCompromiso.First().Comentario = !string.IsNullOrEmpty(beneficiario.BeneficiarioCompromiso.Comentario) ? beneficiario.BeneficiarioCompromiso.Comentario : "";
                BeneficiarioToAdd.BeneficiarioCompromiso.First().NombreIglesia = !string.IsNullOrEmpty(beneficiario.BeneficiarioCompromiso.NombreIglesia) ? beneficiario.BeneficiarioCompromiso.NombreIglesia : "";
                BeneficiarioToAdd.BeneficiarioCompromiso.First().ExistioProblema = beneficiario.BeneficiarioCompromiso.ExistioProblema.Value;
                BeneficiarioToAdd.BeneficiarioCompromiso.First().SeCongrega = beneficiario.BeneficiarioCompromiso.SeCongrega.Value;
                BeneficiarioToAdd.BeneficiarioCompromiso.First().AceptaCompromiso = beneficiario.BeneficiarioCompromiso.AceptaCompromiso.Value;
                BeneficiarioToAdd.BeneficiarioCompromiso.First().ModificadoPor = SystemUsername;
            }

            if (BeneficiarioToAdd.BeneficiarioSalud.Count == 0)
            {
                BeneficiarioSalud BeneficiarioSaludToAdd = new BeneficiarioSalud();
                bool? canAddSalud = null;
                bool? canAddSaludTarjeta = null;

                BeneficiarioSaludToAdd.Discapacidad = !string.IsNullOrEmpty(beneficiario.BeneficiarioSalud.Discapacidad) ? beneficiario.BeneficiarioSalud.Discapacidad : "";
                BeneficiarioSaludToAdd.Enfermedad = !string.IsNullOrEmpty(beneficiario.BeneficiarioSalud.Enfermedad) ? beneficiario.BeneficiarioSalud.Enfermedad : "";
                BeneficiarioSaludToAdd.FechaCurvaCrecimiento = beneficiario.BeneficiarioSalud.FechaCurvaCrecimiento != null ? beneficiario.BeneficiarioSalud.FechaCurvaCrecimiento : new DateTime(1900, 1, 1);
                BeneficiarioSaludToAdd.FechaInmunizacion = beneficiario.BeneficiarioSalud.FechaInmunizacion != null ? beneficiario.BeneficiarioSalud.FechaInmunizacion : new DateTime(1900, 1, 1);
                if (!string.IsNullOrEmpty(beneficiario.BeneficiarioSalud.EstadoSalud))
                {
                    BeneficiarioSaludToAdd.EstadoSalud = beneficiario.BeneficiarioSalud.EstadoSalud;
                    canAddSalud = true;

                }
                if (beneficiario.BeneficiarioSalud.TieneTarjeta.HasValue)
                {
                    BeneficiarioSaludToAdd.TieneTarjeta = beneficiario.BeneficiarioSalud.TieneTarjeta.Value;
                    canAddSaludTarjeta = true;
                }
                if (canAddSalud != null && canAddSaludTarjeta != null)
                {
                    BeneficiarioSaludToAdd.CreadoPor = SystemUsername;
                    BeneficiarioToAdd.BeneficiarioSalud.Add(BeneficiarioSaludToAdd);
                }
            }
            else
            {
                BeneficiarioToAdd.BeneficiarioSalud.First().Discapacidad = !string.IsNullOrEmpty(beneficiario.BeneficiarioSalud.Discapacidad) ? beneficiario.BeneficiarioSalud.Discapacidad : "";
                BeneficiarioToAdd.BeneficiarioSalud.First().Enfermedad = !string.IsNullOrEmpty(beneficiario.BeneficiarioSalud.Enfermedad) ? beneficiario.BeneficiarioSalud.Enfermedad : "";
                BeneficiarioToAdd.BeneficiarioSalud.First().FechaCurvaCrecimiento = beneficiario.BeneficiarioSalud.FechaCurvaCrecimiento != null ? beneficiario.BeneficiarioSalud.FechaCurvaCrecimiento : new DateTime(1900, 1, 1);
                BeneficiarioToAdd.BeneficiarioSalud.First().FechaInmunizacion = beneficiario.BeneficiarioSalud.FechaInmunizacion != null ? beneficiario.BeneficiarioSalud.FechaInmunizacion : new DateTime(1900, 1, 1);
                BeneficiarioToAdd.BeneficiarioSalud.First().EstadoSalud = beneficiario.BeneficiarioSalud.EstadoSalud;
                BeneficiarioToAdd.BeneficiarioSalud.First().TieneTarjeta = beneficiario.BeneficiarioSalud.TieneTarjeta.Value;
                BeneficiarioToAdd.BeneficiarioSalud.First().ModificadoPor = SystemUsername;
            }

            if (BeneficiarioToAdd.BeneficiarioEducacion.Count==0)
            {
                BeneficiarioEducacion BeneficiarioEducacionToAdd = new BeneficiarioEducacion();
                bool? canAddEducacion = null;

                BeneficiarioEducacionToAdd.GradoEducacion = !string.IsNullOrEmpty(beneficiario.BeneficiarioEducacion.GradoEducacion) ? beneficiario.BeneficiarioEducacion.GradoEducacion : "";
                BeneficiarioEducacionToAdd.Motivo = !string.IsNullOrEmpty(beneficiario.BeneficiarioEducacion.Motivo) ? beneficiario.BeneficiarioEducacion.Motivo : "";
                BeneficiarioEducacionToAdd.UltimoAño = !string.IsNullOrEmpty(beneficiario.BeneficiarioEducacion.UltimoAño) ? beneficiario.BeneficiarioEducacion.UltimoAño : "";
                BeneficiarioEducacionToAdd.UltimoGrado = !string.IsNullOrEmpty(beneficiario.BeneficiarioEducacion.UltimoGrado) ? beneficiario.BeneficiarioEducacion.UltimoGrado : "";
                BeneficiarioEducacionToAdd.NombreCentroEscolar = !string.IsNullOrEmpty(beneficiario.BeneficiarioEducacion.NombreCentroEscolar) ? beneficiario.BeneficiarioEducacion.NombreCentroEscolar : "";
                BeneficiarioEducacionToAdd.Turno = !string.IsNullOrEmpty(beneficiario.BeneficiarioEducacion.Turno) ? beneficiario.BeneficiarioEducacion.Turno : "";

                if (beneficiario.BeneficiarioEducacion.Estudia.HasValue)
                {
                    BeneficiarioEducacionToAdd.Estudia = beneficiario.BeneficiarioEducacion.Estudia.Value;
                    canAddEducacion = true;
                }
                if (canAddEducacion != null)
                {
                    BeneficiarioEducacionToAdd.CreadoPor = SystemUsername;
                    BeneficiarioToAdd.BeneficiarioEducacion.Add(BeneficiarioEducacionToAdd);
                }
            }
            else
            {
                BeneficiarioToAdd.BeneficiarioEducacion.First().GradoEducacion = !string.IsNullOrEmpty(beneficiario.BeneficiarioEducacion.GradoEducacion) ? beneficiario.BeneficiarioEducacion.GradoEducacion : "";
                BeneficiarioToAdd.BeneficiarioEducacion.First().Motivo = !string.IsNullOrEmpty(beneficiario.BeneficiarioEducacion.Motivo) ? beneficiario.BeneficiarioEducacion.Motivo : "";
                BeneficiarioToAdd.BeneficiarioEducacion.First().UltimoAño = !string.IsNullOrEmpty(beneficiario.BeneficiarioEducacion.UltimoAño) ? beneficiario.BeneficiarioEducacion.UltimoAño : "";
                BeneficiarioToAdd.BeneficiarioEducacion.First().UltimoGrado = !string.IsNullOrEmpty(beneficiario.BeneficiarioEducacion.UltimoGrado) ? beneficiario.BeneficiarioEducacion.UltimoGrado : "";
                BeneficiarioToAdd.BeneficiarioEducacion.First().NombreCentroEscolar = !string.IsNullOrEmpty(beneficiario.BeneficiarioEducacion.NombreCentroEscolar) ? beneficiario.BeneficiarioEducacion.NombreCentroEscolar : "";
                BeneficiarioToAdd.BeneficiarioEducacion.First().Turno = !string.IsNullOrEmpty(beneficiario.BeneficiarioEducacion.Turno) ? beneficiario.BeneficiarioEducacion.Turno : "";
                BeneficiarioToAdd.BeneficiarioEducacion.First().ModificadoPor = SystemUsername;
            }

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