namespace Bitzen_API.ORM.Model.Reservation
{
    public class CreateReservationModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RoomId { get; set; }
    }
}
