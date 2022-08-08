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
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace MagniboardBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TemplateController : Controller
    {
        private readonly MagniboardDbConnection _context;
        private readonly IMapper mapper;

        public TemplateController(MagniboardDbConnection context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Template
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateDTO>>> GetTemplates()
        {
            if (_context.Template == null)
            {
                return NotFound();
            }

            var Templates = await _context.Template
                .Include(i => i.rows)
                    .ThenInclude(j => j.cells)
                .ToListAsync();
            var TemplateDTOs = mapper.Map<List<TemplateDTO>>(Templates);
            
            return Ok(TemplateDTOs);
        }

        // GET: api/Template/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTemplateDTO>> GetTemplate(int id)
        {
             if (_context.Template == null)
             {
                 return NotFound();
             }

            var Template = await _context.Template
                .Include(i => i.rows)
                    .ThenInclude(j => j.cells)
                .FirstOrDefaultAsync(x => x.id == id);
            
            var TemplateDTO = mapper.Map<GetTemplateDTO>(Template);

            return TemplateDTO;
        }


        // GET: api/Template/GetUnlinkedTemplates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateDTO>>> GetActiveTemplates()
        {
            if (_context.Template == null)
            {
                return NotFound();
            }

            var Templates = await _context.Template
                 .Where(b => b.isActive)
                    .ToListAsync();

            var TemplateDTOs = mapper.Map<List<TemplateDTO>>(Templates);

            return Ok(TemplateDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetActiveBoardDTO>> GetActiveBoardWithPouchById(int id)
        {
            if (_context.Template == null)
            {
                return NotFound();
            }
            var template = await _context.Template
                .Where(b => b.isActive)
                    .Include(j => j.rows)
                        .ThenInclude(k => k.cells)
                        .ThenInclude(m => m.magnet)
                .FirstOrDefaultAsync(i => i.id == id);

            if (template == null)
            {
                return NotFound();
            }

           var templateDTO = mapper.Map<TemplateDTO>(template);

            
            List<Magnet> magnetsOnBoard = new List<Magnet>();

            foreach (Row row in template.rows)
            {
                foreach (Cell cell in row.cells)
                {
                    if (cell.magnet != null)
                        magnetsOnBoard.Add(cell.magnet);
                }
            }

            var magnetsPouch = await _context.Magnet.Where(mag => !magnetsOnBoard.Contains(mag)).ToListAsync();
            //List<Magnet> currentMagnetPouch =  fullMagnetsPouch.Where(magnetsOnBoard);
            var magnetsPouchDTO = mapper.Map<List<MagnetDTO>>(magnetsPouch);

            GetActiveBoardDTO activeBoard = new GetActiveBoardDTO
            {
                magnetPouch = magnetsPouchDTO,
                template = templateDTO
            };

            return Ok(activeBoard);
        }


        // PUT: api/Template/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<TemplateDTO>> PutTemplate(int id,TemplateDTO TemplateDTO)
        {
            if (id != TemplateDTO.id)
            {
                return BadRequest();
            }

            var template = await _context.Template
                .Include(i => i.rows)
                    .ThenInclude(j => j.cells)
                .FirstOrDefaultAsync(x => x.id == id);

            if (template == null)
            {
                return BadRequest();
            }

            mapper.Map(TemplateDTO, template);

            /*foreach (var row in TemplateDTO.rows)
            {
                foreach(var cell in row.cells)
                {
                    if(cell.magnet == null)
                    {
                        template.rows[0].cells[2].magnet = '1099';
                    }
                }
            }*/


            _context.Entry(template).State = EntityState.Modified;
            //_context.Entry(template).Property(p => p).IsModified = true;

            try
            {
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemplateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(template);
        }


        /*// PUT: api/Template/PutTemplateBoardId/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<PutTemplateBoardIdDTO>> PutTemplateBoardId(int id, PutTemplateBoardIdDTO TemplateDTO)
        {
            if (id != TemplateDTO.id || _context.Template == null)
            {
                return BadRequest();
            }

            //remove other Templates first
            var priorTemplate = await _context.Template
                    .Where(b => b.boardId == TemplateDTO.boardId)
                .ToListAsync();

            foreach (var i in priorTemplate)
            {
                i.boardId = null;
            }

            //update new Template
            var Template = await _context.Template.FirstOrDefaultAsync(x => x.id == id);

            if (Template == null)
            {
                return BadRequest();
            }

            mapper.Map(TemplateDTO, Template);
            _context.Entry(Template).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemplateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return Ok(TemplateDTO);
        }*/

        // POST: api/Template
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<PostTemplateDTO>> PostTemplate([FromBody] PostTemplateDTO TemplateDTO)
        {
            if (_context.Template == null)
            {
                return Problem("Entity set 'Connection'  is null.");
            }

            var Template = mapper.Map<Template>(TemplateDTO);
            await _context.Template.AddAsync(Template);
            await _context.SaveChangesAsync();

            return Ok(Template);
        }

        // DELETE: api/Template/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemplate(int id)
        {
            if (_context.Template == null)
            {
                return NotFound();
            }
            var Template = await _context.Template
                .Include(i => i.rows)
                    .ThenInclude(j => j.cells)
                .FirstOrDefaultAsync(x => x.id == id);

            //if Template.boardId != null
            if (Template == null)
            {
                return NotFound();
            }
            /*if(Template.boardId != null)
            {
                var board = await _context.Board
                    .FirstOrDefaultAsync(x => x.id == Template.boardId);
                board.isActive = false;
            }*/

            _context.Template.Remove(Template);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TemplateExists(int id)
        {
            return (_context.Template?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}




