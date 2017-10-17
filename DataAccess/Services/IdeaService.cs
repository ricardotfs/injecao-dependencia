using DataAccess.Entities;
using DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class IdeaService : IIdeaService
    {
        IUnitOfWork _unitOfWork;
        public IdeaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Int64> SalvarJsonSes(AWSSes model)
        {
            return await _unitOfWork.AWSSesRepository.SalvarJsonSes(model);
        }
    }
}
