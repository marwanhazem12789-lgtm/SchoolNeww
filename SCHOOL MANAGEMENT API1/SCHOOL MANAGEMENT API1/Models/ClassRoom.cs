using System.ComponentModel.DataAnnotations;

namespace SCHOOL_MANAGEMENT_API1.Models
{
    public class ClassRoom
    {
        [Key]
        public int Id { get; set; }
        [Required , MaxLength(100)]
        public string Name { get; set; }
        [Required , Range(1 , 100)]
        public int Capacity { get; set; }
        [Required , Range(1 , 12)]
        public int GradeLevel { get; set; }


        public ICollection
            <Student> Students { get; set; } = new List<Student>();
    }
}
