using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymDiary.WebUI.ViewModels
{
    public class TotalStatisticsViewModel
    {
        public int WorkoutsNum { get; set; }
        public int ExercisesNum { get; set; }
        public int SetsNum { get; set; }
        public double TotalWeight { get; set; }
    }

    public class ChartPointViewModel
    {
        public string Date { get; set; }
        public double Value { get; set; }
    }
        
    public class AllStatisticsViewModel
    {
        public TotalStatisticsViewModel TotalStatistics { get; set; }
        public Dictionary<string, int> FavoriteExerciseTypes { get; set; }
        
        public AllStatisticsViewModel()
        {
            TotalStatistics = new TotalStatisticsViewModel();
            FavoriteExerciseTypes = new Dictionary<string, int>();
        }
    }

    public class FilterMeasurementDynamics
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

}