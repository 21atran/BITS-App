using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BITS_App.Views {
    public partial class MultiplePostPage : ContentPage {

        public MultiplePostPage() {
            InitializeComponent();

            Dispatcher.Dispatch(async () => await GetPostsAsync());
        }

        public async Task GetPostsAsync() {
            // gets URI for server counterpart to model
            Uri uri = new Uri("https://gwhsnews.org/wp-json/wp/v2/posts");

            List<int> info = new List<int>();

            // attempts to make an HTTP GET request and deserialize it for easy access
            try {
                HttpResponseMessage response = await App.client.GetAsync(uri);
                if (response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Json.Post> postJson = JsonConvert.DeserializeObject<List<Json.Post>>(content);
                    foreach(Json.Post post in postJson)
                    {
                        info.Add(post.id);
                    }
                    // create new function eventually 
                }
            } catch (Exception ex) {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

    }
}