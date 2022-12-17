using System;
namespace BITS_App.Models {
	/// <summary>
	/// Model representing a staff profile.
	///</summary>
	internal class StaffProfile : RestBase {
		//probably need a constructor of some sort LMAO
		
		//constructor-esqe thing
		public StaffProfile(int id) : base($"/wp/v2/staff_profile/{id}") { }

		// BINDINGS
		public string? Name => null;
		public string? Bio => null;
		public string? FeaturedMedia => null;

		// METHODS

	}
}

