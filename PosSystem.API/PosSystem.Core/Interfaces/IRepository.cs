namespace PosSystem.Core.Interfaces
{
    public interface IRepository<Entity>
    {
        public List<Entity> GetAll();
        public Entity GetById(int id);
        public void Insert(Entity entity);
        public void Update(Entity entity);
        public void Delete(int id);
        public void Save();
    }
}