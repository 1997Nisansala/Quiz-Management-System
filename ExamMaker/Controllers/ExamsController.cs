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
    public class ExamsController : ControllerBase
    {
        private readonly examContext _context;

        public ExamsController(examContext context)
        {
            _context = context;
        }

        // GET: api/Exams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam>>> GetExams()
        {
          if (_context.Exams == null)
          {
              return NotFound();
          }
            return await _context.Exams.ToListAsync();
        }

        // GET: api/Exams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetExam(int? id)
        {
          if (_context.Exams == null)
          {
              return NotFound();
          }
            var exam = await _context.Exams.FindAsync(id);

            if (exam == null)
            {
                return NotFound();
            }

            return exam;
        }

        // GET: api/Exams/5
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Exam>> GetExamByName(string name)
        {
            // Check if the exams collection is null
            if (_context.Exams == null)
            {
                return NotFound();
            }

            // Find the exam with the given name
            var exam = await _context.Exams.FirstOrDefaultAsync(e => e.ExamName == name);

            // If no matching exam is found, return a NotFound result
            if (exam == null)
            {
                return NotFound();
            }

            // Otherwise, return an Ok result with the exam object as the result body
            return Ok(exam);
        }

        // PUT: api/Exams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExam(int? id, Exam exam)
        {
            if (id != exam.ExamId)
            {
                return BadRequest();
            }

            _context.Entry(exam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(id))
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

        // POST: api/Exams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Exam>> PostExam(Exam exam)
        {
          if (_context.Exams == null)
          {
              return Problem("Entity set 'examContext.Exams'  is null.");
          }
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExam", new { id = exam.ExamId }, exam);
        }

        // DELETE: api/Exams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int? id)
        {
            if (_context.Exams == null)
            {
                return NotFound();
            }
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExamExists(int? id)
        {
            return (_context.Exams?.Any(e => e.ExamId == id)).GetValueOrDefault();
        }

        [HttpGet("GetExamsForTeacher")]
        public async Task<List<Exam>> GetExamsForTeacher(string? TeacherId)
        {
            return await _context.Exams
                .Include(p => p.Teacher)
                .Where(p => p.TeacherId == TeacherId)
                .ToListAsync();
        }

        // GET: api/Exams/5
        [HttpGet("exname/{name}")]
        public async Task<ActionResult<int>> GetExamIdByName(string name)
        {
            // Check if the exams collection is null
            if (_context.Exams == null)
            {
                return NotFound();
            }

            // Find the exam with the given name
            var exam = await _context.Exams.FirstOrDefaultAsync(e => e.ExamName == name);

            // If no matching exam is found, return a NotFound result
            if (exam == null)
            {
                return NotFound();
            }

            // Otherwise, return the exam ID as an Ok result
            return Ok(exam.ExamId);
        }


    }
}
