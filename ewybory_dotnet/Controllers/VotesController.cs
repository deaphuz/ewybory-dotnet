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
    public class VotesController : Controller
    {
        private readonly eElectionContext _context;

        public VotesController(eElectionContext context)
        {
            _context = context;
        }

        // GET: api/<VotesController>/GetAll
        [HttpGet]
        public IEnumerable<Vote> GetAll()
        {
            return _context.Votes.ToList();
        }

        // GET: api/<VotesController>/GetVoter/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Vote>> GetVote(int id)
        {
            var vote = await _context.Votes.FindAsync(id);

            if (vote == null)
            {
                return NotFound();
            }

            return vote;
        }

        // POST: api/<VotesController>/AddVote
        [HttpPost]
        public IActionResult AddVote([FromBody] Vote value)
        {
            try
            {
                _context.Votes.Add(value);
                _context.SaveChanges();
                return Ok(value);
            }
            catch (Exception ex)
            {
                return RedirectToAction("GetAll");
            }
        }

        // PUT: api/<VotesController>/PutVote/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVote([FromBody] Vote vote, int id)
        {
            var old_vote = await _context.Votes.FindAsync(id);

            if (id != old_vote.VoteId)
            {
                return BadRequest();
            }

            vote.VoteId = id;
            _context.Entry(old_vote).CurrentValues.SetValues(vote);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoteExists(id))
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

        // DELETE: api/<VotesController>/DeleteVote/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVote(int id)
        {
            var vote = await _context.Votes.FindAsync(id);
            if (vote == null)
            {
                return NotFound();
            }

            _context.Votes.Remove(vote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Index()
        {
            var eElectionContext = _context.Votes.Include(v => v.Election).Include(v => v.Voter);
            return View(await eElectionContext.ToListAsync());
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vote = await _context.Votes
                .Include(v => v.Election)
                .Include(v => v.Voter)
                .FirstOrDefaultAsync(m => m.VoteId == id);
            if (vote == null)
            {
                return NotFound();
            }

            return View(vote);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public IActionResult Create()
        {
            ViewData["ElectionId"] = new SelectList(_context.Elections, "ElectionId", "Surname");
            ViewData["VoterId"] = new SelectList(_context.Voters, "VoterId", "Name");
            return View();
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VoteId,VoterId,ElectionId")] Vote vote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ElectionId"] = new SelectList(_context.Elections, "ElectionId", "Surname", vote.ElectionId);
            ViewData["VoterId"] = new SelectList(_context.Voters, "VoterId", "Name", vote.VoterId);
            return View(vote);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vote = await _context.Votes.FindAsync(id);
            if (vote == null)
            {
                return NotFound();
            }
            ViewData["ElectionId"] = new SelectList(_context.Elections, "ElectionId", "Surname", vote.ElectionId);
            ViewData["VoterId"] = new SelectList(_context.Voters, "VoterId", "Name", vote.VoterId);
            return View(vote);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VoteId,VoterId,ElectionId")] Vote vote)
        {
            if (id != vote.VoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoteExists(vote.VoteId))
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
            ViewData["ElectionId"] = new SelectList(_context.Elections, "ElectionId", "Surname", vote.ElectionId);
            ViewData["VoterId"] = new SelectList(_context.Voters, "VoterId", "Name", vote.VoterId);
            return View(vote);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vote = await _context.Votes
                .Include(v => v.Election)
                .Include(v => v.Voter)
                .FirstOrDefaultAsync(m => m.VoteId == id);
            if (vote == null)
            {
                return NotFound();
            }

            return View(vote);
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vote = await _context.Votes.FindAsync(id);
            if (vote != null)
            {
                _context.Votes.Remove(vote);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DEFAULT METHOD GENERATED WHILE CREATING MODEL
        private bool VoteExists(int id)
        {
            return _context.Votes.Any(e => e.VoteId == id);
        }
    }
}
