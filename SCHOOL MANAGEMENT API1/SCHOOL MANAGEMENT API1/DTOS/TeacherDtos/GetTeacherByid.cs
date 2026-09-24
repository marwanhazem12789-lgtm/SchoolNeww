using System.ComponentModel.DataAnnotations;

namespace SCHOOL_MANAGEMENT_API1.DTOS.TeacherDtos
{
    public class GetTeacherByid
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
public string FullName { get; set; }
        [Required, MaxLength(150), EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(15), Phone]
        public string PhoneNumber { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "must be greater than or equal to 0")]
        public double Salary { get; set; }

public string DepartmentName { get; set; }
    }
}
