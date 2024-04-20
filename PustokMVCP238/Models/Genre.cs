using System.ComponentModel.DataAnnotations;

namespace PustokMVC.Models
{
    public class Genre : BaseEntity
    {
        [StringLength(75)]
        public string Name { get; set; }

        public List<Book>? Books { get; set; }
    }
}
