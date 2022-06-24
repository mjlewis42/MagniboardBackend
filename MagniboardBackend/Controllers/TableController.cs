using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagniboardBackend.Data;
using Newtonsoft.Json;
using MagniboardBackend.Data.EntityModels;
using AutoMapper;
using MagniboardBackend.Data.DTO;

namespace MagniboardBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : Controller
    {
        private readonly MagniboardDbConnection _context;
        private readonly IMapper mapper;

        public TableController(MagniboardDbConnection context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Table
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableDTO>>> GetTable()
        {
            if (_context.Table == null)
            {
                return NotFound();
            }

            var tables = await _context.Table
                .Include(i => i.rows)
                    .ThenInclude(j => j.cells)
                .ToListAsync();
            var tableDTOs = mapper.Map<List<TableDTO>>(tables);
            
            return Ok(tableDTOs);
        }

        // GET: api/Table/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTableDTO>> GetTable(int id)
        {
             if (_context.Table == null)
             {
                 return NotFound();
             }

            var table = await _context.Table
                .Include(i => i.rows)
                    .ThenInclude(j => j.cells)
                .FirstOrDefaultAsync(x => x.id == id);
            
            var tableDTO = mapper.Map<GetTableDTO>(table);

            return tableDTO;
        }

        // PUT: api/Table/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<GetTableDTO>> PutTable(int id, PutTableDTO tableDTO)
        {
            if (id != tableDTO.id || _context.Table == null)
            {
                return BadRequest();
            }

            var table = await _context.Table
                .Include(i => i.rows)
                    .ThenInclude(j => j.cells)
                .FirstOrDefaultAsync(x => x.id == id);

            if(table == null)
            {
                return BadRequest();
            }

            mapper.Map(tableDTO, table);
            _context.Entry(table).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TableExists(id))
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

        // POST: api/Table
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostTableDTO>> PostTable([FromBody] PostTableDTO tableDTO)
        {
            if (_context.Table == null)
            {
                return Problem("Entity set 'Connection'  is null.");
            }

            var table = mapper.Map<Table>(tableDTO);
            await _context.Table.AddAsync(table);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTable), new { id = table.id }, table);
        }

        // DELETE: api/Table/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            if (_context.Table == null)
            {
                return NotFound();
            }
            var table = await _context.Table
                .Include(i => i.rows)
                    .ThenInclude(j => j.cells)
                .FirstOrDefaultAsync(x => x.id == id);

            if (table == null)
            {
                return NotFound();
            }

            _context.Table.Remove(table);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TableExists(int id)
        {
            return (_context.Table?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}




