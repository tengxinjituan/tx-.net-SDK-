using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TxSms.SDK
{
    internal class HttpSender
    {
        private readonly static HttpClient hc = new HttpClient();
        public static async Task<string> Post(string url, string data, string contentType = "application/json")
        {
            var ret = string.Empty;
            HttpContent httpContent = new StringContent(data);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            httpContent.Headers.ContentType.CharSet = "utf-8";
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.PostAsync(url, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    ret = await response.Content.ReadAsStringAsync();
                }
            }
            return ret;
        }

        public static async Task<string> Get(string url, string contentType = "application/json")
        {
            var ret = string.Empty;
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            HttpResponseMessage response = await hc.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                ret = await response.Content.ReadAsStringAsync();
            }
            return ret;
        }
    }
}
