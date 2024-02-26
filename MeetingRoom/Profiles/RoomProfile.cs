using AutoMapper;
using MeetingRoom.Entities;
using MeetingRoom.Models;

namespace MeetingRoom.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile() 
        { 
            CreateMap<Room, RoomDto>();
            CreateMap<Room, RoomWithoutReservations>();
            CreateMap<RoomDto, RoomWithoutReservations>();

        }
    }
}
