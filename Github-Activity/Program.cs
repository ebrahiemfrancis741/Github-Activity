
using System.Net.Http.Headers;

class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        string url = "https://api.github.com/users/ebrahiemfrancis741/events";

        try {
            // required User-Agent header
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Github-Activity");
            
            // optional Accept header
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            else
            {
                Console.WriteLine("Error: {0}", response.StatusCode);
                Console.WriteLine("Reason: {0}", response.ReasonPhrase);
            }
        }
        catch (HttpRequestException e) {
            Console.WriteLine(e.Message);
        }
    }
}