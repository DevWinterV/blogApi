using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_API.Models
{
    [Table("Comments")]

    public class Comments
    {
        [Key]
        public int IdComment { get; set; }
        [Required]
        [MaxLength(100)]  
        public string nameActor {  get; set; }
        [Required]
        [MaxLength(255)]
        public string Comment {  get; set; }
        [Required]
        public DateTime? Created { get; set; }
        // Foreign key to associate the image with a news item
        [ForeignKey("NewsId")]
        public int NewsId { get; set; }
        public News News { get; set; }
        
        public bool isReply { get; set; }
        public ICollection<Comments>? commentsReplay  { get; set; }
    }
}
