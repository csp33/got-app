using GoT.Models;
using GoT.Services;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace GoT.Views
{

    public sealed partial class HousesPage : Page
    {
        public List<House> HousesList { get; set; }
        public List<House> SearchResults { get; set; }

        public HousesPage()
        {
            this.InitializeComponent();
            HousesList = new List<House>();
        }

        // Searchbox
        private void HousesSearchBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            LoadingRing.IsActive = true;
            string text = args.QueryText;
            text = text.ToLower();
            //If no text entered then show all the items
            if (text == "")
            {
                HousesListBox.ItemsSource = HousesList;
            }
            else
            {
                SearchResults = new List<House>();
                foreach (var house in HousesList)
                {
                    if (house.name.ToLower().Contains(text))
                    {
                        SearchResults.Add(house);
                    }
                }
                HousesListBox.ItemsSource = SearchResults;
            }
            LoadingRing.IsActive = false;
        }

        //Fill the boxes with the information of the house
        private async void showHouseInfo(House selected)
        {

            //Textblocks
            DiedOutTextBox.Text = selected.diedOut;
            FoundedTextBox.Text = selected.founded;

            WordsTextBox.Text = selected.words;
            CoatOfArmsTextBox.Text = selected.coatOfArms;
            RegionTextBox.Text = selected.region;
            NameTextBox.Text = selected.name;
            //ListBoxes
            AncestralWeaponsListBox.ItemsSource = selected.ancestralWeapons;
            SeatsListBox.ItemsSource = selected.seats;
            TitlesListBox.ItemsSource = selected.titles;
            /**Links***/

            //House
            CurrentLordListBox.Items.Clear();
            CadetBranchesListBox.Items.Clear();
            HeirListBox.Items.Clear();
            FounderListBox.Items.Clear();
            SwornMembersListBox.Items.Clear();

            foreach (var item in selected.cadetBranches)
            {
                var tmp = HousesList.Find(x => x.url == item);
                if (tmp != null)
                {
                    CadetBranchesListBox.Items.Add(tmp);
                }
            }
            OverlordListBox.Items.Clear();
            var overlord = HousesList.Find(x => x.url == selected.overlord);
            OverlordListBox.Items.Clear();
            if (overlord != null)
            {
                OverlordListBox.Items.Add(overlord);
            }

            //Character
            var service = new GoTService();

            foreach (var item in selected.swornMembers)
            {
                if (item != "")
                {
                    SwornMembersListBox.Items.Add(await service.GetCharacterAsync(item));
                }
            }
            if (selected.founder != "")
            {
                FounderListBox.Items.Add(await service.GetCharacterAsync(selected.founder));
            }

            if (selected.heir != "")
            {
                HeirListBox.Items.Add(service.GetCharacterAsync(selected.heir));
            }

            if (selected.currentLord != "")
            {
                CurrentLordListBox.Items.Add(await service.GetCharacterAsync(selected.currentLord));
            }
        }

        //Update the view with the house information
        private void HousesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadingRing.IsActive = true;

            var selected = (sender as ListBox).SelectedItem as House;
            if (selected != null)
            {
                showHouseInfo(selected);
            }
            LoadingRing.IsActive = false;

        }

        //If a parameter is given, show it. Else, load all the view.
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var arg = e.Parameter as House;
            //If we have to load a house
            if (arg != null)
            {
                LoadingRing.IsActive = true;
                showHouseInfo(arg);
                LoadingRing.IsActive = false;
            }
            //After loading the character, we proceed to load the list if necessary
            if (HousesList.Count() == 0)
            {
                Houses_Searchbox.IsEnabled = false;
                var service = new GoTService();
                LoadingRing.IsActive = true;
                var houses = await service.GetHousesAsync(1);
                int page = 2;
                while (houses.Count() != 0)
                {
                    foreach (var item in houses)
                    {
                        if (item.name != "")
                        {
                            HousesList.Add(item);
                        }
                    }
                    houses = await service.GetHousesAsync(page);
                    page++;
                }
                LoadingRing.IsActive = false;
                Houses_Searchbox.IsEnabled = true;
            }
            HousesListBox.ItemsSource = HousesList;
        }

        //Update the current view

        private void CadetBranchesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadingRing.IsActive = true;
            var selectedHouse = (sender as ListBox).SelectedItem as House;
            HousesListBox.ItemsSource = HousesList;
            HousesListBox.SelectedItem = HousesList.Find(x => x == selectedHouse);
            LoadingRing.IsActive = false;
        }

        //Navigate to the character page
        private void SwornMembersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var character = (sender as ListBox).SelectedItem as Character;
            this.Frame.Navigate(typeof(CharactersPage), character);
        }
    }
}
