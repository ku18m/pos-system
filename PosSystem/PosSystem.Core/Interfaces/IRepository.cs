namespace PosSystem.Core.Interfaces
{
    public interface IRepository<Entity>
    {
        public List<Entity> GetAll();
        public Entity GetById(string id);
        public void Insert(Entity entity);
        public void Update(Entity entity);
        public void Delete(string id);
        public void Save();
    }
}
