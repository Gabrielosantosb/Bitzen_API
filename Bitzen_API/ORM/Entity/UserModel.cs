using System.ComponentModel.DataAnnotations;

namespace Bitzen_API.ORM.Entity
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
