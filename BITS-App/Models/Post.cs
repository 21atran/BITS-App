﻿using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;

namespace BITS_App.Models;

/// <summary>
/// Model representing a single post entry.
/// </summary>
public class Post : RestBase {
    public override event PropertyChangedEventHandler PropertyChanged;

    #region FIELDS
    private Json.Post _json;
    public Json.Post json {
        get {
            return _json;
        }
        set {
            _json = value;
            endpoint = value?.id == null ? "" : $"/wp/v2/posts/{value.id}";
        }
    }
    protected Media featuredMedia;
    #endregion

    #region CONSTRUCTORS
    public Post() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Post">Post</see> class with the specified ID.
    /// </summary>
    /// <param name="id">ID number of the post</param>
    public Post(int id) : base($"/wp/v2/posts/{id}") { }
    #endregion

    #region METHODS
    public override async Task RefreshAsync() {
        // gets URI for server counterpart to model
        Uri uri = GetUri();

        // attempts to make an HTTP GET request and deserialize it for easy access
        try {
            HttpResponseMessage response = await App.client.GetAsync(uri);
            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                _json = JsonConvert.DeserializeObject<Json.Post>(content);
            }
        } catch (Exception ex) {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        // tells the UI that several bindings have been updated and should be refreshed
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Authors"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("JobTitles"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AuthorsAndJobTitlesFormatted"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Excerpt"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));

        // generates a Media model for the Featured Media and registers to updates like a binding would - this is supposed to be directly bound to the view, but MAUI doesn't support that as of this writing, so we use a workaround
        featuredMedia = new Media(_json.featured_media);
        featuredMedia.PropertyChanged += OnPropertyChanged;

        // refreshes the Media model
        await featuredMedia.RefreshAsync();
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) {
        switch (e.PropertyName) {
            // if the Featured Media model has a change of link, it cascades to this model's FeaturedMedia binding, which will in turn cascade up to the UI
            case "Link":
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FeaturedMedia"));
                break;
        }
    }
    #endregion

    #region BINDINGS
#nullable enable
    public int? Id => _json?.id ?? -1;
    public string? Title => _json?.title?.rendered;
    public List<string>? Authors => _json?.custom_fields?.writer;
    public List<string>? JobTitles => _json?.custom_fields?.jobtitle;
    public string? AuthorsAndJobTitlesFormatted { 
        get {
            // TODO: Swap this entire binding out for a proper converter.
            string formatted = "";

            try {
                for (int i = 0; i < _json?.custom_fields?.writer?.Count; i++) {
                    formatted += _json.custom_fields.writer[i];
                    formatted += ", ";
                    //formatted += _json.custom_fields.jobtitle[i];

                    if (i < _json.custom_fields.writer.Count - 1) {
                        formatted += " - ";
                    }
                }
            } catch (NullReferenceException) {
                return null;
            }

            return formatted;
        } 
    }
    public DateTime? Date => _json?.date;
    public string? Excerpt => _json?.excerpt?.rendered;
    public string? Content => _json?.content?.rendered;
    public string? FeaturedMedia => featuredMedia?.Link;
#nullable disable
    #endregion
}
