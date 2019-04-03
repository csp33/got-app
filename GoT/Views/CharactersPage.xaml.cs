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
            Character b = new Character();
            b.name = "Second test character";
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
            var selected = (sender as ListBox).SelectedItem;

            
        }
    }
}
