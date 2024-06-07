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
    public class ResultsController : Controller
    {
        private readonly eElectionContext _context;

        public ResultsController(eElectionContext context)
        {
            _context = context;
        }

        // GET: api/<ResultsController>/GetAll
        [HttpGet]
        public IEnumerable<Result> GetAll()
        {
            return _context.Results.ToList();
        }

        // GET: api/<ResultsController>/GetResult/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetResult(int id)
        {
            var result= await _context.Results.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // POST: api/<ResultsController>/AddResult
        [HttpPost]
        public IActionResult AddResult([FromBody] Result value)
        {
            try
            {
                _context.Results.Add(value);
                _context.SaveChanges();
                return Ok(value);
            }
            catch (Exception ex)
            {
                return RedirectToAction("GetAll");
            }
        }

        // PUT: api/<ResultsController>/PutResult/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResult([FromBody] Result result, int id)
        {
            var old_result = await _context.Results.FindAsync(id);

            if (id != old_result.ResultId)
            {
                return BadRequest();
            }

            result.ResultId = id;
            _context.Entry(old_result).CurrentValues.SetValues(result);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(id))
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

        // DELETE: api/<ResultsController>/DeleteResult/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            var result = await _context.Results.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            _context.Results.Remove(result);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Index()
        {
            var eElectionContext = _context.Results.Include(r => r.Candidate).Include(r => r.Election);
            return View(await eElectionContext.ToListAsync());
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .Include(r => r.Candidate)
                .Include(r => r.Election)
                .FirstOrDefaultAsync(m => m.ResultId == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public IActionResult Create()
        {
            ViewData["CandidateId"] = new SelectList(_context.Candidates, "CandidateId", "Name");
            ViewData["ElectionId"] = new SelectList(_context.Elections, "ElectionId", "Surname");
            return View();
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResultId,ElectionId,CandidateId,NumVotes")] Result result)
        {
            if (ModelState.IsValid)
            {
                _context.Add(result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CandidateId"] = new SelectList(_context.Candidates, "CandidateId", "Name", result.CandidateId);
            ViewData["ElectionId"] = new SelectList(_context.Elections, "ElectionId", "Surname", result.ElectionId);
            return View(result);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            ViewData["CandidateId"] = new SelectList(_context.Candidates, "CandidateId", "Name", result.CandidateId);
            ViewData["ElectionId"] = new SelectList(_context.Elections, "ElectionId", "Surname", result.ElectionId);
            return View(result);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResultId,ElectionId,CandidateId,NumVotes")] Result result)
        {
            if (id != result.ResultId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultExists(result.ResultId))
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
            ViewData["CandidateId"] = new SelectList(_context.Candidates, "CandidateId", "Name", result.CandidateId);
            ViewData["ElectionId"] = new SelectList(_context.Elections, "ElectionId", "Surname", result.ElectionId);
            return View(result);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .Include(r => r.Candidate)
                .Include(r => r.Election)
                .FirstOrDefaultAsync(m => m.ResultId == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _context.Results.FindAsync(id);
            if (result != null)
            {
                _context.Results.Remove(result);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        private bool ResultExists(int id)
        {
            return _context.Results.Any(e => e.ResultId == id);
        }
    }
}
