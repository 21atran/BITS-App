using HtmlAgilityPack;
using System.ComponentModel;

namespace BITS_App.Views;

public partial class PostPanelView : ContentView {
    public PostPanelView(int id) {
        InitializeComponent();

        BindingContext = new Models.Post(id);
        ((Models.Post)BindingContext).PropertyChanged += OnPropertyChanged;

        Dispatcher.Dispatch(async () => await ((Models.Post)BindingContext).RefreshAsync());
    }

    /// <summary>
    /// Loads content to XAML from HTML Content binding of <see cref="Models.Post">Models.Post</see>
    /// </summary>
    public async Task LoadExcerptAsync() {
        // gets html in string format
        string html = ((Models.Post)BindingContext).Excerpt ?? "";

        // uses agility pack and creates doc with string
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        // gets the nodes
        HtmlNodeCollection parNodes = htmlDoc.DocumentNode.SelectNodes("/");
        foreach (HtmlNode node in parNodes.Nodes()) {
            // if node is named p then it is a paragraph
            if (node.Name == "p") {
                ExcerptLabel.Text = node.InnerText;
                break;
            }
        }
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) {
        if (sender == BindingContext) {
            switch (e.PropertyName) {
                // if the Content of the model changes, reload it
                case "Excerpt":
                    Dispatcher.Dispatch(async () => await LoadExcerptAsync());
                    break;
            }
        }
    }
}