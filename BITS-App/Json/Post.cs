using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Models;

namespace BITS_App.Json {
    public class Post {
        public int id { get; set; }
        public DateTime date { get; set; }
        public DateTimeOffset date_gmt { get; set; }
        public Guid guid { get; set; }
        public class Guid : Renderable { }
        public Title title { get; set; }
        public class Title : Renderable { }
        public Content content { get; set; }
        public class Content : Renderable { }
        public CustomFields custom_fields { get; set; }
        public class CustomFields {
            public string[] jobtitle { get; set; }
            public string[] writer { get; set; }
        }
        public int featured_media { get; set; }
        public List<Dictionary<string, string>> attachment { get; set; }
        public class Renderable {
            public string rendered { get; set; }
        }
    }
}
