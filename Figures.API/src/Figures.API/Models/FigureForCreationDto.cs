namespace Figures.API.Models
{
    using System.ComponentModel.DataAnnotations;

    public class FigureForCreationDto
    {
        [Required]
        public FigureType FigureType { get; set; }

        [Required(ErrorMessage = "You should provide at least a First name for the figure.")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string MiddleName { get; set; }

        [MaxLength(152)]
        public string UniquelyDisplayedFullName { get; set; }

        [MaxLength(25)]
        public string Title { get; set; }

        [MaxLength(50)]
        public string Alias { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsLastNameFirst { get; set; }

        public string CalculatedFullName { get; set; }
    }
}
