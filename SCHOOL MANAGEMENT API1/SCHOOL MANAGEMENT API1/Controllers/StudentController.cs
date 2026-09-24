using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCHOOL_MANAGEMENT_API1.DTOS.StudentDto;
using SCHOOL_MANAGEMENT_API1.Mapping.StudentMapping;
using SCHOOL_MANAGEMENT_API1.Models;

namespace SCHOOL_MANAGEMENT_API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly Context c;
        public StudentController(Context c)
        {
            this.c = c;

            mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new StudentProfile());
            }).CreateMapper();


        }


        [HttpGet]
        public ActionResult<List<Student>> GetStudents()
        {
            var t = c.Students.ToList();
            var studentDtos = mapper.Map<List<GetStudents>>(t);
            return Ok(studentDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var t = c.Students.FirstOrDefault(s => s.Id == id);
            if(t == null)
            {
                return NotFound();
            }

            var h = mapper.Map<GetStudentsById>(t);
            return Ok(h);
        }



        [HttpPost]
        public IActionResult CreateStudent(CreateStudentDo y)
        {
            var i = c.classRooms.Any(c => c.Id == y.ClassRoomId);
            if (i == false)
            {
                return BadRequest("ClassRoomId is not valid");
            }

            var e = mapper.Map<Student>(y);
            c.Students.Add(e);
            c.SaveChanges();
            return Created("",e);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, UpdateStudent dto)
        {
            var student = c.Students.Find(id);
            if (student == null) return NotFound();
            mapper.Map(dto, student);
            c.SaveChanges();
            return Ok(student);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchStudent(int id, PatchStudentDto dto)
        {
            var student = c.Students.Find(id);
            if (student == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.Email))
                student.Email = dto.Email;

  

            c.SaveChanges();
            return Ok(student);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = c.Students.Find(id);
            if (student == null) return NotFound(" not found ");

            c.Students.Remove(student);
            c.SaveChanges();

            return NoContent();
        }


















        //////////////////////////////////////////////////////////////////////

        [HttpGet("filter")]
        public IActionResult GetStudentFilter(int ClassRoomId, int minGrade)
        {
            var s = c.Students
                .Include(i => i.ClassRoom)
                .Where(p => p.ClassRoomId == ClassRoomId && p.Enrollments.Any(e => e.Grade >= minGrade))
                .ToList();

            var studentDtos = mapper.Map<List<GetStudents>>(s);
            return Ok(studentDtos);
        }

        [HttpGet("first")]
        public IActionResult GetStudentFirst(int ClassRoomId)
        {
            var s = c.Students.Include(o => o.ClassRoom).First(p => p.ClassRoomId == ClassRoomId);
            var i = mapper.Map<GetStudentsById>(s);
            return Ok(i);
        }

        [HttpGet("first-or-default")]
        public IActionResult GetStudentfirstOrDefault(int ClassRoomId)
        {
            var s = c.Students.Include(o => o.ClassRoom).FirstOrDefault(p => p.ClassRoomId == ClassRoomId);
            if (s == null) return NotFound();

            var i = mapper.Map<GetStudentsById>(s);
            return Ok(i);
        }

        [HttpGet("single-by-email/{email}")]
        public IActionResult GetStudentSingle(string email)
        {
            var s = c.Students.Include(o => o.ClassRoom).Single(p => p.Email == email);
            var i = mapper.Map<GetStudentsById>(s);
            return Ok(i);
        }

        [HttpGet("single-or-default/{email}")]
        public IActionResult GetStudentsingleOrDefault(string email)
        {
            var s = c.Students.Include(o => o.ClassRoom).SingleOrDefault(p => p.Email == email);
            if (s == null) return NotFound();

            var i = mapper.Map<GetStudentsById>(s);
            return Ok(i);
        }

        [HttpGet("at/{index}")]
        public IActionResult GetStudentAtIndex(int index)
        {
            var s = c.Students
                .OrderBy(s => s.Id)
                .AsEnumerable()
                .ElementAt(index);

            var i = mapper.Map<GetStudentsById>(s);
            return Ok(i);
        }

        [HttpGet("all-have-phone/{classRoomId}")]
        public IActionResult CheckPhone(int classRoomId)
        {
            var x = c.Students
                .Where(s => s.ClassRoomId == classRoomId)
                .All(s => !string.IsNullOrEmpty(s.PhoneNumber));

            return Ok(x);
        }

        [HttpGet("contains-classroom/{classRoomId}")]
        public IActionResult ContainClass(int classRoomId)
        {
            var list = c.Students.Select(s => s.ClassRoomId).ToList();
            var res = list.Contains(classRoomId);
            return Ok(res);
        }

        [HttpGet("select")]
        public IActionResult GetSelected(int classRoomId)
        {
            var res = c.Students
                .Where(s => s.ClassRoomId == classRoomId)
                .Select(s => new { s.Id, FullName = s.FirstName + " " + s.LastName })
                .ToList();

            return Ok(res);
        }

        [HttpGet("basic-info")]
        public IActionResult GetInfo(int classRoomId)
        {
            var res = c.Students
                .Where(s => s.ClassRoomId == classRoomId)
                .Select(s => new { s.Id, FullName = s.FirstName + " " + s.LastName, s.Email })
                .ToList();

            return Ok(res);
        }

        [HttpGet("order-by-name")]
        public IActionResult OrderName()
        {
            var s = c.Students.OrderBy(s => s.LastName).ToList();
            var dtos = mapper.Map<List<GetStudents>>(s);
            return Ok(dtos);
        }

        [HttpGet("order-by-grade-desc")]
        public IActionResult OrderGradeDesc(int classRoomId)
        {
            var s = c.Students
                .Where(s => s.ClassRoomId == classRoomId)
                .OrderByDescending(s => s.Enrollments.Max(e => e.Grade))
                .ToList();

            var dtos = mapper.Map<List<GetStudents>>(s);
            return Ok(dtos);
        }

        [HttpGet("order-by-classroom")]
        public IActionResult OrderClassThenName()
        {
            var s = c.Students
                .OrderBy(s => s.ClassRoomId)
                .ThenBy(s => s.LastName)
                .ToList();

            var dtos = mapper.Map<List<GetStudents>>(s);
            return Ok(dtos);
        }

        [HttpGet("order-by-classroom-grade")]
        public IActionResult OrderClassThenGrade()
        {
            var s = c.Students
                .OrderBy(s => s.ClassRoomId)
                .ThenByDescending(s => s.Enrollments.Max(e => e.Grade))
                .ToList();

            var dtos = mapper.Map<List<GetStudents>>(s);
            return Ok(dtos);
        }




    }
}
