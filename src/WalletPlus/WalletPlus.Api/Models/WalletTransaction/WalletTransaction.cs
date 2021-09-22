using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WalletPlus.Api.Models.Common;
using WalletPlus.Api.Models.Enums;
using WalletPlus.Api.Models.Users;

namespace WalletPlus.Api.Models.WalletTransaction
{
    public class WalletTransaction : AuditEntity
    {
        public WalletTransactionType Type { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
