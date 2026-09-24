using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCHOOL_MANAGEMENT_API1.DTOS.Department;
using SCHOOL_MANAGEMENT_API1.DTOS.StudentDto;
using SCHOOL_MANAGEMENT_API1.DTOS.TeacherDtos;
using SCHOOL_MANAGEMENT_API1.Mapping.DeparmentMapping;
using SCHOOL_MANAGEMENT_API1.Models;

namespace SCHOOL_MANAGEMENT_API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly Context c;
        public DepartmentsController(Context c)
        {
            this.c = c;

            mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DepartmentProfile());
            }).CreateMapper();
        }

        [HttpGet]
        public ActionResult<List<Department>> GetTeachers()
        {
            var t = c.Departments.ToList();
            var tt = mapper.Map<List<GetDepartments>>(t);
            return Ok(tt);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var t = c.Departments.FirstOrDefault(s => s.Id == id);
            if (t == null)
            {
                return NotFound();
            }

            var h = mapper.Map<GetDeparmenttByID>(t);
            return Ok(h);
        }


        [HttpPost]
        public IActionResult CreaeDepartment(CreateDeparmentDto y)
        {
            

            var e = mapper.Map<Department>(y);
            c.Departments.Add(e);
            c.SaveChanges();
            return Created("", e);

        }


        [HttpPut("{id}")]
        public IActionResult UpdateDeparment(int id, UpdateDeparmentDto dto)
        {
            var ff = c.Departments.Find(id);
            if (ff == null) return NotFound();
            mapper.Map(dto, ff);
            c.SaveChanges();
            return Ok(ff);
        }


        [HttpPatch("{id}")]
        public IActionResult PatchDepartment(int id, PatchDepartmentDto dto)
        {
            var tt = c.Departments.Find(id);
            if (tt == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.Name))
                tt.Name = dto.Name;

            c.SaveChanges();
            return Ok(tt);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            var tt = c.Departments.Find(id);
            if (tt == null) return NotFound(" not found ");

            c.Departments.Remove(tt);
            c.SaveChanges();

            return NoContent();
        }































        [HttpGet("{id}/has-teacher")]
        public IActionResult CheckTeacher(int id)
        {
            var res = c.Students.Any(s => s.Id == id);
            return Ok(res);
        }

        [HttpGet("{id}/all-students")]
        public IActionResult GetAllDepStudents(int id)
        {
            var res = c.Departments
                .Where(d => d.Id == id)
                .SelectMany(d => d.Teachers)
                .ToList();

            return Ok(res);
        }
    }
}
