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
        public string userName { get; set; }
        public string name { get; set; }
        [MaxLength(12)]
        public string password { get; set; }
        [MaxLength(10)]
        public string phone { get; set; }
        public User()
        {
        }
        /// <summary>
        /// Para chequear que el usuario esta bien
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password));
        }

    }
}
