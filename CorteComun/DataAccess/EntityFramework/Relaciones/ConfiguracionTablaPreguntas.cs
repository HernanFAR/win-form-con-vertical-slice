using System.Data.Entity.ModelConfiguration;
using Dominio;

namespace CorteComun.DataAccess.EntityFramework.Relaciones
{
    internal class ConfiguracionTablaPreguntas
    {
        public static void Configurar(EntityTypeConfiguration<Pregunta> builder)
        {
            builder.ToTable("Preguntas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(x => x.Titulo)
                .HasColumnName("Titulo")
                .IsRequired();

            builder.Property(x => x.Detalle)
                .HasColumnName("Detalle")
                .IsOptional();

            builder.Property(x => x.Respondida)
                .HasColumnName("Respondida")
                .IsRequired();
            
        }
    }
}
