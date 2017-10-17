using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IIdeaRepository : IGenericRepository<AWSSes>
    {
        Task<Int64> SalvarJsonSes(AWSSes model);
        Task<IEnumerable<EmailIdea>> Retorno(EmailIdea email);
    }
}
