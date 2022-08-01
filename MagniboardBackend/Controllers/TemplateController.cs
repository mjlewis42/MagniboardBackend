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
        public async Task<ActionResult<GetTemplateDTO>> GetActiveTemplateById(int id)
        {
            if (_context.Template == null)
            {
                return NotFound();
            }
            var template = await _context.Template
                .Where(b => b.isActive)
                    .Include(j => j.rows)
                        .ThenInclude(k => k.cells)
                .FirstOrDefaultAsync(i => i.id == id);

            if (template == null)
            {
                return NotFound();
            }

            var templateDTO = mapper.Map<GetTemplateDTO>(template);

            return Ok(templateDTO);
        }

        // PUT: api/Template/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<PutTemplateDTO>> PutTemplate(int id, PutTemplateDTO TemplateDTO)
        {
            if (id != TemplateDTO.id || _context.Template == null)
            {
                return BadRequest();
            }

            var Template = await _context.Template
                .Include(i => i.rows)
                    .ThenInclude(j => j.cells)
                .FirstOrDefaultAsync(x => x.id == id);

            if(Template == null)
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

            return NoContent();
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




