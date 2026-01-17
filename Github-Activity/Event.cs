using System;
using System.Collections.Generic;
using System.Text;

namespace Github_Activity
{

    // This should represent the Json returned from the github user events api to serialize it into an Event object
    // Might not be used at the moment
    public class Event
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public Actor Actor { get; set; }
        public Repo Repo { get; set; }
        public Payload Payload { get; set; }
        public string Public { get; set; }
        public string Created_at { get; set; }
    }

    public class Actor
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Display_login { get; set; }
        public string Gravatar_id { get; set; }
        public string Url { get; set; }
        public string Avatar_url { get; set; }
    }

    public class Repo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

    }

    public class Payload
    {
        public string Ref { get; set; }
        public string Ref_type { get; set; }
        public string Full_ref { get; set; }
        public string Master_branch { get; set; }
        public string Description { get; set; }
        public string Pusher_type { get; set; }
    }
}
