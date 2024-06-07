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
    public class VotersController : Controller
    {
        private readonly eElectionContext _context;

        public VotersController(eElectionContext context)
        {
            _context = context;
        }

        // GET: api/<VotersController>/GetAll
        [HttpGet]
        public IEnumerable<Voter> GetAll()
        {
            return _context.Voters.ToList();
        }

        // GET: api/<VotersController>/GetVoter/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Voter>> GetVoter(int id)
        {
            var voter = await _context.Voters.FindAsync(id);

            if (voter == null)
            {
                return NotFound();
            }

            return voter;
        }

        // POST: api/<VotersController>/AddVoter
        [HttpPost]
        public IActionResult AddVoter([FromBody] Voter value)
        {
            try
            {
                _context.Voters.Add(value);
                _context.SaveChanges();
                return Ok(value);
            }
            catch (Exception ex)
            {
                return RedirectToAction("GetAll");
            }
        }

        // PUT: api/<VotersController>/PutVoter/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoter([FromBody] Voter voter, int id)
        {
            var old_voter = await _context.Voters.FindAsync(id);

            if (id != old_voter.VoterId)
            {
                return BadRequest();
            }

            voter.VoterId = id;
            _context.Entry(old_voter).CurrentValues.SetValues(voter);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoterExists(id))
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

        // DELETE: api/<VotersController>/DeleteVoter/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoter(int id)
        {
            var voter = await _context.Voters.FindAsync(id);
            if (voter == null)
            {
                return NotFound();
            }

            _context.Voters.Remove(voter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Index()
        {
            return View(await _context.Voters.ToListAsync());
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voter = await _context.Voters
                .FirstOrDefaultAsync(m => m.VoterId == id);
            if (voter == null)
            {
                return NotFound();
            }

            return View(voter);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public IActionResult Create()
        {
            return View();
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VoterId,PESEL,Name,Surname,Birth")] Voter voter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(voter);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voter = await _context.Voters.FindAsync(id);
            if (voter == null)
            {
                return NotFound();
            }
            return View(voter);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VoterId,PESEL,Name,Surname,Birth")] Voter voter)
        {
            if (id != voter.VoterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoterExists(voter.VoterId))
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
            return View(voter);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voter = await _context.Voters
                .FirstOrDefaultAsync(m => m.VoterId == id);
            if (voter == null)
            {
                return NotFound();
            }

            return View(voter);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voter = await _context.Voters.FindAsync(id);
            if (voter != null)
            {
                _context.Voters.Remove(voter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        private bool VoterExists(int id)
        {
            return _context.Voters.Any(e => e.VoterId == id);
        }
    }
}
