using System;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;

namespace BITS_App.Models
{
    public class PostsSet
    {
        public List<int> postIDs = new List<int>();
        string request = "";
        public PostsSet()
        {
            int catID = 23;
            string query = "categories";
            request = $"https://{App.BASE_URL}/wp-json/wp/v2/{query}/{catID}";
        }

        public async Task GetPostsAsync()
        {
            // gets URI for server counterpart to model
            Uri uri = new(request);
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