using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;

namespace SCHOOL_MANAGEMENT_API1.DTOS.StudentDto
{
    public class GetStudents
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
public string FulName { get; set; } 
        public string Email { get; set; }
        [Required, MaxLength(15), Phone]
        public string PhoneNumber { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }

        public string ClassRoomName { get; set; }
    }
}
