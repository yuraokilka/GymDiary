namespace GymDiary.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteCascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Exercises", "ExerciseType_Id", "dbo.ExerciseTypes");
            DropForeignKey("dbo.Sets", "Exercise_Id", "dbo.Exercises");
            DropForeignKey("dbo.Exercises", "Workout_Id", "dbo.Workouts");
            DropIndex("dbo.Exercises", new[] { "ExerciseType_Id" });
            DropIndex("dbo.Exercises", new[] { "Workout_Id" });
            DropIndex("dbo.Sets", new[] { "Exercise_Id" });
            AlterColumn("dbo.Exercises", "ExerciseType_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Exercises", "Workout_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Sets", "Exercise_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Exercises", "Workout_Id");
            CreateIndex("dbo.Exercises", "ExerciseType_Id");
            CreateIndex("dbo.Sets", "Exercise_Id");
            AddForeignKey("dbo.Exercises", "ExerciseType_Id", "dbo.ExerciseTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Sets", "Exercise_Id", "dbo.Exercises", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Exercises", "Workout_Id", "dbo.Workouts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exercises", "Workout_Id", "dbo.Workouts");
            DropForeignKey("dbo.Sets", "Exercise_Id", "dbo.Exercises");
            DropForeignKey("dbo.Exercises", "ExerciseType_Id", "dbo.ExerciseTypes");
            DropIndex("dbo.Sets", new[] { "Exercise_Id" });
            DropIndex("dbo.Exercises", new[] { "ExerciseType_Id" });
            DropIndex("dbo.Exercises", new[] { "Workout_Id" });
            AlterColumn("dbo.Sets", "Exercise_Id", c => c.Int());
            AlterColumn("dbo.Exercises", "Workout_Id", c => c.Int());
            AlterColumn("dbo.Exercises", "ExerciseType_Id", c => c.Int());
            CreateIndex("dbo.Sets", "Exercise_Id");
            CreateIndex("dbo.Exercises", "Workout_Id");
            CreateIndex("dbo.Exercises", "ExerciseType_Id");
            AddForeignKey("dbo.Exercises", "Workout_Id", "dbo.Workouts", "Id");
            AddForeignKey("dbo.Sets", "Exercise_Id", "dbo.Exercises", "Id");
            AddForeignKey("dbo.Exercises", "ExerciseType_Id", "dbo.ExerciseTypes", "Id");
        }
    }
}
