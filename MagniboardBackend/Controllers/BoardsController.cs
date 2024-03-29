﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagniboardBackend.Data;
using MagniboardBackend.Data.EntityModels;
using MagniboardBackend.Data.DTO;
using AutoMapper;

namespace MagniboardBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BoardsController : ControllerBase
    
    {
        /*
        private readonly MagniboardDbConnection _context;
        private readonly IMapper mapper;

        public BoardsController(MagniboardDbConnection context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
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
                .Include(i => i.templates)
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
                    .Include(i => i.templates)
                .ToListAsync();

            return Ok(boards);
        }

        // GET: api/Boards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Board>> GetBoardById(int id)
        {
          if (_context.Board == null)
          {
              return NotFound();
          }
            //var board = await _context.Board.FindAsync(id);
            var board = await _context.Board
                    .Include(i => i.templates)
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
        public async Task<ActionResult<GetBoardDTO>> GetActiveBoardById(int id)
        {
            if (_context.Board == null)
            {
                return NotFound();
            }
            //var board = await _context.Board.FindAsync(id);
            var board = await _context.Board
                .Where(b => b.isActive)
                    .Include(i => i.templates)
                            .ThenInclude(j => j.rows)
                                .ThenInclude(k => k.cells)
                .FirstOrDefaultAsync(i => i.id == id);

            if (board == null)
            {
                return NotFound();
            }

            var boardDTO = mapper.Map<GetBoardDTO>(board);

            return Ok(boardDTO);
        }

        // PUT: api/Boards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoard(int id, PutBoardDTO boardDTO)
        {
            var board = await _context.Board.FindAsync(id);

            if (board == null)
            {
                return BadRequest();
            }
            if (board.isActive)
            {
                return BadRequest();
            }

            mapper.Map(boardDTO, board);
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


        //PUT board by Id and change current status to the opposite only if Template length of incoming variable is not 0. Otherwise return error.
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeBoardStatus(int id, Board board)
        {
            if (id != board.id)
            {
                return BadRequest();
            }
            if(board.templates.Count == 0)
            {
                Console.WriteLine("ERROR Triggered in ChangeBoardStatus: No linked Templates found in request");
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
        public async Task<ActionResult<PostBoardDTO>> PostBoard(int TemplateId, [FromBody] PostBoardDTO boardDTO)
        {
            if (_context.Board == null)
            {
                return Problem("Entity set 'Connection'  is null.");
            }

            var board = mapper.Map<Board>(boardDTO);
            await _context.Board.AddAsync(board);
            await _context.SaveChangesAsync();

            return Ok(board);
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
            var Template = await _context.Template
                    .Where(b => b.boardId == id)
                .ToListAsync();

            foreach(var i in Template)
            {
                i.boardId = null;
            }

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
        */
    }
}
