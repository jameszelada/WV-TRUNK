using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WV.WebApplication.Utils
{
    [Serializable]
    public class BeneficiarioTemplate
    {
        public int ID_Beneficiario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dui { get; set; }
        public string Codigo { get; set; }
        public string Edad { get; set; }
        public string Sexo { get; set; }
        public string Direccion { get; set; }
        public int ID_Programa { get; set; }
        public string Programa { get; set; }
        public BeneficiarioTempAdicional BeneficiarioAdicional = new BeneficiarioTempAdicional();
        public BeneficiarioTempSalud BeneficiarioSalud = new BeneficiarioTempSalud();
        public BeneficiarioTempEducacion BeneficiarioEducacion = new BeneficiarioTempEducacion();
        public BeneficiarioTempCompromiso BeneficiarioCompromiso = new BeneficiarioTempCompromiso();
    }
    [Serializable]
    public class BeneficiarioTempAdicional
    {
        public int ID_BeneficiarioAdicional { get; set; }
        public string NombreEmergencia { get; set; }
        public string NumeroEmergencia { get; set; }
        public Nullable<bool> TieneRegistroNacimiento { get; set; }
        public int ID_Beneficiario { get; set; }
    }
     [Serializable]
    public class BeneficiarioTempSalud
    {
        public int ID_BeneficiarioSalud { get; set; }
        public string EstadoSalud { get; set; }
        public Nullable<bool> TieneTarjeta { get; set; }
        public Nullable<System.DateTime> FechaCurvaCrecimiento { get; set; }
        public Nullable<System.DateTime> FechaInmunizacion { get; set; }
        public string Enfermedad { get; set; }
        public string Discapacidad { get; set; }
        public int ID_Beneficiario { get; set; }
    }
     [Serializable]
    public class BeneficiarioTempEducacion
    {

        public int ID_BeneficiarioEducacion { get; set; }
        public Nullable<bool> Estudia { get; set; }
        public string GradoEducacion { get; set; }
        public string Motivo { get; set; }
        public string UltimoGrado { get; set; }
        public string UltimoAño { get; set; }
        public string NombreCentroEscolar { get; set; }
        public string Turno { get; set; }
        public int ID_Beneficiario { get; set; }
    }
     [Serializable]
    public class BeneficiarioTempCompromiso
    {
        public int ID_BeneficiarioCompromiso { get; set; }
        public Nullable<bool> AceptaCompromiso { get; set; }
        public Nullable<bool> ExistioProblema { get; set; }
        public Nullable<bool> SeCongrega { get; set; }
        public string NombreIglesia { get; set; }
        public string Comentario { get; set; }
        public int ID_Beneficiario { get; set; }
    }
}