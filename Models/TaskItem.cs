using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleTaskApp.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Название задачи обязательно")]
        [StringLength(100, ErrorMessage = "Название не может превышать 100 символов")]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "Описание не может превышать 500 символов")]
        public string Description { get; set; } = string.Empty;
        
        public bool IsCompleted { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}