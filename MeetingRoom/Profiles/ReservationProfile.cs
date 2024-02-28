using AutoMapper;
using MeetingRoom.Entities;
using MeetingRoom.Models;

namespace MeetingRoom.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationDto>();
            CreateMap<Reservation, ReservationWhithoutUsersRooms>();
            CreateMap<Reservation, ReservationWhithoutUser>();
            CreateMap<Reservation, ReservationWithNames>();
            CreateMap<ReservationDto, Reservation>();


            CreateMap<ReservationWhithoutUser, Reservation>();

            CreateMap<ReservationDto, ReservationWhithoutUsersRooms>();
            CreateMap<ReservationDto, ReservationWhithoutUser>();
            CreateMap<Reservation, Room>();
            CreateMap<ReservationDto, Room>();
            CreateMap<Reservation, RoomDto>();
            CreateMap<ReservationDto, RoomDto>();

            CreateMap<ReservationMails, ReservationDto>();

        }
    }
}
