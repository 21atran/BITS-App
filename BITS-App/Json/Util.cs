using Newtonsoft.Json;

namespace BITS_App.Json {
    public interface IRenderable {
        public string rendered { get; set; }
    }
    public interface IProtectable {
        [JsonProperty("protected")]
        public bool isProtected { get; set; }
    }
    public interface IHyperlinked {
        public string href { get; set; }
    }
    public interface IEmbeddable {
        public bool embeddable { get; set; }
    }
    public class Hyperlink : IHyperlinked {
        public string href { get; set; }
    }
    public class EmbeddableHyperlink : Hyperlink, IEmbeddable {
        public bool embeddable { get; set; }
    }
}