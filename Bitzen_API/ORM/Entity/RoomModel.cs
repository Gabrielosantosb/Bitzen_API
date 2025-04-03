using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Bitzen_API.ORM.Entity
{
    public class RoomModel
    {
        [Key]
        public int RoomId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public int CreatedByUserId { get; set; }
        public UserModel CreatedByUser { get; set; }
    }

}
