using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITS_App.Models {
    internal class RestBase {
        protected static string REST_URL => String.Format("{0}/wp-json", App.BASE_URL);
        protected string endpoint = "";

        protected RestBase(string endpoint) {
            this.endpoint = endpoint;
        }

        public virtual async Task RefreshAsync() { }

        protected Uri getUri() {
            return new Uri("http://" + REST_URL + String.Format(endpoint));
        }

        protected Uri getUri(object? args) {
            return new Uri("http://" + REST_URL + String.Format(endpoint, args));
        }
    }
}
