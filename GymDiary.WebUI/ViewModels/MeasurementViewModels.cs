using GymDiary.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GymDiary.WebUI.ViewModels
{
    public class MeasurementViewModel
    {
        // [ScaffoldColumn(false)] // alternative attribute to hide property 
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Date of measurement")]
        [Required(ErrorMessage = "Please, enter your {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime MeasurementDate { get; set; }

        [Display(Name = "Value")]
        [Required(ErrorMessage = "Please, enter your {0}")]
        [RegularExpression(@"^(\d+\,)?\d+$", ErrorMessage = "Please, enter a valid {0}")] // only accept a number with comma
        public string Value { get; set; }

        [Display(Name = "Difference")]
        public string Difference { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }

    public class AllMeasurementsViewModel
    {
        public IEnumerable<MeasurementViewModel> weightMeasurements { get; set; }
        public IEnumerable<MeasurementViewModel> heightMeasurements { get; set; }
        public IEnumerable<MeasurementViewModel> upperArmMeasurements { get; set; }
        public IEnumerable<MeasurementViewModel> foreArmMeasurements { get; set; }
        public IEnumerable<MeasurementViewModel> neckMeasurements { get; set; }
        public IEnumerable<MeasurementViewModel> chestMeasurements { get; set; }
        public IEnumerable<MeasurementViewModel> waistMeasurements { get; set; }
        public IEnumerable<MeasurementViewModel> hipsMeasurements { get; set; }
        public IEnumerable<MeasurementViewModel> thighMeasurements { get; set; }
        public IEnumerable<MeasurementViewModel> calfMeasurements { get; set; }
    }
}