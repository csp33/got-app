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
            House a = new House();
            a.name = "First test house";
            a.ancestralWeapons.Add("weapon1");
            a.ancestralWeapons.Add("weapon2");
            a.cadetBranches.Add("cadet1");
            a.cadetBranches.Add("cadet2");
            a.coatOfArms = "coat1";
            a.currentLord = "current lord";
            a.diedOut = "27/10/2010";
            a.founded = "12/03/2000";
            a.founder = "founder 1";
            a.heir = "heir 1";
            a.overlord = "overlord 1";
            a.region = "winterfell";
            a.seats.Add("seat 1");
            a.seats.Add("seat 2");
            a.seats.Add("seat 3");
            a.seats.Add("seat 4");
            a.seats.Add("seat 5");
            a.swornMembers.Add("member 1");
            a.swornMembers.Add("member 2");
            a.swornMembers.Add("member 3");
            a.swornMembers.Add("member 4");
            a.titles.Add("title 1");
            a.titles.Add("title 2");
            a.titles.Add("title 3");
            a.words = "The words of the house test number one";
            House b = new House();
            b.name = "Second test house";
            b.ancestralWeapons.Add("weapon1");
            b.ancestralWeapons.Add("weapon2");
            b.cadetBranches.Add("cadet1");
            b.cadetBranches.Add("cadet2");
            b.coatOfArms = "coat1";
            b.currentLord = "current lord";
            b.diedOut = "27/10/2010";
            b.founded = "12/03/2000";
            b.founder = "founder 1";
            b.heir = "heir 1";
            b.overlord = "overlord 1";
            b.region = "winterfell";
            b.seats.Add("seat 1");
            b.seats.Add("seat 2");
            b.seats.Add("seat 3");
            b.seats.Add("seat 4");
            b.seats.Add("seat 5");
            b.swornMembers.Add("member 1");
            b.swornMembers.Add("member 2");
            b.swornMembers.Add("member 3");
            b.swornMembers.Add("member 4");
            b.titles.Add("title 1");
            b.titles.Add("title 2");
            b.titles.Add("title 3");
            b.words = "The words of the house test number one";
            HousesList.Add(a);
            HousesList.Add(b);
            /*
            var service = new GoTService();
            var houses = await service.GetHousesAsync();

            foreach(var item in houses)
            {
                HousesList.Add(item);
            }
            */
            HousesListBox.ItemsSource= HousesList;
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
            AncestralWeaponsListBox.ItemsSource = selected.ancestralWeapons;
            CadetBranchesListBox.ItemsSource = selected.cadetBranches;
            SwornMembersListBox.ItemsSource = selected.swornMembers;
            DiedOutTextBox.Text = selected.diedOut;
            FounderTextBox.Text = selected.founder;
            FoundedTextBox.Text = selected.founded;
            OverlordTextBox.Text = selected.overlord;
            HeirTextBox.Text = selected.heir;
            CurrentLordTextBox.Text = selected.currentLord;
            SeatsListBox.ItemsSource = selected.seats;
            TitlesListBox.ItemsSource = selected.titles;
            WordsTextBox.Text = selected.words;
            CoatOfArmsTextBox.Text = selected.coatOfArms;
            RegionTextBox.Text = selected.region;
            NameTextBox.Text = selected.name;



        }

    }
}
