using AutoMapper;
using TodoList.Core.Entities;
using TodoList.Web.Models;

namespace TodoList.Web
{
  
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ToDoItem, ToDoItemDTO>(); // means you want to map from User to UserDTO
        }
    }
}
