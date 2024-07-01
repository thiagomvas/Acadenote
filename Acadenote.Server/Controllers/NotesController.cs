using Acadenode.Core.Models;
using Acadenode.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acadenote.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;
        private readonly ILogger<NotesController> _logger;

        public NotesController(INoteRepository noteRepository, ILogger<NotesController> logger)
        {
            _noteRepository = noteRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetAllNotesAsync()
        {
            try
            {
                var notes = await _noteRepository.GetAllNotesAsync();
                return Ok(notes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all notes.");
                return StatusCode(500, $"Internal server error. {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNoteByIdAsync(string id)
        {
            try
            {
                var note = await _noteRepository.GetNoteByIdAsync(id);
                if (note == null)
                {
                    return NotFound();
                }
                return Ok(note);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching note with id '{id}'.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddNoteAsync(Note note)
        {
            try
            {
                var response = await _noteRepository.AddNoteAsync(note);
                if (!response.Success)
                {
                    _logger.LogError("Failed to add note: {Error}", response.ErrorMessage);
                    return StatusCode(500, $"Failed to add note: {response.ErrorMessage}");
                }
                return CreatedAtAction(nameof(GetNoteByIdAsync), new { id = note.Id }, note);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a note.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNoteAsync(string id, Note note)
        {
            try
            {
                if (id != note.Id)
                {
                    return BadRequest("ID mismatch");
                }

                var response = await _noteRepository.UpdateNoteAsync(note);
                if (!response.Success)
                {
                    _logger.LogError("Failed to update note: {Error}", response.ErrorMessage);
                    return NotFound(response.ErrorMessage);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating note with id '{id}'.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNoteAsync(string id)
        {
            try
            {
                var response = await _noteRepository.DeleteNoteAsync(id);
                if (!response.Success)
                {
                    _logger.LogError("Failed to delete note: {Error}", response.ErrorMessage);
                    return NotFound(response.ErrorMessage);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting note with id '{id}'.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
