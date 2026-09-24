using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCHOOL_MANAGEMENT_API1.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string FirstName { get; set; }
        [Required, MaxLength(100)]

        public string LastName { get; set; }
        [Required, MaxLength(150), EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(15), Phone]
        public string PhoneNumber { get; set; }
        [Required , DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }


        public  ClassRoom ClassRoom { get; set; }
        [ForeignKey("ClassRoom")]
        public int ClassRoomId { get; set; }



        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
