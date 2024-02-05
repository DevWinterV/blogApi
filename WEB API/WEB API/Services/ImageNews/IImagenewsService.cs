using WEB_API.Dtos.ImageNews;

namespace WEB_API.Services.ImageNews
{
    public interface IImagenewsService
    {
        Task<List<string>> UploadFiles(List<IFormFile> listFiles);
        Task<bool> AddImagesNews(List<ImageNewsCreate> listImage);
    }
}
