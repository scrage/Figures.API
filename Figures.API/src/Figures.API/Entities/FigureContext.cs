namespace Figures.API.Entities
{
    using Microsoft.EntityFrameworkCore;

    public class FigureContext : DbContext
    {
        public FigureContext(DbContextOptions<FigureContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Figure> Figures { get; set; }
    }
}
