using System;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ProjetoA3.Services.Interfaces;
using ProjetoA3.Settings;

namespace ProjetoA3.Services
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly AzureStorageSettings _storageSettings;

        public AzureStorageService(IOptionsSnapshot<AzureStorageSettings> settings)
        {
            _storageSettings = settings.Value;
        }

        public string UploadFile(IFormFile file)
        {
            var containerClient = new BlobContainerClient(
                _storageSettings.StorageConnectionString, 
                _storageSettings.Container
            );

            try
            {
                var blobClient = containerClient.GetBlobClient(GenerateSafeFileName(file.FileName));

                using (var fileStream = file.OpenReadStream())
                {
                    var response = blobClient.Upload(fileStream);

                    return blobClient.Uri.Segments[2];
                }
            }

            catch (Exception e)
            {
                throw new Exception(e + "Serviço de armazenamento indisponível.");
            }
        }

        private string GenerateSafeFileName(string fileName)
        {
            var extVec = fileName.Split('.');
            var extIndex = extVec.Length - 1;
            var extension = extVec[extIndex];

            return Guid.NewGuid().ToString() + '.' + extension;
        }

        public bool DeleteFile(string fileName)
        {
            try
            {
                var containerClient = new BlobContainerClient(
                    _storageSettings.StorageConnectionString,
                    _storageSettings.Container
                );

                var blobClient = containerClient.GetBlobClient(fileName);
                    
                return blobClient.DeleteIfExists().Value;
            }

            catch (Exception e)
            {
                throw new Exception(e + "Serviço de armazenamento indisponível.");
            }
        }
    }
}
