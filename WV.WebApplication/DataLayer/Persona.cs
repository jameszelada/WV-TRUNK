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
    
    public partial class Persona
    {
        public Persona()
        {
            this.Bitacora = new HashSet<Bitacora>();
            this.PlanSemanal = new HashSet<PlanSemanal>();
        }
    
        public int ID_Persona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dui { get; set; }
        public string Sexo { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    
        public virtual ICollection<Bitacora> Bitacora { get; set; }
        public virtual ICollection<PlanSemanal> PlanSemanal { get; set; }
    }
}
