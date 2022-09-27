using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Common;

namespace WebAPI.Models
{
    public sealed class OperationClaim:Entity
    {
        public string Name { get; set; }
        public OperationClaim()
        {
            
        }

        public OperationClaim(string id,string name):this()
        {
            Id=id;
            Name=name;
        }
    }
}