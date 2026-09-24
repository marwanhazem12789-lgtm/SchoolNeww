using AutoMapper;
using SCHOOL_MANAGEMENT_API1.DTOS.ClassRoomDto;
using SCHOOL_MANAGEMENT_API1.DTOS.Department;
using SCHOOL_MANAGEMENT_API1.DTOS.TeacherDtos;
using SCHOOL_MANAGEMENT_API1.Models;

namespace SCHOOL_MANAGEMENT_API1.Mapping.ClassRoomMapping
{
    public class ClassRoomProfile : Profile
    {
        public ClassRoomProfile()
        {
            CreateMap<ClassRoom, GetCalssRoooms>();

            CreateMap<ClassRoom, GetClassRoomsById>();

            CreateMap<CreateClassRoom, ClassRoom>();

            CreateMap<UpdateClassRooom, ClassRoom>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
