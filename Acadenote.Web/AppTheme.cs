using Microsoft.JSInterop;

namespace Acadenote.Web
{
    public class AppTheme
    {
        private readonly IJSRuntime js;
        public AppTheme(IJSRuntime js)
        {
            this.js = js;
        }

        private bool isDarkMode;

        public bool IsDarkMode
        {
            get { return isDarkMode; }
            set 
            { 
                isDarkMode = value;
                js.InvokeVoidAsync("setDarkMode", value);
                OnChange?.Invoke();
            }
        }

        public async Task<bool> IsBrowserDarkMode() => await js.InvokeAsync<bool>("isBrowserDarkMode");

        public event Action OnChange;

        public async Task ListenForThemeChanges()
        {
            var helper = DotNetObjectReference.Create(this);
            await js.InvokeVoidAsync("addThemeEventListener", helper);
        }

        [JSInvokable]
        public async Task SetDarkMode(bool isDarkMode)
        {
            IsDarkMode = isDarkMode;
        }
    }
}
