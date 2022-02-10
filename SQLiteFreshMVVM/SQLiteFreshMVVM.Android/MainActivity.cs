using System;
using FreshMvvm;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using SQLiteFreshMVVM.Services;

namespace SQLiteFreshMVVM.Droid
{
    [Activity(Label = "SQLiteFreshMVVM", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //Inyectar la instancia del repositorio en Android
            //crear el repositorio usando la clase FileAccessHelper de Android
            var repository = new Repository(FileAccessHelper.GetLocalFilePath("Users.db3"));
            //Con esto se puede acceder a esta instancia desde cualquier parte del programa
            FreshIOC.Container.Register(repository);

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}