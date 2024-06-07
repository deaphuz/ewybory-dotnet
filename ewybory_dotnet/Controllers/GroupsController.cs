using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ewybory_dotnet;
using ewybory_dotnet.Models;

namespace ewybory_dotnet.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GroupsController : Controller
    {
        private readonly eElectionContext _context;

        public GroupsController(eElectionContext context)
        {
            _context = context;
        }

        // GET: api/<GroupsController>/GetAll
        [HttpGet]
        public IEnumerable<Group> GetAll()
        {
            return _context.Groups.ToList();
        }

        // GET: api/<GroupsController>/GetGroup/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            return group;
        }

        // POST: api/<GroupsController>/AddGroup
        [HttpPost]
        public IActionResult AddGroup([FromBody] Group value)
        {
            try
            {
                _context.Groups.Add(value);
                _context.SaveChanges();
                return Ok(value);
            }
            catch (Exception ex)
            {
                return RedirectToAction("GetAll");
            }
        }

        // PUT: api/<GroupsController>/PutGroup/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup([FromBody] Group group, int id)
        {
            var old_group= await _context.Groups.FindAsync(id);

            if (id != old_group.GroupId)
            {
                return BadRequest();
            }

            group.GroupId = id;
            _context.Entry(old_group).CurrentValues.SetValues(group);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/<GroupsController>/DeleteGroup/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Index()
        {
            return View(await _context.Groups.ToListAsync());
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public IActionResult Create()
        {
            return View();
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,Name")] Group @group)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@group);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }
            return View(@group);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupId,Name")] Group @group)
        {
            if (id != @group.GroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.GroupId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@group);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @group = await _context.Groups.FindAsync(id);
            if (@group != null)
            {
                _context.Groups.Remove(@group);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.GroupId == id);
        }
    }
}
