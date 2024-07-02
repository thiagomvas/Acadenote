using Acadenode.Core.Models;
using Acadenode.Core.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Acadenote.Server.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly AcadenoteDbContext _context;

        public NoteRepository(AcadenoteDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task<Note> GetNoteByIdAsync(string id)
        {
            return await _context.Notes.FindAsync(id);
        }

        public async Task<ServiceResponse> AddNoteAsync(Note note)
        {
            var response = new ServiceResponse();
            try
            {
                await _context.Notes.AddAsync(note);
                await _context.SaveChangesAsync();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> UpdateNoteAsync(Note note)
        {
            var response = new ServiceResponse();
            try
            {
                _context.Notes.Update(note);
                await _context.SaveChangesAsync();
                response.Success = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                response.Success = false;
                response.Data = "Note not found.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> DeleteNoteAsync(string id)
        {
            var response = new ServiceResponse();
            try
            {
                var note = await _context.Notes.FindAsync(id);
                if (note == null)
                {
                    response.Success = false;
                    response.Data = "Note not found.";
                }
                else
                {
                    _context.Notes.Remove(note);
                    await _context.SaveChangesAsync();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = ex.Message;
            }
            return response;
        }
    }
}
