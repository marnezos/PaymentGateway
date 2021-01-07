using IdentityModel.Client;
using Newtonsoft.Json;
using PaymentGateway.ApiTester.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.ApiTester
{
    public class ApiClient
    {
        private IList<string> _successfulRequests;
        private HttpClient _httpClient;
        public ApiClient(IList<string> successfulRequests, HttpClient httpClient)
        {
            _successfulRequests = successfulRequests;
            _httpClient = httpClient;
        }
        public string AccessToken { get; set; }

        public async Task Authenticate()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:4999");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "merchant1",
                ClientSecret = "supersecurepassword",
                Scope = "paymentgateway.api"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }
            Console.WriteLine("Authenticated");

            AccessToken = tokenResponse.AccessToken;
        }

        public async Task ProcessPayment()
        {
            var client = _httpClient;
            client.SetBearerToken(AccessToken);

            string uniqueId = Guid.NewGuid().ToString();

            var rand = new Random(DateTime.Now.Millisecond * DateTime.Now.Second);
            ProcessPaymentRequest req = new ProcessPaymentRequest()
            {
                Amount = (decimal)rand.NextDouble() * 100.0M,
                CardCvv = rand.Next(100, 999).ToString(),
                CardExpirationMonth = (byte)rand.Next(1, 12),
                CardExpirationYear = (ushort)rand.Next(DateTime.Now.Year + 1, DateTime.Now.Year + 10),
                CardNumber = string.Concat(rand.Next(1000, 9999).ToString(), rand.Next(1000, 9999).ToString(), rand.Next(1000, 9999).ToString(), rand.Next(1000, 9999).ToString()),
                CurrencyIso4217 = "EUR",
                MerchantUniqueRequestId = uniqueId
            };

            string json = JsonConvert.SerializeObject(req);
            
            Console.WriteLine($"Requesting Payment:{uniqueId}");

            var response = await client.PostAsync("https://localhost:5001/api/v1/Payments", new StringContent(json, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed with code: {response.StatusCode} {response.Content}");
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                ProcessPaymentResponse paymentResponse = JsonConvert.DeserializeObject<ProcessPaymentResponse>(content);
                Console.WriteLine($"Returned: {paymentResponse.Success.ToString()} Response Id: {paymentResponse.ResponseId}");
                _successfulRequests.Add(uniqueId);
            }
            Console.WriteLine($"");
        }

        public async Task RequestDetails()
        {
            if (_successfulRequests.Count == 0) 
                return;

            var client = _httpClient;
            client.SetBearerToken(AccessToken);


            var rand = new Random(DateTime.Now.Millisecond * DateTime.Now.Second);
            PaymentDetailsRequest req = new PaymentDetailsRequest()
            {
                MerchantUniqueRequestId = _successfulRequests[rand.Next(0, _successfulRequests.Count - 1)]
            };

            string json = JsonConvert.SerializeObject(req);

            Console.WriteLine($"Requesting Details:{req.MerchantUniqueRequestId}");

            var response = await client.GetAsync($"https://localhost:5001/api/v1/Payments/PaymentDetailsRequest?MerchantUniqueRequestId={req.MerchantUniqueRequestId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed with code: {response.StatusCode} {response.Content}");
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                PaymentDetailsResponse detailsResponse = JsonConvert.DeserializeObject<PaymentDetailsResponse>(content);
                Console.WriteLine($"Returned: {detailsResponse.Success} Response Id: {detailsResponse.ResponseId}");
                if (detailsResponse.Success && detailsResponse.MerchantUniqueRequestId != req.MerchantUniqueRequestId)
                {
                    Console.WriteLine($"Mixed Responses! **************************************************************************");
                }
            }
            Console.WriteLine($"");
        }

    }
}
