//------------------------------------------------------------
// Copyright (c) 2017 AndyCnZh.  All rights reserved.
//------------------------------------------------------------

namespace AsyncTest
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            var bynderContents = DoCurlAsyncNoConfigureContext().Result;

            Console.WriteLine(bynderContents.Length.ToString());

            Console.ReadLine();
        }

        private async static Task<string> DoCurlAsyncNoConfigureContext()
        {
            using (var httpClient = new HttpClient())
            {
                using (var httpResponse = await httpClient.GetAsync("https://www.bynder.com"))
                {
                    return await httpResponse.Content.ReadAsStringAsync();
                }
            }
        }

        private async static Task<string> DoCurlAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var httpResponse = await httpClient.GetAsync("https://www.bynder.com").ConfigureAwait(false))
                {
                    return await httpResponse.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
