using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ProjetoA3.Services.Interfaces
{
    public interface IAzureStorageService
    {
        string UploadFile(IFormFile file);
        bool DeleteFile(string fileName);
    }
}
