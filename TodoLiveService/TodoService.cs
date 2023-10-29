﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoLiveAi.Core;
using TodoLiveAi.Core.DbModels;
using TodoLiveAi.Infrastructure.Data;


namespace TodoLiveAi.Service
{
    public class TodoService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITaskRepository _taskRepository;

        //private string? _currentUserId;
        //private AppUser? _currentUser;

        public TodoService(IMapper mapper, ApplicationDbContext applicationDbContext, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _dbContext = applicationDbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _taskRepository = taskRepository;
        }

        //public TaskDto MapTaskDbToTaskDto(TaskDB taskDb)
        //{
        //    return _mapper.Map<TaskDto>(taskDb);
        //}

        //public TaskDB MapTaskDtoToTaskDb(TaskDto taskDto)
        //{
        //    return _mapper.Map<TaskDB>(taskDto);
        //}









    }
}
