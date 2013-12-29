using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace StackExchangeClient
{
    public interface IUserRepository
    {
        Task<User> GetUser(int userId);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IJsonClient client;

        public UserRepository(IJsonClient client)
        {
            this.client = client;
        }

        public async Task<User> GetUser(int userId)
        {
            var url = string.Format("https://api.stackexchange.com/2.1/users/{0}?site=stackoverflow", userId);

            var response = await client.HttpGetUncompressedAsync(url);

            var jobject = JObject.Parse(response);

            var userJsonString = jobject["items"].First();

            var result = JsonConvert.DeserializeObject<User>(userJsonString.ToString());

            return result;
        }
    }
}
