using Domain.ViewModels;
using Infrastructure.Context;
using Infrastructure.Services.Custom.StudentServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly ILogger<StudentController> _logger;
        private readonly MainDbContext _context;

        public StudentController(ILogger<StudentController> logger,IStudentService service, MainDbContext context)
        {
            _logger = logger;
            _service = service;
            _context = context;
        }

       
        [HttpGet(nameof(GetAll))]
        public async Task<ActionResult<StudentViewModel>> GetAll()
        {
            _logger.LogInformation("Getting All Values Of Students .... !");
            var result = await _service.GetAll();

            if (result == null)
            {
                _logger.LogWarning("Student data was Not Found");
                return BadRequest("Student data was Not Found");
            }
            return Ok(result);
        }

        
        [HttpGet(nameof(GetById))]
        public async Task<ActionResult<StudentViewModel>> GetById(Guid id)
        {
            _logger.LogInformation("Getting All Values Of Students By The ID .... !");
            var result = await _service.GetById(id);
            if (result == null)
            {
                _logger.LogWarning("Student data was Not Found");
                return BadRequest("Student Data Was not Found");
            }
            return Ok(result);
        }

        [HttpGet(nameof(GetByName))]
        public async Task<ActionResult<StudentViewModel>> GetByName(string name)
        {
            _logger.LogInformation("Getting All Values Of Students By the Name.... !");
            var result = await _service.GetByName(name);
            if (result == null)
            {
                _logger.LogWarning("Student data was Not Found");
                return BadRequest("Student Data Was not Found");
            }
            return Ok(result);
        }

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
        public async Task<IActionResult> Insert([FromForm] StudentInsertModel StudentInsertModel)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Inserting Values Of Students .... !");
                var result = await _service.Insert(StudentInsertModel);
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
            {
                return BadRequest("Model State Is not valid...!");
            }
        }

        
        [HttpPut(nameof(Update))]
        public async Task<IActionResult> Update([FromForm] StudentUpdateModel StudentUpdateModel)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Updating Values Of Students .... !");
                var result = await _service.Update(StudentUpdateModel);
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
                _logger.LogInformation("Deleting Values Of Students .... !");
                var result = await _service.Delete(id);
                if (result == true)
                {
                    _logger.LogInformation("Data Deleted Successfully....!");
                    return Ok("Data Deleted Successfully....!");
                }
                else
                {
                    _logger.LogWarning("Somthing Went Wrong......!");
                    return BadRequest("Somthing Went Wrong......!");
                }
            }
            else
            {
                return BadRequest("Id was not Found");
            }
        }


        [HttpGet(nameof(GetCourseByFName))]
        public async Task<IActionResult> GetCourseByFName(string name)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.FirstName.ToLower().Trim() == name.ToLower().Trim());
            if (student == null)
            {
                return BadRequest("Something Went Wrong...!");
            }
            
            var enrollment = await _context.Enrollments.Where(e => e.StudentId == student.Id).ToListAsync();
            return Ok(enrollment);
        }

        [HttpGet(nameof(GetEnrollmentDateByStudentId))]
        public async Task<IActionResult> GetEnrollmentDateByStudentId(Guid id)
        {
            var date = await _context.Enrollments.Where(x => x.StudentId == id).Select(e => e.EnrollmentDate).ToListAsync();
            if (date == null)
            {
                return BadRequest("Something Went Wrong...!");
            }

            return Ok(date);
        }
    }
}
