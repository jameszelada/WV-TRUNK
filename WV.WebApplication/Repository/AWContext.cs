using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using DataLayer;

namespace Repository
{
    public class AWContext: System.Data.Entity.DbContext,IAWContext
    {
        public AWContext()
        {

        }

        public AWContext(string connectionstring) : base(connectionstring)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Recurso> Recurso { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<RolRecurso> RolRecurso { get; set; }
        public DbSet<UsuarioRol> UsuarioRol { get; set; }
        public DbSet<Objetivo> Objetivo { get; set; }
        public DbSet<Indicador> Indicador { get; set; }
        public DbSet<TipoPrograma> TipoPrograma { get; set; }
        public DbSet<TipoPuesto> TipoPuesto { get; set; }
        public DbSet<Puesto> Puesto { get; set; }
        public DbSet<Comunidad> Comunidad { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Bitacora> Bitacora { get; set; }
        public DbSet<BitacoraDetalle> BitacoraDetalle { get; set; }
        public DbSet<PlanSemanal> PlanSemanal { get; set; }
        public DbSet<PlanSemanalDetalle> PlanSemanalDetalle { get; set; }
        public DbSet<Proyecto> Proyecto { get; set; }
        
       
    }
}
