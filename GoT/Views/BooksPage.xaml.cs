using GoT.Models;
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
            Book a = new Book();
            a.name = "First test book";
            a.authors.Add("First author");
            a.authors.Add("Second author");
            a.characters.Add("Char1");
            a.characters.Add("Char2");
            a.country = "USA";
            a.isbn = "123";
            a.mediaType = "book";
            a.numberOfPages = 1000;
            a.povCharacters.Add("Arya");
            a.povCharacters.Add("Daenerys");
            a.povCharacters.Add("Tyrion");
            a.povCharacters.Add("Sansa");
            a.publisher = "P";
            a.released = "27/10/2010";

            Book b = new Book();
            b.name = "Second test book";
            b.authors.Add("First author");
            b.authors.Add("Second author");
            b.characters.Add("Char1");
            b.characters.Add("Char2");
            b.country = "USA";
            b.isbn = "123";
            b.mediaType = "book";
            b.numberOfPages = 1000;
            b.povCharacters.Add("Arya");
            b.povCharacters.Add("Daenerys");
            b.povCharacters.Add("Tyrion");
            b.povCharacters.Add("Sansa");
            b.publisher = "P";
            b.released = "27/10/2010";
            BooksList.Add(a);
            BooksList.Add(b);
            /*
            var service = new GoTService();
            var Books = await service.GetBooksAsync();
            Books_list = new List<Book>();

            foreach(var item in BooksPage)
            {
                Books_list.Add(item);
            }
            */
            BooksListBox.ItemsSource = BooksList;
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
                ReleasedTextBox.Text = selected.released;
                PublisherTextBox.Text = selected.publisher;
                NumberOfPagesTextBox.Text = selected.numberOfPages.ToString();
            }

        }
    }
}
