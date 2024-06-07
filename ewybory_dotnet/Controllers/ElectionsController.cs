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
    public class ElectionsController : Controller
    {
        private readonly eElectionContext _context;

        public ElectionsController(eElectionContext context)
        {
            _context = context;
        }

        // GET: api/<ElectionsController>/GetAll
        [HttpGet]
        public IEnumerable<Election> GetAll()
        {
            return _context.Elections.ToList();
        }

        // GET: api/<ElectionsController>/GetElection/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Election>> GetElection(int id)
        {
            var election = await _context.Elections.FindAsync(id);

            if (election== null)
            {
                return NotFound();
            }

            return election;
        }

        // POST: api/<ElectionsController>/AddElection
        [HttpPost]
        public IActionResult AddElection([FromBody] Election value)
        {
            try
            {
                _context.Elections.Add(value);
                _context.SaveChanges();
                return Ok(value);
            }
            catch (Exception ex) 
            {
                return RedirectToAction("GetAll");
            }
        }

        // PUT: api/<ElectionsController>/AddElection
        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutElection([FromBody] Election election, int id)
        {
            var old_election = await _context.Elections.FindAsync(id);

            if (id != old_election.ElectionId)
            {
                return BadRequest();
            }

            election.ElectionId = id;
            _context.Entry(old_election).CurrentValues.SetValues(election);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectionExists(election.ElectionId))
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

        // DELETE: api/Elections/DeleteElection/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElection(int id)
        {
            var election = await _context.Elections.FindAsync(id);
            if (election == null)
            {
                return NotFound();
            }

            _context.Elections.Remove(election);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Index()
        {
            return View(await _context.Elections.ToListAsync());
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var election = await _context.Elections
                .FirstOrDefaultAsync(m => m.ElectionId == id);
            if (election == null)
            {
                return NotFound();
            }

            return View(election);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public IActionResult Create()
        {
            return View();
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ElectionId,Type,Surname,StartDate,EndDate,MaxVotesPerVoter,VoterTournout")] Election election)
        {
            if (ModelState.IsValid)
            {
                _context.Add(election);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(election);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var election = await _context.Elections.FindAsync(id);
            if (election == null)
            {
                return NotFound();
            }
            return View(election);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ElectionId,Type,Surname,StartDate,EndDate,MaxVotesPerVoter,VoterTournout")] Election election)
        {
            if (id != election.ElectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(election);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectionExists(election.ElectionId))
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
            return View(election);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var election = await _context.Elections
                .FirstOrDefaultAsync(m => m.ElectionId == id);
            if (election == null)
            {
                return NotFound();
            }

            return View(election);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var election = await _context.Elections.FindAsync(id);
            if (election != null)
            {
                _context.Elections.Remove(election);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        private bool ElectionExists(int id)
        {
            return _context.Elections.Any(e => e.ElectionId == id);
        }
    }
}
