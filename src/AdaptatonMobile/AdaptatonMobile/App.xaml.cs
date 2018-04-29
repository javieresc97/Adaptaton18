using System;
using AdaptatonMobile.Helpers;
using AdaptatonMobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly:XamlCompilation(XamlCompilationOptions.Compile)]
namespace AdaptatonMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Page initialPage = ChoosePageFromSession();
            MainPage = initialPage;
        }

        private Page ChoosePageFromSession()
        {
            Page page;
            if (Settings.IsUserSet)
            {
                page = new HomePage();
            }
            else
            {
                page = new CheckLoginPage();                
            }

            return new NavigationPage(page)
            {
                BarBackgroundColor = Color.FromHex("#26547C"),
                BarTextColor = Color.White
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
