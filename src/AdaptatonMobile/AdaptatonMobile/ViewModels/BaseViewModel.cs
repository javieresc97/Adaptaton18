using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AdaptatonMobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public void ChangeMainPage(Page page, bool navigationPage)
        {
            if (navigationPage)
            {
                page = new NavigationPage(page)
                {
                    BarBackgroundColor = Color.FromHex("#26547C"),
                    BarTextColor = Color.White
                };
            }
            Application.Current.MainPage = page;
        }

        public async Task NavigateTo(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public async Task NavigateBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task NavigateToRoot()
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }



        public async Task ShowAlert(string message, string title = "¡Ups!", string okMessage = "OK")
        {
            await Application.Current.MainPage.DisplayAlert(title, message, okMessage);
        }

        public async Task<bool> ShowAlertOptions(string title, string message, string okMessage, string cancelMessage)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, okMessage, cancelMessage);
        }


    }
}
