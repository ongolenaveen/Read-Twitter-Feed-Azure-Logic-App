﻿using Newtonsoft.Json;

namespace Twitter.Feed.Reader.DataProvider.Models
{
    public class Meta
    {
        [JsonProperty("newest_id")]
        public string NewestId { get; set; }

        [JsonProperty("oldest_id")]
        public string OldestId { get; set; }

        [JsonProperty("result_count")]
        public int ResultCount { get; set; }

        [JsonProperty("next_token")]
        public string NextToken { get; set; }
    }
}
