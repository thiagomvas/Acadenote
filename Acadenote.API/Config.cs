namespace Acadenote.API
{
    internal static class Config
    {
        public static string BaseAddress = @"https://localhost:7026";
        public static string BaseApiAddress = Path.Combine(BaseAddress, "api");
        public static string NotesEndpoint => Path.Combine(BaseApiAddress, "notes");
        public static string AuthEndpoint => Path.Combine(BaseAddress, "auth");
    }
}
