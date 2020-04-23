using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowMyOrigin")]
    public class LabelsController : ControllerBase
    {
        // TODO: Identify if TodoContext should be changed to a separate LabelContext or change it to a generic DbContext
        private readonly TodoContext _context;

        public LabelsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Labels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Label>>> GetLabels()
        {
            return await _context.Labels.ToListAsync();
        }

        // GET: api/Labels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Label>> GetLabel(long id)
        {
            var label = await _context.Labels.FindAsync(id);

            if (label == null)
            {
                return NotFound();
            }

            return label;
        }

        // PUT: api/Labels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLabel(long id, Label label)
        {
            if (id != label.Id)
            {
                return BadRequest();
            }

            _context.Entry(label).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LabelExists(id))
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

        // POST: api/Labels
        [HttpPost]
        public async Task<ActionResult<Label>> PostLabel(Label label)
        {
            _context.Labels.Add(label);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLabel), new { id = label.Id }, label);
        }

        // DELETE: api/Labels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Label>> DeleteLabel(long id)
        {
            var label = await _context.Labels.FindAsync(id);
            if (label == null)
            {
                return NotFound();
            }

            _context.Labels.Remove(label);
            await _context.SaveChangesAsync();

            return label;
        }

        private bool LabelExists(long id)
        {
            return _context.Labels.Any(e => e.Id == id);
        }
    }
}
