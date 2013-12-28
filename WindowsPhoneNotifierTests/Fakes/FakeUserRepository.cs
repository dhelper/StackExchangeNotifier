using StackExchangeClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhoneNotifierTests.Fakes
{
    public class FakeUserRepository : IUserRepository
    {
        private User[] _users;
        private int currentIndex;
        public FakeUserRepository(User[] usersToReturn)
        {
            _users = usersToReturn;
        }

        public Task<User> GetUser(int userId)
        {
            var user = _users[currentIndex++];

            return Task.FromResult(user);
        }
    }
}
