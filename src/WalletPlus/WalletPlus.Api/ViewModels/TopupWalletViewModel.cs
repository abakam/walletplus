using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WalletPlus.Api.ViewModels
{
    public class TopupWalletViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        public decimal Amount { get; set; }
    }
}
