using System.ComponentModel.DataAnnotations;

namespace PustokMVC.Areas.Admin.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
