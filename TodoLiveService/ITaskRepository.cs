﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoLiveAi.Core.DbModels;

namespace TodoLiveAi.Service
{
    public interface ITaskRepository
    {
        Task<TaskDB> GetTaskById(int taskId);
        Task<TaskDB> CreateTask(TaskDB task);
        Task UpdateTask(TaskDB updatedTask);
        Task DeleteTask(string taskId, string userId);
        Task<List<TaskDB>> GetAllUserTasks(string userId);
        Task<List<TaskPriorityDB>> GetPriorities();
    }
}
