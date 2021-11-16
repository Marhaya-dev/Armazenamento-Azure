using ProjetoA3.Models;
using System.Collections.Generic;

namespace ProjetoA3.Repositorys.Interfaces
{
    public interface IArquivoRepository
    {
        void Create(Arquivo arquivo);
        IEnumerable<Arquivo> GetList();
        Arquivo Get(int id);
        void Delete(int id);
    }
}
