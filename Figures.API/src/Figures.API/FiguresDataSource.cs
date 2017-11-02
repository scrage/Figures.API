using Figures.API.Models;

namespace Figures.API
{
    using System.Collections.Generic;

    public class FiguresDataStore
    {
        public static FiguresDataStore Current { get; } = new FiguresDataStore();

        public List<FigureDto> Figures { get; set; }

        public FiguresDataStore()
        {
            Figures = new List<FigureDto>()
            {
                new FigureDto()
                {
                    Id = 1,
                    FigureType = FigureType.Fictional,
                    FirstName = "Hanzo",
                    LastName = "Hasashi",
                    Gender = Gender.Male
                },

                new FigureDto()
                {
                    Id = 2,
                    FigureType = FigureType.Fictional,
                    FirstName = "Bi",
                    LastName = "Han",
                    UniquelyDisplayedFullName = "Bi-Han",
                    Gender = Gender.Male
                },

                new FigureDto()
                {
                    Id = 3,
                    FigureType = FigureType.Mythological,
                    FirstName = "Pallas",
                    LastName = "Athene",
                    IsLastNameFirst = true,
                    Gender = Gender.Female
                }
            };
        }
    }
}
