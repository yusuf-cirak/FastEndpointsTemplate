using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Repositories;
using WebAPI.Models;

namespace WebAPI.Endpoints
{
    public sealed class OperationClaimBusinessRules
    {
        private readonly IOperationClaimRepository _repository;

        public OperationClaimBusinessRules(IOperationClaimRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationClaim> OperationClaimMustExistBeforeUpdated(string id)
        {
            OperationClaim operationClaim=await _repository.GetAsync(e=>e.Id==id);
            if (operationClaim==null)
            {
                throw new Exception("You can not update because operation claim does not exist");
            }

            return operationClaim;
        }

        public async Task OperationClaimNameCannotBeDuplicatedBeforeCreatedOrUpdated(string name)
        {
            if (await _repository.Query().FirstOrDefaultAsync(e=>e.Name==name)!=null)
            {
                throw new Exception("You can not create because a operation claim already exists with that name");
            }
        }

        public async Task<OperationClaim> OperationClaimMustExistBeforeDeleted(string id)
        {
            OperationClaim operationClaim=await _repository.GetAsync(e=>e.Id==id);
             if (operationClaim==null)
            {
                throw new Exception("You can not delete because a operation claim does not exist");
            }

            return operationClaim;
        }
    }
}