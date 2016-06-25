using GymDiary.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GymDiary.WebUI.ViewModels
{
    public class WorkoutViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Date of workout")]
        [Required(ErrorMessage = "Please, enter your {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime WorkoutDate { get; set; }

        public string TotalWeight { get; set; }

        [Required]
        public ICollection<ExerciseViewModel> Exercises { get; set; }
       
        public WorkoutViewModel()
        {
            Exercises = new List<ExerciseViewModel>();
        }
    }

    public class ExerciseViewModel
    {
        public int Id { get; set; }

        public string Index { get; set; } // required for generating GUID for HTML-tag

        [Display(Name = "Exercise")]
        [Required(ErrorMessage = "Please, select your {0}")]
        public ExerciseType ExerciseType { get; set; }

        public ICollection<ExerciseType> ExerciseTypeOptions { get; set; }

        [Display(Name = "Comment")]
        [StringLength(1000, ErrorMessage = "The {0} must be less than {1} characters")]
        public string Comment { get; set; }

        [Display(Name = "Sets")]
        [Required]
        public ICollection<SetViewModel> Sets { get; set; }
        
        public ExerciseViewModel()
        {
            ExerciseType = new ExerciseType();
            ExerciseTypeOptions = new List<ExerciseType>();
            Sets = new List<SetViewModel>();
        }
    }

    public class SetViewModel
    {
        public int Id { get; set; }
        public string Index { get; set; } // required for generating ID for HTML-tag

        [Display(Name = "Repetitions")]
        [Required(ErrorMessage = "Please, enter your {0}")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please, enter a valid {0}")] // only accept a number with arabic numerals
        public string Repetitions { get; set; }

        [Display(Name = "Weight")]
        [Required(ErrorMessage = "Please, enter your {0}")]
        [RegularExpression(@"^(\d+\,)?\d+$", ErrorMessage = "Please, enter a valid {0}")] // only accept a number with comma
        public string Weight { get; set; }
        
    }

    public class FilterWorkoutList
    {
        [Display(Name = "From date")]
        [Required(ErrorMessage = "Please, enter your {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime From { get; set; }

        [Display(Name = "To date")]
        [Required(ErrorMessage = "Please, enter your {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime To { get; set; }
    }

    public class FilterClientsWorkoutList : FilterWorkoutList
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please, enter client's {0}")]
        public string Username { get; set; }
    }

}