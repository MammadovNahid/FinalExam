using FinalExam_Bilet7.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalExam_Bilet7.Models
{
    public class OurTeam:BaseEntity
    {
        public string Description { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        [RegularExpression("^[a-zA-Z]+$",ErrorMessage ="Error.Only Letters...")]
        public string Name { get; set; }
		[Required]
		[MinLength(3)]
		[MaxLength(25)]
		[RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Error.Only Letters...")]
		public string Surname { get; set; }
        [Required]
        public string Profession { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
