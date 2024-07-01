namespace Acadenote.API
{
    internal static class Config
    {
        public static string BaseApiAddress = @"https://acadenoteserver20240701134331.azurewebsites.net/api";
        public static string NotesEndpoint => Path.Combine(BaseApiAddress, "notes");
    }
}
