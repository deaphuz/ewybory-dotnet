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
    public class OrganizersController : Controller
    {
        private readonly eElectionContext _context;

        public OrganizersController(eElectionContext context)
        {
            _context = context;
        }

        // GET: api/<OrganizersController>/GetAll
        [HttpGet]
        public IEnumerable<Organizer> GetAll()
        {
            return _context.Organizers.ToList();
        }

        // GET: api/<OrganizersController>/GetOrganizer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Organizer>> GetOrganizer(int id)
        {
            var organizer = await _context.Organizers.FindAsync(id);

            if (organizer == null)
            {
                return NotFound();
            }

            return organizer;
        }

        // POST: api/<OrganizersController>/AddOrganizer
        [HttpPost]
        public IActionResult AddOrganizer([FromBody] Organizer value)
        {
            try
            {
                _context.Organizers.Add(value);
                _context.SaveChanges();
                return Ok(value);
            }
            catch (Exception ex)
            {
                return RedirectToAction("GetAll");
            }
        }

        // PUT: api/<OrganizersController>/PutOrganizer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizer([FromBody] Organizer organizer, int id)
        {
            var old_organizer = await _context.Organizers.FindAsync(id);

            if (id != old_organizer.OrganizerId)
            {
                return BadRequest();
            }

            organizer.OrganizerId = id;
            _context.Entry(old_organizer).CurrentValues.SetValues(organizer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizerExists(id))
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

        // DELETE: api/<OrganizersController>/DeleteOrganizer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizer(int id)
        {
            var organizer = await _context.Organizers.FindAsync(id);
            if (organizer == null)
            {
                return NotFound();
            }

            _context.Organizers.Remove(organizer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Index()
        {
            return View(await _context.Organizers.ToListAsync());
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizer = await _context.Organizers
                .FirstOrDefaultAsync(m => m.OrganizerId == id);
            if (organizer == null)
            {
                return NotFound();
            }

            return View(organizer);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public IActionResult Create()
        {
            return View();
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrganizerId,Name,Surname,Birth,Password")] Organizer organizer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organizer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organizer);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizer = await _context.Organizers.FindAsync(id);
            if (organizer == null)
            {
                return NotFound();
            }
            return View(organizer);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrganizerId,Name,Surname,Birth,Password")] Organizer organizer)
        {
            if (id != organizer.OrganizerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organizer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizerExists(organizer.OrganizerId))
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
            return View(organizer);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizer = await _context.Organizers
                .FirstOrDefaultAsync(m => m.OrganizerId == id);
            if (organizer == null)
            {
                return NotFound();
            }

            return View(organizer);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var organizer = await _context.Organizers.FindAsync(id);
            if (organizer != null)
            {
                _context.Organizers.Remove(organizer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        private bool OrganizerExists(int id)
        {
            return _context.Organizers.Any(e => e.OrganizerId == id);
        }
    }
}
