using Dapper;
using DataAccess.Entities;
using DataAccess.Infrastructure;
using DataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class IdeaRepository : GenericRepository<AWSSes>, IIdeaRepository
    {
        IConnectionFactory _connectionFactory;

        public IdeaRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<Int64> SalvarJsonSes(AWSSes model)
        {
            try
            {
                var cnn = _connectionFactory.GetConnection;
                var query = String.Empty;

                query = $"INSERT INTO VIVO_INTEGRACAO_SES (JSON, MESSAGE_ID, LIDO) VALUES (@JSON, @MESSAGE_ID, @LIDO) ";
                await SqlMapper.ExecuteAsync(cnn, query, model);

                var res = new AWSS3Function().LoadEmlS3(model.Message_Id);

                return await SalvarEmail(res);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<EmailIdea>> Retorno (EmailIdea email)
        {
            try
            {
                var cnn = _connectionFactory.GetConnection;
                var query = String.Empty;

                query = $"SELECT TOP 1 * FROM VIVO_EMAIL (NOLOCK) WHERE  ID = @ID";
                var list = await SqlMapper.QueryAsync<EmailIdea>(cnn, query, email);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<Int64> SalvarEmail (EmailIdea email)
        {
            try
            {
                var cnn = _connectionFactory.GetConnection;
                var query = String.Empty;

                query = $" DECLARE @CONT INT " +
                        $" DECLARE @REM VARCHAR(MAX) " +
                        $" SET @REM = '{email.Remetente}' " +
                        $" SET @CONT = (SELECT [DBO].[UFN_COUNTCHAR] (@REM, '@'))" +
                        $" IF(@CONT >= 2) " +
                        $"      SET @REM = (SELECT [DBO].[UDF_STRIPHTML](@REM))" +
                        $" INSERT INTO VIVO_EMAIL (IDC, DATA, DATA_INCLUSAO, DE, PARA, CC, CCO, ASSUNTO, TEXTO, PRIORIDADE, STATUS, TIPO, IDREGISTRO, EMAIL_CONTA, ID_EMAIL_CONTA, OPERADOR, DEPTO, ID_PRODUTO, ID_TOPICO, IDMOTIVO, UNIQUEID)" +
                        $" VALUES(" +
                        $" '{email.Idc}', '{email.DataRecebimento:yyyy-MM-dd HH:mm:ss}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', @REM, '{email.Destinatario}', '{email.Copia}', '{email.CopiaOculta}', '{email.Assunto}', '{email.Mensagem.Replace("https://e-aj.my.com/", String.Empty)}', " +
                        $" '{(int)email.Prioridade}', '{email.Status}', '{email.Tipo}', '{email.IdRegistro}', '{email.EmailConta}', '{email.IdEmailConta}','{email.Proprietario}','{email.Depto}','{email.IdProduto}','{email.IdAssunto}','{email.IdMotivo}','{email.GuidEmail}') ";

                return await SqlMapper.ExecuteAsync(cnn, query);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
