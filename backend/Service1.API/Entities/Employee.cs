namespace Service1.API.Entities;

public class Employee
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Position { get; set; }
    public DateOnly HiringDate { get; set; }
    public decimal Salary { get; set; }
}