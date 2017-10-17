using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class EmailIdea
    {
        public string GuidEmail { get; set; }
        public Int64 Id { get; set; }
        public int Idc { get; set; }
        public DateTime DataRecebimento { get; set; }
        public Cliente Remetente { get; set; }
        public List<Cliente> Destinatario { get; set; }
        public List<Cliente> Copia { get; set; }
        public List<Cliente> CopiaOculta { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public TipoPrioridade Prioridade { get; set; }
        public int Status { get; set; }
        public int Tipo { get; set; }
        public int IdRegistro { get; set; }
        public int RootId { get; set; }
        public int IdEmailConta { get; set; }
        public int Proprietario { get; set; }
        public string EmailConta { get; set; }
        public int IdOcorrencia { get; set; }
        public bool TemAnexo { get; set; }
        public string IdProduto { get; set; }
        public string IdAssunto { get; set; }
        public string IdMotivo { get; set; }
        public string Depto { get; set; }
        public bool AtivaRegraEmail { get; set; }
        public List<Anexo> Anexos { get; set; }
        public List<AnexoRelacionado> AnexosRelacionado { get; set; }
}
}
