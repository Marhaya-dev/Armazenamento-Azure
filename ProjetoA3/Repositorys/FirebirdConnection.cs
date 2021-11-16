using System.Data;
using ProjetoA3.Repositorys.Interfaces;
using FirebirdSql.Data.FirebirdClient;
using ProjetoA3.Settings;
using Microsoft.Extensions.Options;

namespace ProjetoA3.Repositorys
{
    public class FirebirdConnection : IFirebirdConnection
    {
        private readonly DataBaseConnection _dataBaseConnection;

        public FirebirdConnection(IOptionsSnapshot<DataBaseConnection> dataBaseConnection)
        {
            _dataBaseConnection = dataBaseConnection.Value;
        }

        public FbConnection GetConnection()
        {
            FbConnection conn = new FbConnection(_dataBaseConnection.ConnectionString);
            return conn;
        }
    }
}
