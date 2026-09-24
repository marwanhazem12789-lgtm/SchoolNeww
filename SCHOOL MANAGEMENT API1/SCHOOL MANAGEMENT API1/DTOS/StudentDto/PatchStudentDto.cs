using System.ComponentModel.DataAnnotations;

namespace SCHOOL_MANAGEMENT_API1.DTOS.StudentDto
{
    public class PatchStudentDto
    {

        [EmailAddress(ErrorMessage = "the format of email wrong")]
        public string? Email { get; set; }

 

    }
}
