﻿namespace ToDoAPI.DTO
{
    public class DTOUser
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
    }
}
