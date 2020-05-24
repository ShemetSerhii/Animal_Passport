using System.Threading.Tasks;
using AnimalPassport.DataAccess.Blob.Models;

namespace AnimalPassport.DataAccess.Blob.Interfaces
{
    public interface IBlobManager
    {
        Task<FileModel> DownloadFileAsync(string filePath);

        Task UploadFileAsync(FileModel file);

        Task DeleteFileAsync(string filePath);

        Task<bool> ExistsAsync(string filePath);
    }
}