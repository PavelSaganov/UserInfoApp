using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Text;
using WPF.MVVM.Model;

namespace WPF.Service.Implementation
{
    public class UserService : IUserService
    {
        private User? CurrentUser { get; set; }
        private readonly HttpClient httpClient = new();
        private JsonSerializerSettings _serializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
        };

        public UserService()
        {
            httpClient.BaseAddress = new Uri("https://petstore.swagger.io");
            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public User? GetCurrentUser()
        {
            return CurrentUser;
        }

        public async Task AuthorizeUserAsync(string username, string password)
        {
            var result = await httpClient.GetAsync(@"v2/user/login?username=" + username + "&password=" + password);

            if (result.IsSuccessStatusCode)
                CurrentUser = await GetUserAsync(username);
            else HandleAPIException(result);
        }

        public async Task RegisterUserAsync(User user)
        {
            var body = JsonConvert.SerializeObject(user, _serializerSettings);
            var result = await httpClient.PostAsync(@"v2/user", new StringContent(body, Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
                CurrentUser = user;
            else HandleAPIException(result);
        }

        public async Task<User> GetUserAsync(string username)
        {
            var result = await httpClient.GetAsync(@"v2/user/" + username);
            HandleAPIException(result);

            if (result.IsSuccessStatusCode)
            {
                var responseString = await result.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(responseString);
                return user;
            }
            else HandleAPIException(result);

            return null;
        }

        public async Task LogoutCurrentUserAsync()
        {
            var result = await httpClient.GetAsync(@"v2/user/logout");

            if (result.IsSuccessStatusCode)
                CurrentUser = null;
            else HandleAPIException(result);
        }

        private void HandleAPIException(HttpResponseMessage result)
        {
            if (!result.IsSuccessStatusCode)
                throw new Exception("Something in api call went wrong");
        }
    }
}
