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

            // this should do the same thing as the request above but more automated
            UriBuilder builder = new UriBuilder()
            {
                Scheme = "https",
                Host = App.BASE_URL,
                Path = "/wp-json/wp/v2/posts"
            };

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