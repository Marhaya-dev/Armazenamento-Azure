using ProjetoA3.Repositorys.Interfaces;
using ProjetoA3.Services.Interfaces;
using System;

namespace ProjetoA3.UseCases
{
    public class DeleteArquivoCase
    {
        private IArquivoRepository _repo;
        private IAzureStorageService _serviceAzure;

        public DeleteArquivoCase(
            IArquivoRepository repo,
            IAzureStorageService serviceAzure
            ) {
            _repo = repo;
            _serviceAzure = serviceAzure;
        }

        public void Execute(int id)
        {
            var arquivo = _repo.Get(id);

            if (arquivo == null)
                throw new Exception("O arquivo com o id "+ arquivo.Id + " não foi encontrado");

            _serviceAzure.DeleteFile(arquivo.File);

            _repo.Delete(id);
        }
    }
}
