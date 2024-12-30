using Microsoft.EntityFrameworkCore;

namespace ToDoAPI.Model
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public DateTime PlanedDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
