using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace el.api.locathales.Domain.Commun
{
    public interface IUnitOfWork
    {
        Task<bool> PersistirNoBanco();
    }
}
