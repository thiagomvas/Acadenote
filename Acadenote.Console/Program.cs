using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using Acadenode.Core.Models;
using Newtonsoft.Json;

string databaseFilePath = @"C:\Users\Thiago\source\repos\Acadenote\Acadenote.Console\acadenote.db";
string outputJsonFilePath = "notes.json";

NoteConverter.ConvertNotesTableToJson(databaseFilePath, outputJsonFilePath);

Console.WriteLine("Notes have been successfully converted to JSON.");
public static class NoteConverter
{
    public static void ConvertNotesTableToJson(string databaseFilePath, string outputJsonFilePath)
    {
        var notes = new List<Note>();

        // Connection string for SQLite database
        string connectionString = $"Data Source={databaseFilePath};Version=3;";

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT Id, Title, Content, Tags FROM Notes";
            using (var command = new SQLiteCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tagsString = reader["Tags"]?.ToString()
                            .Replace("[", "")
                            .Replace("\"", "")
                            .Replace("]", "")
                            .Replace(@"\\", @"\");
                        var tagsArray = string.IsNullOrEmpty(tagsString) ? new string[0] : tagsString.Split(',');
                        tagsArray = tagsArray.Select(t => Regex.Unescape(t.Replace(@"\\", @"\"))).ToArray();
                        var note = new Note
                        {
                            Id = reader["Id"].ToString(),
                            Title = reader["Title"].ToString(),
                            Content = reader["Content"].ToString(),
                            Tags = tagsArray
                        };
                        notes.Add(note);
                    }
                }
            }
        }

        // Configure JSON optionsvar options = new JsonSerializerOptions
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        // Convert list of notes to JSON
        string json = System.Text.Json.JsonSerializer.Serialize(notes, options);

        // Write JSON to file
        File.WriteAllText(outputJsonFilePath, json);
        Console.WriteLine(json);
    }
}
