using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoLiveAi.Core.DbModels;
using TodoLiveAi.Infrastructure.Data;
using TodoLiveAi.Service;

namespace TodoLiveAi.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<TaskDB> GetTaskById(int taskId)
        {
            if (_context.TaskDB != null)
            {
                var task = await _context.TaskDB.FindAsync(taskId);
                return task is null ? throw new Exception("Task not found") : task;
            }
            throw new Exception("Task not found");
        }


        public async Task CreateTask(TaskDB task)
        {
            if(task != null && _context.TaskDB != null)
            {
                _context.TaskDB.Add(task);
                await _context.SaveChangesAsync();
            }
        }



        public async Task UpdateTask(TaskDB updatedTask)
        {
            _context.Entry(updatedTask).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }



        public async Task DeleteTask(int taskId)
        {
            if(_context.TaskDB != null)
            {
                var task = await _context.TaskDB.FindAsync(taskId);
                if (task != null)
                {
                    _context.TaskDB.Remove(task);
                    await _context.SaveChangesAsync();
                }
            }

        }
    }
}
