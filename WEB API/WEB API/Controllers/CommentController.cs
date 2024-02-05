using Google.Apis.Upload;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_API.Dtos.CommentsDTO;
using WEB_API.Models;
using WEB_API.Services.AuthService;
using WEB_API.Services.CommentsService;

namespace WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _iCommentService;
        private readonly ILogger<ContactController> _logger;

        public CommentController(ILogger<ContactController> logger, ICommentService iCommentService)
        {
            _iCommentService = iCommentService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<ActionResult<ServerBaseReponse<bool>>> CreateNewcomment(CommentRequest request)
        {
            var results = await _iCommentService.CreateComment(request);
            return results;
        }
        [HttpGet]
        public async Task<ActionResult<ServerBaseReponse<List<CommentResponse>>>> getAllCommentById(int newsId)
        {
            var results = await _iCommentService.GetAllCommentByNewsId(newsId);
            return results;
        }
        [HttpPost]
        [Route("replaycomment")]
        public async Task<ActionResult<ServerBaseReponse<bool>>> ReplayComment(CommentReplayRequest request)
        {
            var results = await _iCommentService.ReplayComment(request);
            return results;
        }
    }
}
