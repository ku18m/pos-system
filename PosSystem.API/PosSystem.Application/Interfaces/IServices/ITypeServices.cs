namespace PosSystem.Application.Interfaces.IServices
{
    public interface ITypeServices<TTypeIn, TTypeOut> where TTypeIn : class where TTypeOut : class
    {
        Task<TTypeOut> Add(TTypeIn unit);
        Task<List<TTypeOut>> GetAll();
        Task Delete(string id);
        Task Edit(string id, TTypeIn unit);
        Task<TTypeOut> GetById(string id);
        Task<TTypeOut> GetByName(string name);
    }
}
