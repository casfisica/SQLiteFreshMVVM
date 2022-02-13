using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using SQLiteFreshMVVM.Models;
using SQLiteFreshMVVM.Services;
using Xamarin.Forms;

namespace SQLiteFreshMVVM.PageModels
{
    public class UserListPageModel : FreshBasePageModel
    {
        private Repository _repository = FreshIOC.Container.Resolve<Repository>();


        /// <summary>
        /// Colleccion para obtener los usuarios desde la base de datos
        /// </summary>
        public ObservableCollection<User> Users { get; private set; }

        /// <summary>
        /// Propiedad para bind a la propiedad selected item de listview
        /// </summary>

        private User selectedUser = null;
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                if (value != null) EditUserCommand.Execute(value);
            }
        }


        /// <summary>
        /// Constructor, lo uso para declarar la lista observable
        /// </summary>
        public UserListPageModel()
        {
            Users = new ObservableCollection<User>();
        }


        /// <summary>
        /// Cuando se navega hacia la pagina se activa esta funcion
        /// </summary>
        public override void Init(object initData)
        {
            LoadUsers();
            if (Users.Count() < 1)
            {
                CreateSampleData();
            }
        }

        /// <summary>
        /// Se llama cuando se navega hacia la pagina desde el metodo pop.
        /// Se recarga los usuarios de la lista
        /// </summary>
        /// <param name="returnedData"></param>
        public override void ReverseInit(object returnedData)
        {
            LoadUsers();
            base.ReverseInit(returnedData);
        }

        /// <summary>
        /// Comando para anadir usuarios
        /// Navega hacia la pagina de usuarios para editar los campos y guardar
        /// no lleva argumento
        /// </summary>
        public ICommand AddUserCommand
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<UserPageModel>();
                });
            }
        }

        /// <summary>
        /// Comando para editar usuarios
        /// navega hacia la pagina de usuarios con un objeto usuario como argumento para editarlo
        /// </summary>
        public ICommand EditUserCommand
        {
            get
            {
                return new Command(async (user) => {
                    await CoreMethods.PushPageModel<UserPageModel>(user);
                });
            }
        }

        /// <summary>
        /// Llena la colleccion obserbable con los nuevos usuarios
        /// </summary>
        private void LoadUsers()
        {
            Users.Clear();
            Task<List<User>> getItemTask = _repository.GetAllUsers();
            getItemTask.Wait();
            foreach (var user in getItemTask.Result)
            {
                Users.Add(user);
            }
        }

        /// <summary>
        /// Creo un usuario por defecto
        /// </summary>
        private void CreateSampleData()
        {
            var user1 = new User
            {
                UserName = "Camilo Salazar",
                Name = "csalazar",
                Password = "Poioiulkj",
                Admin = false
            };

            var user2 = new User
            {
                UserName = "Admin",
                Name = "admin",
                Password = "intecol.123",
                Admin = true

            };


            var task1 = _repository.CreateItem(user1);
            var task2 = _repository.CreateItem(user2);

            // Don't proceed until all the async inserts are complete.
            var allTasks = Task.WhenAll(task1, task2);
            allTasks.Wait();

            LoadUsers();
        }

    }


}
