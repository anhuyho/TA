using AutoMapper;
using Service1.API.Entities;
using Service1.API.Models.Dtos;

namespace Service1.API;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Employee, EmployeeResponseDto>();
        CreateMap<EmployeeRequestDto, Employee>();
    }
}