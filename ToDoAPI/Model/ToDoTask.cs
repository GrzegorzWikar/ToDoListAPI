using Microsoft.EntityFrameworkCore;
using ToDoAPI.DTO;

namespace ToDoAPI.Model
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime Date { get; set; }
        public required int UserId { get; set; }
        public required User User { get; set; }
    }
}
