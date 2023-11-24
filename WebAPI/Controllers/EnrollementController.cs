using Domain.Models;
using Domain.ViewModels;
using Infrastructure.Context;
using Infrastructure.Services.Custom.CourseServices;
using Infrastructure.Services.Custom.EnrollementServices;
using Infrastructure.Services.Custom.StudentServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollementController : ControllerBase
    {
        private readonly IEnrollementService _service;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly MainDbContext _context;
        private readonly ILogger<EnrollementController> _logger;

        public EnrollementController(ILogger<EnrollementController> logger,IEnrollementService service, IStudentService studentService, ICourseService courseService, MainDbContext context)
        {
            _logger = logger;
            _service = service;
            _studentService = studentService;
            _courseService = courseService;
            _context = context;
        }


        [HttpGet(nameof(GetAll))]
        public async Task<ActionResult<EnrollementViewModel>> GetAll()
        {
            _logger.LogInformation("Getting All Data ");
            var result = await _service.GetAll();

            if (result == null)
            {
                _logger.LogWarning("Enrollement data was Not Found");
                return BadRequest("Enrollement data was Not Found");
            }
            return Ok(result);
        }


        [HttpGet(nameof(GetById))]
        public async Task<ActionResult<EnrollementViewModel>> GetById(Guid id)
        {
            _logger.LogInformation("Getting All Data By ID");
            var result = await _service.GetById(id);
            if (result == null)
            {
                _logger.LogWarning("Enrollement data was Not Found");
                return BadRequest("Enrollement Data Was not Found");
            }
            return Ok(result);
        }

        /*[HttpGet(nameof(GetByName))]
        public async Task<ActionResult<EnrollementViewModel>> GetByName(string name)
        {
            var result = await _service.GetByName(name);
            if (result == null)
            {
                return BadRequest("Student Data Was not Found");
            }
            return Ok(result);
        }*/

        /*[HttpGet(nameof(GetLast))]
        public async Task<ActionResult<StudentViewModel>> GetLast()
        {
            var result = await _service.GetLast();
            if (result == null)
            {
                return BadRequest("Student Data Was not Found");
            }
            return Ok(result);
        }*/


        [HttpPost(nameof(Insert))]
        public async Task<IActionResult> Insert([FromForm] EnrollementInsertModel EnrollementInsertModel)
        {
            if (ModelState.IsValid)
            {
                Student student = await _studentService.Find(x => x.Id == EnrollementInsertModel.StudentId);
                if(student != null)
                {
                    Course course = await _courseService.Find(x => x.Id == EnrollementInsertModel.CourseId);
                    if (course != null)
                    {
                        _logger.LogInformation("Inserting Data....!");
                        var result = await _service.Insert(EnrollementInsertModel);
                        if (result == true)
                        {
                            _logger.LogInformation("data Inserted Successfully.....!");
                            return Ok("data Inserted Successfully.....!");
                        }
                        else
                        {
                            _logger.LogWarning("Something Went Wrong.....!");
                            return BadRequest("Something Went Wrong.....!");
                        }
                    }
                    else
                        return BadRequest("Course Id is not found ");
                }
                else
                    return BadRequest("Course Id is not found ");

            }
            else
            {
                return BadRequest("Model State Is not valid...!");
            }
        }


        [HttpPut(nameof(Update))]
        public async Task<IActionResult> Update([FromForm] EnrollementUpdateModel EnrollementUpdateModel)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Updating Data...!");
                var result = await _service.Update(EnrollementUpdateModel);
                if (result == true)
                {
                    _logger.LogInformation("Data Updated Successfully ...... !");
                    return Ok("Data Updated Successfully ...... !");
                }
                else
                {
                    _logger.LogWarning("Something Went Wrong.....!");
                    return BadRequest("Something went Wrong...... !");
                }
            }
            else
            {
                return BadRequest("ModelState is Not valid....!");
            }
        }


        [HttpDelete(nameof(Delete))]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                _logger.LogInformation("Deleting Data...!");
                var result = await _service.Delete(id);
                if (result == true)
                {
                    _logger.LogInformation("Data Deleted Successfully....!");
                    return Ok("Data Deleted Successfully....!");
                }
                else
                {
                    _logger.LogWarning("Something Went Wrong.....!");
                    return BadRequest("Somthing Went Wrong......!");
                }
            }
            else
            {
                return BadRequest("Id was not Found");
            }
        }


        [HttpGet(nameof(GetStudentById))]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var enrollment = await _context.Enrollments.FirstOrDefaultAsync(e => e.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }
            var student = _context.Students.FirstOrDefault(s => s.Id == enrollment.StudentId);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
    }
}
