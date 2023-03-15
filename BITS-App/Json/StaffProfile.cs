using Newtonsoft.Json;

namespace BITS_App.Json;

public class StaffProfile {
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
	public Uri link { get; set; }
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
	public int featured_media { get; set; }
	public string template { get; set; }
	public List<Object> aioseo_notices { get; set; }
	public CustomFields custom_fields { get; set; }
	public class CustomFields {
		public List<string> _edit_lock { get; set; }
		public List<string> _edit_last { get; set; }
		public List<string> _push_sent { get; set; }
		public List<string> _thumbnail_id { get; set; }
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
			public int count { get; set;  }
			public string filter { get; set; }
		}
	}
    public string excerpt { get; set; }
    public Uri profileImageUrl { get; set; }
    public Links _links { get; set; }
    public class Links {
        public List<Hyperlink> self { get; set; }
        public List<Hyperlink> collection { get; set; }
        public List<Hyperlink> about { get; set; }
        [JsonProperty("wp:featuredmedia")]
        public List<EmbeddableHyperlink> wp_featuredmedia { get; set; }
        [JsonProperty("wp:attachment")]
        public List<Hyperlink> wp_attachment { get; set; }
        public List<Curie> curies { get; set; }
        public class Curie : Hyperlink
        {
            public string name { get; set; }
            public bool templated { get; set; }
        }
    }
}