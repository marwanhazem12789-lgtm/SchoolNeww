using SCHOOL_MANAGEMENT_API1.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCHOOL_MANAGEMENT_API1.DTOS.Enrollmentt
{
    public class GetEnrollments
    {
        [Key]
        public int Id { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime EnrollmentDate { get; set; }
        [Required, Range(0, 100)]
        public int Grade { get; set; }
        [ForeignKey(nameof(Student))]
        public string StudentName { get; set; }
        [ForeignKey(nameof(Subject))]

        public string SubjectName { get; set; }
    }
}
