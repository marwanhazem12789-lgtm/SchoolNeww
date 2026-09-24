using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCHOOL_MANAGEMENT_API1.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        [Required , MaxLength(100)]
public string FirstName { get; set; }
        [Required, MaxLength(100)]

        public string LastName { get; set; }
        [Required, MaxLength(150) , EmailAddress]
public string Email { get; set; }
        [Required, MaxLength(15), Phone]
        public string PhoneNumber { get; set; }
        [Required, Range(1,int.MaxValue , ErrorMessage = "must be greater than or equal to 0")]
        public double   Salary { get; set; }



        public Department Department { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }


        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();

    }
}
