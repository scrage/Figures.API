using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Figures.API.Models
{
    public class FigureDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Gender Gender { get; set; }
    }
}
