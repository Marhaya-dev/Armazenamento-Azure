using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ProjetoA3.Services.Interfaces;
using ProjetoA3.Settings;
using ProjetoA3.Models;
using ProjetoA3.Repositorys.Interfaces;

namespace ProjetoA3.UseCases
{
    public class UpdateArquivoCase
    {
        private readonly IArquivoRepository _arquivoRepository;
        private readonly IAzureStorageService _storageService;
        private readonly AzureStorageSettings _storageSettings;

        public UpdateArquivoCase(
            IArquivoRepository repo, 
            IAzureStorageService storageService,
            IOptionsSnapshot<AzureStorageSettings> storageSettings
        ) {
            _arquivoRepository = repo;
            _storageService = storageService;
            _storageSettings = storageSettings.Value;
        }

        public Arquivo Execute(IFormFile file)
        {          
            if (file == null)
                throw new NullReferenceException("Nothing to process");

            var fileName = _storageService.UploadFile(file);

            var anexo = new Arquivo
            {
                Data = DateTime.Now,
                File = fileName
            };

            _arquivoRepository.Create(anexo);

            anexo.File = _storageSettings.LinkFile + fileName;

            return anexo;
        }
    }
}
