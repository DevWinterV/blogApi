using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_API.Models
{
    [Table("News")]
    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)] // Adjust the maximum length as needed
        public string? Title { get; set; }

        [Required]
        public string? Content { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        // Add a collection of images
        public ICollection<NewsImage>? Images { get; set; }
        public ICollection<Comments>? CommentsNews { get; set; }

    }


}
