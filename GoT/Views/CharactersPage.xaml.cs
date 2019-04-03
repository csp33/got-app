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
    public sealed partial class CharactersPage : Page
    {
        public List<Character> CharactersList { get; set; }
        public List<Character> SearchResults { get; set; }
        public CharactersPage()
        {
            this.InitializeComponent();
            CharactersList = new List<Character>();
        }

        private void CharactersSearchBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            string text = args.QueryText;
            text = text.ToLower();
            if (text == "")
            {
                CharactersListBox.ItemsSource = CharactersList;
            }
            else
            {
                SearchResults = new List<Character>();
                foreach (var house in CharactersList)
                {
                    if (house.name.ToLower().Contains(text))
                    {
                        SearchResults.Add(house);
                    }
                }
                CharactersListBox.ItemsSource = SearchResults;
            }
        }

        private void CharactersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (sender as ListBox).SelectedItem as Character;
            NameTextBox.Text = selected.name;
            GenderTextBox.Text = selected.gender;
            CultureTextBox.Text = selected.culture;
            BornTextBox.Text = selected.born;
            DiedTextBox.Text = selected.died;
            TitlesListBox.ItemsSource = selected.titles;
            AliasesListBox.ItemsSource = selected.aliases;
            FatherTextBox.Text = selected.father;
            MotherTextBox.Text = selected.mother;
            SpouseTextBox.Text = selected.spouse;
            AllegiancesListBox.ItemsSource = selected.allegiances;
            BooksListBox.ItemsSource = selected.books;
            POVBooksListBox.ItemsSource = selected.povBooks;
            TVSeriesListBox.ItemsSource = selected.tvSeries;
            PlayedByListBox.ItemsSource = selected.playedBy;      
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var service = new GoTService();
            var characters = await service.GetCharactersAsync();
            foreach (var item in characters)
            {
                if (item.name != null)
                {
                    CharactersList.Add(item);
                }
            }
            CharactersListBox.ItemsSource = CharactersList;
        }
    }
}
