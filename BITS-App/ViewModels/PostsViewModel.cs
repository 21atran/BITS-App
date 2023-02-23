
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.ObjectModel;


namespace BITS_App.ViewModels
{

	
    /// <summary>
    /// Represents an object that has inherited a list of posts to sort through
    /// </summary>
	public class PostsViewModel : INotifyPropertyChanged//Post is to represent the type of elements in the collection view
	{
		IList<post> source; //list of ALL posts
		Post selectedPost; //i dont think we need but in case we want to do something with the selected post
		int selectionCount = 1;

		CollectionView postsView = new CollectionView();
		postsView.SetBinding(ItemsView.ItemsSourceProperty, "post"/*post/article object*/);
		public ObservableCollection<post> Posts { get; set; } //represents name of the grouping (like news, entertainment, etc)
		public IList<PostsModel> EmptyPosts { get; set; } 
		public Command GetPostsCommand { get; set; }
		public PostsViewModel()
		{
			
			source = new List<post>() { } //insert list of ALL posts (getter method maybe)

			GetPostsCommand = new Command(async () => await GetPostsAsync());
			Posts = new ObservableCollection<post>
			{
				new post { /*put necessary data by using a getter method of some sort*/}
			}
		}
	}
}

