using System.ComponentModel.DataAnnotations;
using WEB_API.Models;

namespace WEB_API.Dtos.Post
{
    public class PostResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public ICollection<NewsImage> Images { get; set; }
    }
}
