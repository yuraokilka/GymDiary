namespace GymDiary.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CalfMeasurements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeasurementDate = c.DateTime(nullable: false),
                        Value = c.Double(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Sex = c.Byte(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.ChestMeasurements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeasurementDate = c.DateTime(nullable: false),
                        Value = c.Double(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ExerciseTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        IsFavorite = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        ExerciseType_Id = c.Int(),
                        Workout_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExerciseTypes", t => t.ExerciseType_Id)
                .ForeignKey("dbo.Workouts", t => t.Workout_Id)
                .Index(t => t.ExerciseType_Id)
                .Index(t => t.Workout_Id);
            
            CreateTable(
                "dbo.Sets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Repetitions = c.Int(nullable: false),
                        Weight = c.Double(nullable: false),
                        Exercise_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercises", t => t.Exercise_Id)
                .Index(t => t.Exercise_Id);
            
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkoutDate = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ForeArmMeasurements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeasurementDate = c.DateTime(nullable: false),
                        Value = c.Double(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.HeightMeasurements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeasurementDate = c.DateTime(nullable: false),
                        Value = c.Double(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.HipsMeasurements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeasurementDate = c.DateTime(nullable: false),
                        Value = c.Double(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.NeckMeasurements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeasurementDate = c.DateTime(nullable: false),
                        Value = c.Double(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ThighMeasurements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeasurementDate = c.DateTime(nullable: false),
                        Value = c.Double(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.UpperArmMeasurements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeasurementDate = c.DateTime(nullable: false),
                        Value = c.Double(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.WaistMeasurements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeasurementDate = c.DateTime(nullable: false),
                        Value = c.Double(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.WeightMeasurements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeasurementDate = c.DateTime(nullable: false),
                        Value = c.Double(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.WeightMeasurements", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.WaistMeasurements", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UpperArmMeasurements", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ThighMeasurements", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.NeckMeasurements", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.HipsMeasurements", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.HeightMeasurements", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ForeArmMeasurements", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Exercises", "Workout_Id", "dbo.Workouts");
            DropForeignKey("dbo.Workouts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sets", "Exercise_Id", "dbo.Exercises");
            DropForeignKey("dbo.Exercises", "ExerciseType_Id", "dbo.ExerciseTypes");
            DropForeignKey("dbo.ExerciseTypes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ChestMeasurements", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CalfMeasurements", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.WeightMeasurements", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.WaistMeasurements", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UpperArmMeasurements", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ThighMeasurements", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.NeckMeasurements", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.HipsMeasurements", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.HeightMeasurements", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ForeArmMeasurements", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Workouts", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Sets", new[] { "Exercise_Id" });
            DropIndex("dbo.Exercises", new[] { "Workout_Id" });
            DropIndex("dbo.Exercises", new[] { "ExerciseType_Id" });
            DropIndex("dbo.ExerciseTypes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.ChestMeasurements", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.CalfMeasurements", new[] { "ApplicationUser_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.WeightMeasurements");
            DropTable("dbo.WaistMeasurements");
            DropTable("dbo.UpperArmMeasurements");
            DropTable("dbo.ThighMeasurements");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.NeckMeasurements");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.HipsMeasurements");
            DropTable("dbo.HeightMeasurements");
            DropTable("dbo.ForeArmMeasurements");
            DropTable("dbo.Workouts");
            DropTable("dbo.Sets");
            DropTable("dbo.Exercises");
            DropTable("dbo.ExerciseTypes");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.ChestMeasurements");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CalfMeasurements");
        }
    }
}
