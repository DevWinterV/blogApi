using AutoMapper;
using WEB_API.Dtos.CommentsDTO;
using WEB_API.Dtos.Contact;
using WEB_API.Dtos.Post;
using WEB_API.Models;

namespace WEB_API
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile() {
            CreateMap<Contacts, ContactResponse>();
            CreateMap<ContactRequest, Contacts>();
            CreateMap<News, PostResponse>()
                  .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(image => new NewsImage
                  {
                      Id = image.Id,
                      ImageUrl = image.ImageUrl,
                      NewsId = image.NewsId,
                  })));
            CreateMap<PostRequest, News>();
            CreateMap<Comments, CommentResponse>()
                .ForMember(dest => dest.commentsReplay, opt => opt.MapFrom(src => src.commentsReplay.Select(cmt => new Comments
                {
                    IdComment = cmt.IdComment,
                    nameActor = cmt.nameActor,
                    Comment = cmt.Comment,
                    Created = cmt.Created,
                    commentsReplay = cmt.commentsReplay
                })));
            CreateMap<CommentRequest, Comments>();

        }
    }
}
