using System.IO;
using System.Threading.Tasks;
using BlazorInputFile;

namespace GymVod.Battleships.Services
{
    public class FileUploadService : IFileUploadService
    {
        //private readonly IWebHostEnvironment _environment;

        public async Task UploadAsync(IFileListEntry fileEntry)
        {
            var path = Path.Combine("Upload", fileEntry.Name);
            var ms = new MemoryStream();
            await fileEntry.Data.CopyToAsync(ms);
            using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                ms.WriteTo(file);
            }
        }

    }
}