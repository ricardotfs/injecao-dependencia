using Dapper;
using Gvp.Idea.Api.Model;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Data.SqlClient;

namespace Gvp.Idea.Api.DAO
{
    public class ZapSacDAO
    {
        private IConfiguration _configuracoes;

        public ZapSacDAO(IConfiguration config)
        {
            _configuracoes = config;
        }

        public SacMobileContato ConsultaContatoZapSac(int idContato)
        {
            using (SqlConnection conexao = new SqlConnection(_configuracoes.GetConnectionString("BaseZapSac")))
            {
                var sql =   "SELECT * FROM VIVO_ZAPSAC_CONTATO " +
                            "WHERE ID_CONTATO = @IdContato ";

                var param = new { IdContato = idContato };

                return conexao.QueryFirstOrDefault<SacMobileContato>(sql, param);
            }
        }

        public IEnumerable ConsultaContatoZapSac()
        {
            using (SqlConnection conexao = new SqlConnection(_configuracoes.GetConnectionString("BaseZapSac")))
            {
                var sql = "SELECT * FROM VIVO_ZAPSAC_CONTATO " ;

                return conexao.Query<SacMobileContato>(sql);
            }
        }

        public SacMobileConfig ConsultaConfigZapSac(int idc)
        {
            using (SqlConnection conexao = new SqlConnection(_configuracoes.GetConnectionString("BaseZapSac")))
            {
                var sql =   "SELECT * FROM VIVO_ZAPSAC_CONFIG " +
                            "WHERE IDC = @Idc ";

                var param = new { Idc = idc };

                return conexao.QueryFirstOrDefault<SacMobileConfig>(sql, param);
            }
        }

        public SacMobileTelefone ConsultaTelefoneZapSac(int idc)
        {
            using (SqlConnection conexao = new SqlConnection(_configuracoes.GetConnectionString("BaseZapSac")))
            {
                var sql =   "SELECT * FROM VIVO_ZAPSAC_TELEFONE " +
                            "WHERE IDC = @Idc ";

                var param = new { Idc = idc };

                return conexao.QueryFirstOrDefault<SacMobileTelefone>(sql, param);
            }
        }
    }
}