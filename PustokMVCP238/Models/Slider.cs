using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokMVC.Models
{
    public class Slider : BaseEntity
    {
        [Required]
        [StringLength(25)]
        public string Title1 { get; set; }
        [Required]
        [StringLength(30)]
        public string Title2 { get; set; }
        [Required]
        [StringLength(200)]
        public string Desc { get; set; }
        public string? RedirectUrl { get; set; }
        [Required]
        [StringLength(30)]
        public string RedirectUrlText { get; set; }
        [StringLength(150)]
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
