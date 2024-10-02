using Microsoft.AspNetCore.Mvc;
using ToDoList.DAL;
using ToDoList.Domain.Entity;
using ToDoList.Domain.Filters.Task;
using ToDoList.Domain.ViewModels.Task;
using ToDoList.Service.Interfaces;

namespace ToDoList.Controllers;

public class TaskController : Controller
{
    private readonly ITaskService _taskService;
    private readonly AppDbContext _appDbContext;

    public TaskController(ITaskService taskService, AppDbContext appDbContext)
    {
        _taskService = taskService;
        _appDbContext = appDbContext;
    }
    
    public IActionResult Index()
    {
        var result = new List<TaskEntity>();
        foreach (var el in _appDbContext.Tasks)
        {
            result.Add(el);
        }
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskViewModel model)
    {
        var response = await _taskService.Create(model);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { description = response.Description });
        }

        return BadRequest(new { description = response.Description });
    }
    
    [HttpPost]
    public async Task<IActionResult> TaskHandler(TaskFilter filter)
    {
        var response = await _taskService.GetTasks(filter);
        
        return Json(new {data = response.Data});
    }
    
    [HttpPost]
    public async Task<IActionResult> EndTask(long id)
    {
        var response = await _taskService.EndTask(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { description = response.Description });
        }

        return BadRequest(new { description = response.Description });
    }
    
    public async Task<IActionResult> GetCompletedTasks()
    {
        var result = await _taskService.GetCompletedTask();
        return Json(new { data = result.Data });
    }
}















