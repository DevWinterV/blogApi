using AutoMapper;
using WEB_API.Dtos.ImageNews;
using WEB_API.Models;
using WEB_API.Services.FileUpload;

namespace WEB_API.Services.ImageNews
{
    public class ImagenewService : IImagenewsService
    {

        private readonly IMapper _mapper;
        private readonly ContactContext _contactContext;
        private readonly FirebaseStorageService _firebaseStorageService;

        public ImagenewService(IMapper maper, ContactContext contactContext, FirebaseStorageService firebaseStorageService)
        {
            _mapper = maper;
            _contactContext = contactContext;
            _firebaseStorageService = firebaseStorageService;

        }

        public async Task<bool> AddImagesNews(List<ImageNewsCreate> listImage)
        {
            try{

                foreach (var image in listImage)
                {
                    var imageadd = new NewsImage
                    {
                        NewsId = image.IdNews,
                        ImageUrl = image.imageUrl
                    };
                    _contactContext.NewsImages.Add(imageadd);
                    await _contactContext.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<List<string>> UploadFiles(List<IFormFile> listFiles)
        {
            try
            {
                if (listFiles == null || listFiles.Count == 0)
                {
                    return null;
                }

                var downloadUrls = new List<string>();

                foreach (var file in listFiles)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Specify the remote path (folder) where you want to store the file in Firebase Storage
                    var remotePath = "uploads/" + file.FileName;

                    // Upload the file to Firebase Storage
                    var downloadUrl = await _firebaseStorageService.UploadFileAsync(filePath, remotePath);

                    // You can now use the 'downloadUrl' to access the uploaded file in Firebase Storage
                    downloadUrls.Add(downloadUrl);

                    // Clean up: delete the temporary file
                    System.IO.File.Delete(filePath);
                }
                return downloadUrls;
            }
            catch(Exception e)
            {
                return null;
            }
        }

    }
}
