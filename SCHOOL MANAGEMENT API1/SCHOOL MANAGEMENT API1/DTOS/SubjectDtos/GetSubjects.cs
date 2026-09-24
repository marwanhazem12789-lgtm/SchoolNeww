using System.ComponentModel.DataAnnotations;

namespace SCHOOL_MANAGEMENT_API1.DTOS.SubjectDtos
{
    public class GetSubjects
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]

        public string Description { get; set; }
        [Required, Range(1, 100)]
        public int MaxGrade { get; set; }

        public string TeacherName { get; set; }
    }
}
