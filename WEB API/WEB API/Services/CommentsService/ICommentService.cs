using WEB_API.Dtos.CommentsDTO;
using WEB_API.Models;

namespace WEB_API.Services.CommentsService
{
    public interface ICommentService
    {
        Task<ServerBaseReponse<List<CommentResponse>>> GetAllCommentByNewsId(int newsId);
        Task<ServerBaseReponse<bool>> UpdataeComment(CommentRequest request);
        Task<ServerBaseReponse<bool>> CreateComment(CommentRequest contacts);
        Task<ServerBaseReponse<bool>> ReplayComment(CommentReplayRequest contacts);

        Task<ServerBaseReponse<bool>> RemoveComment(int idcomment);
    }
}
