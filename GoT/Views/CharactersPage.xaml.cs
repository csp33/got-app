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
            if (selected != null)
            {
                //Texblocks
                NameTextBox.Text = selected.name == "" ? "Unknown" : selected.name;
                GenderTextBox.Text = selected.gender == "" ? "Unknown" : selected.gender;
                CultureTextBox.Text = selected.culture == "" ? "Unknown" : selected.culture;
                BornTextBox.Text = selected.born == "" ? "Unknown" : selected.born;
                DiedTextBox.Text = selected.died == "" ? "Unknown" : selected.died;
                //ListBoxes
                TVSeriesListBox.ItemsSource = selected.tvSeries;
                TitlesListBox.ItemsSource = selected.titles;
                AliasesListBox.ItemsSource = selected.aliases;
                PlayedByListBox.ItemsSource = selected.playedBy;
                /**Links**/

                //Character
                var father = CharactersList.Find(x => x.url == selected.father);
                FatherListBox.Items.Clear();
                FatherListBox.Items.Add(father == null ? "Unknown" : father.ToString());

                var mother = CharactersList.Find(x => x.url == selected.mother);
                MotherListBox.Items.Clear();
                MotherListBox.Items.Add(mother == null ? "Unknown" : mother.ToString());

                var spouse = CharactersList.Find(x => x.url == selected.spouse);
                SpouseListBox.Items.Clear();
                SpouseListBox.Items.Add(spouse == null ? "Unknown" : spouse.ToString());

                //House
                AllegiancesListBox.ItemsSource = selected.allegiances;

                //Book
                BooksListBox.ItemsSource = selected.books;
                POVBooksListBox.ItemsSource = selected.povBooks;
            }
           
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var service = new GoTService();
            var characters = await service.GetCharactersAsync();

            foreach (var item in characters)
            {
                if (item.name != "")
                {
                    CharactersList.Add(item);
                }
            }
            CharactersListBox.ItemsSource = CharactersList;
        }

        private void HouseListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var house = (sender as ListBox).SelectedItem;
            //Go to house page

        }

        private void BooksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var book = (sender as ListBox).SelectedItem;
            //Go to book page

        }

        private void FatherListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedstr = (sender as ListBox).SelectedItem as string;
            CharactersListBox.SelectedItem = CharactersList.Find(x => x.name == selectedstr);
        }
    }
}
