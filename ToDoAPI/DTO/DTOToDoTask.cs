namespace ToDoAPI.DTO
{
    public class DTOToDoTask
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string PlanedDate { get; set; }
        public required string PlanedTime {  get; set; }
    }
}
