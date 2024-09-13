namespace PosSystem.Core.Interfaces.Repositories
{
    public interface IRepository<Entity>
    {
        public Task<List<Entity>> GetAll();
        public Task<Entity> GetById(string id);
        public Task Insert(Entity entity);
        public Task Update(Entity entity);
        public Task Delete(string id);
    }
}