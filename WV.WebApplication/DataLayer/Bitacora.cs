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
        }
    
        public int ID_Bitacora { get; set; }
        public int ID_Persona { get; set; }
        public System.DateTime FechaBitacora { get; set; }
    
        public virtual Persona Persona { get; set; }
        public virtual ICollection<BitacoraDetalle> BitacoraDetalle { get; set; }
    }
}
