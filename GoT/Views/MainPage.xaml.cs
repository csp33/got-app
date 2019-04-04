using GoT.Views;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GoT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
