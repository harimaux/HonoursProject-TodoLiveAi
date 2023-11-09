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



        public async Task<TaskDB> CreateTask(TaskDB task)
        {
            if (task != null && _context.TodoTask != null)
            {
                _context.TodoTask.Add(task);
                await _context.SaveChangesAsync();
                return task;
            }
            throw new Exception("Error while creating a task!");
        }


        public async Task UpdateTask(TaskDB updatedTask)
        {
            var itemId = updatedTask.Id;
            var task = await GetTaskById(itemId);

            if (task != null)
            {
                _context.Entry(task).CurrentValues.SetValues(updatedTask);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Error while editing task: Task not found!");
            }
        }


        public async Task DeleteTask(string taskId, string userId)
        {
            if (_context.TodoTask != null)
            {
                var task = await _context.TodoTask.FindAsync(Int32.Parse(taskId));
                if (task != null && task.OwnerId == userId)
                {
                    _context.TodoTask.Remove(task);
                    await _context.SaveChangesAsync();
                }
            }

        }



        public async Task<List<TaskDB>> GetAllUserTasks(string userId)
        {
            if (_context.TodoTask != null)
            {
                var tasks = await _context.TodoTask.Where(task => task.OwnerId == userId).ToListAsync();
                if (tasks.Count == 0)
                {
                    throw new Exception("You don't have any saved tasks.");
                }
                return tasks;
            }
            throw new Exception("You don't have any saved tasks.");
        }


        public async Task<List<TaskPriorityDB>> GetPriorities()
        {
            if (_context.TaskPriority != null)
            {
                var priorities = await _context.TaskPriority.ToListAsync();
                return priorities;
            }
            throw new Exception("Priorities error!");
        }

    }
}
