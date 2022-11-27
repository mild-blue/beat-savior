using AutoMapper;
using BildMlue.Application.DTO.Todo;
using BildMlue.Domain.Entities;

namespace BildMlue.Infrastructure.Mapping.Profiles;

public class TodoProfile : Profile
{
    public TodoProfile()
    {
        CreateMap<CreateTodoInDto, TodoItem>();
        CreateMap<UpdateTodoInDto, TodoItem>();
        CreateMap<TodoItem, TodoDetailOutDto>();
        CreateMap<TodoItem, TodoListOutDto>();
    }
}