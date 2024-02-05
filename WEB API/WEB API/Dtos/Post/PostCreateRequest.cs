namespace WEB_API.Dtos.Post
{
    public class PostCreateRequest
    {
        public PostRequest postRequest {  get; set; }   
        public List<IFormFile> files { get; set; }
    }
}
