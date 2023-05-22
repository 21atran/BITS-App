using AsyncAwaitBestPractices.MVVM;
using BITS_App.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BITS_App.ViewModels;

public class SearchViewModel : INotifyPropertyChanged {
    // FIELDS
    private PostsViewModel postsViewModel;

    // CONSTRUCTORS
    public SearchViewModel() {
        postsViewModel = new PostsViewModel();
        postsViewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) => { 
            switch (e.PropertyName) {
                case "Posts":
                    OnPropertyChanged("SearchResults");
                    break;
            }
        };
    }
    
    // BINDINGS
    public ICommand PerformSearch => new AsyncCommand<string>(async (string query) => {
        postsViewModel.Search = query;
        await postsViewModel.RefreshAsync();
    });

    public ObservableCollection<Post> SearchResults => postsViewModel.Posts;

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    void OnPropertyChanged([CallerMemberName] string propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}