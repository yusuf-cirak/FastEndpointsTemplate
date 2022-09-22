using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers.Token
{
    public sealed class AccessToken
    {
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}