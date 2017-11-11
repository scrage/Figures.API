namespace Figures.API.Services
{
    using System.Linq;
    using Figures.API.Entities;
    using System.Collections.Generic;

    public interface IFigureRepository
    {
        IEnumerable<Figure> GetFigures();

        Figure GetFigure(int id);

        bool DoesFigureExist(int id);

        void AddFigure(Figure newFigure);

        void DeleteFigure(Figure figureToDelete);

        bool Save();
    }
}
