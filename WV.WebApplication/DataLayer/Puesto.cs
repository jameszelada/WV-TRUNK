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
    
    public partial class Puesto
    {
        public Puesto()
        {
            this.AsignacionRecursoHumano = new HashSet<AsignacionRecursoHumano>();
            this.CreadoPor = "";
            this.ModificadoPor = "";
            this.FechaCreacion = new DateTime(1990, 1, 1);
            this.FechaModificacion = new DateTime(1990, 1, 1);
        }
    
        public int ID_Puesto { get; set; }
        public string Puesto1 { get; set; }
        public string PuestoDescripcion { get; set; }
        public int ID_TipoPuesto { get; set; }
        public string CreadoPor { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    
        public virtual TipoPuesto TipoPuesto { get; set; }
        public virtual ICollection<AsignacionRecursoHumano> AsignacionRecursoHumano { get; set; }
    }
}
