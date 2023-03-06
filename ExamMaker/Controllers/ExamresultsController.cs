using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using exam.Data;
using exam.Models;

namespace ExamMaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamresultsController : ControllerBase
    {
        private readonly examContext _context;

        public ExamresultsController(examContext context)
        {
            _context = context;
        }

        // GET: api/Examresults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Examresult>>> GetExamresults()
        {
          if (_context.Examresults == null)
          {
              return NotFound();
          }
            return await _context.Examresults.ToListAsync();
        }

        // GET: api/Examresults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Examresult>> GetExamresult(int id)
        {
          if (_context.Examresults == null)
          {
              return NotFound();
          }
            var examresult = await _context.Examresults.FindAsync(id);

            if (examresult == null)
            {
                return NotFound();
            }

            return examresult;
        }

        [HttpGet("result/{examName}")]
        public async Task<ActionResult<List<Examresult>>> GetExamResultsByExamName(string examName)
        {
            var results = await _context.Examresults
                .Include(e => e.Exam)
                .Where(e => e.Exam.ExamName == examName)
                .ToListAsync();

            return results;
        }

        // PUT: api/Examresults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamresult(int id, Examresult examresult)
        {
            if (id != examresult.ResultId)
            {
                return BadRequest();
            }

            _context.Entry(examresult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamresultExists(id))
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

        // POST: api/Examresults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Examresult>> PostExamresult(Examresult examresult)
        {
          if (_context.Examresults == null)
          {
              return Problem("Entity set 'examContext.Examresults'  is null.");
          }
            _context.Examresults.Add(examresult);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExamresult", new { id = examresult.ResultId }, examresult);
        }

        // DELETE: api/Examresults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamresult(int id)
        {
            if (_context.Examresults == null)
            {
                return NotFound();
            }
            var examresult = await _context.Examresults.FindAsync(id);
            if (examresult == null)
            {
                return NotFound();
            }

            _context.Examresults.Remove(examresult);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExamresultExists(int id)
        {
            return (_context.Examresults?.Any(e => e.ResultId == id)).GetValueOrDefault();
        }
    }
}
