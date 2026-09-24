using AutoMapper;
using SCHOOL_MANAGEMENT_API1.DTOS.Enrollmentt;
using SCHOOL_MANAGEMENT_API1.Models;

namespace SCHOOL_MANAGEMENT_API1.Mapping.EnrollmentMapping
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            CreateMap<Enrollment, GetEnrollments>().ForMember(o => o.StudentName, opt => opt.MapFrom(src => $"{src.Student.FirstName} {src.Student.LastName}"))
                .ForMember(o => o.SubjectName, opt => opt.MapFrom(src => src.Subject.Name));


            CreateMap<Enrollment, GetEnrollmensById>().ForMember(o => o.StudentName, opt => opt.MapFrom(src => $"{src.Student.FirstName} {src.Student.LastName}"))
                .ForMember(o => o.SubjectName, opt => opt.MapFrom(src => src.Subject.Name));


            CreateMap<CreateEnrollment, Enrollment>();

            CreateMap<UpdateEnrollment, Enrollment>().ForMember(d => d.Id, opt => opt.Ignore());



        }
    }
}
