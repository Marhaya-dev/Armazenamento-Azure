using Dapper;
using System.Data;
using System.Collections.Generic;
using System;
using ProjetoA3.Repositorys.Interfaces;
using ProjetoA3.Models;
using FirebirdSql.Data.FirebirdClient;

namespace ProjetoA3.Repositorys
{
    public class ArquivoRepository : IArquivoRepository
    {
        private IFirebirdConnection _connectionFactory { get; }

        public ArquivoRepository(IFirebirdConnection dbConnection)
        {
            _connectionFactory = dbConnection;
        }

        public void Create(Arquivo arquivo)
        {
            using (IDbConnection _connection = _connectionFactory.GetConnection())
            {
                arquivo.Id = RetornaChaveMestra("GEN_ANEXOS");

                _connection.Open();
                _connection.Execute($@"
                    INSERT INTO anexos (

                            id,
                            data,
                            file_name

                    ) VALUES (

                            @{nameof(arquivo.Id)},
                            @{nameof(arquivo.Data)},
                            @{nameof(arquivo.File)}

                    )", arquivo);

                _connection.Close();
            }
        }


        public IEnumerable<Arquivo> GetList()
        {
            using (IDbConnection _connection = _connectionFactory.GetConnection())
            {
                _connection.Open();

                var arquivos = _connection.Query<Arquivo>($@"
                    SELECT
		   		            a.*,
                            a.file_name as File

		               FROM anexos a

		               ORDER BY data DESC

                ");

                _connection.Close();
                return arquivos;
            }
        }

        public Arquivo Get(int id)
        {
            using (IDbConnection _connection = _connectionFactory.GetConnection())
            {
                _connection.Open();

                var arquivo = _connection.QueryFirstOrDefault<Arquivo>($@"
                    SELECT
		   		            a.*,
                            a.file_name as File

		               FROM anexos a
                       WHERE a.id = @id

		               ORDER BY data DESC

                ", new { id });
                _connection.Close();

                return arquivo;
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection _connection = _connectionFactory.GetConnection())
            {
                _connection.Open();

                _connection.Execute($@"
                    DELETE FROM anexos 
                    WHERE id = @id
                    ", new { id });

                _connection.Close();
            }
        }

        public int RetornaChaveMestra(string NomeGenerator)
        {
            try
            {
                using (IDbConnection conn = _connectionFactory.GetConnection()) 
                {
                    conn.Open();

                    var qry = ($"select gen_id({NomeGenerator}, 1) from rdb$database");

                    var chave = conn.QueryFirstOrDefault<int>(qry);

                    conn.Close();

                    return chave;
                }
            }

            catch (FbException E)
            {
                throw new Exception(E.Message);
            }

            catch (Exception E)
            {
                throw new Exception($"Ocorreu um erro ao retonar chave mestra de nome {NomeGenerator}. Erro: {E.Message}");
            }
        }
    }
}
