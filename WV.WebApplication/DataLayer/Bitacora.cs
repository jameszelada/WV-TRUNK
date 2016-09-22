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
    
    public partial class Bitacora
    {
        public Bitacora()
        {
            this.BitacoraDetalle = new HashSet<BitacoraDetalle>();
            this.CreadoPor = "";
            this.ModificadoPor = "";
            this.FechaCreacion = new DateTime(1990, 1, 1);
            this.FechaModificacion = new DateTime(1990, 1, 1);
        }
    
        public int ID_Bitacora { get; set; }
        public int ID_Persona { get; set; }
        public System.DateTime FechaBitacora { get; set; }
        public string CreadoPor { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    
        public virtual Persona Persona { get; set; }
        public virtual ICollection<BitacoraDetalle> BitacoraDetalle { get; set; }
    }
}
