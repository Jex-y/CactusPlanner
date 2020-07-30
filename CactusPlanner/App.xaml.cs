using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CactusPlanner
{
    public partial class App : Application
    {
        Cactus cactus;
        public App()
        {
            InitializeComponent();
            cactus = Cactus.loadCactus();
            MainPage = new NavigationPage(new MainPage(ref cactus));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            cactus.saveToFile();
        }

        protected override void OnResume()
        {
            cactus.saveToFile();
        }
    }
}
