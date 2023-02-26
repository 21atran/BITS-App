using BITS_App.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace BITS_App.ViewModels
{

	public class PostsViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<Post> Posts { get; private set; }

		public PostsViewModel()
		{


        }

        public async Task RefreshAsync() {
            // gets URI for server counterpart to model
            Uri uri = new($"https://{App.BASE_URL}/wp-json/wp/v2/posts");
            List<Post> postList = new List<Post>();
            // attempts to make an HTTP GET request and deserialize it for easy access
            try {
                HttpResponseMessage response = await App.client.GetAsync(uri);
                if (response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Json.Post> postJsonList = JsonConvert.DeserializeObject<List<Json.Post>>(content);
                    postList = new List<Post>();
                    foreach (Json.Post postJson in postJsonList) {
                        postList.Add(new Post()
                        {
                            json = postJson
                        });
                    }

                }
            } catch (Exception ex) {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            Posts = new ObservableCollection<Post>(postList);
            OnPropertyChanged(nameof(Posts));

            foreach (Post post in Posts) {
                await post.RefreshAsync();
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

