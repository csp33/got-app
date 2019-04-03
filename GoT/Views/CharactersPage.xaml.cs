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
    public sealed partial class CharactersPage : Page
    {
        public List<Character> CharactersList { get; set; }
        public List<Character> SearchResults { get; set; }
        public CharactersPage()
        {
            this.InitializeComponent();
            CharactersList = new List<Character>();
            Character a = new Character();
            a.name = "First test character";
            a.gender = "Male";
            a.aliases.Add("Alias1");
            a.aliases.Add("Alias2");
            a.allegiances.Add("allegiance 1");
            a.allegiances.Add("allegiance 2");
            a.books.Add("first");
            a.books.Add("second");
            a.books.Add("third");
            a.culture = "culture 1";
            a.born = "17/10/1980";
            a.died = "20/10/2010";
            a.father = "father";
            a.mother = "mother";
            a.playedBy.Add("actor");
            a.playedBy.Add("actor2");
            a.playedBy.Add("actor3");
            a.povBooks.Add("first book");
            a.povBooks.Add("sec book");
            a.povBooks.Add("third book");
            a.spouse = "spouse 1";
            a.titles.Add("king");
            a.titles.Add("title2");
            a.tvSeries.Add("season 1");
            a.tvSeries.Add("season 2");

            Character b = new Character();
            b.name = "Second test character";
            b.aliases.Add("Alias1");
            b.aliases.Add("Alias2");
            b.allegiances.Add("allegiance 1");
            b.allegiances.Add("allegiance 2"); b.books.Add("first");
            b.books.Add("second");
            b.books.Add("third");
            b.born = "17/10/1980";
            b.died = "20/10/2010";
            b.father = "father";
            b.culture = "culture 2";
            b.mother = "mother";
            b.playedBy.Add("actor");
            b.gender = "Female";
            b.playedBy.Add("actor2");
            b.playedBy.Add("actor3");
            b.povBooks.Add("first book");
            b.povBooks.Add("sec book");
            b.povBooks.Add("third book");
            b.spouse = "spouse 1";
            b.titles.Add("king");
            b.titles.Add("title2");
            b.tvSeries.Add("season 1");
            b.tvSeries.Add("season 2");
            CharactersList.Add(a);
            CharactersList.Add(b);


            /*
            var service = new GoTService();
            var Characters = await service.GetCharactersAsync();
            Characters_list = new List<House>();

            foreach(var item in CharactersPage)
            {
                Characters_list.Add(item);
            }
            */
            CharactersListBox.ItemsSource = CharactersList;
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
    }
}
