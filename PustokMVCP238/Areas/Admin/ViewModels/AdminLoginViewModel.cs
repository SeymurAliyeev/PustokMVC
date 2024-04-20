using System.ComponentModel.DataAnnotations;

namespace PustokMVC.Areas.Admin.ViewModels
{
    public class AdminLoginViewModel
    {
        [DataType(DataType.Text)]
        [StringLength(20)]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
