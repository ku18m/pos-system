using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Core.Interfaces
{
    public interface ICompanyServices<TCompanyIn, TCompanyOut> where TCompanyIn : class where TCompanyOut : class
    {
        Task<TCompanyOut> Add(TCompanyIn company);
        Task<List<TCompanyOut>> GetAll();
        Task Delete(int id);
        Task Edit(int id, TCompanyIn company);
        Task<TCompanyOut> GetById(int id);
        Task<TCompanyOut> GetByName(string name); 
        Task<IEnumerable<TCompanyOut>> GetCompaniesByName(string name);
    }
}
