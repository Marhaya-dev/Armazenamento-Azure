using System.Data;
using FirebirdSql.Data.FirebirdClient;

namespace ProjetoA3.Repositorys.Interfaces
{
    public interface IFirebirdConnection
    {
        FbConnection GetConnection();
    }
}
