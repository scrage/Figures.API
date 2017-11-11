﻿namespace Figures.API.Services
{
    using System;
    using System.Linq;
    using Figures.API.Entities;
    using System.Collections.Generic;

    public class FigureRepository : IFigureRepository
    {
        private FigureContext _context;

        public FigureRepository(FigureContext context)
        {
            _context = context;
        }

        public void AddFigure(Figure newFigure)
        {
            _context.Figures.Add(newFigure);
        }

        public Figure GetFigure(int id)
        {
            return _context.Figures.FirstOrDefault(f => f.Id == id);
        }

        public IEnumerable<Figure> GetFigures()
        {
            return _context.Figures.OrderBy(f => f.Id).ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
