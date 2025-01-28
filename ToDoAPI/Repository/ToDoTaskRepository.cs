using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ToDoAPI.Data;
using ToDoAPI.Interface;
using ToDoAPI.Model;

namespace ToDoAPI.Repository
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {

        private readonly ToDoAPIContext _context;

        private bool disposed = false;

        public ToDoTaskRepository(ToDoAPIContext context)
        {
            _context = context;
        }

        public async void  Delete(int ToDoTaskId)
        {
            ToDoTask? toDoTask = await _context.ToDoTask.FindAsync(ToDoTaskId);

            if (toDoTask != null)
            {
                _context.ToDoTask.Remove(toDoTask);
            }
        }

        public async Task<IEnumerable<ToDoTask>?> GetAllTaskOfUser(int userId)
        {
            return await _context.ToDoTask.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<ToDoTask>?> GetAll()
        {
            return await _context.ToDoTask.ToListAsync();
        }

        public async Task<ToDoTask?> GetById(int toDoTaskId)
        {
            ToDoTask? toDoTask = await _context.ToDoTask.FindAsync(toDoTaskId);

            if (toDoTask == null) return null;

            return toDoTask;
        }

        public async void Insert(ToDoTask toDoTask)
        {
            await _context.ToDoTask.AddAsync(toDoTask);
        }

        public async void Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(ToDoTask toDoTask)
        {
             _context.Entry(toDoTask).State = EntityState.Modified;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed) 
            {
                if (disposing) 
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
