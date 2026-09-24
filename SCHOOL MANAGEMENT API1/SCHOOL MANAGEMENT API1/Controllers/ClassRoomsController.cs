using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCHOOL_MANAGEMENT_API1.DTOS.ClassRoomDto;
using SCHOOL_MANAGEMENT_API1.DTOS.Department;
using SCHOOL_MANAGEMENT_API1.Mapping.ClassRoomMapping;
using SCHOOL_MANAGEMENT_API1.Models;

namespace SCHOOL_MANAGEMENT_API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassRoomsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly Context c;
        public ClassRoomsController(Context c)
        {
            this.c = c;

            mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ClassRoomProfile());
            }).CreateMapper();
        }

        [HttpGet]
        public ActionResult<List<ClassRoom>> GetClassRoooms()
        {
            var t = c.classRooms.ToList();
            var tt = mapper.Map<List<GetCalssRoooms>>(t);
            return Ok(tt);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var t = c.classRooms.FirstOrDefault(s => s.Id == id);
            if (t == null)
            {
                return NotFound();
            }

            var h = mapper.Map<GetClassRoomsById>(t);
            return Ok(h);
        }


        [HttpPost]
        public IActionResult CreaeClassroom(CreateClassRoom y)
        {


            var e = mapper.Map<ClassRoom>(y);
            c.classRooms.Add(e);
            c.SaveChanges();
            return Created("", e);

        }


        [HttpPut("{id}")]
        public IActionResult UpdateClassRoom(int id, UpdateClassRooom dto)
        {
            var ff = c.classRooms.Find(id);
            if (ff == null) return NotFound();
            mapper.Map(dto, ff);
            c.SaveChanges();
            return Ok(ff);
        }


        [HttpPatch("{id}")]
        public IActionResult PatchClassRoom(int id, PatchClassRoom dto)
        {
            var tt = c.classRooms.Find(id);
            if (tt == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.Name))
                tt.Name = dto.Name;

            c.SaveChanges();
            return Ok(tt);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteClassRoom(int id)
        {
            var tt = c.classRooms.Find(id);
            if (tt == null) return NotFound(" not found ");

            c.classRooms.Remove(tt);
            c.SaveChanges();

            return NoContent();
        }
    }
}
