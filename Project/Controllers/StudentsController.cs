using Core.Dtos;
using Core.Services;
using DataLayer.Dtos;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private StudentService studentService { get; set; }


        public StudentsController(StudentService studentService)
        {
            this.studentService = studentService;
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("/add")]
        public IActionResult Add(StudentAddDto payload)
        {
            var result = studentService.AddStudent(payload);

            if (result == null)
            {
                return BadRequest("Student cannot be added");
            }

            return Ok(result);
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("/get-all")]
        public ActionResult<List<Student>> GetAll()
        {
            var results = studentService.GetAll();

            return Ok(results);
        }

        [Authorize(Roles = "Student,Teacher")]
        [HttpGet("/get/{studentId}")]
        public ActionResult<Student> GetById(int studentId)
        {
            var result = studentService.GetById(studentId);

            if(result == null)
            {
                return BadRequest("Student not fount");
            }

            return Ok(result);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPatch("edit-name")]
        public ActionResult<bool> GetById([FromBody] StudentUpdateDto studentUpdateModel)
        {
            var result = studentService.EditName(studentUpdateModel);

            if (!result)
            {
                return BadRequest("Student could not be updated.");
            }

            return result;
        }

        [Authorize(Roles = "Student,Teacher")]
        [HttpPost("grades-by-course")]
        public ActionResult<GradesByStudent> Get_CourseGrades_ByStudentId([FromBody] StudentGradesRequest request)
        {
            var result = studentService.GetGradesById(request.StudentId, request.CourseType);
            return Ok(result);
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("{classId}/class-students")]
        public IActionResult GetClassStudents([FromRoute] int classId)
        {
            var results = studentService.GetClassStudents(classId);

            return Ok(results);
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("grouped-students")]
        public IActionResult GetGroupedStudents()
        {
            var results = studentService.GetGroupedStudents();

            return Ok(results);
        }
    }
}