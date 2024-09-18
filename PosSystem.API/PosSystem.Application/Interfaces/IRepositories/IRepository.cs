namespace PosSystem.Application.Interfaces.IRepositories
{
    public interface IRepository<Entity>
    {
        public Task<List<Entity>> GetAll();
        public Task<List<Entity>> GetPage(int page, int pageSize);
        public Task<int> GetTotalPages(int pageSize);
        public Task<Entity> GetById(string id);
        public Task Insert(Entity entity);
        public Task Update(Entity entity);
        public Task Delete(string id);
    }
}