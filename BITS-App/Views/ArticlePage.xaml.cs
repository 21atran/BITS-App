using HtmlAgilityPack;
using System.ComponentModel;

namespace BITS_App.Views;

public partial class ArticlePage : ContentPage {
    public ArticlePage() {
        InitializeComponent();

        BindingContext = new Models.Article(7767);
        ((Models.Article)BindingContext).PropertyChanged += OnPropertyChanged;

        Dispatcher.Dispatch(async () => await ((Models.Article)BindingContext).RefreshAsync());
    }

    /// <summary>
    /// Loads content to XAML from HTML Content binding of <see cref="Models.Article">Models.Article</see>
    /// </summary>
    public async Task LoadContentAsync() {
        // gets html in string format
        string html = ((Models.Article)BindingContext).Content ?? "";

        // uses agility pack and creates doc with string
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        // spacing between paragraphs, images, etc.
        contentStackLayout.Spacing = 7;

        // gets the nodes
        HtmlNodeCollection parNodes = htmlDoc.DocumentNode.SelectNodes("/");
        foreach (HtmlNode node in parNodes.Nodes()) {
            // if node is named p then it is a paragraph
            if (node.Name == "p") {
                Label label = new Label() {
                    Text = node.InnerText,
                    FontSize = 14
                    // can format here
                };

                // adds the paragraph to document
                contentStackLayout.Add(label);

            } else if (node.Name == "#text") {
                // if node is named #text then that is a blank line (spacing already takes care of that, so we do nothing)
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
                contentStackLayout.Children.Add(chlidLayout);
            }
        }
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) {
        if (sender == BindingContext) {
            switch (e.PropertyName) {
                // if the Content of the model changes, reload it
                case "Content":
                    Dispatcher.Dispatch(async () => await LoadContentAsync());
                    break;
            }
        }
    }
}