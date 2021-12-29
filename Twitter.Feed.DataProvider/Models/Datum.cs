using Newtonsoft.Json;
using System;

namespace Twitter.Feed.Reader.DataProvider.Models
{
    public class Datum
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
