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
    public class MagnetController : ControllerBase
    {
        private readonly MagniboardDbConnection _context;
        private readonly IMapper mapper;

        public MagnetController(MagniboardDbConnection context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }


        // GET: api/Magnets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MagnetDTO>>> GetMagnets()
        {
            if (_context.Magnet == null)
            {
                return NotFound();
            }

            var magnet = await _context.Magnet.ToListAsync();
            var magnetDTOs = mapper.Map<List<MagnetDTO>>(magnet);

            return Ok(magnetDTOs);
        }

        // GET: api/Magnet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetMagnetDTO>> GetMagnetById(int id)
        {
            if (_context.Magnet == null)
            {
                return NotFound();
            }
            var magnet = await _context.Magnet.FindAsync(id);
            var magnetDTO = mapper.Map<GetMagnetDTO>(magnet);

            if (magnet == null)
            {
                return NotFound();
            }

            return Ok(magnetDTO);
        }

        // PUT: api/Magnet/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMagnet(int id, PutMagnetDTO magnetDTO)
        {
            if (id != magnetDTO.Id || _context.Magnet == null) 
            {
                return BadRequest();
            }

            var magnet = await _context.Magnet.FindAsync(id);

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
                if (!MagnetExists(id))
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

        // POST: api/Magnet
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostMagnetDTO>> PostMagnet(PostMagnetDTO magnetDTO)
        {
            if (_context.Magnet == null)
            {
                return Problem("Entity set 'Connection Magnet'  is null.");
            }

            var magnet = mapper.Map<Magnet>(magnetDTO);
            _context.Magnet.Add(magnet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMagnet", new { id = magnet.Id }, magnet);
        }

        // DELETE: api/Magnet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMagnet(int id)
        {
            if (_context.Magnet == null)
            {
                return NotFound();
            }
            var Magnet = await _context.Magnet.FindAsync(id);
            if (Magnet == null)
            {
                return NotFound();
            }

            _context.Magnet.Remove(Magnet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MagnetExists(int id)
        {
            return (_context.Magnet?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
