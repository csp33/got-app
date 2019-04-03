using GoT.Models;
using GoT.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GoT.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BooksPage : Page
    {
        public List<Book> BooksList { get; set; }
        public List<Book> SearchResults { get; set; }

        public BooksPage()
        {
            this.InitializeComponent();
            BooksList = new List<Book>();
        
        }

        private void BooksSearchBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
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
        }

        private void BooksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (sender as ListBox).SelectedItem as Book;
            if(selected!=null)
            {
                NameTextBox.Text = selected.name;
                AuthorsItemsControl.ItemsSource = selected.authors;
  
                CharactersListBox.ItemsSource = selected.characters;

                povCharactersListBox.ItemsSource = selected.povCharacters;


                CountryTextBox.Text = selected.country;
                ISBNTextBox.Text = selected.isbn;
                MediaTypeTextBox.Text = selected.mediaType;
                ReleasedTextBox.Text = selected.released.Substring(0,10);
                PublisherTextBox.Text = selected.publisher;
                NumberOfPagesTextBox.Text = selected.numberOfPages.ToString();
            }

        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var service = new GoTService();
            var books = await service.GetBooksAsync();
            foreach(var item in books)
            {
                if (item.name != null)
                {
                    BooksList.Add(item);
                }
            }
            BooksListBox.ItemsSource = BooksList;
        }
    }
}
