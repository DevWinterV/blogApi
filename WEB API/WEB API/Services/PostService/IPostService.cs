using WEB_API.Dtos.Post;
using WEB_API.Models;

namespace WEB_API.Services.PostService
{
    public interface IPostService
    {
        Task<ServerBaseReponse<List<PostResponse>>> getAllPost(int cusror, int limit);
        Task<ServerBaseReponse<int>> createPost(PostRequest reques);
        Task<ServerBaseReponse<bool>> deletePost(int id);
        
        Task<ServerBaseReponse<PostResponse>> getpostById(int id);

    }
}
