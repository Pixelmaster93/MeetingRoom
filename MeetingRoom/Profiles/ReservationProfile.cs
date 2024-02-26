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
            CreateMap<Reservation, ReservationWhithoutRoom>();
            CreateMap<ReservationDto, Reservation>();

            CreateMap<ReservationWhithoutUser, Reservation>();
            CreateMap<ReservationWhithoutRoom, Reservation>();

            CreateMap<ReservationDto, ReservationWhithoutUsersRooms>();
            CreateMap<ReservationDto, ReservationWhithoutUser>();
            CreateMap<ReservationDto, ReservationWhithoutRoom>();
            CreateMap<Reservation, Room>();
            CreateMap<ReservationDto, Room>();
            CreateMap<Reservation, RoomDto>();
            CreateMap<ReservationDto, RoomDto>();

            CreateMap<ReservationDbAdd, Reservation>();
            CreateMap<Reservation, ReservationDbAdd > ();

        }
    }
}
