﻿using ToDoList.Domain.Enum;
using ToDoList.Domain.ViewModels.Task;

namespace ToDoList.Domain.Response;

public class BaseResponse<T> : IBaseResponse<T>
{
    public string? Description { get; set; }
    public StatusCode StatusCode { get; set; }
    public List<TaskViewModel> Data { get; set; }
}

public interface IBaseResponse<T>
{
    string? Description { get; }
    
    StatusCode StatusCode { get; }
    
    List<TaskViewModel> Data { get; }
}