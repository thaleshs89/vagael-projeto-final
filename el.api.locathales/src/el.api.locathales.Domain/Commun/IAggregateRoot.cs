using System;
using System.Collections.Generic;
using System.Text;

namespace el.api.locathales.Domain.Commun
{
    public interface IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
