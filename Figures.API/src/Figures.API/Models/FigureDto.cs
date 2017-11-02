﻿namespace Figures.API.Models
{
    using System.ComponentModel.DataAnnotations;

    public class FigureDto
    {
        [Required(ErrorMessage = "Id must be set for a new figure!")]
        public int Id { get; set; }

        [Required(ErrorMessage = "You should provide a name for the figure.")]
        [MaxLength(40)]
        public string FirstName { get; set; }

        [MaxLength(40)]
        public string LastName { get; set; }

        public string Title { get; set; }

        public string Aka { get; set; }

        public Gender Gender { get; set; }

        public string FullName { get; set; }
    }
}
