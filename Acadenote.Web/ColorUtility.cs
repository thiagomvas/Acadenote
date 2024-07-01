using System.Reflection;

namespace Acadenote.Web
{
    public class ColorUtility
    {
        private readonly AppTheme theme;
        public ColorUtility(AppTheme theme)
        {
            this.theme = theme;
            Text = new(theme);
        }
        public readonly TextColor Text;
        public Dictionary<string, string> GetColors()
        {
            // Use reflection
            var properties = typeof(ColorUtility).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var colors = new Dictionary<string, string>();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    colors[property.Name] = (string)property.GetValue(this);
                }
            }
            return colors;
        }

        // When changing these, also update index.html
        public string Background => theme.IsDarkMode ? "#121212" : "#dee2e6"; 
        


        public string Primary => theme.IsDarkMode ? "#8338ec" : "#cdb4db"; 
        public string Secondary => theme.IsDarkMode ? "#3a86ff" : "#a2d2ff"; 
        public string BackgroundAccent => theme.IsDarkMode ? "#1a1a1a" : "#cbcfd4";


        public string Success => theme.IsDarkMode ? "#388e3c" : "#81c784";
        public string Warning => theme.IsDarkMode ? "#f57c00" : "#ffb74d";
        public string Error => theme.IsDarkMode ? "#d32f2f" : "#e57373";

        public class TextColor
        {
            private readonly AppTheme theme;
            public TextColor(AppTheme theme)
            {
                this.theme = theme;
            }
            public string Primary => theme.IsDarkMode ? "#ffffff" : "#000000";
            public string Secondary => theme.IsDarkMode ? "#cccccc" : "#666666";
            public string Disabled => theme.IsDarkMode ? "#757575" : "#bdbdbd";
            public string Hint => theme.IsDarkMode ? "#9e9e9e" : "#757575";
        }
    }
}
