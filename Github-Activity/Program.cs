
using System.Net.Http.Headers;
using System.Text.Json;

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
                using JsonDocument doc = JsonDocument.Parse(responseBody);

                foreach (JsonElement element in doc.RootElement.EnumerateArray()) {
                    //Console.WriteLine(element.GetProperty("payload").ToString());
                    switch (element.GetProperty("type").GetString())
                    {
                        case "PushEvent":
                            //int numberOfCommits = element.GetProperty("payload").GetProperty("commits").GetArrayLength();
                            //string repoName = element.GetProperty("repo").GetProperty("name").ToString();
                            Console.WriteLine("Pushed {0} commits to {1}", "numberOfCommits", "repoName");
                            //Console.WriteLine(repoName);
                            break;
                        case "PullRequestEvent":
                            Console.WriteLine("Opened a pull request in {0}", "repoName");
                            break;
                        case "IssuesEvent":
                            Console.WriteLine("Opened a new issue in {0}", "repoName");
                            break;
                        case "IssueCommentEvent":
                            Console.WriteLine("Commented on issue #{0} in {1}", "issueNumber", "repoName");
                            break;
                        case "ForkEvent":
                            Console.WriteLine("Forked {0} to {1}", "repoName", "yourRepo");
                            break;
                        case "CreateEvent":
                            Console.WriteLine("Created branch {0} in {1}", "branchName", "repoName");
                            break;
                        case "DeleteEvent":
                            Console.WriteLine("Deleted branch {0} in {1}", "branchName", "repoName");
                            break;
                        case "GollumEvent":
                            Console.WriteLine("Updated the Wiki page {0} in {1}", "wikiPageName", "repoName");
                            break;
                        case "WatchEvent":
                            Console.WriteLine("Starred {0}", "repoName");
                            break;
                        case "FollowEvent":
                            Console.WriteLine("Followed user {0}", "userName");
                            break;
                        case "MemberEvent":
                            Console.WriteLine("Added user {0} as collaborator to {1}", "devName", "repoName");
                            break;
                        case "PublicEvent":
                            Console.WriteLine("Made repositroy {0} public", "repoName");
                            break;
                        case "CommitCommentEvent":
                            Console.WriteLine("Commented on commit {0} in {1}", "commitId", "repoName");
                            break;
                        default:
                            break;
                    }
                }

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