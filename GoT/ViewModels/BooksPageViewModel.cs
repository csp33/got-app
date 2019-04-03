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
    public class BooksPageViewModel : ViewModelBase
    {

        public ObservableCollection<Book> Books { get; set ; } = new ObservableCollection<Book>();

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var service = new GoTService();

            var books = await service.GetBooksAsync();
            foreach (var item in books)
            {
                Books.Add(item);
            }
  
            await base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
  
}
