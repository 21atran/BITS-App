using Newtonsoft.Json;

namespace BITS_App.Json {
    public class Category {
        public int id { get; set; }
        public int count { get; set; }
        public string description { get; set; }
        public Uri link { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string taxonomy { get; set; }
        public int parent { get; set; }
        public List<object> meta { get; set; }
        public Links _links { get; set; }
        public class Links {
            public List<Hyperlink> links { get; set; }
            public List<Hyperlink> collection { get; set; }
            public List<Hyperlink> about { get; set; }
            public List<EmbeddableHyperlink> up { get; set; }
            [JsonProperty("wp:post_type")]
            public List<Hyperlink> wp_post_type { get; set; }
            public List<Curie> curies { get; set; }

        }
    }
}