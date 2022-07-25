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
    [Route("api/[controller]/[action]")]
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
        public async Task<ActionResult<IEnumerable<TableDTO>>> GetTables()
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

        // GET: api/Table/GetUnlinkedTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableDTO>>> GetUnlinkedTables()
        {
            if (_context.Table == null)
            {
                return NotFound();
            }

            var tables = await _context.Table
                 .Where(b => b.boardId == null)
                    
                    .ToListAsync();

            var tableDTOs = mapper.Map<List<TableDTO>>(tables);

            return Ok(tableDTOs);
        }

        // PUT: api/Table/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<PutTableDTO>> PutTable(int id, PutTableDTO tableDTO)
        {
            if (id != tableDTO.id || _context.Table == null)
            {
                return BadRequest();
            }

            var table = await _context.Table
                .Include(i => i.rows)
                    .ThenInclude(j => j.cells)
                .FirstOrDefaultAsync(x => x.id == id);

            if(table.boardId != null) { 
                return BadRequest(); 
            }
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


        [HttpPut("{id}")]
        public async Task<ActionResult<PutUnlinkTableDTO>> PutUnlinkTable(int id, PutUnlinkTableDTO tableDTO)
        {
            if (_context.Table == null)
            {
                return BadRequest();
            }

            var table = await _context.Table
                .FirstOrDefaultAsync(x => x.id == id);

            if (table == null)
            {
                return BadRequest();
            }

            var board = await _context.Board
                .FirstOrDefaultAsync(x => x.id == table.boardId);

            if (board == null)
            {
                return BadRequest();
            }

            if (board.isActive)
            {

                return StatusCode(400, "Unable to unlink table from live board.");
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

        // PUT: api/Table/PutTableBoardId/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<PutTableBoardIdDTO>> PutTableBoardId(int id, PutTableBoardIdDTO tableDTO)
        {
            if (id != tableDTO.id || _context.Table == null)
            {
                return BadRequest();
            }

            //remove other tables first
            var priorTable = await _context.Table
                    .Where(b => b.boardId == tableDTO.boardId)
                .ToListAsync();

            foreach (var i in priorTable)
            {
                i.boardId = null;
            }

            //update new table
            var table = await _context.Table.FirstOrDefaultAsync(x => x.id == id);

            if (table == null)
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


            return Ok(tableDTO);
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

            return Ok(table);
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

            //if table.boardId != null
            if (table == null)
            {
                return NotFound();
            }
            if(table.boardId != null)
            {
                var board = await _context.Board
                    .FirstOrDefaultAsync(x => x.id == table.boardId);
                board.isActive = false;
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




