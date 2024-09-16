using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Core.Interfaces
{
    public interface IUnitServices<TUnitIn, TUnitOut> where TUnitIn : class where TUnitOut : class
    {
        Task<TUnitOut> Add(TUnitIn unit);
        Task<List<TUnitOut>> GetAll();
        Task Delete(string id);
        Task Edit(string id, TUnitIn unit);
        Task<TUnitOut> GetById(string id);
        Task<TUnitOut> GetByName(string name);
    }
}
