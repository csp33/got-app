using GoT.Models;
using GoT.Services;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace GoT.Views
{

    public sealed partial class CharactersPage : Page
    {
        public List<Character> CharactersList { get; set; }
        public List<Character> SearchResults { get; set; }
        public CharactersPage()
        {
            this.InitializeComponent();
            CharactersList = new List<Character>();
        }


        //Searchbox
        private void CharactersSearchBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            LoadingRing.IsActive = true;
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
            LoadingRing.IsActive = false;
        }

        //Update the current view
        private void CharactersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadingRing.IsActive = true;
            var selected = (sender as ListBox).SelectedItem as Character;
            if (selected != null)
            {
                showCharacterInfo(selected);
            }
            LoadingRing.IsActive = false;
        }

        //Fill in the boxes
        private async void showCharacterInfo(Character selected)
        {
            //Texblocks
            NameTextBox.Text = selected.name;
            GenderTextBox.Text = selected.gender;
            CultureTextBox.Text = selected.culture;
            BornTextBox.Text = selected.born;
            DiedTextBox.Text = selected.died;
            //ListBoxes
            TVSeriesListBox.ItemsSource = selected.tvSeries;
            TitlesListBox.ItemsSource = selected.titles;
            AliasesListBox.ItemsSource = selected.aliases;
            PlayedByListBox.ItemsSource = selected.playedBy;
            /**Links**/

            //Character
            var father = CharactersList.Find(x => x.url == selected.father);

            FatherListBox.Items.Clear();
            if (father != null)
            {
                if(father.name!="")
                {
                    FatherListBox.Items.Add(father);
                }
            }

            var mother = CharactersList.Find(x => x.url == selected.mother);

            MotherListBox.Items.Clear();
            if (mother != null)
            {
                if(mother.name!="")
                {
                    MotherListBox.Items.Add(mother);
                }
            }
            var spouse = CharactersList.Find(x => x.url == selected.spouse);

            SpouseListBox.Items.Clear();
            if (spouse != null)
            {
                if(spouse.name!="")
                {
                    SpouseListBox.Items.Add(spouse);
                }
            }

            //House
            var service = new GoTService();

            AllegiancesListBox.Items.Clear();
            foreach (var item in selected.allegiances)
            {
                if (item != "")
                {
                    var tmp = await service.GetHouseAsync(item);
                    if(tmp.name!="")
                    {
                        AllegiancesListBox.Items.Add(tmp);
                    }
                }
            }

            //Book
            BooksListBox.Items.Clear();
            foreach (var item in selected.books)
            {
                if (item != "")
                {
                    var tmp = await service.GetBookAsync(item);
                    if(tmp.name!="")
                    {
                        BooksListBox.Items.Add(tmp);
                    }
                }
            }
            POVBooksListBox.Items.Clear();
            foreach (var item in selected.povBooks)
            {
                if (item != "")
                {
                    var tmp = await service.GetBookAsync(item);
                    if(tmp.name!="")
                    {
                        POVBooksListBox.Items.Add(tmp);
                    }
                }
            }
        }

        //Load the parameter, if there is one. Else, load the whole list.
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var arg = e.Parameter as Character;
            //If we have to load a character
            if (arg != null)
            {
                LoadingRing.IsActive = true;
                showCharacterInfo(arg);
                LoadingRing.IsActive = false;
            }
            //After loading the character, we proceed to load the list if we have to
            if (CharactersList.Count() == 0)
            {
                Characters_Searchbox.IsEnabled = false;
                var service = new GoTService();
                LoadingRing.IsActive = true;
                //We need to take all the pages
                var characters = await service.GetCharactersAsync(1);
                int page = 2;
                while (characters.Count != 0)
                {
                    foreach (var item in characters)
                    {
                        if (item.name != "")
                        {
                            CharactersList.Add(item);
                        }
                    }
                    characters = await service.GetCharactersAsync(page);
                    page++;
                }
                LoadingRing.IsActive = false;
                Characters_Searchbox.IsEnabled = true;
            }
            CharactersListBox.ItemsSource = CharactersList;
        }

        //Go to the house page
        private void HouseListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var house = (sender as ListBox).SelectedItem as House;
            this.Frame.Navigate(typeof(HousesPage), house);

        }

        //Go to the book page
        private void BooksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var book = (sender as ListBox).SelectedItem as Book;
            this.Frame.Navigate(typeof(BooksPage), book);

        }

        //Look for the character and load his/her information
        private void FatherListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadingRing.IsActive = true;
            var selectedCharacter = (sender as ListBox).SelectedItem as Character;
            CharactersListBox.ItemsSource = CharactersList;
            CharactersListBox.SelectedItem = CharactersList.Find(x => x == selectedCharacter);
            LoadingRing.IsActive = false;
        }
    }
}
