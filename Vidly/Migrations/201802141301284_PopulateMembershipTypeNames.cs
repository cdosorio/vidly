namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMembershipTypeNames : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE [MembershipTypes] set Name='Bronze' where Id=1");
            Sql("UPDATE [MembershipTypes] set Name='Silver' where Id=2");
            Sql("UPDATE [MembershipTypes] set Name='Gold' where Id=3");
            Sql("UPDATE [MembershipTypes] set Name='Platinum' where Id=4");
        }
        
        public override void Down()
        {
        }
    }
}
