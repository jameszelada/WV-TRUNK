//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class BeneficiarioAdicional
    {
        public BeneficiarioAdicional()
        {
            this.CreadoPor = "";
            this.ModificadoPor = "";
            this.FechaCreacion = new DateTime(1990, 1, 1);
            this.FechaModificacion = new DateTime(1990, 1, 1);
        }
        public int ID_BeneficiarioAdicional { get; set; }
        public string NombreEmergencia { get; set; }
        public string NumeroEmergencia { get; set; }
        public bool TieneRegistroNacimiento { get; set; }
        public int ID_Beneficiario { get; set; }
        public string CreadoPor { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    
        public virtual Beneficiario Beneficiario { get; set; }
    }
}
