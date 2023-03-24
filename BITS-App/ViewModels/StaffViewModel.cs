using BITS_App.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace BITS_App.ViewModels;

public class StaffViewModel : INotifyPropertyChanged {
	public ObservableCollection<StaffProfile> StaffProfiles { get; private set; }

    public async Task RefreshAsync() {
        // gets URI for server counterpart to model
        Uri uri = new($"https://{App.BASE_URL}/wp-json/wp/v2/staff_profile");
        List<StaffProfile> staffProfileList = new List<StaffProfile>();

        // attempts to make an HTTP GET request and deserialize it for easy access
        try {
            HttpResponseMessage response = await App.client.GetAsync(uri);

            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                List<Json.StaffProfile> staffProfileJsonList = JsonConvert.DeserializeObject<List<Json.StaffProfile>>(content);
                staffProfileList = new List<StaffProfile>();

                foreach (Json.StaffProfile staffProfileJson in staffProfileJsonList) {
                    staffProfileList.Add(new StaffProfile() {
                        json = staffProfileJson
                    });
                }
            }
        } catch (Exception ex) {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        StaffProfiles = new ObservableCollection<StaffProfile>(staffProfileList);
        OnPropertyChanged(nameof(StaffProfiles));

        foreach (StaffProfile staffProfile in StaffProfiles) {
            await staffProfile.RefreshAsync();
        }
    }

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    void OnPropertyChanged([CallerMemberName] string propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}