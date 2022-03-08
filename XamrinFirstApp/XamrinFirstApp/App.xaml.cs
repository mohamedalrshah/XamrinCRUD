using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamrinFirstApp.Services;
using XamrinFirstApp.Views;

namespace XamrinFirstApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
