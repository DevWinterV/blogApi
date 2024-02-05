using WEB_API.Models;

namespace WEB_API.Dtos.CommentsDTO
{
    public class CommentResponse
    {
        public int IdComment { get; set; }
        public string nameActor { get; set; }
        public string Comment { get; set; }
        public DateTime? Created { get; set; }
        public int NewsId { get; set; }
        public bool isReply { get; set; }
        public List<Comments> commentsReplay { get; set; }
    }
}
