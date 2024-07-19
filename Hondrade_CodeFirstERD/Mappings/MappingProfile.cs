using AutoMapper;
using Hondrade_CodeFirstERD.DTOs;
using Hondrade_CodeFirstERD.Entities;

namespace Hondrade_CodeFirstERD.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
        
            CreateMap<Department, DepartmentDto>().ReverseMap();

            CreateMap<DepartmentDto, Department>()
            .ForMember(dest => dest.DepID, opt => opt.Ignore());

            CreateMap<Service, ServiceDto>()
                .ForMember(dest => dest.DepID, opt => opt.MapFrom(src => src.Department.DepID))
                .ReverseMap()
                .ForMember(dest => dest.ServiceID, opt => opt.Ignore())
                .ForMember(dest => dest.Department, opt => opt.Ignore());

            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Application, ApplicationDto>();

        }
    }
}
