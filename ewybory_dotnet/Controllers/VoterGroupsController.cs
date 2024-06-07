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
    public class VoterGroupsController : Controller
    {
        private readonly eElectionContext _context;

        public VoterGroupsController(eElectionContext context)
        {
            _context = context;
        }

        // GET: api/<VoterGroupsController>/GetAll
        [HttpGet]
        public IEnumerable<VoterGroup> GetAll()
        {
            return _context.VoterGroups.ToList();
        }

        // GET: api/<VoterGroupsController>/GetVoterGroup/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<VoterGroup>> GetVoterGroup(int id)
        {
            var voterGroup= await _context.VoterGroups.FindAsync(id);

            if (voterGroup == null)
            {
                return NotFound();
            }

            return voterGroup;
        }

        // POST: api/<VoterGroupsController>/AddVoterGroup
        [HttpPost]
        public IActionResult AddVoterGroup([FromBody] VoterGroup value)
        {
            try
            {
                _context.VoterGroups.Add(value);
                _context.SaveChanges();
                return Ok(value);
            }
            catch (Exception ex)
            {
                return RedirectToAction("GetAll");
            }
        }

        // PUT: api/<VoterGroupsController>/PutVoterGroup/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoterGroup([FromBody] VoterGroup voterGroup, int id)
        {
            var old_voterGroup = await _context.VoterGroups.FindAsync(id);

            if (id != old_voterGroup.VoterGroupId)
            {
                return BadRequest();
            }

            voterGroup.VoterGroupId = id;
            _context.Entry(old_voterGroup).CurrentValues.SetValues(voterGroup);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoterGroupExists(id))
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

        // DELETE: api/<VoterGroupsController>/DeleteVoterGroup/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoterGroup(int id)
        {
            var voterGroup = await _context.VoterGroups.FindAsync(id);
            if (voterGroup == null)
            {
                return NotFound();
            }

            _context.VoterGroups.Remove(voterGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Index()
        {
            var eElectionContext = _context.VoterGroups.Include(v => v.Group).Include(v => v.Voter);
            return View(await eElectionContext.ToListAsync());
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voterGroup = await _context.VoterGroups
                .Include(v => v.Group)
                .Include(v => v.Voter)
                .FirstOrDefaultAsync(m => m.VoterGroupId == id);
            if (voterGroup == null)
            {
                return NotFound();
            }

            return View(voterGroup);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "Name");
            ViewData["VoterId"] = new SelectList(_context.Voters, "VoterId", "Name");
            return View();
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VoterGroupId,VoterId,GroupId")] VoterGroup voterGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voterGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "Name", voterGroup.GroupId);
            ViewData["VoterId"] = new SelectList(_context.Voters, "VoterId", "Name", voterGroup.VoterId);
            return View(voterGroup);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voterGroup = await _context.VoterGroups.FindAsync(id);
            if (voterGroup == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "Name", voterGroup.GroupId);
            ViewData["VoterId"] = new SelectList(_context.Voters, "VoterId", "Name", voterGroup.VoterId);
            return View(voterGroup);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VoterGroupId,VoterId,GroupId")] VoterGroup voterGroup)
        {
            if (id != voterGroup.VoterGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voterGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoterGroupExists(voterGroup.VoterGroupId))
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
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "Name", voterGroup.GroupId);
            ViewData["VoterId"] = new SelectList(_context.Voters, "VoterId", "Name", voterGroup.VoterId);
            return View(voterGroup);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voterGroup = await _context.VoterGroups
                .Include(v => v.Group)
                .Include(v => v.Voter)
                .FirstOrDefaultAsync(m => m.VoterGroupId == id);
            if (voterGroup == null)
            {
                return NotFound();
            }

            return View(voterGroup);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voterGroup = await _context.VoterGroups.FindAsync(id);
            if (voterGroup != null)
            {
                _context.VoterGroups.Remove(voterGroup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        private bool VoterGroupExists(int id)
        {
            return _context.VoterGroups.Any(e => e.VoterGroupId == id);
        }
    }
}
