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
    public int Count { get; set; } = 10;

	public PostsViewModel() { }

    public async Task RefreshAsync() {
        // builds URI for server counterpart to model
        UriBuilder builder = new UriBuilder();
        builder.Scheme = "https";
        builder.Host = App.BASE_URL;
        builder.Path = "/wp-json/wp/v2/posts";       

        // attempts to make an HTTP GET request and deserialize it for easy access
        List<Post> postList = new List<Post>();
        for (int page = 1; page <= Math.Ceiling(Count/10d); page++)
        {
            builder.Query = await new FormUrlEncodedContent(
                new Dictionary<string, string> {
                    { "categories", string.Join(", ", Categories) },
                    { "per_page", "10" },
                    { "page", page.ToString() }
                }
            ).ReadAsStringAsync();
            Uri uri = builder.Uri;

            try
            {
                HttpResponseMessage response = await App.client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Json.Post> postJsonList = JsonConvert.DeserializeObject<List<Json.Post>>(content);

                    foreach (Json.Post postJson in postJsonList)
                    {
                        postList.Add(new Post()
                        {
                            json = postJson
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            if (postList.Count > Count)
            {
                postList.RemoveRange(Count, postList.Count - Count);
            }

            Posts = new ObservableCollection<Post>(postList);
            OnPropertyChanged(nameof(Posts));
        } 

        

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