using Figures.API.Models;

namespace Figures.API
{
    using System.Linq;
    using Figures.API.Entities;
    using System.Collections.Generic;

    public static class FigureContextExtensions
    {
        public static void EnsureSeedDataForContext(this FigureContext context)
        {
            if (context.Figures.Any())
            {
                return;
            }

            // Init seed data.
            var figures = new List<Figure>()
            {
                new Figure()
                {
                    FigureType = FigureType.Fictional,
                    FirstName = "Hanzo",
                    LastName = "Hasashi",
                    Gender = Gender.Male,
                    Alias = "Scorpion",
                    Description = "Leader of the Shirai Ryu clan, master assassin. Deceased, and later resurrected by Quan Chi."
                },
                new Figure()
                {
                    FigureType = FigureType.Fictional,
                    FirstName = "Bi",
                    LastName = "Han",
                    Gender = Gender.Male,
                    Alias = "Sub-Zero",
                    UniquelyDisplayedFullName = "Bi-Han",
                    Description = "Leader of the Lin Kuei clan. Childhood friend of Hanzo Hasashi, later on, killed by him."
                },
                new Figure()
                {
                    FigureType = FigureType.Mythological,
                    FirstName = "Athene",
                    LastName = "Pallas",
                    Gender = Gender.Female,
                    Alias = "Athena",
                    Description = "Godess of wisdom, justice, just war, art and education. Daughter of Zeus and Metis",
                    IsLastNameFirst = true
                }
            };

            context.AddRange(figures);
            context.SaveChanges();
        }
    }
}
