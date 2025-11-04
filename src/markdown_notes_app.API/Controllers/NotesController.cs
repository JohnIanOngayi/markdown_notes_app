using markdown_notes_app.Core.Entities;
using markdown_notes_app.Core.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;

namespace markdown_notes_app.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly ILoggerManager<NotesController> _loggerManager;

        public NotesController(ILoggerManager<NotesController> logger)
        {
            _loggerManager = logger;
        }

        public IActionResult Index()
        {
            return View(); 
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok();
        }

        [HttpGet("health")]
        public IActionResult GetHealth()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(Note note)
        {
            return Ok();
        }

        [HttpPost("{id}/{action}")]
        public IActionResult Post(Guid id, string action)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Note note)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok();
        }
    }
}
