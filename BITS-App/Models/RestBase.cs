using System.ComponentModel;

namespace BITS_App.Models {
    /// <summary>
    /// Provides basic REST API helpers to expedite the process of creating REST-based models.
    /// </summary>
    public class RestBase : INotifyPropertyChanged {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected static string REST_URL => String.Format("{0}/wp-json", App.BASE_URL);
        protected string endpoint = "";

        protected RestBase() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestBase">RestBase</see> class with the specified endpoint.
        /// </summary>
        /// <param name="endpoint">Final endpoint to be linked to. <see cref="string.Format(string, object?[])">String.Format</see>-style placeholders are permitted.</param>
        protected RestBase(string endpoint) { this.endpoint = endpoint; }

        /// <summary>
        /// Refreshes the model asynchronously with its server counterpart.
        /// </summary>
        /// <returns>A Task for the refresh operation to be awaited.</returns>
        public virtual async Task RefreshAsync() { }

        /// <summary>
        /// Generates a URI dynamically based on the endpoint.
        /// </summary>
        /// <param name="args">Formatting arguments if placeholders were in endpoint</param>
        /// <returns>A URI for the server counterpart of the model.</returns>
        protected Uri GetUri(params object[] args) => new Uri("https://" + REST_URL + String.Format(endpoint, args));
    }
}
