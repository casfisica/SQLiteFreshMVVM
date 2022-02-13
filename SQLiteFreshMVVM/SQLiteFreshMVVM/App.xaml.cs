using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FreshMvvm;
using SQLiteFreshMVVM.PageModels;

namespace SQLiteFreshMVVM
{
    public partial class App : Application
    {

        private static bool isAdmin;

        public static bool IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; }
        }

        public App()
        {
            InitializeComponent();

            var page = FreshPageModelResolver.ResolvePageModel<LogInPageModel>();
            var navegationPage = new FreshNavigationContainer(page);

            IsAdmin = false;
            MainPage = navegationPage;


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
