using FakeItEasy;
using NUnit.Framework;
using StackExchangeClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StackExchangeClientTests
{
    [TestFixture]
    public class JsonWebClientTests
    {
        [Test, Explicit]
        public async void GetUserToOutput()
        {
            var client = new JsonWebClient();

            var result = await client.HttpGetUncompressedAsync("https://api.stackexchange.com/2.1/users/11361?site=stackoverflow");

            Console.WriteLine(result);
        }

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

            Assert.That(user.Id, Is.EqualTo(11361));
        }
    }
}
