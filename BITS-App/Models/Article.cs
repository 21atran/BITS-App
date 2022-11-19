using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL;

namespace BITS_App.Models
{
    internal class Article {
        // JSON deserialization classes
        protected class Post {
            public int id { get; set; }
            public DateTime date { get; set; }
            public DateTimeOffset date_gmt { get; set; }
            public Renderable title { get; set; }
            public Renderable content { get; set; }
            public CustomFields custom_fields { get; set; }

            public int featured_media { get; set; }

            public List<Dictionary<string, string>> attachment { get; set; }
        }

        protected class Renderable {
            public string rendered { get; set; }
        }

        protected class CustomFields {
            public string[] jobtitle { get; set; }
            public string[] writer { get; set; }
        }

        // fields
        protected WordPressClient client;
        protected WordPressPCL.Models.MediaItem featured;
        protected int id;
        protected WordPressPCL.Models.Post post;
        protected string raw;
        protected Post postJson;
        protected List<WordPressPCL.Models.MediaItem> medias;

        // constructor
        public Article(int id) {
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

            var pictasks = client.Media.GetByIDAsync(postJson.featured_media);
            pictasks.Wait();
            featured = pictasks.Result;


            //list of all the media
            var pictask = client.Media.GetAllAsync();
            pictask.Wait();

            //list of all the medias that are in website, gets a result
            medias = (List<WordPressPCL.Models.MediaItem>)pictask.Result;
        }

        // helper methods
        private string authorsTitlesFormatted() {
            string formatted = "";
            for (int i = 0; i < postJson.custom_fields.writer.Length; i++) {
                formatted += postJson.custom_fields.writer[i];
                formatted += ", ";
                formatted += postJson.custom_fields.jobtitle[i];

                if (i < postJson.custom_fields.writer.Length - 1) {
                    formatted += " - ";
                }
            }
            
            return formatted;
        }

        // Bindings
        // title string 
        public string Title => post.Title.Rendered;

        // gets a list of authors and joins them in a string
        public string Authors => String.Join(", ", postJson.custom_fields.writer);

        // joins a list of job titles into a string
        public string Titles => String.Join(", ", postJson.custom_fields.jobtitle);

        public string AuthorsAndTitles => authorsTitlesFormatted();

        // DateTime object for the publication date
        public DateTime Date => postJson.date;

        // a link for an image
        public string Image => medias[0].Link.ToString();

        // content string
        public string Content => post.Content.Rendered;

        // photo using MediaItem format
        public string FeaturedMediaPhoto => featured.Link.ToString();
    }
}
