using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lingva.DAL.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        [MaxLength(16)]
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<DictionaryRecord> UserDictionaryRecords { get; set; }

        public User()
        {
            UserDictionaryRecords = new List<DictionaryRecord>();
        }
    }
}
