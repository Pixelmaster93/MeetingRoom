using AutoMapper;
using MeetingRoom.Entities;
using MeetingRoom.Models;
using MeetingRoom.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
namespace MeetingRoom.Controllers;

[ApiController]
[Route("api/reservations")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationsInfoRepository _reservationInfoRepository;
    private readonly IMapper _mapper;
    private readonly IMailService _mailService;

    static TimeOnly openTime = new TimeOnly(08, 00, 00);
    static TimeOnly closeTime = new TimeOnly(20, 00, 00);

    public ReservationsController(
        IReservationsInfoRepository reservationInfoRepository,
        IMapper mapper,
         IMailService mailService)
    {
        _reservationInfoRepository = reservationInfoRepository ??
            throw new ArgumentNullException(nameof(reservationInfoRepository));

        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));

        _mailService = mailService ?? 
            throw new ArgumentNullException(nameof(mailService));
    }

    [HttpGet]
    public async Task<ActionResult<ReservationWhithoutUsersRooms>> GetReservations()
    {
        var reservationEntities = await _reservationInfoRepository.GetReservationsAsync();
        return Ok(_mapper.Map<IEnumerable<ReservationWhithoutUsersRooms>>(reservationEntities));
    }

    [HttpGet("{reservationId}", Name = "GetReservation")]
    public async Task<ActionResult<Reservation>> GetReservation(int reservationId)
    {
        var reservation = await _reservationInfoRepository.GetReservationAsync(reservationId);

        if (reservation == null) return NotFound(reservation);

        var exitReservation = _mapper.Map<ReservationWhithoutUsersRooms>(reservation);

        return Ok(exitReservation);
    }

    [HttpPost]
    public async Task<ActionResult<ReservationDto>> PostReservation(ReservationDto reservation)
    {
        var reservationByDate = (await _reservationInfoRepository.GetReservationsAsync())
            .Where(r => r.Date == reservation.Date && r.RoomId == reservation.RoomId);

        //var reservationByDate2 = (reservationByDate.Any(r => reservation.EndTime <= r.StartTime ^ r.EndTime <= reservation.StartTime));
        //if (reservationByDate.IsNullOrEmpty() || !reservationByDate2)
        //{
        //    return BadRequest("gnaaaa");
        //}

        var overlappingReservationStart = reservationByDate.Any(r => reservation.StartTime >= r.StartTime && reservation.StartTime <= r.EndTime && reservation.StartTime != r.EndTime);
        var overlappingReservationEnd = reservationByDate.Any(r => reservation.EndTime >= r.StartTime && reservation.EndTime <= r.EndTime && reservation.EndTime != r.StartTime);
        var overlappingReservation = reservationByDate.Any(r => r.StartTime > reservation.StartTime && r.StartTime < reservation.EndTime);
        var overlapping = reservationByDate.Any(r => reservation.StartTime == r.StartTime && reservation.EndTime == r.EndTime);

        if (overlappingReservationStart || overlappingReservationEnd || overlappingReservation || overlapping)
        {
            return BadRequest("Occupato!");
        }

        if (reservation.Date < DateOnly.FromDateTime(DateTime.Today))
        {
            return BadRequest("You should choose a future day!🍍");
        }
        if (new DateTime(reservation.Date, reservation.StartTime) <= DateTime.Now)
        {
            return BadRequest("You should choose a future hour!🍍");
        }
        if (reservation.StartTime >= reservation.EndTime)
        {
            return BadRequest("The end of the meeting can't be beafore the start!🍍");
        }
        if(reservation.StartTime < openTime)
        {
            return BadRequest("Start time can't be before our opening.🍍");
        }
        if (reservation.StartTime >= closeTime)
        {
            return BadRequest("Start time can't be our closing time or later.🍍");
        }
        if (reservation.EndTime > closeTime)
        {
            return BadRequest("End time can't be after our closing.🍍");
        }
        if (await _reservationInfoRepository.RoomExistsAsync(reservation.RoomId) == false)
        {
            return BadRequest("The room does not exist!");
        }

        var entity = _mapper.Map<Reservation>(reservation);

        _reservationInfoRepository.AddReservation(entity);

        var userEntities = await _reservationInfoRepository.GetUserAsync(entity.UserId);

        var roomEntities = await _reservationInfoRepository.GetRoomAsync(entity.RoomId);


        await _reservationInfoRepository.SaveChangesAsync();

        reservation = _mapper.Map<ReservationDto>(entity);


        //_mailService.HostSend("Room Reservation",
        //    $"User {userEntities.UserName} has Reserved the room {roomEntities.Name}, in {entity.Date} from {entity.StartTime} to {entity.EndTime}.");

        //_mailService.CustomerSend("Room Reservation",
        //    $"Dear {userEntities.UserName}, your request for your reservation in {entity.Date} from {entity.StartTime} to {entity.EndTime} has been confirmed.",
        //    userEntities.MailAddres);

        return Ok(reservation);
    }

    [HttpPatch ("{reservationId}")]
    public async Task<ActionResult<ReservationDto>> UpdateReservation (int reservationId , ReservationDto reservation)
    {
        var reservationMail = await _reservationInfoRepository.GetReservationAsync(reservationId);

        if(reservationMail == null) 
        {
            return NotFound();
        }
        var roomEntities = await _reservationInfoRepository.GetRoomAsync(reservationMail.RoomId);

        var userEntities = await _reservationInfoRepository.GetUserAsync(reservationMail.UserId);

        var reservationEntity = await _reservationInfoRepository.GetReservationAsync(reservationId);

        var entity = _mapper.Map<Reservation>(reservation);

        reservationEntity.StartTime = entity.StartTime;
        reservationEntity.EndTime = entity.EndTime;
        reservationEntity.Date = entity.Date;
        reservationEntity.RoomId = entity.RoomId;

        var reservationByDate = (await _reservationInfoRepository.GetReservationsAsync())
             .Where(r => r.Date == reservation.Date && r.RoomId == reservation.RoomId && r.Id != reservationId);

        //var overlapping = reservationByDate.Any(r =>
        //    (reservation.StartTime >= r.StartTime && reservation.StartTime <= r.EndTime && reservation.StartTime != r.EndTime) ||
        //    (reservation.EndTime >= r.StartTime && reservation.EndTime <= r.EndTime && reservation.EndTime != r.StartTime) ||
        //    (r.StartTime > reservation.StartTime && r.StartTime < reservation.EndTime) ||
        //    (reservation.StartTime == r.StartTime && reservation.EndTime == r.EndTime));

        //if (overlapping)
        //{
        //    return BadRequest("Occupato!");
        //}

        var overlappingReservationStart = reservationByDate.Any(r => reservation.StartTime >= r.StartTime && reservation.StartTime <= r.EndTime && reservation.StartTime != r.EndTime);
        var overlappingReservationEnd = reservationByDate.Any(r => reservation.EndTime >= r.StartTime && reservation.EndTime <= r.EndTime && reservation.EndTime != r.StartTime);
        var overlappingReservation = reservationByDate.Any(r => r.StartTime > reservation.StartTime && r.StartTime < reservation.EndTime);
        var overlapping = reservationByDate.Any(r => reservation.StartTime == r.StartTime && reservation.EndTime == r.EndTime);

        if (overlappingReservationStart || overlappingReservationEnd || overlappingReservation || overlapping)
        {
            return BadRequest("Occupato!");
        }

        if (reservationEntity.Date < DateOnly.FromDateTime(DateTime.Today))
        {
            return BadRequest("You should choose a future day!🍍");
        }
        if (new DateTime(reservationEntity.Date, reservationEntity.StartTime) <= DateTime.Now)
        {
            return BadRequest("You can't choose today for your meeting.🍍");
        }
        if (reservationEntity.StartTime >= reservationEntity.EndTime)
        {
            return BadRequest("The end of the meeting can't be beafore the start!🍍");
        }
        if (reservationEntity.StartTime < openTime)
        {
            return BadRequest("Start time can't be before our opening.🍍");
        }
        if (reservationEntity.StartTime >= closeTime)
        {
            return BadRequest("Start time can't be our closing time or later.🍍");
        }
        if (reservationEntity.EndTime > closeTime)
        {
            return BadRequest("End time can't be after our closing.🍍");
        }

        if (await _reservationInfoRepository.RoomExistsAsync(entity.RoomId) == false)
        {
            return BadRequest("The room does not exist!");
        }

        await _reservationInfoRepository.SaveChangesAsync();


        var exitReservation = _mapper.Map<ReservationDto>(reservationEntity);

        //_mailService.HostSend("Change Room Reservation",
        //   $"User {userEntities.UserName} has changed his reservation from room: {roomEntities.Name}, in: {reservationMail.Date} from: {reservationMail.StartTime} to: {reservationMail.EndTime} " +
        //   $"to room: {roomEntities.Name}, in: {entity.Date} from: {entity.StartTime} to: {entity.EndTime}.");

        //_mailService.CustomerSend("Room Reservation",
        //    $"Dear {userEntities.UserName}, your request for your reservation in {entity.Date} from {entity.StartTime} to {entity.EndTime} has been confirmed.",
        //    userEntities.MailAddres);


        return Ok(exitReservation);
      
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteReservation(int ReservationId)
    {
        var reservationEntity = await _reservationInfoRepository.GetReservationAsync(ReservationId);

        var userEntities = await _reservationInfoRepository.GetUserAsync(reservationEntity.UserId);

        _reservationInfoRepository.DeleteReservation(ReservationId);

        
        await _reservationInfoRepository.SaveChangesAsync();

        //_mailService.HostSend("Reservation Deleted",
        //    $"User {userEntities.UserName} has deleted the Reservation {ReservationId}, in {reservationEntity.Date}.");

        //_mailService.CustomerSend("Reservation Deleted",
        //    $"Dear {userEntities.UserName}, your deletion request for your reservation in {reservationEntity.Date} has been confirmed.",
        //    userEntities.MailAddres);

        return NoContent();
    }
}

