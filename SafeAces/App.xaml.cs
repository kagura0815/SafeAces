using SafeAces.Views;

namespace SafeAces
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new FrontPage();
        }
    }
}
