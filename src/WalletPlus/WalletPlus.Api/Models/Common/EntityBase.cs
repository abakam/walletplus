using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletPlus.Api.Models.Common
{
    public class EntityBase : IEntityBase
    {
        public virtual string Id { get; set; } = Guid.NewGuid().ToString().Replace("-", "");
    }
}
