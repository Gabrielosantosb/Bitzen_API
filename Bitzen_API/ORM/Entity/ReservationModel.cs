using System.ComponentModel.DataAnnotations;

namespace Bitzen_API.ORM.Entity
{
    public class ReservationModel
    {
        [Key]
        public int ReservationId { get; set; }
        
        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }
        
        public bool Status { get; set; }

        public int CreatedByUserId { get; set; }

        public int RoomId { get; set; }
        public RoomModel Room { get; set; }
    }
}
