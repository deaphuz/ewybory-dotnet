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
    public class GroupElectionsController : Controller
    {
        private readonly eElectionContext _context;

        public GroupElectionsController(eElectionContext context)
        {
            _context = context;
        }

        // GET: api/<GroupElectionsController>/GetAll
        [HttpGet]
        public IEnumerable<GroupElection> GetAll()
        {
            return _context.GroupElections.ToList();
        }

        // GET: api/<GroupElectionsController>/GetGroupElection/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupElection>> GetGroupElection(int id)
        {
            var groupElection = await _context.GroupElections.FindAsync(id);

            if (groupElection == null)
            {
                return NotFound();
            }

            return groupElection;
        }

        // POST: api/<GroupElectionsController>/AddGroupElection
        [HttpPost]
        public IActionResult AddGroupElection([FromBody] GroupElection value)
        {
            try
            {
                _context.GroupElections.Add(value);
                _context.SaveChanges();
                return Ok(value);
            }
            catch (Exception ex)
            {
                return RedirectToAction("GetAll");
            }
        }

        // PUT: api/<GroupElectionsController>/PutGroupElection/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroupElection([FromBody] GroupElection groupElection, int id)
        {
            var old_groupElection = await _context.GroupElections.FindAsync(id);

            if (id != old_groupElection.GroupElectionId)
            {
                return BadRequest();
            }

            groupElection.GroupElectionId = id;
            _context.Entry(old_groupElection).CurrentValues.SetValues(groupElection);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupElectionExists(id))
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

        // DELETE: api/<GroupElectionsController>/DeleteGroupElection/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupElection(int id)
        {
            var groupElection = await _context.GroupElections.FindAsync(id);
            if (groupElection == null)
            {
                return NotFound();
            }

            _context.GroupElections.Remove(groupElection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Index()
        {
            var eElectionContext = _context.GroupElections.Include(g => g.Election).Include(g => g.Group);
            return View(await eElectionContext.ToListAsync());
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupElection = await _context.GroupElections
                .Include(g => g.Election)
                .Include(g => g.Group)
                .FirstOrDefaultAsync(m => m.GroupElectionId == id);
            if (groupElection == null)
            {
                return NotFound();
            }

            return View(groupElection);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public IActionResult Create()
        {
            ViewData["ElectionId"] = new SelectList(_context.Elections, "ElectionId", "Surname");
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "Name");
            return View();
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupElectionId,ElectionId,GroupId")] GroupElection groupElection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupElection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ElectionId"] = new SelectList(_context.Elections, "ElectionId", "Surname", groupElection.ElectionId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "Name", groupElection.GroupId);
            return View(groupElection);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupElection = await _context.GroupElections.FindAsync(id);
            if (groupElection == null)
            {
                return NotFound();
            }
            ViewData["ElectionId"] = new SelectList(_context.Elections, "ElectionId", "Surname", groupElection.ElectionId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "Name", groupElection.GroupId);
            return View(groupElection);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupElectionId,ElectionId,GroupId")] GroupElection groupElection)
        {
            if (id != groupElection.GroupElectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupElection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupElectionExists(groupElection.GroupElectionId))
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
            ViewData["ElectionId"] = new SelectList(_context.Elections, "ElectionId", "Surname", groupElection.ElectionId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "Name", groupElection.GroupId);
            return View(groupElection);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupElection = await _context.GroupElections
                .Include(g => g.Election)
                .Include(g => g.Group)
                .FirstOrDefaultAsync(m => m.GroupElectionId == id);
            if (groupElection == null)
            {
                return NotFound();
            }

            return View(groupElection);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupElection = await _context.GroupElections.FindAsync(id);
            if (groupElection != null)
            {
                _context.GroupElections.Remove(groupElection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        private bool GroupElectionExists(int id)
        {
            return _context.GroupElections.Any(e => e.GroupElectionId == id);
        }
    }
}
