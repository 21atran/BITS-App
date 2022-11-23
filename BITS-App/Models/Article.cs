using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL;

namespace BITS_App.Models
{
    internal class Article : RestModel, INotifyPropertyChanged {
        // fields
        protected WordPressClient client;
        protected Media featuredMedia;
        protected int id;
        protected WordPressPCL.Models.Post post;
        protected string raw;
        protected Json.Post postJson;
        protected List<WordPressPCL.Models.MediaItem> medias;

        public event PropertyChangedEventHandler PropertyChanged;

        // constructor
        public Article(int id) {
            endpoint = delegate { return "/wp/v2/posts/{0}"; };

            //creating a client, and taking the wordpress data, storing it into a client variable
            client = new WordPressClient("https://gwhsnews.org/wp-json/");
            this.id = id;

            /*var refreshTask = this.RefreshAsync();
            refreshTask.Wait();*/

            /*var pictasks = client.Media.GetByIDAsync(postJson.featured_media);
            pictasks.Wait();
            featured = pictasks.Result;


            //list of all the media
            var pictask = client.Media.GetAllAsync();
            pictask.Wait();*/

            //list of all the medias that are in website, gets a result
            //medias = (List<WordPressPCL.Models.MediaItem>)pictask.Result;*
        }

        public async Task<object> RefreshAsync() {
            Uri uri = getUri(id);
            try {
                HttpResponseMessage response = await App.client.GetAsync(uri);
                if (response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();
                    postJson = JsonConvert.DeserializeObject<Json.Post>(content);
                }
            } catch (Exception ex) {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AuthorsAndTitles"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));

            featuredMedia = new Media(postJson.featured_media);
            featuredMedia.PropertyChanged += OnPropertyChanged;
            await featuredMedia.RefreshAsync();

            return null;
        }

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e) {
            switch (e.PropertyName) {
                case "Link":
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FeaturedMedia"));
                    break;
            }
        }

        // helper methods
        private string authorsTitlesFormatted() {
            string formatted = "";

            try {
                for (int i = 0; i < postJson?.custom_fields?.writer?.Count; i++) {
                    formatted += postJson.custom_fields.writer[i];
                    formatted += ", ";
                    formatted += postJson.custom_fields.jobtitle[i];

                    if (i < postJson.custom_fields.writer.Count - 1) {
                        formatted += " - ";
                    }
                }
            } catch (NullReferenceException ignored) {
                return null;
            }
            
            return formatted;
        }

        // Bindings
        // title string 
        public string Title => postJson?.title?.rendered;

        // gets a list of authors and joins them in a string
        public string Authors => String.Join(", ", postJson.custom_fields.writer);

        // joins a list of job titles into a string
        public string Titles => String.Join(", ", postJson.custom_fields.jobtitle);

        public string AuthorsAndTitles => authorsTitlesFormatted();

        // DateTime object for the publication date
        public DateTime Date => postJson?.date!=null ? postJson.date : DateTime.UnixEpoch;

        // a link for an image
        public string Image => medias[0].Link.ToString();

        // content string
        public string Content => postJson?.content?.rendered;

        // photo using MediaItem format
        public string FeaturedMediaPhoto => null /*featured.Link.ToString()*/;

        public string FeaturedMedia => featuredMedia?.Link;
    }
}
