namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populateGenres : DbMigration
    {
        public override void Up()
        {
            Sql("insert into [Genres] ([Id],[Name]) VALUES (1,'Action')");
            Sql("insert into [Genres] ([Id],[Name]) VALUES (2,'Drama')");
            Sql("insert into [Genres] ([Id],[Name]) VALUES (3,'Comedy')");
            Sql("insert into [Genres] ([Id],[Name]) VALUES (4,'Horror')");
            Sql("insert into [Genres] ([Id],[Name]) VALUES (5,'SciFi')");
            Sql("insert into [Genres] ([Id],[Name]) VALUES (6,'Suspense')");
            Sql("insert into [Genres] ([Id],[Name]) VALUES (7,'Animated')");
        }
        
        public override void Down()
        {
        }
    }
}
