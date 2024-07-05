using Acadenode.Core.Models;
using Acadenode.Core.Repositories;
using Newtonsoft.Json;
using System.Text;

namespace Acadenote.API.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly HttpClient _httpClient;
        public NoteRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse> AddNoteAsync(Note note)
        {
            var response = await _httpClient.PostAsync(Config.NotesEndpoint, new StringContent(JsonConvert.SerializeObject(note), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return new ServiceResponse { Success = true };
            }
            return new ServiceResponse { Success = false, Message = "Failed to add note" };
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
