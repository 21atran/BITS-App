using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL;

namespace BITS_App.Models {
    internal class Article : RestBase, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        // fields
        protected Json.Post postJson;
        protected Media featuredMedia;

        // constructor
        public Article(int id) : base($"/wp/v2/posts/{id}") { }

        public override async Task RefreshAsync() {
            Uri uri = getUri();
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
        }

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e) {
            switch (e.PropertyName) {
                case "Link":
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FeaturedMedia"));
                    break;
            }
        }

        // Bindings
        // title string 
        public string? Title => postJson?.title?.rendered;

        // gets a list of authors and joins them in a string
        public List<string>? Authors => postJson?.custom_fields?.writer;

        // joins a list of job titles into a string
        public List<string>? JobTitles => postJson?.custom_fields?.jobtitle;

        public string? AuthorsAndJobTitlesFormatted { get
            {
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
        }

        // DateTime object for the publication date
        public DateTime? Date => postJson?.date;

        // content string
        public string? Content => postJson?.content?.rendered;

        public string? FeaturedMedia => featuredMedia?.Link;
    }
}
