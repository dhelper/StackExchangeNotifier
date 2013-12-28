using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackExchangeClient
{
    [JsonObject(MemberSerialization.OptIn)]
    public class User
    {
        [JsonProperty(PropertyName = "user_id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "profile_image")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "reputation")]
        public int Reputation { get; set; }

        [JsonProperty(PropertyName = "badge_counts")]
        public BadgeCounts BadgeCounts { get; set; }

        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class BadgeCounts
    {
        [JsonProperty(PropertyName = "gold")]
        public int Gold { get; set; }

        [JsonProperty(PropertyName = "silver")]
        public int Silver { get; set; }

        [JsonProperty(PropertyName = "bronze")]
        public int Bronze { get; set; }
    }
}
