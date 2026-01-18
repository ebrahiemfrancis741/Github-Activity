
using System.Net.Http.Headers;
using System.Text.Json;

class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please specify github username: example Github-Activity \"kamranahmedse\"");
        }
        string githubUser = args[0];
        string url = "https://api.github.com/users/" + githubUser + "/events";

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
                    string repoName = element.GetProperty("repo").GetProperty("name").ToString();
                    DateTime datetime = element.GetProperty("created_at").GetDateTime();
                    
                    switch (element.GetProperty("type").GetString())
                    {
                        case "PushEvent":
                            Console.WriteLine("Pushed commits to {0} at {1}", repoName, datetime.ToString());
                            break;
                        case "PullRequestEvent":
                            Console.WriteLine("Opened a pull request in {0} at {1}", repoName, datetime.ToString());
                            break;
                        case "IssuesEvent":
                            Console.WriteLine("Opened a new issue in {0} at {1}", repoName, datetime.ToString());
                            break;
                        case "IssueCommentEvent":
                            int payloadIssueNumber = element.GetProperty("payload").GetProperty("issue").GetProperty("number").GetInt32();
                            Console.WriteLine("Commented on issue #{0} in {1} at {2}", payloadIssueNumber, repoName, datetime.ToString());
                            break;
                        case "ForkEvent":
                            string forkee = element.GetProperty("payload").GetProperty("forkee").GetProperty("full_name").ToString();
                            Console.WriteLine("Forked {0} to {1} at {2}", repoName, forkee, datetime.ToString());
                            break;
                        case "CreateEvent":
                            Console.WriteLine("Created new branch in {0} at {1}", repoName, datetime.ToString());
                            break;
                        case "DeleteEvent":
                            Console.WriteLine("Deleted branch in {0} at {1}", "repoName", datetime.ToString());
                            break;
                        case "GollumEvent":
                            Console.WriteLine("Updated the Wiki page in {0} at {1}", repoName, datetime.ToString());
                            break;
                        case "WatchEvent":
                            Console.WriteLine("Starred {0} at {1}", repoName, datetime.ToString());
                            break;
                        case "FollowEvent":
                            Console.WriteLine("Followed user {0} at {1}", "userName", datetime.ToString());
                            break;
                        case "MemberEvent":
                            string memberName = element.GetProperty("payload").GetProperty("member").GetProperty("login").ToString();
                            Console.WriteLine("Added user {0} as collaborator to {1} at {2}", memberName, repoName, datetime.ToString());
                            break;
                        case "PublicEvent":
                            Console.WriteLine("Made repositroy {0} public at {1}", repoName, datetime.ToString());
                            break;
                        case "CommitCommentEvent":
                            Console.WriteLine("Commented on commit in {0} at {1}", repoName, datetime.ToString());
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