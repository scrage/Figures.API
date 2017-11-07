using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Figures.API.Models
{
    interface IFigureDto
    {
        string UniquelyDisplayedFullName { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string MiddleName { get; set; }

        string Title { get; set; }

        bool IsLastNameFirst { get; set; }
    }
}
