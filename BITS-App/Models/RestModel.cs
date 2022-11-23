using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITS_App.Models {
    internal class RestModel {
        protected static string REST_URL { get { return String.Format("{0}/wp-json", App.BASE_URL); } }
        protected string endpoint = "";

        protected RestModel(string endpoint) {
            this.endpoint = endpoint;
        }

        protected Uri getUri() {
            return new Uri("http://" + REST_URL + String.Format(endpoint));
        }

        protected Uri getUri(object? args) {
            return new Uri("http://" + REST_URL + String.Format(endpoint, args));
        }
    }
}
