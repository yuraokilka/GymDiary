using System;

namespace GymDiary.DAL.Entities
{
    public interface IMeasurementModel
    {
        int Id { get; set; }
        DateTime MeasurementDate { get; set; }
        double Value { get; set; }

        ApplicationUser ApplicationUser { get; set; }
    }

    public class Measurement : IMeasurementModel
    {
        public int Id { get; set; }
        public DateTime MeasurementDate { get; set; }
        public double Value { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public class WeightMeasurement : Measurement { }
    public class HeightMeasurement : Measurement { }
    public class UpperArmMeasurement : Measurement { }
    public class ForeArmMeasurement : Measurement { }
    public class NeckMeasurement : Measurement { }
    public class ChestMeasurement : Measurement { }
    public class WaistMeasurement : Measurement { }
    public class HipsMeasurement : Measurement { }
    public class ThighMeasurement : Measurement { }
    public class CalfMeasurement : Measurement { }

}
