using Microsoft.EntityFrameworkCore;

namespace demo.Models
{
    public class Hotel: DbContext
    {
        public DbSet<Room> rooms { get; set; }

        public Hotel(DbContextOptions options) : base(options)
        {

        }
    }
}
