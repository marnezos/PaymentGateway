using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentGateway.ApiTester
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<string> SuccesfulRequest = new List<string>();
            HttpClient client = new HttpClient();

            ApiClient[] apiClients = new ApiClient[10];

            for (int i = 0; i <= 9; i++)
            {
                apiClients[i] = new ApiClient(SuccesfulRequest, client);
                apiClients[i].Authenticate().Wait();
            }


            var rand = new Random(DateTime.Now.Millisecond * DateTime.Now.Second);
            while (true)
            {
                var tasks = new List<Task>();
                for (int i = 0; i <= 9; i++)
                {
                    if (rand.Next(1, 10) <= 1)
                    {
                        tasks.Add(apiClients[i].ProcessPayment());
                    }
                    else
                    {
                        tasks.Add(apiClients[i].RequestDetails());
                    }                    
                }
                Task.WaitAll(tasks.ToArray());

            }

        }
    }
}
