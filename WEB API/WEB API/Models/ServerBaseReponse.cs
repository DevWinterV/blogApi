using WEB_API.Dtos.Post;

namespace WEB_API.Models
{
    public class ServerBaseReponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public Paging paging { get; set; } = new Paging
        {
            cursor = 0,
            hasNext = false,
            nextCursor = 0,
            page = 0,
            total = 0,
            limit = 0
        };
    }
}
