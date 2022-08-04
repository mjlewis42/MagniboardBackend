using AutoMapper;
using MagniboardBackend.Data;
using MagniboardBackend.Data.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagniboardBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CellController : ControllerBase
    {
        private readonly MagniboardDbConnection _context;
        private readonly IMapper mapper;

        public CellController(MagniboardDbConnection context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // PUT: api/Cell/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<CellDTO>> PutCell(int id, CellDTO cellDTO)
        {
            if (id != cellDTO.id)
            {
                return BadRequest();
            }

            var cell = await _context.Cell
                .Include(c => c.magnet)
                .FirstOrDefaultAsync(x => x.id == id);

            if (cell == null)
            {
                return BadRequest();
            }

            mapper.Map(cellDTO, cell);
            _context.Entry(cell).State = EntityState.Modified;

            try
            {

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CellExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(cell);
        }

        private bool CellExists(int id)
        {
            return (_context.Cell?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
