using Core.Dtos;
using Core.Services;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/grades")]
    public class GradeController : ControllerBase
    {
        private GradeService gradeService { get; set; }

        public GradeController (GradeService gradeService)
        {
            this.gradeService = gradeService;
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("add-grade")]
        public IActionResult Add(GradeAddDto payload)
        {
            var result = gradeService.AddGrade(payload);

            if (result == null)
            {
                return BadRequest("Student cannot be added");
            }

            return Ok(result);
        }

        [Authorize(Roles = "Student,Teacher")]
        [HttpGet("/get-grades/{studentId}")]
        public ActionResult<Student> GetById(int studentId)
        {
            var result = gradeService.GetStudentGradesOrdered(studentId);

            if (result == null)
            {
                return BadRequest("Student not fount");
            }

            return Ok(result);
        }
        [Authorize(Roles = "Teacher")]
        [HttpGet("/get-all-grades")]
        public ActionResult<Student> GetAll()
        {
            var result = gradeService.GetAll();

            if (result == null)
            {
                return BadRequest("Student not fount");
            }

            return Ok(result);
        }
    }
}