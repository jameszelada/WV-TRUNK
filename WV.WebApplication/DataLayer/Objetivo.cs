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
    
    public partial class Objetivo
    {
        public Objetivo()
        {
            this.CreadoPor = "";
            this.ModificadoPor = "";
            this.FechaCreacion = new DateTime(1990, 1, 1);
            this.FechaModificacion = new DateTime(1990, 1, 1);
        }
        public int ID_Objetivo { get; set; }
        public string Objetivo1 { get; set; }
        public string ObjetivoDescripcion { get; set; }
        public string TipoObjetivo { get; set; }
        public int ID_Programa { get; set; }
        public string CreadoPor { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    
        public virtual Programa Programa { get; set; }
    }
}
