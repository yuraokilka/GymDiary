using GymDiary.DAL.Entities;
using System;
using System.Collections.Generic;

namespace GymDiary.DAL.EF
{
    public static class InitialData
    {
        #region ApplicationUsers
        public static List<ApplicationUser> ApplicationUsers = new List<ApplicationUser>()
        {
            new ApplicationUser()
            {
                UserName = "andriy.kolobovskyy",
                Email = "andriy.kolobov@gmail.com",
                TrainerName = "andriy.hryfovych",
                Sex = (byte)Sex.Male,
                BirthDate = DateTime.Now,
                RegistrationDate = DateTime.Now
            },
            new ApplicationUser()
            {
                UserName = "mykola.baget",
                Email = "mykola.baget@gmail.com",
                TrainerName = "andriy.hryfovych",
                Sex = (byte)Sex.Male,
                BirthDate = DateTime.Now,
                RegistrationDate = DateTime.Now
            },
            new ApplicationUser()
            {
                UserName = "denys.baranovskyy",
                Email = "denys.baranov@gmail.com",
                TrainerName = "andriy.hryfovych",
                Sex = (byte)Sex.Male,
                BirthDate = DateTime.Now,
                RegistrationDate = DateTime.Now
            },
            new ApplicationUser()
            {
                UserName = "stepan.fedotov",
                Email = "stepan.fedotov@gmail.com",
                TrainerName = "andriy.hryfovych",
                Sex = (byte)Sex.Male,
                BirthDate = DateTime.Now,
                RegistrationDate = DateTime.Now
            },
            new ApplicationUser()
            {
                UserName = "viktor.petrov",
                Email = "viktor.petrov@gmail.com",
                TrainerName = "andriy.hryfovych",
                Sex = (byte)Sex.Male,
                BirthDate = DateTime.Now,
                RegistrationDate = DateTime.Now
            },
        };
        #endregion

        #region Measurements

