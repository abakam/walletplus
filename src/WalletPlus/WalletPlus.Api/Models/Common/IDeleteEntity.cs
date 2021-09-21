using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletPlus.Api.Models.Common
{
    public interface IDeleteEntity
    {
        bool IsDeleted { get; set; }
    }
}
