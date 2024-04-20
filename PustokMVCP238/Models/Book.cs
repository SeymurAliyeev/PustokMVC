using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokMVC.Models
{
    public class Book : BaseEntity
    {
        public int GenreId { get; set; }
        public int AuthorId { get; set; }

        [StringLength(60)]
        public string Title { get; set; }
        [StringLength(300)]
        public string Desc { get; set; }
        public string BookCode { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public double DiscountPercent { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsNew { get; set; }
        public bool IsBestSeller { get; set; }
        public bool IsAvailable { get; set; } 
        public int StockCount { get; set; }

        public Genre? Genre { get; set; }
        public Author? Author { get; set; }

        public List<BookImage>? BookImages { get; set; }
        [NotMapped]
        public IFormFile? PosterImageFile { get; set; }
        [NotMapped]
        public IFormFile? HoverImageFile { get; set; }
        [NotMapped]
        public List<IFormFile>? ImageFiles { get; set; }

        [NotMapped]
        public List<int>? BookImageIds { get; set; }
    }
}
