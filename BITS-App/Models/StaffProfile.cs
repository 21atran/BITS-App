using System;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
namespace BITS_App.Models {
	/// <summary>
	/// Model representing a staff profile.
	///</summary>
	internal class StaffProfile : RestBase {

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
		public string? FeaturedMedia => null;

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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));

			//do we need to account for when a staff member updates their info?
			//what methods would i need to create for the staff profile? would it be the same as the post since it's retrieving data from a source?


			



        }
	}
}


