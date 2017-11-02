using System.ComponentModel.DataAnnotations.Schema;

namespace Figures.API.Entities
{
    using Figures.API.Models;
    using System.ComponentModel.DataAnnotations;

    public class Figure
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string Aka { get; set; }

        public Gender Gender { get; set; }

        public string Description { get; set; }
    }
}
