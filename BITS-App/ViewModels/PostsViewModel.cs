using BITS_App.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace BITS_App.ViewModels;

public class PostsViewModel : INotifyPropertyChanged {
	public ObservableCollection<Post> Posts { get; private set; }
    
    public int[] Categories { get; set; } = new int[0];

    public async Task RefreshAsync() {
        // builds URI for server counterpart to model
        UriBuilder builder = new UriBuilder();
        builder.Scheme = "https";
        builder.Host = App.BASE_URL;
        builder.Path = "/wp-json/wp/v2/posts";
        builder.Query = await new FormUrlEncodedContent(
            new Dictionary<string, string> {
                { "categories", string.Join(", ", Categories) },
            }
        ).ReadAsStringAsync();
        Uri uri = builder.Uri;

        // attempts to make an HTTP GET request and deserialize it for easy access
        List<Post> postList = new List<Post>();
        try {
            HttpResponseMessage response = await App.client.GetAsync(uri);

            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                List<Json.Post> postJsonList = JsonConvert.DeserializeObject<List<Json.Post>>(content);
                postList = new List<Post>();

                foreach (Json.Post postJson in postJsonList) {
                    postList.Add(new Post() {
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

    void OnPropertyChanged([CallerMemberName] string propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}