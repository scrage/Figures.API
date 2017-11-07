namespace Figures.API.Models
{
    using Figures.API.Entities;
    using System.Text;

    internal static class FieldProcessor
    {
        internal static string CalculateFullName(IFigureDto figure)
        {
            if (!string.IsNullOrEmpty(figure.UniquelyDisplayedFullName))
            {
                return figure.UniquelyDisplayedFullName;
            }

            StringBuilder fullName = new StringBuilder();

            if (!string.IsNullOrEmpty(figure.Title))
            {
                fullName.Append(figure.Title);
                fullName.Append(" ");
            }

            if (figure.IsLastNameFirst && !string.IsNullOrEmpty(figure.LastName))
            {
                fullName.Append(figure.LastName);
                fullName.Append(" ");

                if (!string.IsNullOrEmpty(figure.MiddleName))
                {
                    fullName.Append(figure.MiddleName);
                    fullName.Append(" ");
                }

                fullName.Append(figure.FirstName);
                fullName.Append(" ");
            }
            else
            {
                fullName.Append(figure.FirstName);
                fullName.Append(" ");

                if (!string.IsNullOrEmpty(figure.MiddleName))
                {
                    fullName.Append(figure.MiddleName);
                    fullName.Append(" ");
                }

                if (!string.IsNullOrEmpty(figure.LastName))
                {
                    fullName.Append(figure.LastName);
                    fullName.Append(" ");
                }
            }

            return fullName.ToString().Trim();
        }
    }
}
