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
    
    public partial class BeneficiarioCompromiso
    {
        public int ID_BeneficiarioCompromiso { get; set; }
        public bool AceptaCompromiso { get; set; }
        public bool ExistioProblema { get; set; }
        public bool SeCongrega { get; set; }
        public string NombreIglesia { get; set; }
        public string Comentario { get; set; }
        public int ID_Beneficiario { get; set; }
    
        public virtual Beneficiario Beneficiario { get; set; }
    }
}