using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Georgi.GoRestProject.Core.Support
{
    public class GoRestUser
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]

        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
    public class GoRestRequestUser
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class GoRestRequestUserBogus
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("gender")]
        public Bogus.DataSets.Name.Gender Gender { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
