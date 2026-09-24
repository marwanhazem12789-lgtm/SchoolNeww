using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCHOOL_MANAGEMENT_API1.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [ MaxLength(500)]

        public string Description { get; set; }
        [Required , Range(1 , 100)]
        public int MaxGrade   { get; set; }

        public Teacher Teacher { get; set; }
        [ForeignKey(nameof(Teacher))]
        public int TeacherId { get; set; }


        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
