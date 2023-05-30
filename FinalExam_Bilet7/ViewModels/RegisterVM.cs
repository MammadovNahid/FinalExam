using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace FinalExam_Bilet7.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        [RegularExpression("^[a-zA-Z]+$")]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        [RegularExpression("^[a-zA-Z]+$")]
        public string Surname { get; set; }

        [Required]
        public string Username { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
