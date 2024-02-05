using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_API.Dtos.CommentsDTO
{
    public class CommentRequest
    {
        public string nameActor { get; set; }
        public string Comment { get; set; }
        public DateTime? Created { get; set; }
        public int NewsId { get; set; }
    }
}
