using Acadenode.Core.Services;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace Acadenote.Web.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<T> GetItem<T>(string key)
        {
            var json = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", key);

            if (json == null)
                return default;

            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task SetItem<T>(string key, T value)
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", key, JsonConvert.SerializeObject(value));
        }

        public async Task RemoveItem(string key)
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", key);
        }
    }
}
