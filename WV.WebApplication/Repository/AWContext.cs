using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using DataLayer;
using System.Data;

namespace Repository
{
    public class AWContext: System.Data.Entity.DbContext,IAWContext
    {
       
        public AWContext() :base("VISIONMUNDIALEntities")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public AWContext(string connectionstring) : base(connectionstring)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public override int SaveChanges()
        {
            DateTime saveTime = DateTime.Now;
            string defaultStringDate = new DateTime(1990, 1, 1).ToString();
            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == (EntityState)System.Data.EntityState.Added))
            {
                if (entry.Property("FechaCreacion").CurrentValue.ToString() == defaultStringDate)
                {
                    entry.Property("FechaCreacion").CurrentValue = saveTime;
                }
                    
            }

            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == (EntityState)System.Data.EntityState.Modified))
            {  
                entry.Property("FechaModificacion").CurrentValue = saveTime;   
            }

            return base.SaveChanges();
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
        public DbSet<AsignacionRecursoHumano> AsignacionRecursoHumano { get; set; }
        public DbSet<Beneficiario> Beneficiario { get; set; }
        public DbSet<BeneficiarioAdicional> BeneficiarioAdicional { get; set; }
        public DbSet<BeneficiarioEducacion> BeneficiarioEducacion { get; set; }
        public DbSet<BeneficiarioSalud> BeneficiarioSalud { get; set; }
        public DbSet<BeneficiarioCompromiso> BeneficiarioCompromiso { get; set; }
        public DbSet<Actividad> Actividad { get; set; }
        public DbSet<Asistencia> Asistencia { get; set; }
        public DbSet<AsignacionMateria> AsignacionMateria { get; set; }
        public DbSet<Materia> Materia { get; set; }
        public DbSet<Examen> Examen { get; set; }
        public DbSet<ExamenResultado> ExamenResultado { get; set; }
       

        
       
    }
}
