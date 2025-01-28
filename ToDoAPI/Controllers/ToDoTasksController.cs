using ToDoAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.DTO;
using ToDoAPI.Model;
using ToDoAPI.Interface;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ToDoTasksController : ControllerBase
    {
        private IToDoTaskRepository _toDoTaskRepository;
        private IUserService _userService;
        private Converter _converter = Converter.GetInstance();


        public ToDoTasksController(IToDoTaskRepository toDoTaskRepository, IUserService userService)
        {
            _toDoTaskRepository = toDoTaskRepository;
            _userService = userService;
        }

        // GET: api/ToDoTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOToDoTask>>?> GetToDoTasks()
        {
            IEnumerable<ToDoTask>? toDoTasks = await _toDoTaskRepository.GetAll();

            List<DTOToDoTask> dtoToDoTasks = new List<DTOToDoTask>();

            if (toDoTasks != null)
            {
                foreach (ToDoTask toDoTask in toDoTasks)
                {

                    DTOToDoTask dtoToDoTask = _converter.ToDoTaskToDTOToDoTask(toDoTask);

                    dtoToDoTasks.Add(dtoToDoTask);
                }

                return dtoToDoTasks;
            }

            return null; 
        }

        // GET: api/ToDoTasks/5
        [HttpGet("{TaskId}")]
        public async Task<ActionResult<ToDoTask>> GetToDoTask(int TaskId)
        {
            ToDoTask? toDoTask = await _toDoTaskRepository.GetById(TaskId);

            if (toDoTask == null)
            {
                return NotFound();
            }

            return toDoTask;
        }

        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<DTOToDoTask>>?> GetAllUserToDoTasks(int userId)
        {
            IEnumerable<ToDoTask>? toDoTasks = await _toDoTaskRepository.GetAllTaskOfUser(userId);

            List<DTOToDoTask> DTOToDoTasks = new List<DTOToDoTask>();

            if (toDoTasks != null)
            {
                foreach (ToDoTask toDoTask in toDoTasks)
                {
                    DTOToDoTask DTOToDoTask = _converter.ToDoTaskToDTOToDoTask(toDoTask);

                    DTOToDoTasks.Add(DTOToDoTask);
                }

                return DTOToDoTasks;
            }

            return null;
        }

        // PUT: api/ToDoTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutToDoTask([FromBody] DTOToDoTask DTOToDoTask)
        {
            ToDoTask? toDoTask = await _toDoTaskRepository.GetById(DTOToDoTask.Id);

            if (toDoTask == null) return NotFound();

            ToDoTask newToDoTask = _converter.DTOToDoTaskToToDoTask(toDoTask.User, DTOToDoTask);
            
            _toDoTaskRepository.Insert(newToDoTask);

            _toDoTaskRepository.Save();

            return Ok();
        }

        // POST: api/ToDoTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{userId}")]
        public async Task<IActionResult> PostToDoTask(int userId, [FromBody] DTOToDoTask DTOToDoTask)
        {
            DTOUser? dTOUser = await _userService.GetById(userId);
            if (dTOUser == null) return NotFound();

            User? user = _converter.DTOUserToUser(dTOUser);
            if(user == null) return NotFound();

            ToDoTask newToDoTask = _converter.DTOToDoTaskToToDoTask(user,DTOToDoTask);
            _toDoTaskRepository.Insert(newToDoTask);
            _toDoTaskRepository.Save();

            return Ok();
        }

        // DELETE: api/ToDoTasks/5
        [HttpDelete("{TaskId}")]
        public async Task<IActionResult> DeleteToDoTask(int TaskId)
        {
            _toDoTaskRepository.Delete(TaskId);
            _toDoTaskRepository.Save();

            return Ok();
        }
    }
}
