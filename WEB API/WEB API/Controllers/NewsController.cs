using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_API.Dtos.Contact;
using WEB_API.Dtos.ImageNews;
using WEB_API.Dtos.Post;
using WEB_API.Models;
using WEB_API.Services.ImageNews;
using WEB_API.Services.PostService;

namespace WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NewsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IImagenewsService _IimagenewsService;

        public NewsController(IPostService postService, IImagenewsService IimagenewsService) {
            _postService = postService;
            _IimagenewsService = IimagenewsService;
        }

        [HttpGet]
        public async Task<ActionResult<ServerBaseReponse<List<PostResponse>>>> GetAllPost(int cursor = 0, int limit = 10)
        {
            var listpost = await _postService.getAllPost(cursor, limit);
            return Ok(listpost);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ServerBaseReponse<PostResponse>>> GetPostById(int Id)
        {
            var post = await _postService.getpostById(Id);
            return Ok(post);
        }


        [HttpPost]
        [Route("remove")] // Remove "api/[controller]/" from the route
        public async Task<ActionResult<ServerBaseReponse<PostResponse>>> DeletePost(int Id)
        {
            var post = await _postService.deletePost(Id);
            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<ServerBaseReponse<int>>> Createpost([FromForm] PostRequest request, List<IFormFile>? listfile)
        {
            var results = new ServerBaseReponse<int>();
            var listStringimage = new List<string>();
            if (listfile.Count > 0)
            {
                listStringimage  = await _IimagenewsService.UploadFiles(listfile);
            }
            results = await _postService.createPost(request);
            var listImagenew = new List<ImageNewsCreate>();
            if(listStringimage != null)
                {
                    if (results.Success == true)
                    {
                        var idnews = results.Data;
                        foreach (var item in listStringimage)
                        {
                            var imagenew = new ImageNewsCreate
                            {
                                IdNews = idnews,
                                imageUrl = item
                            };
                            listImagenew.Add(imagenew);
                        }
                        await _IimagenewsService.AddImagesNews(listImagenew);
                    }
                    return Ok(results);
                }
            results.Message = "OK";
            results.Success = true;
            return Ok(results);
        }
 
    }
}
