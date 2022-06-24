using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagniboardBackend.Data;
using MagniboardBackend.Data.EntityModels;
using AutoMapper;
using MagniboardBackend.Data.DTO;

namespace MagniboardBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MagnetsController : ControllerBase
    {
        private readonly MagniboardDbConnection _context;
        private readonly IMapper mapper;

        public MagnetsController(MagniboardDbConnection context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Magnets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MagnetDTO>>> GetMagnets()
        {
            if (_context.Magnets == null)
            {
                return NotFound();
            }

            var magnets = await _context.Magnets.ToListAsync();
            var magnetDTOs = mapper.Map<List<MagnetDTO>>(magnets);

            return Ok(magnetDTOs);
        }

        // GET: api/Magnets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetMagnetDTO>> GetMagnets(int id)
        {
            if (_context.Magnets == null)
            {
                return NotFound();
            }
            var magnet = await _context.Magnets.FindAsync(id);
            var magnetDTO = mapper.Map<GetMagnetDTO>(magnet);

            if (magnet == null)
            {
                return NotFound();
            }

            return Ok(magnetDTO);
        }

        // PUT: api/Magnets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMagnets(int id, PutMagnetDTO magnetDTO)
        {
            if (id != magnetDTO.Id || _context.Magnets == null) 
            {
                return BadRequest();
            }

            var magnet = await _context.Magnets.FindAsync(id);

            if (magnet == null)
            {
                return BadRequest();
            }

            mapper.Map(magnetDTO, magnet);
            _context.Entry(magnet).State = EntityState.Modified;

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
        public async Task<ActionResult<PostMagnetDTO>> PostMagnets(PostMagnetDTO magnetDTO)
        {
            if (_context.Magnets == null)
            {
                return Problem("Entity set 'Connection Magnets'  is null.");
            }

            var magnet = mapper.Map<Magnets>(magnetDTO);
            _context.Magnets.Add(magnet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMagnets", new { id = magnet.Id }, magnet);
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
