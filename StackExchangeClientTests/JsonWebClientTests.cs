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
    }
}
