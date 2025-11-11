using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webserver.Models;

namespace webserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class student : ControllerBase
    {
        private readonly BlazorTutorialContext _blazortutorialcontext;
        public student(BlazorTutorialContext blazortutorialcontext)
        {
            _blazortutorialcontext = blazortutorialcontext;
        }


        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var student = _blazortutorialcontext.Students.ToList();
            return Ok(student);
        }
    }
}
