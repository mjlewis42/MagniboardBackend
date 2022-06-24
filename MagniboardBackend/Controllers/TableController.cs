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
            var tables = await _context.Table
                .Include(i => i.rows)
                    .ThenInclude(j => j.cells)
                .ToListAsync();
            var tableDTOs = mapper.Map<List<TableDTO>>(tables);
            
            return Ok(tableDTOs);
        }

        // GET: api/Table/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Table>> GetTable(int id)
        {
          if (_context.Table == null)
          {
              return NotFound();
          }
            var Table = await _context.Table.FindAsync(id);

            if (Table == null)
            {
                return NotFound();
            }

            return Table;
        }

        // PUT: api/Table/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTable(int id, Table Table)
        {
            if (id != Table.id)
            {
                return BadRequest();
            }

            _context.Entry(Table).State = EntityState.Modified;

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
        public async Task<IActionResult> PostTable([FromBody] Table Table)
        {
            if (_context.Table == null)
            {
                return Problem("Entity set 'MagniboardDbConnection.Table'  is null.");
            }

            Console.WriteLine(Json(Table));

            _context.Table.Add(Table);
            await _context.SaveChangesAsync();

            //return Ok(Json(tableData));
            //return NoContent();
            return CreatedAtAction("GetTable", new { id = Table.id }, Table);
        }

        // DELETE: api/Table/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            if (_context.Table == null)
            {
                return NotFound();
            }
            var Table = await _context.Table.FindAsync(id);
            if (Table == null)
            {
                return NotFound();
            }

            _context.Table.Remove(Table);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TableExists(int id)
        {
            return (_context.Table?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}




