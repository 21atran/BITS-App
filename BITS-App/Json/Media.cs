namespace BITS_App.Json;

public class Media {
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
    public int author { get; set; }
    public string comment_status { get; set; }
    public string ping_status { get; set; }
    public string template { get; set; }
    public List<Object> meta { get; set; }
    public List<Object> aioseo_notices { get; set; }
    public MetaFields meta_fields { get; set; }
    public class MetaFields {
        List<string> _wp_attached_file { get; set; }
        List<string> _wp_attachment_metadata { get; set; }
        List<string> photographer { get; set; }
        bool terms { get; set; }
    }
    public Description description { get; set; }
    public class Description : IRenderable {
        public string rendered { get; set; }
    }
    public Caption caption { get; set; }
    public class Caption : IRenderable {
        public string rendered { get; set; }
    }
    public string alt_text { get; set; }
    public string media_type { get; set; }
    public string mime_type { get; set; }
    public MediaDetails media_details { get; set; }
    public class MediaDetails {
        public int width { get; set; }
        public int height { get; set; }
        public string file { get; set; }
        public Dictionary<string, Size> sizes { get; set; }
        public class Size {
            public string file { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string mime_type { get; set; }
            public Uri source_url { get; set; }
        }
        public class ImageMeta {
            public string aperture { get; set; }
            public string credit { get; set; }
            public string camera { get; set; }
            public string caption { get; set; }
            public string created_timestamp { get; set; }
            public string copyright { get; set; }
            public string focal_length { get; set; }
            public string iso { get; set; }
            public string shutter_speed { get; set; }
            public string title { get; set; }
            public string orientation { get; set; }
            public List<string> keyword { get; set; }
        }
    }
    public int post { get; set; }
    public Uri source_url { get; set; }
    public Links _links { get; set; }
    public class Links {
        public List<Hyperlink> self { get; set; }
        public List<Hyperlink> collection { get; set; }
        public List<Hyperlink> about { get; set; }
        public List<EmbeddableHyperlink> author { get; set; }
        public List<EmbeddableHyperlink> replies { get; set; }
    }
}