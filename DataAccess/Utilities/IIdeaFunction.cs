using Chilkat;
using DataAccess.Entities;
using System.Collections.Generic;

namespace DataAccess.Utilities
{
    public interface IIdeaFunction
    {
        TipoPrioridade ConvertPrioridade(string prioridade);
        List<Cliente> TratarDestinatarios(Email email);
    }
}
