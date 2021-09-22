using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletPlus.Api.Services.Helpers
{
    public class BonusCalculator
    {
        public static decimal GetBonus(decimal topupAmount)
        {
            decimal bonusAmount = 0;

            if (topupAmount >= 5000 && topupAmount <= 10000)
                bonusAmount = PercentageAmount(1m, topupAmount);
            else if (topupAmount > 10000 && topupAmount <= 25000)
                bonusAmount = PercentageAmount(2.5m, topupAmount);
            else if (topupAmount > 25000)
                bonusAmount = PercentageAmount(5m, topupAmount);

            return bonusAmount;
            
        }

        private static decimal PercentageAmount(decimal percentage, decimal amount)
        {
            return percentage / 100m * amount;
        }

    }
}
