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
            House b = new House();
            b.name = "Second test house";
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
            var selected = (sender as ListBox).SelectedItem;


        }

    }
}
