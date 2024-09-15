
namespace PosSystem.Core.Interfaces
{
    public interface ICompanyServices<TCompanyIn, TCompanyOut> where TCompanyIn : class where TCompanyOut : class
    {
        Task<TCompanyOut> Add(TCompanyIn company);
        Task<List<TCompanyOut>> GetAll();
        Task Delete(string id);
        Task Edit(string id, TCompanyIn company);
        Task<TCompanyOut> GetById(string id);
        Task<TCompanyOut> GetByName(string name); 
        Task<IEnumerable<TCompanyOut>> GetCompaniesByName(string name);
    }
}
