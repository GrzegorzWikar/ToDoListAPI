﻿

namespace ToDoAPI.Model
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public ICollection<ToDoTask>? Tasks { get; set; }
    }
}
