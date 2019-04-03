using GoT.Models;
using GoT.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace GoT.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        public ObservableCollection<Book> Books { get; set ; } = new ObservableCollection<Book>();
        public ObservableCollection<Character> Characters { get; set; } = new ObservableCollection<Character>();
        public ObservableCollection<House> Houses { get; set; } = new ObservableCollection<House>();

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var service = new GoTService();

            /**BooksPage**/
            var books = await service.GetBooksAsync();
            foreach (var item in books)
            {
                Books.Add(item);
            }
            /**CharactersPage**/
            var characters = await service.GetCharactersAsync();
            foreach (var item in characters)
            {
                Characters.Add(item);
            }

            /**HousesPage**/
            var houses = await service.GetHousesAsync();
            foreach (var item in houses)
            {
                Houses.Add(item);
            }

            await base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
  
}