        #region WeightMeasurements
        public static List<WeightMeasurement> WeightMeasurement = new List<WeightMeasurement>()
        {
            new WeightMeasurement()
            {
                MeasurementDate = DateTime.Today,
                Value = 70
            },
            new WeightMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-4),
                Value = 72
            },
            new WeightMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-8),
                Value = 73
            },
            new WeightMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-16),
                Value = 69.5
            },
            new WeightMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-28),
                Value = 70.2
            },
            new WeightMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-36),
                Value = 67.8
            },
            new WeightMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-44),
                Value = 65
            },
            new WeightMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-52),
                Value = 66
            },
            new WeightMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-57),
                Value = 68
            },
            new WeightMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-60),
                Value = 65
            }
        };
        #endregion

        #region HeightMeasurements
        public static List<HeightMeasurement> HeightMeasurement = new List<HeightMeasurement>()
        {
            new HeightMeasurement()
            {
                MeasurementDate = DateTime.Today,
                Value = 180
            },
            new HeightMeasurement()
            {
                MeasurementDate = DateTime.Today.AddYears(-1),
                Value = 179
            },
            new HeightMeasurement()
            {
                MeasurementDate = DateTime.Today.AddYears(-2),
                Value = 178
            },
            new HeightMeasurement()
            {
                MeasurementDate = DateTime.Today.AddYears(-3),
                Value = 177.5
            },
            new HeightMeasurement()
            {
                MeasurementDate = DateTime.Today.AddYears(-4),
                Value = 175
            }
        };
        #endregion

        #region UpperArmMeasurements
        public static List<UpperArmMeasurement> UpperArmMeasurement = new List<UpperArmMeasurement>()
        {
            new UpperArmMeasurement()
            {
                MeasurementDate = DateTime.Today,
                Value = 40
            },
            new UpperArmMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-4),
                Value = 43.2
            },
            new UpperArmMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-8),
                Value = 43.3
            },
            new UpperArmMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-16),
                Value = 43.5
            },
            new UpperArmMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-28),
                Value = 42.7
            },
            new UpperArmMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-36),
                Value = 42.8
            },
            new UpperArmMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-44),
                Value = 42.6
            },
            new UpperArmMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-52),
                Value = 41
            },
            new UpperArmMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-57),
                Value = 42.2
            },
            new UpperArmMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-60),
                Value = 41.1
            }
        };
        #endregion

        #region ForeArmMeasurements
        public static List<ForeArmMeasurement> ForeArmMeasurement = new List<ForeArmMeasurement>()
        {
            new ForeArmMeasurement()
            {
                MeasurementDate = DateTime.Today,
                Value = 24
            },
            new ForeArmMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-4),
                Value = 23.7
            },
            new ForeArmMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-8),
                Value = 23.5
            },
            new ForeArmMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-16),
                Value = 23.2
            },
            new ForeArmMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-28),
                Value = 22
            },
            new ForeArmMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-36),
                Value = 22.8
            },
            new ForeArmMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-44),
                Value = 22.6
            },
            new ForeArmMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-52),
                Value = 21
            },
            new ForeArmMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-57),
                Value = 21.2
            },
            new ForeArmMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-60),
                Value = 21.1
            }
        };
        #endregion

        #region NeckMeasurements
        public static List<NeckMeasurement> NeckMeasurement = new List<NeckMeasurement>()
        {
            new NeckMeasurement()
            {
                MeasurementDate = DateTime.Today,
                Value = 39
            },
            new NeckMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-4),
                Value = 38
            },
            new NeckMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-8),
                Value = 37.9
            },
            new NeckMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-16),
                Value = 37.8
            },
            new NeckMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-28),
                Value = 37.7
            },
            new NeckMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-36),
                Value = 37.6
            },
            new NeckMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-44),
                Value = 37.5
            },
            new NeckMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-52),
                Value = 37.4
            },
            new NeckMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-57),
                Value = 37.3
            },
            new NeckMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-60),
                Value = 36
            }
        };
        #endregion

        #region ChestMeasurements
        public static List<ChestMeasurement> ChestMeasurement = new List<ChestMeasurement>()
        {
            new ChestMeasurement()
            {
                MeasurementDate = DateTime.Today,
                Value = 108
            },
            new ChestMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-4),
                Value = 107
            },
            new ChestMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-8),
                Value = 106
            },
            new ChestMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-16),
                Value = 107
            },
            new ChestMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-28),
                Value = 105
            },
            new ChestMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-36),
                Value = 106
            },
            new ChestMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-44),
                Value = 105
            },
            new ChestMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-52),
                Value = 104
            },
            new ChestMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-57),
                Value = 103
            },
            new ChestMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-60),
                Value = 102
            }
        };
        #endregion

        #region WaistMeasurements
        public static List<WaistMeasurement> WaistMeasurement = new List<WaistMeasurement>()
        {
            new WaistMeasurement()
            {
                MeasurementDate = DateTime.Today,
                Value = 70
            },
            new WaistMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-4),
                Value = 71
            },
            new WaistMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-8),
                Value = 73
            },
            new WaistMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-16),
                Value = 73
            },
            new WaistMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-28),
                Value = 74
            },
            new WaistMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-36),
                Value = 75
            },
            new WaistMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-44),
                Value = 76
            },
            new WaistMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-52),
                Value = 75
            },
            new WaistMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-57),
                Value = 77
            },
            new WaistMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-60),
                Value = 79
            }
        };
        #endregion

        #region HipsMeasurements
        public static List<HipsMeasurement> HipsMeasurement = new List<HipsMeasurement>()
        {
            new HipsMeasurement()
            {
                MeasurementDate = DateTime.Today,
                Value = 90
            },
            new HipsMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-4),
                Value = 90
            },
            new HipsMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-8),
                Value = 89
            },
            new HipsMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-16),
                Value = 88
            },
            new HipsMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-28),
                Value = 87
            },
            new HipsMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-36),
                Value = 85
            },
            new HipsMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-44),
                Value = 86
            },
            new HipsMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-52),
                Value = 84
            },
            new HipsMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-57),
                Value = 84
            },
            new HipsMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-60),
                Value = 84
            }
        };
        #endregion

        #region ThighMeasurements
        public static List<ThighMeasurement> ThighMeasurement = new List<ThighMeasurement>()
        {
            new ThighMeasurement()
            {
                MeasurementDate = DateTime.Today,
                Value = 67
            },
            new ThighMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-4),
                Value = 67
            },
            new ThighMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-8),
                Value = 66
            },
            new ThighMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-16),
                Value = 65
            },
            new ThighMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-28),
                Value = 65
            },
            new ThighMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-36),
                Value = 64
            },
            new ThighMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-44),
                Value = 63
            },
            new ThighMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-52),
                Value = 62
            },
            new ThighMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-57),
                Value = 62.4
            },
            new ThighMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-60),
                Value = 62
            }
        };
        #endregion

        #region CalfMeasurements
        public static List<CalfMeasurement> CalfMeasurement = new List<CalfMeasurement>()
        {
            new CalfMeasurement()
            {
                MeasurementDate = DateTime.Today,
                Value = 35.5
            },
            new CalfMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-4),
                Value = 35.4
            },
            new CalfMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-8),
                Value = 35.4
            },
            new CalfMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-16),
                Value = 35.4
            },
            new CalfMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-28),
                Value = 35.3
            },
            new CalfMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-36),
                Value = 35.3
            },
            new CalfMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-44),
                Value = 35.2
            },
            new CalfMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-52),
                Value = 35.2
            },
            new CalfMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-57),
                Value = 35.2
            },
            new CalfMeasurement()
            {
                MeasurementDate = DateTime.Today.AddDays(-60),
                Value = 35
            }
        };
        #endregion

        #endregion

        #region ExerciseTypes
        public static List<ExerciseType> ExerciseTypes = new List<ExerciseType>()
        {
            new ExerciseType()
            {
                Name = "Жим лежачи вузьким хватом",
                Description = "Під час руху лікті рухаються строго уздовж боків, " +
                "завжди спрямовані вперед і не розходяться в сторони. " +
                "Згинання рук відбувається виключно у вертикальній площині.",
                IsFavorite = true
            },
            new ExerciseType()
            {
                Name = "Французький жим",
                Description = "Фіксація рук у верхній частині в нахилі під кутом в 45° до вертикалі " +
                "є ключовим моментом вправи. Руки треба повністю випрямляти у верхній точці вправи. " +
                "Інакше не домогтися максимального скорочення трiцепса.",
                IsFavorite = false
            },
            new ExerciseType()
            {
                Name = "Станова тяга",
                Description = "Завжди одягати на кінці грифа замки! " +
                "Один з крайніх блінів цілком може «від’їхати» від своїх побратимів на пару сантиметрів. " +
                "Це означатиме збільшення ваги на одному кінці грифа, тобто виникне високий ризик травми.",
                IsFavorite = true
            },
            new ExerciseType()
            {
                Name = "Присідання",
                Description = "Виконуючи підхід, не відходити далеко від стійок – " +
                "після виконаного підходу доведеться поставити штангу назад. " +
                "При важких повтореннях присідати з важкоатлетичним поясом, " +
                "він захистить від травм поперек. Не забувати про еластичні бинти для колін і зап’ясть.",
                IsFavorite = true
            },
            new ExerciseType()
            {
                Name = "Підйом гантелей на біцепс",
                Description = "Слідкувати, щоб весь час верхня частина руки (від ліктя до плеча) " +
                "залишалася нерухомою і займала вертикальне положення. " +
                "Виводячи лікті вперед, щоб полегшити підйом, зменшується навантаження на біцепс.",
                IsFavorite = false
            },
            new ExerciseType()
            {
                Name = "Згинання рук зі штангою на лавці Скотта",
                Description = "Основні помилки: обертання грифа штанги кистями, " +
                "при цьому він ніби закидається наверх. Тут починають працювати м’язи передпліччя, " +
                "а біцепс відпочиває. Для запобігання цьому кисті повинні бути злегка відвернутi від себе. " +
                "Плечі мають бути розслаблені, лікті строго зафіксовані на пюпітрі. " +
                "Рух вгору проводиться в більш швидкому темпі, а донизу – плавно і повільно.",
                IsFavorite = false
            },
            new ExerciseType()
            {
                Name = "Віджимання на брусах",
                Description = "Ширина брусів повинна бути трохи більша за ширину плечей, " +
                "оскільки занадто велика відстань може призвести до травми плечей, " +
                "а надто вузька відстань між брусами, знову ж таки, змістить навантаження на тріцепси.",
                IsFavorite = true
            },
            new ExerciseType()
            {
                Name = "Розведення гантелей на похилій лаві",
                Description = "У даній вправі набагато більшу роль відіграє правильна техніка, " +
                "ніж, наприклад, в жимі штанги лежачи. Не робити різких рухів, не пружинити грудьми в нижній точці. " +
                "Розводити руки до комфортного стану щоб уникнути травм плечей.",
                IsFavorite = false
            },
            new ExerciseType()
            {
                Name = "Гіперекстензії",
                Description = "Дана вправа досить травмонебезпечна і може призвести до серйозних " +
                "пошкоджень попереку, особливо якщо виконувати її з неправильною технікою.",
                IsFavorite = true
            },
            new ExerciseType()
            {
                Name = "Нахили зі штангою на плечах",
                Description = "Обов'язково зігнути ноги в колінах у вихідному положенні. " +
                "Це підвищить стійкість.",
                IsFavorite = false
            },
            new ExerciseType()
            {
                Name = "Тяга верхнього блоку до грудей",
                Description = "У вихідному положенні руки потрібно повністю випрямити, а плечовий пояс злегка підняти.",
                IsFavorite = false
            }
        };
        #endregion

        #region Exercises
        public static List<Exercise> Exercises = new List<Exercise>()
        {
            new Exercise()
            {
                // comment is filled for easily adding a link to ExerciseType in future and adding relevant comment
                Comment = "Жим лежачи вузьким хватом",
                Sets = new List<Set>()
                {
                    new Set()
                    {
                        Weight = 50,
                        Repetitions = 10
                    },
                    new Set()
                    {
                        Weight = 55,
                        Repetitions = 10
                    },
                    new Set()
                    {
                        Weight = 60,
                        Repetitions = 10
                    }
                }
            },
            new Exercise()
            {
                Comment = "Французький жим",
                Sets = new List<Set>()
                {
                    new Set()
                    {
                        Weight = 32,
                        Repetitions = 15
                    },
                    new Set()
                    {
                        Weight = 35,
                        Repetitions = 13
                    },
                    new Set()
                    {
                        Weight = 40,
                        Repetitions = 12
                    }
                }
            },
            new Exercise()
            {
                Comment = "Станова тяга",
                Sets = new List<Set>()
                {
                    new Set()
                    {
                        Weight = 80,
                        Repetitions = 8
                    },
                    new Set()
                    {
                        Weight = 86,
                        Repetitions = 7
                    },
                    new Set()
                    {
                        Weight = 90,
                        Repetitions = 7
                    },
                    new Set()
                    {
                        Weight = 92,
                        Repetitions = 6
                    }
                }
            },
            new Exercise()
            {
                Comment = "Присідання",
                Sets = new List<Set>()
                {
                    new Set()
                    {
                        Weight = 78,
                        Repetitions = 8
                    },
                    new Set()
                    {
                        Weight = 85,
                        Repetitions = 8
                    },
                    new Set()
                    {
                        Weight = 87,
                        Repetitions = 9
                    },
                    new Set()
                    {
                        Weight = 92,
                        Repetitions = 6
                    }
                }
            },
            new Exercise()
            {
                Comment = "Підйом гантелей на біцепс",
                Sets = new List<Set>()
                {
                    new Set()
                    {
                        Weight = 30,
                        Repetitions = 6
                    },
                    new Set()
                    {
                        Weight = 30,
                        Repetitions = 8
                    },
                    new Set()
                    {
                        Weight = 32,
                        Repetitions = 10
                    }
                }
            },
            new Exercise()
            {
                Comment = "Згинання рук зі штангою на лавці Скотта",
                Sets = new List<Set>()
                {
                    new Set()
                    {
                        Weight = 30,
                        Repetitions = 6
                    },
                    new Set()
                    {
                        Weight = 30,
                        Repetitions = 8
                    },
                    new Set()
                    {
                        Weight = 32,
                        Repetitions = 10
                    }
                }
            },
            new Exercise()
            {
                Comment = "Віджимання на брусах",
                Sets = new List<Set>()
                {
                    new Set()
                    {
                        Weight = 70,
                        Repetitions = 14
                    },
                    new Set()
                    {
                        Weight = 70,
                        Repetitions = 15
                    },
                    new Set()
                    {
                        Weight = 70,
                        Repetitions = 16
                    },
                    new Set()
                    {
                        Weight = 70,
                        Repetitions = 15
                    }
                }
            },
            new Exercise()
            {
                Comment = "Розведення гантелей на похилій лаві",
                Sets = new List<Set>()
                {
                    new Set()
                    {
                        Weight = 12,
                        Repetitions = 15
                    },
                    new Set()
                    {
                        Weight = 15,
                        Repetitions = 12
                    },
                    new Set()
                    {
                        Weight = 15,
                        Repetitions = 12
                    },
                    new Set()
                    {
                        Weight = 15,
                        Repetitions = 12
                    }
                }
            },
            new Exercise()
            {
                Comment = "Гіперекстензії",
                Sets = new List<Set>()
                {
                    new Set()
                    {
                        Weight = 35,
                        Repetitions = 15
                    },
                    new Set()
                    {
                        Weight = 35,
                        Repetitions = 15
                    },
                    new Set()
                    {
                        Weight = 35,
                        Repetitions = 15
                    }
                }
            },
            new Exercise()
            {
                Comment = "Нахили зі штангою на плечах",
                Sets = new List<Set>()
                {
                    new Set()
                    {
                        Weight = 35,
                        Repetitions = 15
                    },
                    new Set()
                    {
                        Weight = 35,
                        Repetitions = 15
                    },
                    new Set()
                    {
                        Weight = 40,
                        Repetitions = 13
                    },
                    new Set()
                    {
                        Weight = 45,
                        Repetitions = 12
                    }
                }
            },
            new Exercise()
            {
                Comment = "Тяга верхнього блоку до грудей",
                Sets = new List<Set>()
                {
                    new Set()
                    {
                        Weight = 35,
                        Repetitions = 15
                    },
                    new Set()
                    {
                        Weight = 40,
                        Repetitions = 15
                    },
                    new Set()
                    {
                        Weight = 40,
                        Repetitions = 13
                    },
                    new Set()
                    {
                        Weight = 45,
                        Repetitions = 12
                    },
                    new Set()
                    {
                        Weight = 45,
                        Repetitions = 6
                    }
                }
            }
        };
        #endregion
        
    }
}