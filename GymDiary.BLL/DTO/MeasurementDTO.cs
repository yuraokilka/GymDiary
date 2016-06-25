using GymDiary.DAL.Entities;
using System;

namespace GymDiary.BLL.DTO
{
    public class MeasurementDTO
    {
        public int Id { get; set; }
        public DateTime MeasurementDate { get; set; }
        public string Value { get; set; }
        public string Difference { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
