using System;

namespace Gvp.Idea.Api.Model
{
    public class SacMobileContato
    {
        public int Id_Contato { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Chave_Acesso { get; set; }
        public DateTime Data_Visualizacao_Cliente { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Device_Token { get; set; }
        public string Origem { get; set; }
        public Boolean Autenticado { get; set; }
        public Boolean Valor_Custom { get; set; }
    }
}
