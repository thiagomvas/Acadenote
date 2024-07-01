using Acadenode.Core.Models;
using Acadenode.Core.Repositories;
using Newtonsoft.Json;

namespace Acadenote.API.Repositories
{
    internal class NoteRepository : INoteRepository
    {
        private readonly HttpClient _httpClient;
        public NoteRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<ServiceResponse> AddNoteAsync(Note note)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> DeleteNoteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            var response = await _httpClient.GetAsync(Config.NotesEndpoint);
            if (response.IsSuccessStatusCode)
            {
                var notes = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<Note>>(notes);
                if(result != null)
                    return result;
            }
                return [];
        }

        public async Task<Note> GetNoteByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{Config.NotesEndpoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var note = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Note>(note);
                if(result != null)
                    return result;
            }
            return Note.NotFound;
            
        }

        public Task<ServiceResponse> UpdateNoteAsync(Note note)
        {
            throw new NotImplementedException();
        }
    }
}
