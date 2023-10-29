using AutoMapper;
using TodoLiveAi.Core.DbModels;
using TodoLiveAi.Web.Models;

namespace TodoLiveAi.Web.MappedModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskDB, TaskModel>();
            CreateMap<TaskModel, TaskDB>();

            CreateMap<TaskPriorityDB, TaskPriorityModel>();
            CreateMap<TaskPriorityModel, TaskPriorityDB>();

        }
    }
}
