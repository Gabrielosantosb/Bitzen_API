using Bitzen_API.ORM.Entity;
using Microsoft.EntityFrameworkCore;

namespace Bitzen_API.ORM.Context
{
    public class BitzenDbContext : DbContext
    {
        public BitzenDbContext(DbContextOptions<BitzenDbContext> options) : base(options)
        {
        }

        public DbSet<UserModel> User { get; set; }
        public DbSet<RoomModel> Room { get; set; }
        public DbSet<ReservationModel> Reservation { get; set; }

    }
}
