using System.ComponentModel.DataAnnotations;

namespace Lab10.API.Entities;

public class Employee
{
    [Key]
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;
    
    public int Age { get; set; }
    
    public string Address { get; set; } = null!;
}