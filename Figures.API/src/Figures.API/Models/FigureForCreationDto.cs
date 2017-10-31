namespace Figures.API.Models
{
    using System.ComponentModel.DataAnnotations;

    public class FigureForCreationDto
    {
        [Required(ErrorMessage = "You should provide a name for the figure.")]
        [MaxLength(120)]
        public string Name { get; set; }

        public Gender Gender { get; set; }
    }
}
