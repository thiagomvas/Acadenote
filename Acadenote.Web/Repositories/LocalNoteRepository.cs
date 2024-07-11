using Acadenode.Core.Models;
using Acadenode.Core.Repositories;
using System.Net.Http.Json;
using System.Text.Json;

namespace Acadenote.Web.Repositories
{
    public class LocalNoteRepository : INoteRepository
    {
        private readonly HttpClient _httpClient;
        private List<Note> _notes;

        public LocalNoteRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse> AddNoteAsync(Note note)
        {
            Console.WriteLine("Permission Denied.");
            return new() { Success = false, Message = "Permission denied" };
        }

        public async Task<ServiceResponse> DeleteNoteAsync(string id)
        {
            Console.WriteLine("Permission Denied.");
            return new() { Success = false, Message = "Permission denied" };
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            if (_notes == null)
            {
                _notes = await LoadNotesAsync();
            }
            return _notes;
        }

        public async Task<Note> GetNoteByIdAsync(string id)
        {
            if (_notes == null)
            {
                _notes = await LoadNotesAsync();
            }
            return _notes.FirstOrDefault(note => note.Id == id);
        }

        public async Task<ServiceResponse> UpdateNoteAsync(Note note)
        {
            Console.WriteLine("Permission Denied.");
            return new() { Success = false, Message = "Permission denied" };
        }

        private async Task<List<Note>> LoadNotesAsync()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var notes = await _httpClient.GetFromJsonAsync<List<Note>>("notes.json", options);
            return notes ?? new List<Note>();
        }
    }
}
