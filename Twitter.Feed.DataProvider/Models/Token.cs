using Newtonsoft.Json;

namespace Twitter.Feed.Reader.DataProvider.Models
{
    public class Token
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
