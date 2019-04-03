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
    public class HousesPageViewModel : ViewModelBase
    {
        public ObservableCollection<House> Houses { get; set; } = new ObservableCollection<House>();

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var service = new GoTService();

            var houses = await service.GetHousesAsync();
            foreach (var item in houses)
            {
                Houses.Add(item);
            }

            await base.OnNavigatedToAsync(parameter, mode, state);
        }

    }
}
