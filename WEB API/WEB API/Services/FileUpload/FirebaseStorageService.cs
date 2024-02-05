using System.IO;
using System.Threading.Tasks;
using Firebase.Storage;

namespace WEB_API.Services.FileUpload
{

    public class FirebaseStorageService
    {
        private readonly FirebaseStorage _storage;

        public FirebaseStorageService(string firebaseStorageBucket)
        {
            _storage = new FirebaseStorage(firebaseStorageBucket);
        }

        public async Task<string> UploadFileAsync(string filePath, string remotePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open))
            {
                var task = await _storage.Child(remotePath)
                                          .PutAsync(stream);

                return task;
            }
        }
    }

}
