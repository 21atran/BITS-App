using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;

namespace BITS_App.Models;
public class Category : RestBase {
    public override event PropertyChangedEventHandler PropertyChanged;

    #region FIELDS
    protected Json.Category json;
    #endregion

    #region CONSTRUCTORS
    /// <summary>
    /// Initializes a new instance of the <see cref="Media">Media</see> class with the specified ID.
    /// </summary>
    /// <param name="id">ID number of the post</param>
    public Category(int id) : base($"/wp/v2/categories/{id}") { }
    #endregion

    #region METHODS
    public override async Task RefreshAsync() {
        Uri uri = GetUri();
        try {
            HttpResponseMessage response = await App.client.GetAsync(uri);
            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                json = JsonConvert.DeserializeObject<Json.Category>(content);
            }
        } catch (Exception ex) {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Link"));
    }
    #endregion

    #region BINDINGS
#nullable enable
    public int? Id => json?.id;
    public string? Name => json?.name;
#nullable disable
    #endregion
}

