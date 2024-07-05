using Acadenode.Core.Models;
using Acadenote.API.Repositories;
using Acadenote.Console.StudyProject.Core.ArticleAggregate;
using Newtonsoft.Json;
using System.Text;


static async Task ExtractFromFirebase()
{
    var http = new HttpClient();
    NoteRepository repo = new NoteRepository(http);

    var result = await http.GetStringAsync("https://wikiforum-6f73d-default-rtdb.firebaseio.com/articles.json?shallow=true&print=pretty");

    var articles = JsonConvert.DeserializeObject<Dictionary<string, bool>>(result);

    foreach (var id in articles)
    {
        var article = await http.GetStringAsync($"https://wikiforum-6f73d-default-rtdb.firebaseio.com/articles/{id.Key}.json?print=pretty");
        var articleObject = JsonConvert.DeserializeObject<Article>(article);
        if (articleObject == null)
            continue;

        string[] tags = [.. articleObject.DisciplineIds, .. articleObject.SubjectIds, .. articleObject.TopicIds];

        var note = new Note
        {
            Id = id.Key,
            Title = articleObject.Title,
            Content = articleObject.Content,
            Tags = tags,
        };

        var response = await repo.AddNoteAsync(note);

        if (!response.Success)
        {
            Console.WriteLine($"Failed to add note for article {articleObject.Id} - {response.Message}");
        }
        else
        {
            Console.WriteLine($"Added note for article {articleObject.Id}");
        }
    }
}
