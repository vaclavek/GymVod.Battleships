using System;
using System.IO;
using System.Threading.Tasks;
using BlazorInputFile;

namespace GymVod.Battleships.Services
{
    public class FileUploadService : IFileUploadService
    {
        public async Task UploadAsync(IFileListEntry fileEntry, Guid fileGuid)
        {
            var path = Path.Combine("Upload", $"{fileGuid}.dll");
            var ms = new MemoryStream();
            await fileEntry.Data.CopyToAsync(ms);
            using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                ms.WriteTo(file);
            }
        }
    }
}