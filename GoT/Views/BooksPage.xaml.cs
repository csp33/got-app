using GoT.Models;
using GoT.Services;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace GoT.Views
{

    public sealed partial class BooksPage : Page
    {
        public List<Book> BooksList { get; set; }
        public List<Book> SearchResults { get; set; }

        public BooksPage()
        {
            this.InitializeComponent();
            BooksList = new List<Book>();

        }

        //Searchbox
        private void BooksSearchBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            LoadingRing.IsActive = true;
            string text = args.QueryText;
            text = text.ToLower();
            if (text == "")
            {
                BooksListBox.ItemsSource = BooksList;
            }
            else
            {
                SearchResults = new List<Book>();
                foreach (var house in BooksList)
                {
                    if (house.name.ToLower().Contains(text))
                    {
                        SearchResults.Add(house);
                    }
                }
                BooksListBox.ItemsSource = SearchResults;
            }
            LoadingRing.IsActive = false;
        }

        //Update view
        private void BooksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadingRing.IsActive = true;
            povCharactersListBox.Items.Clear();
            CharactersListBox.Items.Clear();
            var selected = (sender as ListBox).SelectedItem as Book;
            if (selected != null)
            {
                showBookInfo(selected);
            }
            LoadingRing.IsActive = false;

        }

        //Show the information
        private async void showBookInfo(Book selected)
        {
            //Texblocks
            NameTextBox.Text = selected.name;
            CountryTextBox.Text = selected.country;
            ISBNTextBox.Text = selected.isbn;
            MediaTypeTextBox.Text = selected.mediaType;
            ReleasedTextBox.Text = selected.released.Substring(0, 10);
            PublisherTextBox.Text = selected.publisher;
            NumberOfPagesTextBox.Text = selected.numberOfPages.ToString();
            //ListBoxes
            AuthorsListBox.ItemsSource = selected.authors;

            /**Links to Characters**/
            var service = new GoTService();

            foreach (var item in selected.povCharacters)
            {
                if (item != "")
                {
                    var tmp = await service.GetCharacterAsync(item);
                    if (tmp.name != "")
                    {
                        povCharactersListBox.Items.Add(tmp);
                    }
                }
            }

            foreach (var item in selected.characters)
            {
                if (item != "")
                {
                    var tmp = await service.GetCharacterAsync(item);
                    if(tmp.name!="")
                    {
                        CharactersListBox.Items.Add(tmp);
                    }
                }
            }
        }

        //Load the parameter and/or the whole list.
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var arg = e.Parameter as Book;
            //If we have to load a book
            if (arg != null)
            {
                LoadingRing.IsActive = true;
                showBookInfo(arg);
                LoadingRing.IsActive = false;
            }
            //After that, load the whole list if has not been loaded yet
            if (BooksList.Count() == 0)
            {
                Books_Searchbox.IsEnabled = false;
                var service = new GoTService();
                LoadingRing.IsActive = true;
                var books = await service.GetBooksAsync(1);
                int page = 2;
                while (books.Count() != 0)
                {
                    foreach (var item in books)
                    {
                        if (item.name != "")
                        {
                            BooksList.Add(item);
                        }
                    }
                    books = await service.GetBooksAsync(page);
                    page++;
                }
                LoadingRing.IsActive = false;
                Books_Searchbox.IsEnabled = true;
            }
            BooksListBox.ItemsSource = BooksList;
        }

        //Go to character page
        private void CharactersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var character = (sender as ListBox).SelectedItem as Character;
            this.Frame.Navigate(typeof(CharactersPage), character);

        }
    }
}
