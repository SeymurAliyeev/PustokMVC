using System.ComponentModel.DataAnnotations;

namespace PustokMVC.Models
{
    public class Author : BaseEntity
    {
        [StringLength(50)]
        public string Fullname { get; set; }

        public List<Book> Books { get; set; }

    }
}
