using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletPlus.Api.Dtos
{
    public class RegisterRequestDto : LoginRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }
}
