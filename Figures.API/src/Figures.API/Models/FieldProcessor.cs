using Figures.API.Entities;

namespace Figures.API.Models
{
    using System.Text;

    internal static class FieldProcessor
    {
        // TODO: Create interface for FigureDto, remove code duplication.
        internal static string CalculateFullName(FigureDto figure)
        {
            if (figure.UniquelyDisplayedFullName != string.Empty)
            {
                return figure.UniquelyDisplayedFullName;
            }

            StringBuilder fullName = new StringBuilder();

            if (figure.Title != string.Empty)
            {
                fullName.Append(figure.Title);
                fullName.Append(" ");
            }

            if (figure.IsLastNameFirst && figure.LastName != string.Empty)
            {
                fullName.Append(figure.LastName);
                fullName.Append(" ");

                if (figure.MiddleName != string.Empty)
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

                if (figure.MiddleName != string.Empty)
                {
                    fullName.Append(figure.MiddleName);
                    fullName.Append(" ");
                }

                if (figure.IsLastNameFirst && figure.LastName != string.Empty)
                {
                    fullName.Append(figure.LastName);
                    fullName.Append(" ");
                }
            }

            return fullName.ToString().Trim();
        }

        // TODO: Create interface for FigureDto, remove code duplication.
        internal static string CalculateFullName(FigureForCreationDto figure)
        {
            if (figure.UniquelyDisplayedFullName != string.Empty)
            {
                return figure.UniquelyDisplayedFullName;
            }

            StringBuilder fullName = new StringBuilder();

            if (figure.Title != string.Empty)
            {
                fullName.Append(figure.Title);
                fullName.Append(" ");
            }

            if (figure.IsLastNameFirst && figure.LastName != string.Empty)
            {
                fullName.Append(figure.LastName);
                fullName.Append(" ");

                if (figure.MiddleName != string.Empty)
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

                if (figure.MiddleName != string.Empty)
                {
                    fullName.Append(figure.MiddleName);
                    fullName.Append(" ");
                }

                if (figure.IsLastNameFirst && figure.LastName != string.Empty)
                {
                    fullName.Append(figure.LastName);
                    fullName.Append(" ");
                }
            }

            return fullName.ToString().Trim();
        }

        // TODO: Create interface for FigureDto, remove code duplication.
        internal static string CalculateFullName(FigureForUpdateDto figure)
        {
            if (figure.UniquelyDisplayedFullName != string.Empty)
            {
                return figure.UniquelyDisplayedFullName;
            }

            StringBuilder fullName = new StringBuilder();

            if (figure.Title != string.Empty)
            {
                fullName.Append(figure.Title);
                fullName.Append(" ");
            }

            if (figure.IsLastNameFirst && figure.LastName != string.Empty)
            {
                fullName.Append(figure.LastName);
                fullName.Append(" ");

                if (figure.MiddleName != string.Empty)
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

                if (figure.MiddleName != string.Empty)
                {
                    fullName.Append(figure.MiddleName);
                    fullName.Append(" ");
                }

                if (figure.IsLastNameFirst && figure.LastName != string.Empty)
                {
                    fullName.Append(figure.LastName);
                    fullName.Append(" ");
                }
            }

            return fullName.ToString().Trim();
        }

        // TODO: Create interface for FigureDto, remove code duplication.
        internal static string CalculateFullName(Figure figure)
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
