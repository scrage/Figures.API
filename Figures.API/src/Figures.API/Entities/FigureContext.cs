namespace Figures.API.Entities
{
    using Microsoft.EntityFrameworkCore;

    public class FigureContext : DbContext
    {
        public FigureContext(DbContextOptions<FigureContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Figure> Figures { get; set; }
    }
}
