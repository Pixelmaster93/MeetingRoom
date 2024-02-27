using AutoMapper;
using MeetingRoom.Entities;
using MeetingRoom.Models;
using MeetingRoom.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeetingRoom.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IReservationsInfoRepository _reservationInfoRepository;
    private readonly IMapper _mapper;
    private readonly IMailService _mailService;

    public UsersController(
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
    public async Task<ActionResult<IEnumerable<UserWithoutReservations>>> GetRooms()
    {
        var userEntities = await _reservationInfoRepository.GetUsersAsync();
        return Ok(_mapper.Map<IEnumerable<UserWithoutReservations>>(userEntities));
    }

    [HttpGet("{userId}", Name = "GetUser")]
    public async Task<ActionResult<UserDto>> GetUser(
        int userId, bool includeReservations = false)
    {
        //find Room
        var userEntities = await _reservationInfoRepository.GetUserAsync(userId, includeReservations);

        if (userEntities == null)
        {
            return NotFound();
        }

        if (includeReservations)
        {
            int pageSize = 10;

            int numberOfPages = (int)Math.Ceiling((double)userEntities
                .NumbersOfReservation / pageSize);


            //for ( int page = 1; page < numberOfPages; page++ )
            //{
                
            //}

            return Ok(_mapper.Map<UserDto>(userEntities));
        }
        return Ok(_mapper.Map<UserWithoutReservations>(userEntities));
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(User user)
    {
        //user.Id = (await _reservationInfoRepository.GetUsersAsync()).Max(r => r.Id)+1;

        _reservationInfoRepository.CreateUserAsync(user);

        await _reservationInfoRepository.SaveChangesAsync();

        return CreatedAtRoute("GetUser",
            new
            {
                userId = user.Id
            },
            user);
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult> DeleteUser(int userId, bool includeReservations)
    {
        var userEntities = await _reservationInfoRepository.GetUserAsync(userId);

        if (userEntities == null)
        {
            return NotFound();
        }

        if (includeReservations)
        {
            if (userEntities.Reservations is not null)
            {
                foreach (var reservationEntity in userEntities.Reservations)
                {
                    _reservationInfoRepository.DeleteReservation(reservationEntity.Id);
                }
            }
        }


        _reservationInfoRepository.DeleteUser(userEntities);

        await _reservationInfoRepository.SaveChangesAsync();

        _mailService.HostSend("User Deleted", 
            $"User {userEntities.UserName} with Id: {userEntities.Id} has been deleted.");

        _mailService.CustomerSend("User Deleted",
            $"Dear {userEntities.UserName}, your deletion request has been confirmed.",
            userEntities.MailAddres);

        return NoContent();
    }
}
