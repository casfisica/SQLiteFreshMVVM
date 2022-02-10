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

        public Task<List<User>> GetAllItems()
        {
            // Return a list of items saved to the Item table in the database.
            return conn.Table<User>().ToListAsync();
        }
    }
}
