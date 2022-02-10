using SQLite;

namespace SQLiteFreshMVVM.Models
{
    /// <summary>
    /// Esta clase contiene los usuarios, roles y password
    /// </summary>

    [Table(nameof(User))]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        [MaxLength(12)]
        public string Password { get; set; }

        public bool Admin { get; set; }
        public User()
        {
        }
        /// <summary>
        /// Para chequear que el usuario esta bien
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password));
        }

    }
}
