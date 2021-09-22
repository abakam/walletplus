using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WalletPlus.Api.Models.Enums
{
    public enum WalletType
    {
        [Description("Main")]
        Main,
        [Description("Bonus")]
        Bonus
    }

    public enum WalletTransactionType
    {
        [Description("TopUp")]
        TopUp,
        [Description("Transfer")]
        Transfer
    }
}
