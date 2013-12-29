using System;
using StackExchangeClient;
using System.Threading.Tasks;

namespace WindowsPhoneNotifier
{
    public class DummyUserRepository : IUserRepository
    {
        private int index;
        private readonly User[] _users = new[]
        {
            new User{Id=404, DisplayName="Dummy", ImageUrl="", Reputation=10},
            new User{Id=404, DisplayName="Dummy", ImageUrl="", Reputation=20},
            new User{Id=404, DisplayName="Dummy", ImageUrl="", Reputation=30},
            new User{Id=404, DisplayName="Dummy", ImageUrl="", Reputation=25},
            new User{Id=404, DisplayName="Dummy", ImageUrl="", Reputation=70},
            new User{Id=404, DisplayName="Dummy", ImageUrl="", Reputation=60},
            new User{Id=404, DisplayName="Dummy", ImageUrl="", Reputation=90},
        };
        public Task<User> GetUser(int userId)
        {
            var user = _users[index];
            index = (index + 1) % _users.Length;

            return Task.FromResult(user);
        }
    }
}
