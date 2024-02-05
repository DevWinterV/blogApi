using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using WEB_API.Dtos.CommentsDTO;
using WEB_API.Dtos.Contact;
using WEB_API.Models;

namespace WEB_API.Services.CommentsService
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly ContactContext _contactContext;
        public CommentService(IMapper maper, ContactContext contactContext)
        {
            _mapper = maper;
            _contactContext = contactContext;
        }

        public async Task<ServerBaseReponse<bool>> CreateComment(CommentRequest request)
        {
            var results = new ServerBaseReponse<bool>();
            try
            {
                results.Data = false;
                results.Success = false;
                results.Message = "NotOK";
                var newComment = _mapper.Map<Comments>(request);
                _contactContext.Comments.Add(newComment);
                var success = await _contactContext.SaveChangesAsync();
                if (success == 1)
                {
                    results.Data = true;
                    results.Success = true;
                    results.Message = "OK";
                    return results;
                }
            }
            catch(Exception ex) {
                results.Message = ex.Message;

            }
            return results;
        }

        public async Task<ServerBaseReponse<List<CommentResponse>>> GetAllCommentByNewsId(int newsId)
        {
            var results = new ServerBaseReponse<List<CommentResponse>>();
            try
            {
                results.Data = null;
                results.Success = false;
                results.Message = "NotOK";
                var listComment = await _contactContext.Comments.Where(x => x.NewsId.Equals(newsId)).ToListAsync();
                results.Data = listComment.Select(x => _mapper.Map<CommentResponse>(x)).ToList();
                results.Success &= listComment.Any();
                results.Message = "OK";
                return results;
            }
            catch (Exception ex) {
                results.Message = ex.Message;
            }
            return results;
        }

        public Task<ServerBaseReponse<bool>> RemoveComment(int idcomment)
        {
            throw new NotImplementedException();
        }

        public async Task<ServerBaseReponse<bool>> ReplayComment(CommentReplayRequest request)
        {
            var results = new ServerBaseReponse<bool>();
            try
            {
                var checkComment = await _contactContext.Comments.FirstOrDefaultAsync(x => x.IdComment.Equals(request.CommnetIdReplay));
                if(checkComment == null)
                {
                    results.Data = false;
                    results.Success = false;
                    results.Message = "CommentNotFound";
                    return results;
                }
                var newcomment = new Comments
                {
                    Comment = request.Comment,
                    nameActor = request.nameActor,
                    Created = DateTime.Now,
                    NewsId = request.NewsId,
                    isReply = true
                };
                if(checkComment.commentsReplay == null)
                {
                    checkComment.commentsReplay = new List<Comments>();
                }
                _contactContext.Comments.Add(newcomment);
                await _contactContext.SaveChangesAsync();
                checkComment.commentsReplay.Add(newcomment);
                _contactContext.Comments.Update(checkComment);
                await _contactContext.SaveChangesAsync();
                results.Data = true;
                results.Success = true;
                results.Message = "OK";
            }
            catch (Exception ex)
            {
                results.Data = false;
                results.Success = false;
                results.Message = ex.Message;
            }
            return results;
        }

        public Task<ServerBaseReponse<bool>> UpdataeComment(CommentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
