using Microsoft.Extensions.Options;
using ProjetoA3.Models;
using ProjetoA3.Repositorys.Interfaces;
using ProjetoA3.Settings;
using System.Collections.Generic;

namespace ProjetoA3.UseCases
{
    public class ListArquivoCase
    {
        private IArquivoRepository _repo;
        private readonly AzureStorageSettings _storageSettings;

        public ListArquivoCase(IArquivoRepository repo
            , IOptionsSnapshot<AzureStorageSettings> storageSettings)
        {
            _repo = repo;
            _storageSettings = storageSettings.Value;
        }

        public IEnumerable<Arquivo> Execute()
        {
            var anexos = _repo.GetList();
            
            foreach (var file in anexos)
            {
                file.File = _storageSettings.LinkFile + file.File;
            }

            return anexos;
        }
    }
}
