using System;

namespace DataAccess.Entities
{
    public class Cliente
    {

        public int Id { get; set; }
        public int Idc { get; set; }
        public string Nome { get; set; }
        public string Razao { get; set; }
        public string Email { get; set; }
        public int Tipo { get; set; }
        public TipoPessoa Pessoa { get; set; }
        public DateTime DataProxAtualizacao { get; set; }
    }
}
