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
       
    }
}
