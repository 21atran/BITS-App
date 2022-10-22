using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL;

namespace BITS_App.Models
{
    internal class Article
    {
        // JSON deserialization classes
        protected class Post {
            public int id { get; set; }
            public DateTime date { get; set; }
            public DateTimeOffset date_gmt { get; set; }
            public Renderable title { get; set; }
            public Renderable content {get; set; }
            public CustomFields custom_fields { get; set; }
        }

        protected class Renderable {
            public string rendered {get; set; }
        }

        protected class CustomFields {
            public string[] writer { get; set; }
        }

        // fields
        protected WordPressClient client;
        protected int id;
        protected WordPressPCL.Models.Post post;
        protected string raw;
        protected Post postJson;
        protected List<WordPressPCL.Models.MediaItem> medias;

        // constructor
        public Article(int id)
        {
            //creating a client, and taking the wordpress data, storing it into a client variable
            client = new WordPressClient("https://gwhsnews.org/wp-json/");
            this.id = id;

            raw = "{}";
            client.HttpResponsePreProcessing = (response) => raw = response;

            //gets Posts from website
            var task = client.Posts.GetByIDAsync(id);
            task.Wait();
            post = task.Result;

            //returns the json backend of site
            postJson = JsonConvert.DeserializeObject<Post>(raw);


            //list of all the media
            var pictask = client.Media.GetAllAsync();
            pictask.Wait();

            //list of all the medias that are in website, gets a result
            medias = (List<WordPressPCL.Models.MediaItem>)pictask.Result;
        }

        // Bindings
        // title string
        public string Title => post.Title.Rendered;


        //gets a list of authors and joins them in a string
        public string Authors => String.Join(", ", postJson.custom_fields.writer);

        //a link for an image
        public string Image => medias[0].Link.ToString();

        // content string
        public string Content => post.content.rendered;
    }
}
