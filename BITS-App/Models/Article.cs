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
        WordPressClient client;
        private int id;
        WordPressPCL.Models.Post post;

        public Article(int id)
        {
            client = new WordPressClient("https://gwhsnews.org/wp-json/");
            this.id = id;

            var task = client.Posts.GetByIDAsync(id);
            task.Wait();
            post = task.Result;

        }

        public string Title => post.Title.Rendered;

        public string Author => post.Author.ToString();
    }
}
