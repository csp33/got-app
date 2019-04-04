using GoT.Views;
using Windows.UI.Xaml.Controls;

namespace GoT
{
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
            RootFrame.Navigate(typeof(HomePage));

        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var invokedItem = args.InvokedItem as string;

            switch (invokedItem.ToLower())
            {
                case "houses":
                    RootFrame.Navigate(typeof(HousesPage));
                    break;
                case "books":
                    RootFrame.Navigate(typeof(BooksPage));
                    break;
                case "characters":
                    RootFrame.Navigate(typeof(CharactersPage));
                    break;
            }
        }
    }
}
