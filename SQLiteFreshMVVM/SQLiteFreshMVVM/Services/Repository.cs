using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using SQLiteFreshMVVM.Models;

namespace SQLiteFreshMVVM.Services
{
    /// <summary>
    /// Repository maneja la base de datos
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// Coneccion asincrona con la base de datos
        /// </summary>
        private readonly SQLiteAsyncConnection conn;

        /// <summary>
        /// Mensaje de estatus
        /// </summary>
        public string StatusMessage { get; set; }

        /// <summary>
        /// Constructor, crea una coneccion a la base de datos y la almacena en conn
        /// Ademas crea una tabla de Usuarios, usando la clase Users
        /// </summary>
        /// <param name="dbPath"></param>
        public Repository(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<User>().Wait();
        }

        public async Task CreateItem(User user)
        {
            try
            {
                // Basic validation to ensure we have an item name.
                if (string.IsNullOrWhiteSpace(user.Name))
                    throw new Exception("Name is required");

                // Insert/update items.
                var result = await conn.InsertOrReplaceAsync(user).ConfigureAwait(continueOnCapturedContext: false);
                StatusMessage = $"{result} record(s) added [Item Name: {user.Name}])";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to create item: {user.Name}. Error: {ex.Message}";
            }
        }

        public Task<List<User>> GetAllUsers()
        {
            // Return a list of items saved to the Item table in the database.
            return conn.Table<User>().ToListAsync();
        }


        /// <summary>
        /// Funcion para saber que si el usuario y la contrasena existen,
        /// devuelve el id
        /// </summary>
        //public Task<int> IsValidUser(User user) 
        //{

        //} 

        public Task<User> GetUserByID(int id)
        {
            // Get a specific note.
            return conn.Table<User>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<User> GetUserByUserName(string userName, string pwd)
        {
            //Validar si es null
            var data = conn.Table<User>();
            return data.Where(x => x.UserName == userName && x.Password == pwd).FirstOrDefaultAsync();
        }

        public bool LoginValidate(string userName, string pwd)
        {

            var data = conn.Table<User>();
            var userToCheck = GetUserByUserName(userName, pwd);
            if (userToCheck != null)
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Para optener un usuario espesifico, con su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<User> GetSpecificUser(int id)
        {
            return conn.Table<User>().FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Para Borrar usuario
        /// </summary>
        /// <param name="id"></param>
        public void DeleteUser(int id)
        {
            conn.DeleteAsync<User>(id);
        }

        public Repository()
        {
            //User administrador = new User();
            //administrador.Name = "camilo";
            //administrador.Password ="poioiulkj";
            //administrador.Admin = true;

            //_ = CreateItem(administrador);
        }

    }
}
