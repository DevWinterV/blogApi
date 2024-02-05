namespace WEB_API.Dtos.CommentsDTO
{
    public class CommentReplayRequest
    {

        public string nameActor { get; set; }
        public string Comment { get; set; }
        public DateTime? Created { get; set; }
        public int NewsId { get; set; }
        public int CommnetIdReplay { get; set; }

    }
}
