using Microsoft.AspNetCore.Mvc;
using TaskManager.Backend.Data;
using TaskManager.Shared.Entities;

namespace TaskManager.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyTasksController : ControllerBase
    {
        private readonly DataContext _context;

        public MyTasksController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.MyTasks.ToList());
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var task = _context.MyTasks.FirstOrDefault(x => x.Id == id);
            if(task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public IActionResult Post(MyTask myTask)
        {
            _context.Add(myTask);
            _context.SaveChanges();
            return Ok(myTask);
        }

        [HttpPut]
        public IActionResult Put(MyTask myTask)
        {
            var task = _context.MyTasks.FirstOrDefault(x => x.Id == myTask.Id);
            if (task == null)
            {
                return NotFound();
            }

            task.Description = myTask.Description;
            task.Date = myTask.Date;
            task.IsComplete = myTask.IsComplete;

            _context.Update(task);
            _context.SaveChanges();
            
            return Ok(task);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var task = _context.MyTasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Remove(task);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
