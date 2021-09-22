using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalletPlus.Api.Models.Common;
using WalletPlus.Api.Models.Wallets;

namespace WalletPlus.Api.Models.Users
{
    public class User : DeleteEntity
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordSalt { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlocked { get; set; }

        public List<Wallet> Wallets { get; set; }
    }
}
