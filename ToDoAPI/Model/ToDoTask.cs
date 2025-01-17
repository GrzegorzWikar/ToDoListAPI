using Microsoft.EntityFrameworkCore;

namespace ToDoAPI.Model
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string PlanedDate { get; set; }
        public string PlanedTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
