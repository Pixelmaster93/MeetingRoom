using MeetingRoom.Models;
using System.Xml.Linq;

namespace MeetingRoom
{
    /*
    public class RoomsDataStore
    {
        public List<RoomDto> Rooms { get; set; }
        public static RoomsDataStore Current { get; set; } = 
            new RoomsDataStore();

        public RoomsDataStore()
        {
            //init dummy data
            Rooms = new List<RoomDto>()
            {
                new RoomDto()
                {
                    Id = 1,
                    Name = "den",
                    Seats = 6,
                    Description = "in the den room, you will find ergonomic chairs, a monitor for each person, and a spacious table. " +
                        "the room can accommodate a maximum of 6 people. it is not very soundproof. " +
                        "additionally, the room has a small balcony for those who wish to smoke.",
                    Users = new List<UserDto>()
                    {
                        new UserDto()
                        {
                            Id = 1,
                            UserName = "Earin",
                            MailAddres = "brucogianluco@sio.com",
                            PhoneNumber = "446744082664",
                            Reservations = new List<ReservationDto>()
                            {
                                new ReservationDto()
                                {
                                    Id = 1,
                                    Date = new DateOnly(2024, 02, 19),
                                    StartTime = new TimeOnly(16, 0, 0),
                                    EndTime = new TimeOnly(19, 0, 0)
                                }
                            }
                        }
                       
                    }
                },
                new RoomDto()
                {
                    Id =  2,
                    Name = "White",
                    Seats = 8,
                    Description = "",
                    Users = new List<UserDto>()
                    {
                        new UserDto()
                        {
                            Id = 2,
                            UserName = "Behlgur",
                            MailAddres = "terrasquegood@rgdr.com",
                            PhoneNumber = "322246724436",
                            Reservations = new List<ReservationDto>()
                            {
                                new ReservationDto()
                                {
                                    Id = 2,
                                    Date = new DateOnly(2024, 02, 14),
                                    StartTime = new TimeOnly(08, 00, 00),
                                    EndTime = new TimeOnly(11, 00, 00)
                                }
                            }
                        }
                    }
                },
                new RoomDto()
                {
                    Id = 3,
                    Name = "Rabbit",
                    Seats = 6,
                    Description = "",
                    Users = new List<UserDto>()
                   
                },
                new RoomDto()
                {
                    Id =  4,
                    Name = "Skynet",
                    Seats = 10,
                    Description = "",
                    Users = new List<UserDto>()
                    {
                        new UserDto()
                        {
                            Id = 3,
                            UserName = "Pixelmaster",
                            MailAddres = "dinoresin@dices.com",
                            PhoneNumber = "7672673567",
                            Reservations = new List<ReservationDto>()
                            {
                                new ReservationDto()
                                {
                                    Id = 3,
                                    Date = new DateOnly(2024, 02, 19),
                                    StartTime = new TimeOnly(08, 00, 00),
                                    EndTime = new TimeOnly(18, 00, 00)
                                }
                            }
                        }
                    }
                }
            };
        }
    }
    */
}
