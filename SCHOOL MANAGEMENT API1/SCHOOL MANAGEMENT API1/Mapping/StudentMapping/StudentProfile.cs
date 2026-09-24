using AutoMapper;
using SCHOOL_MANAGEMENT_API1.DTOS.StudentDto;
using SCHOOL_MANAGEMENT_API1.Models;

namespace SCHOOL_MANAGEMENT_API1.Mapping.StudentMapping
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, GetStudents>()
                .ForMember(m => m.FulName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(m => m.ClassRoomName, opt => opt.MapFrom(src => src.ClassRoom.Name));

            CreateMap<Student, GetStudentsById>()
                .ForMember(m => m.FulName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(m => m.ClassRoomName, opt => opt.MapFrom(src => src.ClassRoom.Name));

            CreateMap<CreateStudentDo, Student>()
                          .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FulName.Split(new[] { ' ' })[0]))
                          .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.FulName.Split(new[] { ' ' })[1]));



            CreateMap<UpdateStudent, Student>()
                          .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FulName.Split(new[] { ' ' })[0]))
                          .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.FulName.Split(new[] { ' ' })[1]));
        }
    }
}