using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace AdaptatonMobile.Views
{
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
            Title = CurrentPage.Title;
            CurrentPageChanged += CurrentPageChangedHandler;
        }

        private void CurrentPageChangedHandler(object sender, EventArgs e)
        {
            this.Title = this.CurrentPage.Title;
        }
    }
}
