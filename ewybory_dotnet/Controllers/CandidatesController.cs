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
    public class CandidatesController : Controller
    {
        private readonly eElectionContext _context;

        public CandidatesController(eElectionContext context)
        {
            _context = context;
        }

        // GET: api/<CandidatesController>/GetAll
        [HttpGet]
        public IEnumerable<Candidate> GetAll()
        {
            return _context.Candidates.ToList();
        }

        // GET: api/<CandidatesController>/GetCandidate/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Candidate>> GetCandidate(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);

            if (candidate == null)
            {
                return NotFound();
            }

            return candidate;
        }

        // POST: api/<CandidatesController>/AddCandidate
        [HttpPost]
        public IActionResult AddCandidate([FromBody]Candidate value)
        {
            try
            {
                _context.Candidates.Add(value);
                _context.SaveChanges();
                return Ok(value);
            }
            catch (Exception ex) 
            {
                return RedirectToAction("GetAll");
            }
        }

        // PUT: api/<CandidatesController>/PutCandidate/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidate([FromBody]Candidate candidate, int id)
        {
            var old_candidate = await _context.Candidates.FindAsync(id);

            if (id != old_candidate.CandidateId)
            {
                return BadRequest();
            }

            candidate.CandidateId = id;
            _context.Entry(old_candidate).CurrentValues.SetValues(candidate);            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateExists(id))
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

        // DELETE: api/<CandidatesController>/DeleteCandidate/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Index()
        {
            return View(await _context.Candidates.ToListAsync());
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidates
                .FirstOrDefaultAsync(m => m.CandidateId == id);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public IActionResult Create()
        {
            return View();
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CandidateId,Name,Surname,Birth")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(candidate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(candidate);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }
            return View(candidate);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CandidateId,Name,Surname,Birth")] Candidate candidate)
        {
            if (id != candidate.CandidateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidateExists(candidate.CandidateId))
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
            return View(candidate);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidates
                .FirstOrDefaultAsync(m => m.CandidateId == id);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate != null)
            {
                _context.Candidates.Remove(candidate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        private bool CandidateExists(int id)
        {
            return _context.Candidates.Any(e => e.CandidateId == id);
        }
    }
}
