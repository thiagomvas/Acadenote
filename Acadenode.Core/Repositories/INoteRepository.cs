using Acadenode.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadenode.Core.Repositories
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllNotesAsync();
        Task<Note> GetNoteByIdAsync(string id);
        Task<ServiceResponse> AddNoteAsync(Note note);
        Task<ServiceResponse> UpdateNoteAsync(Note note);
        Task<ServiceResponse> DeleteNoteAsync(string id);
    }

    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string Data { get; set; }
    }
}
