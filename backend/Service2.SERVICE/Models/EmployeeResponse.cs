namespace Service1.Models.Dtos;

public class EmployeeResponse
{
    public Guid id { get; set; }
    public string? name { get; set; }
    public string? position { get; set; }
    public DateOnly hiringDate { get; set; }
    public decimal salary { get; set; }
}