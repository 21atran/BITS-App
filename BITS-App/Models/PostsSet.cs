using System;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;

namespace BITS_App.Models
{
    public class PostsSet
    {
        private List<int> postIDs = new List<int>();
        Uri uri;

        public PostsSet()
        {
            string catID = "23";
            string queryName = "categories";
            //request = $"https://{App.BASE_URL}/wp-json/wp/v2/{queryName}/{catID}";

            // the articles for "news" (23) is in the link gwhsnews.org/wp-json/wp/v2/posts?categories=23
            // but it doesn't show all 195 that we see in the count from gwhsnews.org/wp-json/wp/v2/categories/23

            // this should do the same thing as the request above but more automated
            // not sure how the uri builder works
            UriBuilder builder = new UriBuilder()
            {
                Scheme = "https",
                Host = App.BASE_URL,
                Path = "/wp-json/wp/v2/posts"
            };


            // i think that once you get to the url above with the articles you can get the different things we need but idk how to do that
            // also within each individual article json there is a "categories" tag that lists the ids of the categorries it is connect to since there is usually more than 1

            uri = builder.Uri;
        }

        public async Task GetPostsAsync()
        {
            // gets URI for server counterpart to model
            // attempts to make an HTTP GET request and deserialize it for easy access
            try
            {
                HttpResponseMessage response = await App.client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Json.Post> postJson = JsonConvert.DeserializeObject<List<Json.Post>>(content);
                    foreach (Json.Post post in postJson)
                    {
                        postIDs.Add(post.id);
                    }
                    // TODO: create new function eventually
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

        }

    }
}