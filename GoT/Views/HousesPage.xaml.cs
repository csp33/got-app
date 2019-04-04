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
    public sealed partial class HousesPage : Page
    {
        public List<House> HousesList { get; set; }
        public List<House> SearchResults { get; set; }

        public HousesPage()
        {
            this.InitializeComponent();
            HousesList = new List<House>();
        }

        private void HousesSearchBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            string text = args.QueryText;
            text = text.ToLower();
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
        }

        private void HousesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (sender as ListBox).SelectedItem as House;
            if (selected != null)
            {

                //Textblocks
                DiedOutTextBox.Text = selected.diedOut == "" ? "Unknown" : selected.diedOut;
                FoundedTextBox.Text = selected.founded == "" ? "Unknown" : selected.founded;

                WordsTextBox.Text = selected.words == "" ? "Unknown" : selected.words;
                CoatOfArmsTextBox.Text = selected.coatOfArms == "" ? "Unknown" : selected.coatOfArms;
                RegionTextBox.Text = selected.region == "" ? "Unknown" : selected.region;
                NameTextBox.Text = selected.name == "" ? "Unknown" : selected.name;
                //ListBoxes
                AncestralWeaponsListBox.ItemsSource = selected.ancestralWeapons;
                SeatsListBox.ItemsSource = selected.seats;
                TitlesListBox.ItemsSource = selected.titles;
                /**Links***/

                //House
                List<string> cadetBranches = new List<string>();
                foreach (var item in selected.cadetBranches)
                {
                    cadetBranches.Add(HousesList.Find(x => x.url == item).ToString());
                }
                CadetBranchesListBox.ItemsSource = cadetBranches;

                var overlord = HousesList.Find(x => x.url == selected.overlord);
                OverlordListBox.Items.Clear();
                OverlordListBox.Items.Add(overlord == null ? "Unknown" : overlord.ToString());

                //Character
                SwornMembersListBox.ItemsSource = selected.swornMembers;
                FounderTextBox.Text = selected.founder == "" ? "Unknown" : selected.founder;
                HeirTextBox.Text = selected.heir == "" ? "Unknown" : selected.heir;
                CurrentLordTextBox.Text = selected.currentLord == "" ? "Unknown" : selected.currentLord;
            }

        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var service = new GoTService();
            var houses = await service.GetHousesAsync();
            foreach (var item in houses)
            {
                if (item.name != "")
                {
                    HousesList.Add(item);
                }
            }
            HousesListBox.ItemsSource = HousesList;
        }

        private void OverlordButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedstr = (sender as Button).Content as string;
            HousesListBox.SelectedItem = HousesList.Find(x => x.name == selectedstr);
        }

        private void CadetBranchesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedstr = (sender as ListBox).SelectedItem as string;
            HousesListBox.SelectedItem= HousesList.Find(x => x.name == selectedstr);
        }
    }
}
