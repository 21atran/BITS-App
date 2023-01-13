using System;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
namespace BITS_App.Models {
	/// <summary>
	/// Model representing a staff profile.
	///</summary>
	internal class StaffProfile : RestBase {
        public override event PropertyChangedEventHandler PropertyChanged;

        //FIELDS
        protected Json.StaffProfile staffProfileJson;
		protected Media featuredMedia;

		//probably need a constructor of some sort LMAO
		//constructor-esqe thing
		public StaffProfile(int id) : base($"/wp/v2/staff_profile/{id}") { }

		// BINDINGS
		public string? Name => staffProfileJson?.title?.rendered;
		public string? Excerpt => staffProfileJson?.excerpt;
		public string? Bio => staffProfileJson?.content?.rendered;
		public string? FeaturedMedia => featuredMedia?.Link;

		// METHODS - i need something that would snatch the data to make it accessible for Amy
		public override async Task RefreshAsync() {
			// gets URI for server counterpart to model
			Uri uri = GetUri();

			//attempts to make an HTTP GET request and deserialize it for easy access (but make it staff)
			try {
				HttpResponseMessage response = await App.client.GetAsync(uri);
				if (response.IsSuccessStatusCode) {
					string content = await response.Content.ReadAsStringAsync();
					staffProfileJson = JsonConvert.DeserializeObject<Json.StaffProfile>(content);
				}
			} catch (Exception ex) {
				Debug.WriteLine(@"\tERROR {0}", ex.Message);
			}

            //update bindings according to the profile selected
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Excerpt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Bio"));

            // generates a Media model for the Featured Media and registers to updates like a binding would - this is supposed to be directly bound to the view, but MAUI doesn't support that as of this writing, so we use a workaround
            featuredMedia = new Media(staffProfileJson.featured_media);
            featuredMedia.PropertyChanged += OnPropertyChanged;

            // refreshes the Media model
            await featuredMedia.RefreshAsync();

            //do we need to account for when a staff member updates their info?
            //what methods would i need to create for the staff profile? would it be the same as the post since it's retrieving data from a source?
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) {
            switch (e.PropertyName) {
                // if the Featured Media model has a change of link, it cascades to this model's FeaturedMedia binding, which will in turn cascade up to the UI
                case "Link":
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FeaturedMedia"));
                    break;
            }
        }
    }
}


