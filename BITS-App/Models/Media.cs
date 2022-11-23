using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BITS_App.Models {
    internal class Media : RestBase, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        // fields
        protected Json.Media mediaJson;

        // constructor
        public Media(int id) : base($"/wp/v2/media/{id}") { }

        public override async Task RefreshAsync() {
            Uri uri = getUri();
            try {
                HttpResponseMessage response = await App.client.GetAsync(uri);
                if (response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();
                    mediaJson = JsonConvert.DeserializeObject<Json.Media>(content);
                }
            } catch (Exception ex) {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Link"));
        }

        public string Link => mediaJson.link;
    }
}
