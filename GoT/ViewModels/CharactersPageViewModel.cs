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
    public class CharactersPageViewModel : ViewModelBase
    {
        public ObservableCollection<Character> Characters { get; set; } = new ObservableCollection<Character>();

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var service = new GoTService();

            var characters = await service.GetCharactersAsync();
            foreach (var item in characters)
            {
                Characters.Add(item);
            }


            await base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
