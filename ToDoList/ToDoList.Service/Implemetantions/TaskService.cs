using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDoList.DAL.Interfaces;
using ToDoList.Domain.Entity;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Response;
using ToDoList.Domain.ViewModels.Task;
using ToDoList.Service.Interfaces;

namespace ToDoList.Service.Implemetantions;

public class TaskService : ITaskService
{
    private readonly IBaseRepository<TaskEntity> _taskRepository;
    private readonly ILogger<TaskEntity> _logger;
    
    public TaskService(IBaseRepository<TaskEntity> taskRepository, ILogger<TaskEntity> logger)
    {
        _taskRepository = taskRepository;
        _logger = logger;
    }
    
    public async Task<IBaseResponse<TaskEntity>> Create(CreateTaskViewModel model)
    {
        try
        {
            _logger.LogInformation($"Запрос на создании задчи - {model.Name}");

            var task = await _taskRepository
                .GetAll()
                .Where(x => x.Created.Date == DateTime.UtcNow.Date)
                .FirstOrDefaultAsync(x => x.Name == model.Name);

            if (task != null)
            {
                return new BaseResponse<TaskEntity>()
                {
                    Description = "Задача с таким названием уже есть",
                    StatusCode = StatusCode.TaskIsHasAlready
                };
            }

            task = new TaskEntity()
            {
                Name = model.Name,
                Description = model.Description,
                IsDone = false,
                Priority = model.Priority,
                Created = DateTime.UtcNow
            };

            await _taskRepository.Create(task);
            
            _logger.LogInformation($"Задача создалась: {task.Name} {task.Created}");
            return new BaseResponse<TaskEntity>()
            {
                Description = "Задача создалась",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"[TaskService.Create]: {e.Message}");

            return new BaseResponse<TaskEntity>()
            {
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}