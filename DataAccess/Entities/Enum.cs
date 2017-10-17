using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public enum TipoPessoa
    {
        NaoInformado = 0,
        Contato = 1,
        Conta = 2,
        Lead = 3
    }

    public enum TipoPrioridade
    {
        Baixa = 1,
        Normal = 2,
        Alta = 3
    }
}
