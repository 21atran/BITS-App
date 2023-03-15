using HtmlAgilityPack;
using System.ComponentModel;

namespace BITS_App.Pages;

public partial class StaffProfilePage : ContentPage {
	public StaffProfilePage() {
		InitializeComponent();

        BindingContext = new Models.StaffProfile(6889);
        ((Models.StaffProfile)BindingContext).PropertyChanged += OnPropertyChanged;

        Dispatcher.Dispatch(async () => await ((Models.StaffProfile)BindingContext).RefreshAsync());
    }

    /// <summary>
    /// Loads bio to XAML from HTML Content binding of <see cref="Models.StaffProfile">Models.StaffProfile</see>
    /// </summary>
    public async Task LoadBioAsync() {
        // gets html in string format
        string html = ((Models.StaffProfile)BindingContext).Bio ?? "";

        // uses agility pack and creates doc with string
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        // spacing between paragraphs, images, etc.
        bioStackLayout.Spacing = 7;

        // gets the nodes
        HtmlNodeCollection parNodes = htmlDoc.DocumentNode.SelectNodes("/");
        foreach (HtmlNode node in parNodes.Nodes()) {
            // if node is named p then it is a paragraph
            if (node.Name == "p") {
                Label label = new Label() {
                    Text = node.InnerText,
                    FontSize = 10,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = new Color(21, 21, 21)
                    // can format here
                };

                // adds the paragraph to document
                bioStackLayout.Add(label);

            } else if (node.Name == "figure") {
                // this gets the image details of both the image and the caption
                StackLayout chlidLayout = new StackLayout();

                Image img = new Image() {
                    Source = node.SelectSingleNode("//img").Attributes["src"].Value.ToString()
                    // can format here
                };

                Label label = new Label() {
                    Padding = 10,
                    Text = node.SelectSingleNode("//figcaption").InnerText,
                    FontSize = 10.5,
                    BackgroundColor = Colors.Transparent
                    // can format here
                };

                // construct sub-layout with image-caption combination
                chlidLayout.Add(img);
                chlidLayout.Add(label);

                // adds both caption and image at the same time so they are together
                bioStackLayout.Children.Add(chlidLayout);
            }
        }
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) {
        if (sender == BindingContext) {
            switch (e.PropertyName) {
                // if the Content of the model changes, reload it
                case "Bio":
                    Dispatcher.Dispatch(async () => await LoadBioAsync());
                    break;
            }
        }
    }
}