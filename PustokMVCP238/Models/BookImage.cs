    namespace PustokMVC.Models
{
    public class BookImage : BaseEntity
    {
        public int BookId { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsPoster { get; set; }

        public Book Book { get; set; }

    }
}
