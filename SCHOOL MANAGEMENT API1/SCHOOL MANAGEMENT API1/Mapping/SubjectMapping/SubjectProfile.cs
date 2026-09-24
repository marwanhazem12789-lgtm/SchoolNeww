using AutoMapper;
using SCHOOL_MANAGEMENT_API1.DTOS.SubjectDtos;
using SCHOOL_MANAGEMENT_API1.DTOS.TeacherDtos;
using SCHOOL_MANAGEMENT_API1.Models;

namespace SCHOOL_MANAGEMENT_API1.Mapping.SubjectMapping
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, GetSubjects>()
                .ForMember(m => m.TeacherName, opt => opt.MapFrom(src => $"{src.Teacher.FirstName} {src.Teacher.LastName}"));

            CreateMap<Subject, GetSubjectsById>()
                .ForMember(m => m.TeacherName, opt => opt.MapFrom(src => $"{src.Teacher.FirstName} {src.Teacher.LastName}"));

            CreateMap<CreateSubject, Subject>();

            CreateMap<UpdateSubject, Subject>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
