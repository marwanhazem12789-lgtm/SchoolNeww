using AutoMapper;
using SCHOOL_MANAGEMENT_API1.DTOS.StudentDto;
using SCHOOL_MANAGEMENT_API1.DTOS.TeacherDtos;
using SCHOOL_MANAGEMENT_API1.Models;

namespace SCHOOL_MANAGEMENT_API1.Mapping.TeacherMapping
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher, GetTeachers>()
     .ForMember(m => m.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
     .ForMember(m => m.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<Teacher, GetTeacherByid>()
                .ForMember(m => m.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(m => m.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<CraeteeacherDto, Teacher>()
                          .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FullName.Split(new[] { ' ' })[0]))
                          .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.FullName.Split(new[] { ' ' })[1]));



            CreateMap<UpdateTeacher, Teacher>()
                          .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FullName.Split(new[] { ' ' })[0]))
                          .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.FullName.Split(new[] { ' ' })[1]));
        }
    }
}
