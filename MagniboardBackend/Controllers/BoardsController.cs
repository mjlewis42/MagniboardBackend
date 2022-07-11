using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagniboardBackend.Data;
using MagniboardBackend.Data.EntityModels;

namespace MagniboardBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly MagniboardDbConnection _context;

        public BoardsController(MagniboardDbConnection context)
        {
            _context = context;
        }

        // GET: api/Boards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Board>>> GetBoards()
        {
          if (_context.Board == null)
          {
              return NotFound();
          }
            var boards = await _context.Board
                .Include(i => i.tables)
                    .ThenInclude(j => j.rows)
                        .ThenInclude(k => k.cells)
                .ToListAsync();

            return Ok(boards);
        }

        //Get only active boards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Board>>> GetActiveBoards()
        {
            if (_context.Board == null)
            {
                return NotFound();
            }
            var boards = await _context.Board
                .Where(b => b.isActive)
                    .Include(i => i.tables)
                .ToListAsync();

            return Ok(boards);
        }

        // GET: api/Boards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Board>> GetBoard(int id)
        {
          if (_context.Board == null)
          {
              return NotFound();
          }
            //var board = await _context.Board.FindAsync(id);
            var board = await _context.Board
                    .Include(i => i.tables)
                            .ThenInclude(j => j.rows)
                                .ThenInclude(k => k.cells)
                .FirstOrDefaultAsync(i => i.id == id);

            if (board == null)
            {
                return NotFound();
            }

            return board;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Board>> GetBoardActive(int id)
        {
            if (_context.Board == null)
            {
                return NotFound();
            }
            //var board = await _context.Board.FindAsync(id);
            var board = await _context.Board
                .Where(b => b.isActive)
                    .Include(i => i.tables)
                            .ThenInclude(j => j.rows)
                                .ThenInclude(k => k.cells)
                .FirstOrDefaultAsync(i => i.id == id);

            if (board == null)
            {
                return NotFound();
            }

            return board;
        }

        // PUT: api/Boards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoard(int id, Board board)
        {
            if (id != board.id)
            {
                return BadRequest();
            }

            _context.Entry(board).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoardExists(id))
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


        //PUT board by Id and change current status to the opposite only if table length of incoming variable is not 0. Otherwise return error.
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeBoardStatus(int id, Board board)
        {
            if (id != board.id)
            {
                return BadRequest();
            }
            if(board.tables.Count == 0)
            {
                Console.WriteLine("ERROR Triggered in ChangeBoardStatus: No linked tables found in request");
                return BadRequest();
            }

            _context.Entry(board).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoardExists(id))
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


        // POST: api/Boards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Board>> PostBoard(Board board)
        {
          if (_context.Board == null)
          {
              return Problem("Entity set 'MagniboardDbConnection.Board'  is null.");
          }
            _context.Board.Add(board);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoard", new { id = board.id }, board);
        }

        // DELETE: api/Boards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(int id)
        {
            if (_context.Board == null)
            {
                return NotFound();
            }
            var board = await _context.Board.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }

            _context.Board.Remove(board);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoardExists(int id)
        {
            return (_context.Board?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
