using Newtonsoft.Json;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL;

namespace BITS_App.Models
{
    internal class Post {
        public int id { get; set; }
        public DateTime date { get; set; }
        public DateTimeOffset date_gmt { get; set; }
        public Dictionary<string, string> title { get; set; }
        public CustomFields custom_fields { get; set; }
    }

    internal class CustomFields {
        public string[] writer { get; set; }
    }


    internal class Article
    {
        WordPressClient client;
        private int id;
        WordPressPCL.Models.Post post;
        string raw;
        Post postJson;

        public Article(int id)
        {
            client = new WordPressClient("https://gwhsnews.org/wp-json/");
            this.id = id;

            raw = "{}";
            client.HttpResponsePreProcessing = (response) => raw = response;

            var task = client.Posts.GetByIDAsync(id);
            task.Wait();
            post = task.Result;

            postJson = JsonConvert.DeserializeObject<Post>(raw);
        }

        public string Title => post.Title.Rendered;

        public string Author => post.Author.ToString();
    }
}
