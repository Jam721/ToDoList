using ToDoList.Domain.Enum;

namespace ToDoList.Domain.ViewModels.Task;

public class CreateTaskViewModel
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public Priority Priority { get; set; }

    public void Validate()
    {
        if (String.IsNullOrWhiteSpace(Name)) 
            throw new ArgumentNullException(Name, "Укажите название задачи");
        
        if (String.IsNullOrWhiteSpace(Description))
            throw new ArgumentNullException(Description, "Укажите описание задачи");
        
    }
}