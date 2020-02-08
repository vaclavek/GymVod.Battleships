using System;
using System.IO;
using System.Threading.Tasks;
using BlazorInputFile;

namespace GymVod.Battleships.Services
{
    public class FileService : IFileService
    {
        public async Task UploadAsync(IFileListEntry fileEntry, Guid fileGuid)
        {
            var path = GetPath(fileGuid);
            using (var ms = new MemoryStream())
            {
                await fileEntry.Data.CopyToAsync(ms);
                using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    ms.WriteTo(file);
                }
            }
        }

        public Task DeleteAsync(Guid fileGuid)
        {
            var path = GetPath(fileGuid);
            File.Delete(path);
            return Task.CompletedTask;
        }

        private string GetPath(Guid fileGuid)
        {
            return Path.Combine("Upload", $"{fileGuid}.dll"); ;
        }
    }
}