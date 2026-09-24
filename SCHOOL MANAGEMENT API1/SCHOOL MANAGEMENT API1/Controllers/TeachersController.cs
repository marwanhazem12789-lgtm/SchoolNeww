using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCHOOL_MANAGEMENT_API1.DTOS.StudentDto;
using SCHOOL_MANAGEMENT_API1.DTOS.TeacherDtos;
using SCHOOL_MANAGEMENT_API1.Mapping.TeacherMapping;
using SCHOOL_MANAGEMENT_API1.Models;

namespace SCHOOL_MANAGEMENT_API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly Context c;
        public TeachersController(Context c)
        {
            this.c = c;

            mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TeacherProfile());
            }).CreateMapper();
        }

        [HttpGet]
        public ActionResult<List<Teacher>> GetTeachers()
        {
            var t = c.Teachers.ToList();
            var tt = mapper.Map<List<GetTeachers>>(t);
            return Ok(tt);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var t = c.Teachers.FirstOrDefault(s => s.Id == id);
            if (t == null)
            {
                return NotFound();
            }

            var h = mapper.Map<GetTeacherByid>(t);
            return Ok(h);
        }




        [HttpPost]
        public IActionResult CreaeTeacher(CraeteeacherDto y)
        {
            var i = c.Departments.Any(c => c.Id == y.DepartmentId);
            if (i == false)
            {
                return BadRequest("Departemnt Id is not valid");
            }

            var e = mapper.Map<Teacher>(y);
            c.Teachers.Add(e);
            c.SaveChanges();
            return Created("", e);

        }



        [HttpPatch("{id}")]
        public IActionResult PatchTeacher(int id, PatchTeacher dto)
        {
            var tt = c.Teachers.Find(id);
            if (tt == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.Email))
                tt.Email = dto.Email;



            c.SaveChanges();
            return Ok(tt);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteTeacher(int id)
        {
            var tt = c.Teachers.Find(id);
            if (tt == null) return NotFound(" not found ");

            c.Teachers.Remove(tt);
            c.SaveChanges();

            return NoContent();
        }




        [HttpPut("{id}")]
        public IActionResult UpdateTeacher(int id, UpdateTeacher dto)
        {
            var tt = c.Teachers.Find(id);
            if (tt == null) return NotFound();
            mapper.Map(dto, tt);
            c.SaveChanges();
            return Ok(tt);
        }
    }
}
