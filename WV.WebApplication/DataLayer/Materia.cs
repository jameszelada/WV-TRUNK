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
    
    public partial class Materia
    {
        public Materia()
        {
            this.AsignacionMateria = new HashSet<AsignacionMateria>();
            this.Examen = new HashSet<Examen>();
        }
    
        public int ID_Materia { get; set; }
        public string Nombre { get; set; }
        public string Anio { get; set; }
        public string Grado { get; set; }
    
        public virtual ICollection<AsignacionMateria> AsignacionMateria { get; set; }
        public virtual ICollection<Examen> Examen { get; set; }
    }
}