using ToDoAPI.Data;
using ToDoAPI.DTO;
using ToDoAPI.Model;

namespace ToDoAPI.Helpers
{
    public sealed class Converter
    {

        private readonly ToDoAPIContext _context;

        private Converter() {}

        private static Converter _instance;

        public static Converter GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Converter();
            }    
            return _instance;
        }


        public  User DTOUserToUser(DTOUser dTOUser)
        {
            return _context.Users.Find(dTOUser.Id); ;
        }

        public DTOUser UserToDTOUser(User user)
        {
            return new DTOUser()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
            };
        }
        public DateTime Date(string planedDate, string planedTime)
        {
            DateOnly dateOnly = DateOnly.Parse(planedDate);
            TimeOnly timeOnly = TimeOnly.Parse(planedTime);
            return new DateTime(dateOnly, timeOnly);
        }
        public ToDoTask DTOToDoTaskToToDoTask(User user, DTOToDoTask DTOToDoTask)
        {
            return new ToDoTask()
            {
                Id = DTOToDoTask.Id,
                Title = DTOToDoTask.Title,
                Description = DTOToDoTask.Description,
                Date = Date(DTOToDoTask.PlanedDate, DTOToDoTask.PlanedTime),
                UserId = user.Id,
                User = user,
            };
        }

        public DTOToDoTask ToDoTaskToDTOToDoTask(ToDoTask toDoTask)
        {
            return new DTOToDoTask()
            {
                Id = toDoTask.Id,
                Title = toDoTask.Title,
                Description = toDoTask.Description,
                PlanedDate = toDoTask.Date.Date.ToString(),
                PlanedTime = toDoTask.Date.TimeOfDay.ToString(),
            };
        }
    }
}
