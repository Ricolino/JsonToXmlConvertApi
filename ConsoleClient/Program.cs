namespace ConsoleClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            do
            {
                string json = @"{ ""id"": 5, ""name"": ""John"", ""description"": null, ""values"": null}";

                string xml = await ConvertJsonToXml(json);


                Console.WriteLine(xml);
            }
            while (Console.ReadKey().Key != ConsoleKey.X);

        }

        public static async Task<string> ConvertJsonToXml(string json)
        {
            string serviceURL = "http://localhost:5115/Convert";


            var client = new HttpClient();

            client.BaseAddress = new Uri(serviceURL);
            client.DefaultRequestHeaders.Add("ApiKey", "HarryPotter");

            string urlRequest = serviceURL + "?requestBody=" + json;

            var response = await client.PostAsync(urlRequest, new StringContent(json));

            string result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}