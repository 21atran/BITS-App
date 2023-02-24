using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using BITS_App.ViewModels;

namespace BITS_App.Views {
    public partial class MultiplePostPage : ContentPage {

        public MultiplePostPage() {
            InitializeComponent();

            BindingContext = new PostsViewModel();
            Dispatcher.Dispatch(async () => await ((PostsViewModel)BindingContext).RefreshAsync());
        }

        public async Task GetPostsAsync() {
            // gets URI for server counterpart to model
            Uri uri = new($"https://{App.BASE_URL}/wp-json/wp/v2/posts");

            // attempts to make an HTTP GET request and deserialize it for easy access
            try {
                HttpResponseMessage response = await App.client.GetAsync(uri);
                if (response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Json.Post> postJson = JsonConvert.DeserializeObject<List<Json.Post>>(content);
                    foreach(Json.Post post in postJson) {
                        PostPanelView add = new PostPanelView(post.id);
                        //PostPanelStack.Add(add);
                    }
                    // TODO: create new function eventually
                }
            } catch (Exception ex) {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}