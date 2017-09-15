using BringMeSoup.mobile.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BringMeSoup.mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // check for expired token
            if (Helpers.Settings.AccessTokenExpirationDate <= DateTime.UtcNow)
            {
                Helpers.Settings.ClearAllValues();
            }

            if (string.IsNullOrEmpty(Helpers.Settings.AccessToken))
            {
                MainPage = new NavigationPage(new LogonPage());
            }
            else
            {
                MainPage = new NavigationPage(new ChooseUserTypePage());
            }

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
