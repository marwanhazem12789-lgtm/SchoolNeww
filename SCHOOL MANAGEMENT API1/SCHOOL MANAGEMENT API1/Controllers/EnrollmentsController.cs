using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCHOOL_MANAGEMENT_API1.DTOS.ClassRoomDto;
using SCHOOL_MANAGEMENT_API1.DTOS.Enrollmentt;
using SCHOOL_MANAGEMENT_API1.DTOS.StudentDto;
using SCHOOL_MANAGEMENT_API1.Mapping.EnrollmentMapping;
using SCHOOL_MANAGEMENT_API1.Models;

namespace SCHOOL_MANAGEMENT_API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly Context c;
        public EnrollmentsController(Context c)
        {
            this.c = c;
            mapper = new MapperConfiguration(o => o.AddProfile(new EnrollmentProfile())).CreateMapper();

        }

        [HttpGet]
        public ActionResult<List<Enrollment>> GetEnrollments()
        {
            var y = c.Enrollments.Include(p => p.Student).Include(p => p.Subject).ToList();
            if (y == null)
            {
                return NotFound();
            }
            var i = mapper.Map<List<GetEnrollments>>(y);
            return Ok(i);
        }
        [HttpGet("{Id}")]
        public IActionResult GetEnrollmentById(int Id)
        {
            var y = c.Enrollments.Include(p => p.Student).Include(p => p.Subject).FirstOrDefault(s => s.Id == Id);
            if (y == null)
            {
                return NotFound();
            }
            var i = mapper.Map<GetEnrollmensById>(y);
            return Ok(i);
        }

        [HttpPost]
        public IActionResult CreateEnrollment(CreateEnrollment y)
        {
            var l = c.Students.Any(s => s.Id == y.StudentId);
            if (l == false)
            {
                return NotFound("Student not found");
            }
            var k = c.Subjects.Any(s => s.Id == y.SubjectId);
            if (k == false)
            {
                return NotFound("Subject not found");
            }


            var e = mapper.Map<Enrollment>(y);
            c.Enrollments.Add(e);
            c.SaveChanges();
            return Created($"/api/Enrollments/{e.Id}", e);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEnrollment(int id, UpdateEnrollment d)
        {
            var f = c.Enrollments.Find(id);
            if (f == null)
            {
                return NotFound();

            }
            var l = c.Students.FirstOrDefault(s => s.Id == d.StudentId);
            if (l == null)
            {
                return NotFound("Student not found");

            }
            var k = c.Subjects.FirstOrDefault(j => j.Id == d.SubjectId);
            if (k == null)
            {
                return NotFound("Subject not found");
            }
            var e = mapper.Map(d, f);
            c.SaveChanges();
            return Ok(e);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchEnrollment(int id, PatchEnrollment dto)
        {
            var tt = c.Enrollments.Find(id);
            if (tt == null) return NotFound();

            if (dto.Grade == null)
                return BadRequest("Grade is Wrong");


            tt.Grade = dto.Grade;

            c.SaveChanges();
            return Ok(tt);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteEnrollment(int id)
        {
            var tt = c.Enrollments.Find(id);
            if (tt == null) return NotFound(" not found ");

            c.Enrollments.Remove(tt);
            c.SaveChanges();

            return NoContent();
        }



















        [HttpGet("last")]
        public IActionResult GetLastEnrollment(int studentId)
        {
            var res = c.Enrollments
                .Where(e => e.StudentId == studentId)
                .OrderBy(e => e.EnrollmentDate)
                .AsEnumerable()
                .LastOrDefault();

            if (res == null) return NotFound();
            return Ok(res);
        }
    }
}