using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WPF.MVVM.Model;

namespace WPF.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient = new();
        private static string? CurrentUserJWT { get; set; }

        public UserService()
        {
            httpClient.BaseAddress = new Uri("https://petstore.swagger.io/v2");
            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task AuthorizeUserAsync(string username, string password)
        {
            var result = await httpClient.GetAsync("user/login?username=" + username + "?password=" + password);
            ValidateResponse(result);
        }

        public async Task RegisterUserAsync(User user)
        {
            var body = JsonConvert.SerializeObject(user);
            var result = await httpClient.PostAsync("v2/user", new StringContent(body, Encoding.UTF8, "application/json"));

            ValidateResponse(result);
        }

        public async Task<User> GetCurrentUserAsync()
        {
            var result = await httpClient.GetAsync(@"user/username=");
            ValidateResponse(result);

            var a = result.Content.ToString();
            var b = JsonConvert.DeserializeObject<User>(a);
            return b;
        }

        public async Task<User> GetUserAsync(string username)
        {
            var result = await httpClient.GetAsync(@"user/username=" + username);
            ValidateResponse(result);

            var a = result.Content.ToString();
            var b = JsonConvert.DeserializeObject<User>(a);
            return b;
        }

        public async Task LogoutCurrentUserAsync()
        {
            var result = await httpClient.GetAsync(@"user/logout");
            ValidateResponse(result);
        }

        private static void ValidateResponse(HttpResponseMessage result)
        {
            ValidateResponse(result);
        }
    }
}
