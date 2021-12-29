
namespace Twitter.Feed.Reader.Domain.Models
{
    public class TwitterConfig
    {
        public string ApiKey { get; set; }

        public string ApiKeySecret { get; set; }

        public string SearchUrl { get; set; }

        public string OauthTokenUrl { get; set; }
    }
}
