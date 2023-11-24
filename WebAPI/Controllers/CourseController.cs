using Domain.ViewModels;
using Infrastructure.Services.Custom.CourseServices;
using Infrastructure.Services.Custom.StudentServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _service;
        private readonly ILogger<CourseController> _logger;

        public CourseController(ICourseService service, ILogger<CourseController> logger)
        {
            _service = service;
            _logger = logger;
        }


        [HttpGet(nameof(GetAll))]
        public async Task<ActionResult<CourseViewModel>> GetAll()
        {
            _logger.LogInformation("Getting All The Data ");
            var result = await _service.GetAll();

            if (result == null)
            {
                _logger.LogWarning("Course data was Not Found");
                return BadRequest("Course data was Not Found");
            }
            return Ok(result);
        }


        [HttpGet(nameof(GetById))]
        public async Task<ActionResult<CourseViewModel>> GetById(Guid id)
        {
            _logger.LogInformation("Getting All The Data By ID");
            var result = await _service.GetById(id);
            if (result == null)
            {
                _logger.LogWarning("Course data was Not Found");
                return BadRequest("Course Data Was not Found");
            }
            return Ok(result);
        }

        [HttpGet(nameof(GetByName))]
        public async Task<ActionResult<CourseViewModel>> GetByName(string name)
        {
            _logger.LogInformation("Getting All The Data By Name");
            var result = await _service.GetByName(name);
            if (result == null)
            {
                _logger.LogWarning("Course data was Not Found");
                return BadRequest("Course Data Was not Found");
            }
            return Ok(result);
        }

        /*[HttpGet(nameof(GetLast))]
        public async Task<ActionResult<CourseViewModel>> GetLast()
        {
            var result = await _service.GetLast();
            if (result == null)
            {
                return BadRequest("Student Data Was not Found");
            }
            return Ok(result);
        }*/


        [HttpPost(nameof(Insert))]
        public async Task<IActionResult> Insert([FromForm]CourseInsertModel CourseInsertModel)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Inserting The Data ");
                var result = await _service.Insert(CourseInsertModel);
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
        public async Task<IActionResult> Update([FromForm]CourseUpdateModel CourseUpdateModel)

        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Updating The Data ");
                var result = await _service.Update(CourseUpdateModel);
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
                _logger.LogInformation("Deleting The Data ");
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
    }
}
