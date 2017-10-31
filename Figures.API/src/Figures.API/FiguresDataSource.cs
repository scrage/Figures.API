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
                    Name = "Hanzo Hasashi",
                    Gender = Gender.Male
                },

                new FigureDto()
                {
                    Id = 2,
                    Name = "Bi-Han",
                    Gender = Gender.Male
                },

                new FigureDto()
                {
                    Id = 3,
                    Name = "Pallas Athene",
                    Gender = Gender.Female
                }
            };
        }
    }
}
