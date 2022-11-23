using Newtonsoft.Json;

namespace BITS_App.Json {
    public class Post {
        public int id { get; set; }
        public DateTime date { get; set; }
        public DateTimeOffset date_gmt { get; set; }
        public Guid guid { get; set; }
        public class Guid : IRenderable {
            public string rendered { get; set; }
        }
        public DateTime modified { get; set; }
        public DateTimeOffset modified_gmt { get; set; }
        public string slug { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string link { get; set; }
        public Title title { get; set; }
        public class Title : IRenderable {
            public string rendered { get; set; }
        }
        public Content content { get; set; }
        public class Content : IRenderable, IProtectable {
            public string rendered { get; set; }
            [JsonProperty("protected")]
            public bool isProtected { get; set; }
        }
        public Excerpt excerpt { get; set; }
        public class Excerpt : IRenderable, IProtectable {
            public string rendered { get; set; }
            [JsonProperty("protected")]
            public bool isProtected { get; set; }
        }
        public int author { get; set; }
        public int featured_media { get; set; }
        public string comment_status { get; set; }
        public string ping_status { get; set; }
        public bool sticky { get; set; }
        public string template { get; set; }
        public string format { get; set; }
        public List<Object> meta { get; set; }
        public List<int> categories { get; set; }
        public List<Object> tags { get; set; }
        public List<Object> aioseo_notices { get; set; }
        public CustomFields custom_fields { get; set; }
        public class CustomFields {
            public List<string> _edit_lock { get; set; }
            public List<string> _edit_last { get; set; }
            public List<string> _mf_write_panel_id { get; set; }
            public List<string> sno_headline { get; set; }
            public List<string> sno_format { get; set; }
            public List<string> featureimage { get; set; }
            public List<string> videolocation { get; set; }
            public List<string> _thumbnail_id { get; set; }
            public List<string> jobtitle { get; set; }
            public List<string> _push_sent { get; set; }
            public List<string> _wp_old_date { get; set; }
            public List<string> branch_link { get; set; }
            public List<string> sno_views { get; set; }
            public List<string> writer { get; set; }
            public List<string> branch_link_new { get; set; }
            public List<Term> terms { get; set; }
            public class Term {
                public int term_id { get; set; }
                public string name { get; set; }
                public string slug { get; set; }
                public int term_group { get; set; }
                public int term_taxonomy_id { get; set; }
                public string taxonomy { get; set; }
                public string description { get; set; }
                public int parent { get; set; }
                public int count { get; set; }
                public string filter { get; set; }
            }
        }
        public Links _links { get; set; }
        public class Links {
            public List<Hyperlink> self { get; set; }
            public List<Hyperlink> collection { get; set; }
            public List<Hyperlink> about { get; set; }
            public List<EmbeddableHyperlink> author { get; set; }
            public List<EmbeddableHyperlink> replies { get; set; }
            [JsonProperty("version-history")]
            public List<VersionHistory> version_history { get; set; }
            public class VersionHistory : Hyperlink {
                public int count { get; set; }
            }
            [JsonProperty("predecessor-version")]
            public List<PredecessorVersion> predecessor_version { get; set; }
            public class PredecessorVersion : Hyperlink {
                public int id { get; set; }
            }
            [JsonProperty("wp:featuredmedia")]
            public List<EmbeddableHyperlink> wp_featuredmedia { get; set; }
            [JsonProperty("wp:attachment")]
            public List<Hyperlink> wp_attachment { get; set; }
            [JsonProperty("wp:term")]
            public List<Term> wp_term { get; set; }
            public class Term : EmbeddableHyperlink {
                public string taxonomy { get; set; }
            }
            public List<Curie> curies { get; set; }
            public class Curie : Hyperlink {
                public string name { get; set; }
                public bool templated { get; set; }
            }
        }
    }
}
