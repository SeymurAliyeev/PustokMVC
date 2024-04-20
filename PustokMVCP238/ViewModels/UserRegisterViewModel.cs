using System.ComponentModel.DataAnnotations;

namespace PustokMVC.ViewModels
{
    public class UserRegisterViewModel
    {
        [DataType(DataType.Text)]
        public string Fullname { get; set; }
        [DataType(DataType.Text)]
        public string Username { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
