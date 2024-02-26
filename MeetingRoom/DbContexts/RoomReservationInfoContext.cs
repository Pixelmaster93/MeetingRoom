using MeetingRoom.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoom.DbContexts
{
    public class RoomReservationInfoContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;

        public RoomReservationInfoContext(
            DbContextOptions<RoomReservationInfoContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var room = new Room("Den")
            {
                Id = 1,
                Seats = 6,
                Description = "in the den room, you will find ergonomic chairs, a monitor for each person, and a spacious table. " +
                    "the room can accommodate a maximum of 6 people. it is not very soundproof. " +
                    "additionally, the room has a small balcony for those who wish to smoke."
            };
            var room2 = new Room("White")
            {
                Id = 2,
                Seats = 8,
                Description = ""
            };
            var room3 = new Room("Rabbit")
            {
                Id = 3,
                Seats = 6,
                Description = ""
            };
            var room4 = new Room("Skynet")
            {
                Id = 4,
                Seats = 10,
                Description = ""
            };


            var user = new User("Belghur")
            {
                Id = 1,
                MailAddres = "terrasquegood@rgdr.com",
                PhoneNumber = "322246724436",
            };
            var user2 = new User("Earin")
            {
                Id = 2,
                MailAddres = "brucogianluco@sio.com",
                PhoneNumber = "446744082664"
            };
            var user3 = new User("Pixelmaster")
            {
                Id = 3,
                MailAddres = "dinoresin@dices.com",
                PhoneNumber = "7672673567"
            };
            /*
            var reservation = new Reservation()
            {
                Id = 1,
                Date = new DateOnly(2024, 02, 19),
                StartTime = new TimeOnly(08, 00, 00),
                EndTime = new TimeOnly(18, 00, 00),
                Room = room,
                User = user3
            };
            var reservation2 = new Reservation()
            {
                Id = 2,
                Room = room3,
                User = user2,
                Date = new DateOnly(2024, 02, 14),
                StartTime = new TimeOnly(08, 00, 00),
                EndTime = new TimeOnly(11, 00, 00)
            };
            var reservation3 = new Reservation()
            {
                Id = 3,
                Room = room,
                User = user3,
                Date = new DateOnly(2024, 02, 19),
                StartTime = new TimeOnly(16, 0, 0),
                EndTime = new TimeOnly(19, 0, 0)
            };
            var reservation4 = new Reservation()
            {
                Id = 4,
                Date = new DateOnly(2024, 02, 20),
                StartTime = new TimeOnly(08, 00, 00),
                EndTime = new TimeOnly(18, 00, 00),
                Room = room4,
                User = user,
            };
            */



            modelBuilder.Entity<Room>(b =>
            {
                b.Property(x => x.Id).UseIdentityColumn();
                b.HasKey(x => x.Id);

                b.Property(x => x.Name).IsRequired();
                b.Property(x => x.Seats).IsRequired();
                b.Property(x => x.Description).IsRequired();
                b.Ignore(x => x.NumbersOfReservation);

                b.HasData(room, room2, room3, room4);
            });

            modelBuilder.Entity<User>(b =>
            {
                b.Property(x => x.Id).UseIdentityColumn();
                b.HasKey(x => x.Id);

                b.Property(x => x.UserName).IsRequired();
                b.Property(x => x.MailAddres).IsRequired();
                b.Property(x => x.PhoneNumber).IsRequired();
                b.Ignore(x => x.NumbersOfReservation);

                b.HasData(user, user2, user3);
            });

            modelBuilder.Entity<Reservation>(b =>
            {
                b.Property(x => x.Id).UseIdentityColumn();
                b.HasKey(x => x.Id);

                b.Property(x => x.Date).IsRequired();
                b.Property(x => x.StartTime).IsRequired();
                b.Property(x => x.EndTime).IsRequired();
                b.HasOne(x => x.Room).WithMany(x => x.Reservations);
                b.HasOne(x => x.User).WithMany(x => x.Reservations);

               // b.HasData(reservation, reservation2, reservation3, reservation4);
                
            });
            base.OnModelCreating(modelBuilder);
        }
        
    }
}

