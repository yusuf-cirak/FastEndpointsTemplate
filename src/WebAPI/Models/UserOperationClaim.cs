using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Common;

namespace WebAPI.Models
{
    public class UserOperationClaim : Entity
    {
        public string UserId { get; set; }
        public string OperationClaimId { get; set; }

        public virtual User User { get; set; }
        public virtual OperationClaim OperationClaim { get; set; }

        public UserOperationClaim()
        {
        }

        public UserOperationClaim(string id, string userId, string operationClaimId):this()
        {
            Id=id;
            UserId = userId;
            OperationClaimId = operationClaimId;
        }
    }
}