using ToDoAPI.DTO;
using ToDoAPI.Model;

namespace ToDoAPI.Interface
{
    public interface IUserService
    {
        Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model);
        Task<IEnumerable<DTOUser>> GetAll();
        Task<DTOUser?> GetById(int id);
        Task<DTOUser?> AddAndUpdateUser(User userObj);

    }
}
