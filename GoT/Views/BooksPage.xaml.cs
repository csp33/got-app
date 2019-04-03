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
            a.name = "A test book";
            Book b = new Book();
            b.name = "B test book";
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
            var selected = (sender as ListBox).SelectedItem;


        }
    }
}
