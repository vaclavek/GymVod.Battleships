using System;
using System.Threading.Tasks;
using BlazorInputFile;

namespace GymVod.Battleships.Services
{
    public interface IFileUploadService
    {
        Task UploadAsync(IFileListEntry file, Guid fileGuid);
    }
}
