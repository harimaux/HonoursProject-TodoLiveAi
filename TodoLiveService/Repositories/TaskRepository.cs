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
            if (_context.TodoTask != null)
            {
                var task = await _context.TodoTask.FindAsync(taskId);
                return task is null ? throw new Exception("Task not found") : task;
            }
            throw new Exception("Task not found");
        }


        public async Task CreateTask(TaskDB task)
        {
            if(task != null && _context.TodoTask != null)
            {
                _context.TodoTask.Add(task);
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
            if(_context.TodoTask != null)
            {
                var task = await _context.TodoTask.FindAsync(taskId);
                if (task != null)
                {
                    _context.TodoTask.Remove(task);
                    await _context.SaveChangesAsync();
                }
            }

        }



        //public async Task<List<TaskDB>> GetAllUserTasks(int userId)
        //{
        //    if (_context.TaskDB != null)
        //    {
                
        //        var tasks = await _context.TaskDB.Where(task => Int32.Parse(task.OwnerId) == userId).ToListAsync();
        //        if (tasks.Count == 0)
        //        {
        //            throw new Exception("Tasks not found"); // Handle this exception as needed
        //        }
        //        return tasks;
        //    }
        //    throw new Exception("Tasks not found"); // Handle this exception as needed
        //}




    }
}
