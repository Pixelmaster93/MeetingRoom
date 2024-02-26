using AutoMapper;
using MeetingRoom.Entities;
using MeetingRoom.Models;

namespace MeetingRoom.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserWithoutReservations>();
            CreateMap<UserDto, UserWithoutReservations>();
        }
    }
}
