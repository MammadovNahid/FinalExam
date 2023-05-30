using System.ComponentModel.DataAnnotations;

namespace FinalExam_Bilet7.ViewModels
{
    public class LoginVM
    {
        public string UsernameorEmail { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemember { get; set; }

    }
}
