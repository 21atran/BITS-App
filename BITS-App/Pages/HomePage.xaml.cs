using BITS_App.ViewModels;

namespace BITS_App.Pages;

public partial class HomePage : ContentPage {
	public HomePage() {
		InitializeComponent();
        BindingContext = new PostsViewModel();

        // photo creds to grange #slay
        // to get a spefic category all you need to change is what we put into RefreshAsync as it works the same
        // "?categories?id=(int here of an ID of a category)" is prob what we want idk
        // here is a dictionary of all 17 categories (inlcuding ones that arent shown) -grange
        Dictionary<string, int> categories = new Dictionary<string, int>() {
            {"Book Reviews", 38},
            {"Entertainment", 7},
            {"Fashion", 4},
            {"Features", 8},
            {"Movie Reviews", 45},
            {"Music Reviews", 39},
            {"News", 23},
            {"News Stories", 282},
            {"Opinion", 26},
            {"Recipe Reviews", 277},
            {"Satire", 1106},
            {"Showcase",11},
            {"Sports", 6},
            {"TV Reviews", 83},
            {"Uncategorized", 1},
            {"Video", 12},
            {"West Winds", 105}
        };

        // with the dictionary we need to make the input for RefreshAsync $"?categories=caegories{categories["Book Reviews"]}" which might seem like a lot but helps with understanding what we are getting
        // the id is the words in the dictionary #merriam-webstercouldnever

        String id = "Showcase";
        Dispatcher.Dispatch(async () => await ((PostsViewModel)BindingContext).RefreshAsync($"?categories={categories[id]}"));

        // we should probably make "Showcase" the default category for the home page
        // Dispatcher.Dispatch(async () => await ((PostsViewModel)BindingContext).RefreshAsync());
        
    }

    void OnTapGestureRecognizerTapped(object sender, TappedEventArgs e) {
        Dispatcher.Dispatch(async () => { 
            await Navigation.PushAsync(new PostPage() {
                BindingContext = ((BindableObject)sender).BindingContext
            }); 
        });
    }
}