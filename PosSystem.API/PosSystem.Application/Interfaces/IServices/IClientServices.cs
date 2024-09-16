namespace PosSystem.Application.Interfaces.IServices
{
    public interface IClientServices<TClientIn, TClientOut> where TClientIn : class where TClientOut : class
    {
        Task<TClientOut> Add(TClientIn client);
        Task<List<TClientOut>> GetAll();
        Task DeleteById(string id);
        Task Edit(string id, TClientIn client);
        Task<TClientOut> GetById(string id);
        Task<TClientOut> GetByName(string name);
        Task<IEnumerable<TClientOut>> GetClientsByName(string name);
        Task<TClientOut> GetByPhone(string phone);
        Task<IEnumerable<TClientOut>> GetClientsByAddress(string address);
    }
}
