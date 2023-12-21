namespace CorteComun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Preguntas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Titulo = c.String(nullable: false),
                        Detalle = c.String(),
                        Respondida = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Preguntas");
        }
    }
}
