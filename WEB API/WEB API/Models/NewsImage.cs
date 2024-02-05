using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB_API.Models
{
    [Table("NewsImages")]
    public class NewsImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        // Foreign key to associate the image with a news item
        [ForeignKey("NewsId")]
        public int NewsId { get; set; }

        public News News { get; set; }
    }
}
