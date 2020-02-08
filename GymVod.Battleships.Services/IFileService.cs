using System;
using System.Threading.Tasks;
using BlazorInputFile;

namespace GymVod.Battleships.Services
{
    public interface IFileService
    {
        Task UploadAsync(IFileListEntry file, Guid fileGuid);
        Task DeleteAsync(Guid fileGuid);
    }
}
