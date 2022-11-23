using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BITS_App.Models
{
    internal class Media: RestModel, INotifyPropertyChanged
    {
        // fields
        protected int id;
        protected Json.Media mediaJson;

        public event PropertyChangedEventHandler PropertyChanged;

        // constructor
        public Media(int id) {
            endpoint = delegate { return "/wp/v2/media/{0}"; };

            this.id = id;
        }

        public async Task<object> RefreshAsync() {
            Uri uri = getUri(id);
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

            return null;
        }

        public string Link => mediaJson.link;
    }
}
