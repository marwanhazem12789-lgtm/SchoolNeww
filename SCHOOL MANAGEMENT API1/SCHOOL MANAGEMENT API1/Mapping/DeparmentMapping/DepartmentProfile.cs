using AutoMapper;
using SCHOOL_MANAGEMENT_API1.DTOS.Department;
using SCHOOL_MANAGEMENT_API1.Models;

namespace SCHOOL_MANAGEMENT_API1.Mapping.DeparmentMapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {


            CreateMap<Department, GetDepartments>();

            CreateMap<Department, GetDeparmenttByID>();

            CreateMap<CreateDeparmentDto, Department>();

            CreateMap<UpdateDeparmentDto, Department>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
