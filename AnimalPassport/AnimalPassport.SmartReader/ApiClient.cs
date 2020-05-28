using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AnimalPassport.SmartReader.Models;
using Newtonsoft.Json;

namespace AnimalPassport.SmartReader
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string Url = "http://localhost:54063/api";

        private const string Role = "Член контрольних органів";

        public async Task<bool> LoginAsync(Auth auth)
        {
            var content = CreateContent(auth);

            var response = await _httpClient.PostAsync($"{Url}/User/Login", content);

            var user = await ReadContentAsync<User>(response);

            return user.Role == Role;
        }

        public async Task<bool> SendIdAsync(string id)
        {
            try
            {
                var access = new Access
                {
                    Id = new Guid(id)
                };

                var content = CreateContent(access);

                var response = await _httpClient.PostAsync($"{Url}/RemoteAccess", content);

                return response.StatusCode == System.Net.HttpStatusCode.OK;

            }
            catch (Exception)
            {
                return false;
            }
          
        }

        private StringContent CreateContent(object content)
        {
            return new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        }

        private async Task<T> ReadContentAsync<T>(HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}