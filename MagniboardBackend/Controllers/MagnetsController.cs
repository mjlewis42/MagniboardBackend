using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagniboardBackend.Data;

namespace MagniboardBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MagnetsController : ControllerBase
    {
        private readonly MagniboardDbConnection _context;

        public MagnetsController(MagniboardDbConnection context)
        {
            _context = context;
        }

        // GET: api/Magnets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Magnets>>> GetMagnets()
        {
            if (_context.Magnets == null)
            {
                return NotFound();
            }
            return await _context.Magnets.ToListAsync();
        }

        // GET: api/Magnets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Magnets>> GetMagnets(int id)
        {
            if (_context.Magnets == null)
            {
                return NotFound();
            }
            var magnets = await _context.Magnets.FindAsync(id);

            if (magnets == null)
            {
                return NotFound();
            }

            return magnets;
        }

        // PUT: api/Magnets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMagnets(int id, Magnets magnets)
        {
            if (id != magnets.Id)
            {
                return BadRequest();
            }

            _context.Entry(magnets).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MagnetsExists(id))
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

        // POST: api/Magnets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Magnets>> PostMagnets(Magnets magnets)
        {
            if (_context.Magnets == null)
            {
                return Problem("Entity set 'MagniboardBackendContext.Magnets'  is null.");
            }
            _context.Magnets.Add(magnets);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMagnets", new { id = magnets.Id }, magnets);
        }

        // DELETE: api/Magnets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMagnets(int id)
        {
            if (_context.Magnets == null)
            {
                return NotFound();
            }
            var magnets = await _context.Magnets.FindAsync(id);
            if (magnets == null)
            {
                return NotFound();
            }

            _context.Magnets.Remove(magnets);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MagnetsExists(int id)
        {
            return (_context.Magnets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
