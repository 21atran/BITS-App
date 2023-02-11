using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;

namespace BITS_App.Models {
	/// <summary>
	/// Model representing a staff profile.
	///</summary>
	internal class StaffProfile : RestBase {
        public override event PropertyChangedEventHandler PropertyChanged;

        // FIELDS
        protected Json.StaffProfile json;
		protected Media featuredMedia;

        // CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="StaffProfile">StaffProfile</see> class with the specified ID.
        /// </summary>
        /// <param name="id"></param>
		public StaffProfile(int id) : base($"/wp/v2/staff_profile/{id}") { }

		// METHODS
		public override async Task RefreshAsync() {
			// gets URI for server counterpart to model
			Uri uri = GetUri();

			// attempts to make an HTTP GET request and deserialize it for easy access
			try {
				HttpResponseMessage response = await App.client.GetAsync(uri);
				if (response.IsSuccessStatusCode) {
					string content = await response.Content.ReadAsStringAsync();
					json = JsonConvert.DeserializeObject<Json.StaffProfile>(content);
				}
			} catch (Exception ex) {
				Debug.WriteLine(@"\tERROR {0}", ex.Message);
			}

            // update bindings according to the profile selected
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Excerpt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Bio"));

            // generates a Media model for the Featured Media and registers to updates like a binding would - this is supposed to be directly bound to the view, but MAUI doesn't support that as of this writing, so we use a workaround
            featuredMedia = new Media(json.featured_media);
            featuredMedia.PropertyChanged += OnPropertyChanged;

            // refreshes the Media model
            await featuredMedia.RefreshAsync();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) {
            switch (e.PropertyName) {
                // if the Featured Media model has a change of link, it cascades to this model's FeaturedMedia binding, which will in turn cascade up to the UI
                case "Link":
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FeaturedMedia"));
                    break;
            }
        }

        // BINDINGS
#nullable enable
        public string? Name => json?.title?.rendered;
        public string? Excerpt => json?.excerpt;
        public string? Bio => json?.content?.rendered;
        public string? FeaturedMedia => featuredMedia?.Link;
#nullable disable
    }
}


