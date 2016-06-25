using GymDiary.BLL.DTO;
using GymDiary.BLL.Interfaces;
using GymDiary.DAL.EF;
using GymDiary.DAL.Entities;
using GymDiary.DAL.Interfaces;
using GymDiary.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GymDiary.BLL.Services
{
    public class MeasurementService<TMeasurement> :
        IMeasurementService<TMeasurement>
        where TMeasurement : class, IMeasurementModel
    {
        private ApplicationDbContext context;
        private IRepository<TMeasurement> repository;

        public MeasurementService(ApplicationDbContext db)
        {
            context = db;
            repository = new Repository<TMeasurement>(context);
        }

        public IEnumerable<TMeasurement> Find(Func<TMeasurement, Boolean> predicate)
        {
            return repository.GetAll().ToList().Where(predicate).ToList();
        }

        public IEnumerable<MeasurementDTO> GetAllSortedByDate(string userId)
        {
            // Get user`s measurements
            var measurements = Find(m => m.ApplicationUser.Id == userId).ToList();
            //var measurements = context.Set<TMeasurement>().Where(m => m.ApplicationUser.Id == userId).ToList();
            // Sort by date descending (from new measurements to old ones)
            measurements.Sort((a, b) => b.MeasurementDate.CompareTo(a.MeasurementDate));
            // Map model to viewModel
            var viewModel = new List<MeasurementDTO>();
            foreach (var m in measurements)
            {
                viewModel.Add(MapMeasurement(m));
            }
            // Calculate difference
            CalculateDifference(ref viewModel);
            return viewModel;
        }

        public TMeasurement Get(int id)
        {
            return repository.Get(id);
        }

        public void Create(TMeasurement entity)
        {
            repository.Create(entity);
        }

        public void Update(TMeasurement entity)
        {
            repository.Update(entity);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        protected MeasurementDTO MapMeasurement(IMeasurementModel model)
        {
            var modelDTO = new MeasurementDTO
            {
                Id = model.Id,
                MeasurementDate = model.MeasurementDate,
                Value = String.Format("{0:0.##}", Math.Round(model.Value, 2)),
                ApplicationUser = model.ApplicationUser
            };
            return modelDTO;
        }

        protected void CalculateDifference(ref List<MeasurementDTO> viewModel)
        {
            if (viewModel.Count == 0)
            {
                return;
            }
            viewModel.Last().Difference = String.Format("{0:0.##}", 0d);
            for (int i = 0; i < viewModel.Count - 1; i++)
            {
                double currentVal = Convert.ToDouble(viewModel[i].Value);
                double nextVal = Convert.ToDouble(viewModel[i + 1].Value);
                double difference = currentVal - nextVal;
                viewModel[i].Difference = String.Format("{0:0.##}", Math.Round(difference, 2));
            }
        }
    }

    public class WeightMeasurementService : MeasurementService<WeightMeasurement>
    {
        public WeightMeasurementService(ApplicationDbContext db) : base(db) { }
    }
    public class HeightMeasurementService : MeasurementService<HeightMeasurement>
    {
        public HeightMeasurementService(ApplicationDbContext db) : base(db) { }
    }
    public class UpperArmMeasurementService : MeasurementService<UpperArmMeasurement>
    {
        public UpperArmMeasurementService(ApplicationDbContext db) : base(db) { }
    }
    public class ForeArmMeasurementService : MeasurementService<ForeArmMeasurement>
    {
        public ForeArmMeasurementService(ApplicationDbContext db) : base(db) { }
    }
    public class NeckMeasurementService : MeasurementService<NeckMeasurement>
    {
        public NeckMeasurementService(ApplicationDbContext db) : base(db) { }
    }
    public class ChestMeasurementService : MeasurementService<ChestMeasurement>
    {
        public ChestMeasurementService(ApplicationDbContext db) : base(db) { }
    }
    public class WaistMeasurementService : MeasurementService<WaistMeasurement>
    {
        public WaistMeasurementService(ApplicationDbContext db) : base(db) { }
    }
    public class HipsMeasurementService : MeasurementService<HipsMeasurement>
    {
        public HipsMeasurementService(ApplicationDbContext db) : base(db) { }
    }
    public class ThighMeasurementService : MeasurementService<ThighMeasurement>
    {
        public ThighMeasurementService(ApplicationDbContext db) : base(db) { }
    }
    public class CalfMeasurementService : MeasurementService<CalfMeasurement>
    {
        public CalfMeasurementService(ApplicationDbContext db) : base(db) { }
    }
}
