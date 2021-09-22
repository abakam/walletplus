using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletPlus.Api.Dtos
{
    public class TopupWalletRequestDto
    {
        public string Email { get; set; }
        public decimal Amount { get; set; }
    }
}
