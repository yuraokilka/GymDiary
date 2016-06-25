namespace GymDiary.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TrainerName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TrainerName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TrainerName");
        }
    }
}
