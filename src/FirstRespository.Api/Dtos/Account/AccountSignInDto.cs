using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstRespository.Api.Dtos.Account
{
    public sealed class AccountSignInDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
