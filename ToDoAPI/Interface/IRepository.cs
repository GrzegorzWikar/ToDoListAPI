namespace ToDoAPI.Interface
{
    public interface IRepository<T>
    {
        T GetById(int Id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
