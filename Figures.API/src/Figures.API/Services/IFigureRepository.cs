namespace Figures.API.Services
{
    using System.Linq;
    using Figures.API.Entities;
    using System.Collections.Generic;

    public interface IFigureRepository
    {
        IEnumerable<Figure> GetFigures();

        Figure GetFigure(int id);

        void AddFigure(Figure newFigure);

        bool Save();
    }
}
