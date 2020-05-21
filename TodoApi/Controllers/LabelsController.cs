using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowMyOrigin")]
    // TODO: Look in to adding back "await" keywords and updating the service layer to be asynchronous
    public class LabelsController : ControllerBase
    {
        private readonly ILabelService _labelService;

        public LabelsController(ILabelService labelService)
        {
            _labelService = labelService;
        }

        // GET: api/Labels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Label>>> GetLabels()
        {
            return  _labelService.GetAll().ToList();
        }

        // GET: api/Labels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Label>> GetLabel(long id)
        {
            var label =  _labelService.Get(id);

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

            // Q: I don't like that this logic is here, maybe discuss a different way to detect that an item was not found, but no server problem occurred
            if (_labelService.Get(id) == null)
            {
                // Could not find the todoitem
                return NotFound();
            }

            if (_labelService.Update(label))
            {
                // Label was updated successfully
                return NoContent();
            }

            // Something went wrong internally with updating the Label
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // POST: api/Labels
        [HttpPost]
        public async Task<ActionResult<Label>> PostLabel(Label label)
        {
            if (_labelService.Add(label))
            {
                // Label was added successfully
                return CreatedAtAction(nameof(GetLabel), new { id = label.Id }, label);
            }

            // Something went wrong internally with adding the TodoItem
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // DELETE: api/Labels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Label>> DeleteLabel(long id)
        {
            var label = _labelService.Remove(id);
            if (label == null)
            {
                return NotFound();
            }

            // Return the item that was deleted
            return label;
        }
    }
}
