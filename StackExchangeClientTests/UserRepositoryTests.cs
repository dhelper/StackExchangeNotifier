using FakeItEasy;

using NUnit.Framework;
using StackExchangeClient;
using System.Threading.Tasks;

namespace StackExchangeClientTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        [Test, Explicit]
        public async void GetUserFromUrlReal()
        {
            var client = new JsonWebClient();

            var userRepository = new UserRepository(client);

            var user = await userRepository.GetUser(11361);

            Assert.That(user.Id, Is.EqualTo(11361));
        }        

        [Test]
        public async void GetUserFromUrl()
        {
            const string jsonResult = @"{""items"":[{""badge_counts"":{""gold"":7,""silver"":44,""bronze"":86},""profile_image"":""https://www.gravatar.com/avatar/db8b562e21f8f67bd924c3d6bcc60f4c?s=128&d=identicon&r=PG"",""display_name"":""Dror Helper"",""link"":""http://stackoverflow.com/users/11361/dror-helper"",""website_url"":""http://blog.drorhelper.com"",""location"":""Israel"",""accept_rate"":97,""age"":35,""user_id"":11361,""user_type"":""registered"",""creation_date"":1221549928,""reputation"":13904,""reputation_change_day"":0,""reputation_change_week"":18,""reputation_change_month"":228,""reputation_change_quarter"":632,""reputation_change_year"":2551,""last_access_date"":1388087583,""last_modified_date"":1385986009,""is_employee"":false,""account_id"":6692}],""quota_remaining"":292,""quota_max"":300,""has_more"":false}";

            var clientFake = A.Fake<IJsonClient>();
            A.CallTo(() => clientFake.HttpGetUncompressedAsync(A<string>.Ignored)).Returns(Task.FromResult(jsonResult));

            var userRepository = new UserRepository(clientFake);

            var user = await userRepository.GetUser(11361);

            var expected = new User
            {
                Id=11361,
                DisplayName = "Dror Helper",
                ImageUrl="https://www.gravatar.com/avatar/db8b562e21f8f67bd924c3d6bcc60f4c?s=128&d=identicon&r=PG",
                Reputation=13904
            };

            Assert.That(user, Is.EqualTo(expected));
        }
    }
}
