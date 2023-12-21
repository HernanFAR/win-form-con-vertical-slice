using System;
using CorteComun.DataAccess.EntityFramework;
using Dominio;

namespace CorteComun.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppDbContext context)
        {
            context.Preguntas
                .AddOrUpdate(
                    new Pregunta(
                        new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1), 
                        "¿Qué es el Data sedding?",
                        "Es el proceso en el que se generan registros en la base de datos, normalmente con el proposito de ser utilizados como mantenedores") 
                );
        }
    }
}
