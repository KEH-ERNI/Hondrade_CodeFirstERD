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
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.DepID, opt => opt.Ignore());


            CreateMap<Employee, EmployeeDto>()
               .ForMember(dest => dest.DepID, opt => opt.MapFrom(src => src.Department.DepID))
               .ReverseMap()
               .ForMember(dest => dest.EmpID, opt => opt.Ignore())
               .ForMember(dest => dest.Department, opt => opt.Ignore())
               .ForMember(dest => dest.DepID, opt => opt.Ignore());


            CreateMap<Citizen, CitizenDto>().ReverseMap();
            CreateMap<CitizenDto, Citizen>()
                .ForMember(dest => dest.CitizenID, opt => opt.Ignore());

            CreateMap<Application, ApplicationDto>()
                .ForMember(dest => dest.ServiceID, opt => opt.MapFrom(src => src.Service.ServiceID))
                .ForMember(dest => dest.CitizenID, opt => opt.MapFrom(src => src.Citizen.CitizenID))
                .ReverseMap()
                .ForMember(dest => dest.ApplicationID, opt => opt.Ignore())
                .ForMember(dest => dest.Service, opt => opt.Ignore())
                .ForMember(dest => dest.ServiceID, opt => opt.Ignore())
                .ForMember(dest => dest.Citizen, opt => opt.Ignore())
                .ForMember(dest => dest.CitizenID, opt => opt.Ignore());

            CreateMap<Contact, ContactDto>()
                .ForMember(dest => dest.EmpID, opt => opt.MapFrom(src => src.Employee.EmpID))
                .ForMember(dest => dest.CitizenID, opt => opt.MapFrom(src => src.Citizen.CitizenID))
                .ReverseMap()
                .ForMember(dest => dest.ContactID, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.EmpID, opt => opt.Ignore())
                .ForMember(dest => dest.Citizen, opt => opt.Ignore())
                .ForMember(dest => dest.CitizenID, opt => opt.Ignore());

        }
    }
}
