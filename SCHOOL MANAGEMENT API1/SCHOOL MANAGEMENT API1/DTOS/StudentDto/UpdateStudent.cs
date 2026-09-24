using System.ComponentModel.DataAnnotations;

namespace SCHOOL_MANAGEMENT_API1.DTOS.StudentDto
{
    public class UpdateStudent
    {
        [Required, MaxLength(100)]
        public string FulName { get; set; }
        public string Email { get; set; }
        [Required, MaxLength(15), Phone]
        public string PhoneNumber { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }

        public int ClassRoomId { get; set; }

    }
}
