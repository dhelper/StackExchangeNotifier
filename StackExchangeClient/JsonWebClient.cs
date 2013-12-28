using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StackExchangeClient
{
    public interface IJsonClient
    {
        Task<string> HttpGetUncompressedAsync(string url);
    }
    public class JsonWebClient : IJsonClient
    {
        public async Task<WebResponse> HttpGetAsync(WebRequest request)
        {
            var task = Task.Factory.FromAsync((callback, o) =>
                ((HttpWebRequest)o).BeginGetResponse(callback, o), result => ((HttpWebRequest)result.AsyncState)
                    .EndGetResponse(result), request);

            return await task;
        }

        public async Task<string> HttpGetUncompressedAsync(WebRequest request)
        {
            return await HttpGetAsync(request).ContinueWith(t =>
                {
                    using (var inputStream = t.Result.GetResponseStream())
                    using (var decompressed = new GZipStream(inputStream, CompressionMode.Decompress))
                    using (var reader = new StreamReader(decompressed))
                    {
                        var result = reader.ReadToEnd();

                        return result;
                    }
                });
        }

        public async Task<string> HttpGetUncompressedAsync(string url)
        {
           var request = HttpWebRequest.CreateHttp(url);

           return await HttpGetUncompressedAsync(request);
        }
    }
}
