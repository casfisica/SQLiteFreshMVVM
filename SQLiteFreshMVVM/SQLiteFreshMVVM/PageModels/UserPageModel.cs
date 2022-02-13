using System.Windows.Input;
using FreshMvvm;
using SQLiteFreshMVVM.Models;
using SQLiteFreshMVVM.Services;
using Xamarin.Forms;

namespace SQLiteFreshMVVM.PageModels
{
    /// <summary>
    /// Modelo de pagina de usuarios, heredamos de FreshBasePageModel:
    /// PropertyChanged, PageWasPopped, ToolbarItems, PreviousPageModel
    /// CurrentPage, CoreMethods, ReverseInit,...
    /// </summary>
    public class UserPageModel : FreshBasePageModel
    {
        // Use IoC to get our repository.
        private Repository _repository = FreshIOC.Container.Resolve<Repository>();

        // Backing data model.
        private User _user;

        /// <summary>
        /// Propiedad pública que expone el nombre del usuario para el enlace de página.
        /// </summary>
        public string User_UserName
        {
            get { return _user.UserName; }
            set { _user.UserName = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string User_Name
        {
            get { return _user.Name; }
            set { _user.Name = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string User_Password
        {
            get { return _user.Password; }
            set { _user.Password = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool User_Admin
        {
            get { return _user.Admin; }
            set { _user.Admin = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// Llamado cada vez que se navega a la página.
        /// Utilice un elemento proporcionado o cree uno nuevo si no se proporciona.
        /// FreshMVVM no proporciona RaiseAllPropertyChanged,
        /// así que hacemos esto para cada propiedad enlazada.
        /// </summary>
        public override void Init(object initData)
        {
            _user = initData as User;
            if (_user == null) _user = new User();
            base.Init(initData);
            //RaisePropertyChanged(nameof(User_UserName));
            //RaisePropertyChanged(nameof(User_Name));
            //RaisePropertyChanged(nameof(User_Password));
            //RaisePropertyChanged(nameof(User_Admin));
        }

        /// <summary>
        /// Comando asociado con la acción de guardar.
        /// Persiste el elemento en la base de datos si el elemento es válido.
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                return new Command(async () => {
                    if (_user.IsValid())
                    {
                        await _repository.CreateItem(_user);
                        //await CoreMethods.PopPageModel(_user);
                        await CoreMethods.PushPageModel<UserListPageModel>();
                    }
                });
            }
        }


    }
}
