using System.ComponentModel.DataAnnotations;

namespace Tutorial.Models.DTOs;

public class AddAnimal
{
    [Required]
    [MinLength(3)]
    [MaxLength(200)]
    public string Name { get; set; }

    [MaxLength(200)] public string? Description { get; set; }
    
    public string? Category { get; set; }
    public string? Area { get; set; }
}