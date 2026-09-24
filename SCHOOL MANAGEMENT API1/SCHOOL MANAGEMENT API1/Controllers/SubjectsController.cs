using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCHOOL_MANAGEMENT_API1.DTOS.SubjectDtos;
using SCHOOL_MANAGEMENT_API1.DTOS.TeacherDtos;
using SCHOOL_MANAGEMENT_API1.Mapping.SubjectMapping;
using SCHOOL_MANAGEMENT_API1.Models;

namespace SCHOOL_MANAGEMENT_API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly Context c;
        public SubjectsController(Context c)
        {
            this.c = c;

            mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SubjectProfile());
            }).CreateMapper();
        }


        [HttpGet]
        public ActionResult<List<Subject>> Getsubjects()
        {
            var t = c.Subjects.ToList();
            var tt = mapper.Map<List<GetSubjects>>(t);
            return Ok(tt);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var t = c.Subjects.FirstOrDefault(s => s.Id == id);
            if (t == null)
            {
                return NotFound();
            }

            var h = mapper.Map<GetSubjects>(t);
            return Ok(h);
        }



        [HttpPatch("{id}")]
        public IActionResult Patchsubject(int id, PatchSubject dto)
        {
            var tt = c.Subjects.Find(id);
            if (tt == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.Name))
                tt.Name = dto.Name;



            c.SaveChanges();
            return Ok(tt);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteSubject(int id)
        {
            var tt = c.Subjects.Find(id);
            if (tt == null) return NotFound(" not found ");

            c.Subjects.Remove(tt);
            c.SaveChanges();

            return NoContent();
        }




        [HttpPut("{id}")]
        public IActionResult UpdateSubject(int id, UpdateSubject dto)
        {
            var tt = c.Subjects.Find(id);
            if (tt == null) return NotFound();
            mapper.Map(dto, tt);
            c.SaveChanges();
            return Ok(tt);
        }



        [HttpPost]
        public IActionResult Creaesubject(CreateSubject y)
        {
            var i = c.Teachers.Any(c => c.Id == y.TeacherId);
            if (i == false)
            {
                return BadRequest("Teacher Id is not valid");
            }

            var e = mapper.Map<Subject>(y);
            c.Subjects.Add(e);
            c.SaveChanges();
            return Created("", e);

        }
    }
}
