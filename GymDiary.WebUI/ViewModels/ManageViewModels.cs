using System;
using System.ComponentModel.DataAnnotations;

namespace GymDiary.WebUI.ViewModels
{
    public class ManageViewModel
    {
        [Display(Name = "Account name")]
        [Required(ErrorMessage = "Please, enter your {0}")]
        [RegularExpression(@"[a-zA-Z\.\'\-_@]*", ErrorMessage = "Please, enter a valid {0}")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "The {0} must be at least {1} characters long")]
        public string UserName { get; set; }

        [Display(Name = "First name")]
        [RegularExpression(@"[a-zA-Zа-яА-ЯІіЇїЄє\.\'\-_]*", ErrorMessage = "Please, enter a valid {0}")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The {0} must be at least {1} characters long")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [RegularExpression(@"[a-zA-Zа-яА-ЯІіЇїЄє\.\'\-_]*", ErrorMessage = "Please, enter a valid {0}")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The {0} must be at least {1} characters long")]
        public string LastName { get; set; }

        [Display(Name = "Trainer name")]
        [RegularExpression(@"[a-zA-Zа-яА-ЯІіЇїЄє\.\'\-_]*", ErrorMessage = "Please, enter a valid {0}")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The {0} must be at least {1} characters long")]
        public string TrainerName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please, enter your {0}")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Sex")]
        public byte Sex { get; set; }

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        public byte[] ImageData { get; set; }

        //[FileExtensions(Extensions = ".png", ".jpg" ".jpeg")]
        //[HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }


        public bool HasPassword { get; set; }
        
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
    }

}