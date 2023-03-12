using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;

namespace BITS_App.Models {
    /// <summary>
    /// Model representing a single media entry.
    /// </summary>
    public class Media : RestBase {
        public override event PropertyChangedEventHandler PropertyChanged;

        // FIELDS
        protected Json.Media json;

        // CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="Media">Media</see> class with the specified ID.
        /// </summary>
        /// <param name="id">ID number of the post</param>
        public Media(int id) : base($"/wp/v2/media/{id}") { }

        // METHODS
        public override async Task RefreshAsync() {
            Uri uri = GetUri();
            try {
                HttpResponseMessage response = await App.client.GetAsync(uri);
                if (response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();
                    json = JsonConvert.DeserializeObject<Json.Media>(content);
                }
            } catch (Exception ex) {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Link"));
        }

        // BINDINGS
#nullable enable
        public int? Id => json?.id;
        public string? Link => json?.link?.ToString();
#nullable disable
    }
}
