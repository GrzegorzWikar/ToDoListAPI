using ToDoAPI.Model;

namespace ToDoAPI.Interface
{
    public interface IToDoTaskRepository
    {
        Task<IEnumerable<ToDoTask>?> GetAll();
        Task<IEnumerable<ToDoTask>?> GetAllTaskOfUser(int userId);
        Task<ToDoTask?> GetById(int toDoTaskId);
        void Insert(ToDoTask toDoTask);
        void Update(ToDoTask toDoTask);
        void Delete(int ToDoTaskId);
        void Save();
    }
}
