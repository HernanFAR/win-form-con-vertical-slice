using System.Data.Entity;
using CorteComun.DataAccess.EntityFramework.Relaciones;
using Dominio;

namespace CorteComun.DataAccess.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=" + AppConfiguration.DataAccess.AppDatabaseKey)
        {
            
        }

        public DbSet<Pregunta> Preguntas => Set<Pregunta>();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfiguracionTablaPreguntas.Configurar(modelBuilder.Entity<Pregunta>());
        }
    }
}
