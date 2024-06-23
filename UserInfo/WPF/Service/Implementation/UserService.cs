using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Windows.Documents;
using WPF.MVVM.Model;

namespace WPF.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient = new();
        private static string? CurrentUserJWT { get; set; }

        public UserService()
        {
            httpClient.BaseAddress = new Uri("https://petstore.swagger.io");
        }

        public async Task AuthorizeUserAsync(string username, string password)
        {
            var result = await httpClient.GetAsync(@"user/login?username=" + username + "?password=" + password);
            ValidateResponse(result);
        }

        public async Task RegisterUserAsync(User user)
        {
            var body = JsonConvert.SerializeObject(user);
            var result = await httpClient.PostAsync(@"user", new StringContent(body));

            ValidateResponse(result);
        }

        public async Task<User> GetCurrentUserAsync()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        private static void ValidateResponse(HttpResponseMessage result)
        {
            ValidateResponse(result);
        }
    }
}
