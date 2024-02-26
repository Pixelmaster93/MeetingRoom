using AutoMapper;
using MeetingRoom.Entities;
using MeetingRoom.Models;
using MeetingRoom.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeetingRoom.Controllers;

[ApiController]
[Route("api/rooms")]
public class RoomsController : ControllerBase
{
    private readonly IReservationsInfoRepository _reservationInfoRepository;
    private readonly IMapper _mapper;

    public RoomsController(
        IReservationsInfoRepository reservationInfoRepository,
        IMapper mapper)
    {
        _reservationInfoRepository = reservationInfoRepository ??
            throw new ArgumentNullException(nameof(reservationInfoRepository));

        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomWithoutReservations>>> GetRooms()
    {
        var roomEntities = await _reservationInfoRepository.GetRoomsAsync();
        return Ok(_mapper.Map<IEnumerable<RoomWithoutReservations>>(roomEntities));
    }

    [HttpGet("{roomId}")]
    public async Task<ActionResult<RoomDto>> GetRoom(
        int roomId, bool includeReservations = false)
    {
        //find Room
        var room = await _reservationInfoRepository.GetRoomAsync(roomId, includeReservations);

        if (room == null)
        {
            return NotFound();
        }

        if (includeReservations)
        {
            return Ok(_mapper.Map<RoomDto>(room));
        }
        return Ok(_mapper.Map<RoomWithoutReservations>(room));
    }


//    [HttpPost]
//    public async Task<ActionResult<Room>> AddRoom(Room room)
//    {
//        if (room == null) return BadRequest(nameof(room));
//        _reservationInfoRepository.AddRoom(room);
//        await _reservationInfoRepository.SaveChangesAsync();
//        return Ok(room);

//    }

//    [HttpDelete("{roomId}")]
//    public async Task<ActionResult> DeleteRoom(int roomId, bool includeReservations)
//    {
//        var roomEntities = await _reservationInfoRepository.GetRoomAsync(roomId);

//        if (roomEntities == null)
//        {
//            return NotFound();
//        }

//        if (includeReservations)
//        {
//            if (roomEntities.Reservations is not null)
//            {
//                foreach (var reservationEntity in roomEntities.Reservations)
//                {
//                    _reservationInfoRepository.DeleteReservation(reservationEntity);
//                }
//            }
//        }

//        _reservationInfoRepository.DeleteRoom(roomEntities);

//        await _reservationInfoRepository.SaveChangesAsync();
//        return NoContent();

//    }

}
