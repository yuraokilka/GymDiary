using GymDiary.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GymDiary.WebUI.ViewModels
{
    public class ExerciseTypeListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsFavorite { get; set; }
        
        public ApplicationUser ApplicationUser { get; set; }
    }

    public class CreateExerciseTypeViewModel
    {
        [Display(Name = "Exercise name")]
        [Required(ErrorMessage = "Please, enter your {0}")]
        [StringLength(100, ErrorMessage = "The {0} must be less than {1} characters")]
        public string Name { get; set; }
    }

    public class EditExerciseTypeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please, enter your {0}")]
        [StringLength(100, ErrorMessage = "The {0} must be less than {1} characters")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(5000, ErrorMessage = "The {0} must be less than {1} characters")]
        public string Description { get; set; }

        [Display(Name = "Favorite")]
        public bool IsFavorite { get; set; }
    }
}