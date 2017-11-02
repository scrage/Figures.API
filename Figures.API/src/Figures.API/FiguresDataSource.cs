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
                    FullName = "Hanzo Hasashi",
                    Gender = Gender.Male
                },

                new FigureDto()
                {
                    Id = 2,
                    FullName = "Bi-Han",
                    Gender = Gender.Male
                },

                new FigureDto()
                {
                    Id = 3,
                    FullName = "Pallas Athene",
                    Gender = Gender.Female
                }
            };
        }
    }
}
